using System.ComponentModel;

namespace AbstractCookShopServiceDAL.ViewModel
{
    public class StockIngridientsViewModel
    {
        public int Id { get; set; }
        public int SStockId { get; set; }
        public int IngridientsId { get; set; }
        [DisplayName("Название Ингридиента")]
        public string IngridientsName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
