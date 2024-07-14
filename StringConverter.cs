using System.Windows;

namespace MyShop.Models
{
    internal class StringConverter
    {
        public static int ConvertToInt(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch (FormatException)
            {
                MessageBox.Show("Невозможно сконвертировать в целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }

        public static decimal ConvertToDecimal(string input)
        {
            try
            {
                return decimal.Parse(input);
            }
            catch (FormatException)
            {
                MessageBox.Show("Невозможно сконвертировать в десятичное число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0m; 
            }
        }
    }
}

