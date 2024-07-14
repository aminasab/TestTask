namespace WpfApp6.Models;

public partial class Product
{
   
    public int Id { get; set; }

    public string NameOfProduct { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; } = [];
}
