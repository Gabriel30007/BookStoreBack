namespace ShopAPI.DTO
{
    public class ProductFilterContainerDto
    {
        public Dictionary<string,string> Filters { get;set; }
        public int Page { get; set; }
        public int ItemsOnPage { get; set; }
    }
}
