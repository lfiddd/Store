namespace Store.Requests;

public class BasketQuery
{
    public int ProdCount { get; set; }
    public decimal ResultPrice { get; set; }
    public bool IsOrdered { get; set; }
    public int id_user { get; set; }
    public int[] id_product { get; set; }
    public int id_order { get; set; }

}