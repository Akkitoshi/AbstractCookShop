using System;
using System.ComponentModel.DataAnnotations;

namespace AbstractCookShopModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class SOrder
    {

        public int Id { get; set; }

        public int SClientId { get; set; }

        public int CookId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public SOrderStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
        public virtual SClient SClient { get; set; }
        public virtual Cook Cook { get; set; }
    }
}

