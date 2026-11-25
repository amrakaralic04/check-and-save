namespace Market.Shared.Options;

public sealed class SupabaseSettings
{
    public const string SectionName = "Supabase";
    public string ProjectUrl { get; set; } = string.Empty;
    public string ServiceRoleKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = "product-images";
}