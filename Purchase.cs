namespace WpfApp6.Models;

public partial class Purchase
{
    public int Id { get; set; }

    public DateOnly DateOfPurchase { get; set; }

    public int QuantityOfPurchases { get; set; }

    public decimal TotalCost { get; set; }

    public int IdProduct { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;
}
