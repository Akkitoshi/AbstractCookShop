using System.ComponentModel;

namespace AbstractCookShopServiceDAL.ViewModels
{
    public class CookIngridientsViewModel
    {
        public int Id { get; set; }
        public int CookId { get; set; }
        public int IngridientsId { get; set; }
        public string IngridientsName { get; set; }
        public int Count { get; set; }
    }
}