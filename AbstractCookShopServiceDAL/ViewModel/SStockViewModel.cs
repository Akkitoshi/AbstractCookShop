using System.Collections.Generic;
using System.ComponentModel;

namespace AbstractCookShopServiceDAL.ViewModel
{
   public class SStockViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string SStockName { get; set; }
        public List<StockIngridientsViewModel> StockIngridientss { get; set; }
    }
}

