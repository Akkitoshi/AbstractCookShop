using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractCookShopServiceDAL.ViewModels
{
    public class CookViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название Блюда")]
        public string CookName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<CookIngridientsViewModel> CookIngridients { get; set; }
    }
}
