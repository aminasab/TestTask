using System.ComponentModel;

namespace MyShop.Models
{
    internal class ProductDetails: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        private decimal generalPrice;

        public decimal GeneralPrice
        {
            get { return generalPrice; }
            set
            {
                if (generalPrice != value)
                {
                    generalPrice = value;
                    OnPropertyChanged(nameof(GeneralPrice));
                }
            }
        }
        public string Date { get; set; }

        public ProductDetails() { }
        public ProductDetails(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Вычисление итоговой цены.
        /// </summary>
        public void CalculationGeneralQuantity()
        {
            GeneralPrice= Quantity * Price;
        }
    }
}