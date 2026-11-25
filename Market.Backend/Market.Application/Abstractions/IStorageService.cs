namespace Market.Application.Abstractions;

public interface IStorageService
{
    Task<string> UploadImageAsync(Stream imageStream, string fileName, CancellationToken ct = default);
}