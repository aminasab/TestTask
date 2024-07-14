using MyShop.Models;
using MyShop.Models.DataAccess;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp6.Models;
using WpfApp6.Views;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List <Product> Products { get; set; }
        private ObservableCollection<ProductDetails> _myProductDetails = [];
        private string? _name;
        private string _date = DateTime.Now.ToString("d");
        private ObservableCollection<string> _historyOfPurchase = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            listBoxOfPurchase.ItemsSource = _myProductDetails;
            history.ItemsSource = _historyOfPurchase;
        }

        /// <summary>
        /// Добавление новых товаров в каталог в другом окне.
        /// </summary>
        private void Button_AddProducts(object sender, RoutedEventArgs e)
        {
            DirectoryOfProducts directoryOfProducts = new();
            directoryOfProducts.Show();
        }

        /// <summary>
        /// Добавление товаров в список покупок.
        /// </summary>
        private void ButtonAddToPurchase(object sender, RoutedEventArgs e)
        {
            if (listBoxOfPurchase.Visibility == Visibility.Hidden)
            {
                if (!string.IsNullOrEmpty(_name))
                {
                    listBoxOfPurchase.Visibility = Visibility.Visible;
                    AddProductToList();
                }
                else
                {
                    MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                AddProductToList();
            }
        }

        public void AddProductToList()
        {
            if (!string.IsNullOrEmpty(_name))
            {
                if (!_myProductDetails.Any(product => product.Name == _name))
                {
                    _myProductDetails.Add(new ProductDetails(Name = _name));
                    _name = null;
                }
                else
                {
                    MessageBox.Show("Выбранный товар уже есть в списке. Выберите другой товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработка кликов по TextBox товаров из списка Products.
        /// </summary>
        private void PreviewMouseDownTextBoxOfProducts(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxOfDate.Text))
            {
                MessageBox.Show("Нажмите кнопку - Новая, чтобы начать!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                TextBox? textBox = sender as TextBox;
                if (textBox != null)
                {
                    _name = textBox.Text;
                }
            }
        }

        /// <summary>
        /// Создать новую покупку.
        /// </summary>
        private void ButtonAddNewPurchase(object sender, RoutedEventArgs e)
        {
            textBoxOfDate.Text = _date;
        }

        /// <summary>
        /// Добавление даты прибавления продукта в список покупок.
        /// </summary>
        /// <param name="itemIndex">Индекс карточки продукта</param>
        public void AddingDateToListProductDetails(int itemIndex)
        {
            _myProductDetails[itemIndex].Date = _date;
        }

        /// <summary>
        /// Обработка ввода текста в Textbox количества товаров. Завершением ввода является нажатие кнопки - Enter.
        /// </summary>
        private void TextBoxOfQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Предотвращаем добавление символа новой строки.
                e.Handled = true;
                // Код обработки после нажатия клавиши Enter.
                TextBox textBox = (TextBox)sender;
                string text = textBox.Text;
                ProductDetails product = (ProductDetails)((FrameworkElement)sender).DataContext;
                int itemIndex = listBoxOfPurchase.Items.IndexOf(product);
                int quantity = MyShop.Models.StringConverter.ConvertToInt(text);
                _myProductDetails[itemIndex].Quantity = quantity;
                AddingDateToListProductDetails(itemIndex);
                CheckingFields(itemIndex);
            }
        }

        /// <summary>
        /// Обработка ввода текста в TextBox цены товара. Завершением ввода является нажатие кнопки - Enter.
        /// </summary>
        private void TextBoxOfPrice_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Предотвращаем добавление символа новой строки.
                e.Handled = true;
                // Код обработки после нажатия клавиши Enter.
                TextBox? textBox = sender as TextBox;
                string text = textBox.Text;
                ProductDetails product = (ProductDetails)((FrameworkElement)sender).DataContext;
                int itemIndex = listBoxOfPurchase.Items.IndexOf(product);
                decimal price = MyShop.Models.StringConverter.ConvertToDecimal(text);
                _myProductDetails[itemIndex].Price = price;
                CheckingFields(itemIndex);
            }
        }

        /// <summary>
        /// Проверяем заполнены ли поля - цена и количество для расчета итоговой цены и заполняем, если все верно.
        /// </summary>
        /// <param name="itemIndex">Индекс карточки продукта</param>
        private void CheckingFields(int itemIndex)
        {
            if (_myProductDetails[itemIndex].Quantity != 0 && _myProductDetails[itemIndex].Price != 0)
            {
                _myProductDetails[itemIndex].CalculationGeneralQuantity();
            }
        }

        // Обработка нажатия кнопки - Сбросить.
        private void ButtonResetPurches(object sender, RoutedEventArgs e)
        {
            _myProductDetails.Clear();
            listBoxOfPurchase.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Получение выбранных продуктов с другого окна.
        /// </summary>
        public void SetSelectedProducts(List<Product> selectedProducts)
        {
           itemsOfProducts.ItemsSource= selectedProducts;
        }

        // Обработка нажатия кнопки - Сохранить.
        private void ButtonSaveToHistory(object sender, RoutedEventArgs e)
        {
            ProductsToStringConverter productsToStringConverter = new ProductsToStringConverter();
            _historyOfPurchase.Add( productsToStringConverter.Convert(_myProductDetails));
        }
    }
}