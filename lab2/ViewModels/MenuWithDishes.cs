using lab2.Entities;

namespace lab2.ViewModels;

public class MenuWithDishes
{
    public Menu? Menu { get; set; }
    public IEnumerable<DishWithPrice>? Dishes { get; set; }
}