namespace Factory
{
    public interface IProduct
    {
        string Description { get; set; }
        double? Discount { get; set; }
        string ImageUrl { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}