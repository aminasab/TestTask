using System.Collections.ObjectModel;
using System.Text;

namespace MyShop.Models
{
    internal class ProductsToStringConverter
    {
        public string Convert(ObservableCollection<ProductDetails> products)
        {
            var sortedProducts = products.OrderBy(p => p.Date).ToList();
            var result = new StringBuilder();
            // Флаг для отслеживания первого продукта.
            bool isFirstProduct = true;

            foreach (var product in sortedProducts)
            {
                if (isFirstProduct)
                {
                    result.Append($"Дата - {product.Date}\n");
                    // Помечаем, что первый продукт уже был обработан.
                    isFirstProduct = false;
                }
                result.Append($"  {product.Name} , {product.Quantity} шт, {product.Price} рублей\n");
            }
            return result.ToString();
        }
    }
}