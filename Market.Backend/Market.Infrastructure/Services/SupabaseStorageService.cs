using Market.Application.Abstractions;
using Market.Shared.Options;
using Microsoft.Extensions.Options;
using SkiaSharp;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace Market.Infrastructure.Services;

public sealed class SupabaseStorageService : IStorageService
{
    private readonly SupabaseSettings _settings;
    private readonly HttpClient _httpClient;

    public SupabaseStorageService(IOptions<SupabaseSettings> options, IHttpClientFactory httpClientFactory)
    {
        _settings = options.Value;
        _httpClient = httpClientFactory.CreateClient("Supabase");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _settings.ServiceRoleKey);
        _httpClient.DefaultRequestHeaders.Add("apikey", _settings.ServiceRoleKey);
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName, CancellationToken ct = default)
    {
        var compressedStream = CompressImage(imageStream);

        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext)) ext = ".jpg";
        var uniqueFileName = $"{Guid.NewGuid():N}{ext}";

        var uploadUrl = $"{_settings.ProjectUrl}/storage/v1/object/{_settings.BucketName}/{uniqueFileName}";

        using var content = new StreamContent(compressedStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

        var response = await _httpClient.PostAsync(uploadUrl, content, ct);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(ct);
            throw new InvalidOperationException($"Supabase upload failed: {error}");
        }

        return $"{_settings.ProjectUrl}/storage/v1/object/public/{_settings.BucketName}/{uniqueFileName}";
    }

    private static Stream CompressImage(Stream inputStream)
    {
        using var ms = new MemoryStream();
        inputStream.CopyTo(ms);
        ms.Position = 0;

        using var bitmap = SKBitmap.Decode(ms);
        if (bitmap == null)
            throw new InvalidOperationException("Failed to decode image.");

        SKBitmap resized = bitmap;
        if (bitmap.Width > 800)
        {
            var ratio = 800.0f / bitmap.Width;
            var newHeight = (int)(bitmap.Height * ratio);
            resized = bitmap.Resize(new SKImageInfo(800, newHeight), SKFilterQuality.High);
        }

        using var image = SKImage.FromBitmap(resized);
        using var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);

        var outputStream = new MemoryStream();
        data.SaveTo(outputStream);
        outputStream.Position = 0;

        if (!ReferenceEquals(resized, bitmap))
            resized.Dispose();

        return outputStream;
    }
}