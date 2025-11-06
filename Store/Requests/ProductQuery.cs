namespace Store.Requests;

public class ProductQuery
{
    public int id_product { get; set; }
    public string NameProduct { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; }
    public DateOnly CreatedAt { get; set; }
    public int id_category { get; set; }
    
}