using System.Collections.Generic;

namespace AbstractCookShopServiceDAL.BindingModels
{
    public class CookBindingModel
    {
        public int Id { get; set; }
        public string CookName { get; set; }
        public decimal Price { get; set; }
        public List<CookIngridientsBindingModel> CookIngridientss { get; set; }
    }
}