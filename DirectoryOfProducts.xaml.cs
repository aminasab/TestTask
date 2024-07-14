using MyShop.Models.DataAccess;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp6.Models;

namespace WpfApp6.Views
{
    /// <summary>
    /// Логика взаимодействия для DirectoryOfProducts.xaml
    /// </summary>
    public partial class DirectoryOfProducts : Window
    {
        private ShopContext shopContext = new();
        private List<Product> _selectedProducts = [];

        public DirectoryOfProducts()
        {
            InitializeComponent();
            var products = shopContext.Products.ToList();
            dataGridOfProducts.ItemsSource = products;
        }

        /// <summary>
        /// Обработка нажатия кнопки назад.
        /// </summary>
        private void ClickOnButtonBack(object sender, RoutedEventArgs e)
        {
            ShowSelectedProducts();
            this.Close();
        }

        /// <summary>
        /// Сохранение выбранных элементов.
        /// </summary>
        private void SelectionChanged_dataGridOfProducts(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (Product item in dataGridOfProducts.SelectedItems)
            {
                _selectedProducts.Add(item);
            }
        }

        /// <summary>
        /// Передача выбранных продуктов в другое окно.
        /// </summary>
        private void ShowSelectedProducts()
        {
            MainWindow myWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (myWindow != null)
            {
                myWindow.SetSelectedProducts(_selectedProducts);
            }
        }

        /// <summary>
        /// Поиск товара в справочнике по нескольким буквам.
        /// </summary>
        private void SearchProduct(object sender, TextChangedEventArgs e)
        {
            // Получаем текст из TextBox и приводим к нижнему регистру.
            string searchText = searchTextBox.Text.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                var filteredProducts = shopContext.Products.Where(p => p.NameOfProduct.ToLower().Contains(searchText)).ToList();
                dataGridOfProducts.ItemsSource = filteredProducts;
            }
            else
            {
                // Если строка поиска пустая, отображаем все товары.
                dataGridOfProducts.ItemsSource = shopContext.Products.ToList();
            }
        }

        private void ClickOnButtonAddNewProduct(object sender, RoutedEventArgs e)
        {
            var existingProduct = shopContext.Products.FirstOrDefault(p => p.NameOfProduct == searchTextBox.Text);
            if (existingProduct != null)
            {
                MessageBox.Show("Данный товар уже имеется в списке!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!string.IsNullOrEmpty(searchTextBox.Text))
            {
                Product newProduct = new()
                {
                    NameOfProduct = searchTextBox.Text
                };
                shopContext.Products.Add(newProduct);
                shopContext.SaveChanges();
                dataGridOfProducts.ItemsSource = shopContext.Products.ToList();
            }
            else
            {
                MessageBox.Show("Введите наименование товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
