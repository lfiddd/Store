namespace Store.Requests;

public class ChangeMindSet
{
    public int order_id { get; set; }
    public string DeliveryAddress { get; set; }
    public int PaymentType { get; set; }
}