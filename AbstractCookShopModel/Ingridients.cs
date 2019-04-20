
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractCookShopModel
{
    /// <summary>
    /// ингридиенты, требуемые для изготовления Блюда
    /// </summary>
    public class Ingridients
    {
        public int Id { get; set; }
        [Required]
        public string IngridientsName { get; set; }
        public virtual List<CookIngridients> CookIngridientss { get; set; }
        public virtual List<StockIngridients> StockIngridientss { get; set; }
    }
}
