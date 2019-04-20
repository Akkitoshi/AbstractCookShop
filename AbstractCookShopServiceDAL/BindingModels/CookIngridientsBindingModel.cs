namespace AbstractCookShopServiceDAL.BindingModels
{
    public class CookIngridientsBindingModel
    {
        public int Id { get; set; }
        public int CookId { get; set; }
        public int IngridientsId { get; set; }
        public int Count { get; set; }
    }
}