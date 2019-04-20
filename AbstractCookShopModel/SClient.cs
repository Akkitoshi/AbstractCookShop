using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractCookShopModel
{
    /// <summary>
    /// Клиент магазина
    /// </summary>
    public class SClient
    {
        public int Id { get; set; }

        [Required]
        public string SClientFIO { get; set; }

        [ForeignKey("SClientId")]
        public virtual List<SOrder> SOrders { get; set; }
    }
}
