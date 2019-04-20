using System.ComponentModel.DataAnnotations;

namespace AbstractCookShopModel
{
    /// <summary>
    /// Сколько Ингридиента, требуется при изготовлении Блюда
    /// </summary>
    public class CookIngridients
    {
        public int Id { get; set; }

        public int CookId { get; set; }

        public int IngridientsId { get; set; }

        public string IngridientsName { get; set; }

        public int Count { get; set; }

        public virtual Ingridients Ingridients { get; set; }
    }
}

