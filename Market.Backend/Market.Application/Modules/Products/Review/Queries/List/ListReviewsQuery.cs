namespace Market.Application.Modules.Products.Review.Queries.List
{
    public sealed class ListReviewsQuery : BasePagedQuery<ListReviewsQueryDto>
    {
        public int? ProductEntityId { get; set; }
        public int? PublicUserEntityId { get; set; }
        public PageRequest Page { get; internal set; }
    }
}
