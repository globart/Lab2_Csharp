using lab2.Entities;

namespace lab2.ViewModels;

public class ProductWithDishes
{
    public Product? Product { get; set; }
    public IEnumerable<Dish>? Dishes { get; set; }
}