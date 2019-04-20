namespace AbstractCookShopServiceDAL.BindingModels
{
    public class SOrderBindingModel
 {
    public int Id { get; set; }
    public int SClientId { get; set; }
    public int CookId { get; set; }
    public int Count { get; set; }
    public decimal Sum { get; set; }
}
}
