using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractCookShopModel
{
    /// <summary>
    /// Блюдо, изготавливаемый в магазине
    /// </summary>
    public class Cook
    {
        public int Id { get; set; }
        [Required]
        public string CookName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<SOrder> SOrders { get; set; }
    }
}

