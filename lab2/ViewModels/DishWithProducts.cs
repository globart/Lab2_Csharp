using lab2.Entities;

namespace lab2.ViewModels;

public class DishWithProducts
{
    public Dish? Dish { get; set; }
    public IEnumerable<Product>? Products { get; set; }
}