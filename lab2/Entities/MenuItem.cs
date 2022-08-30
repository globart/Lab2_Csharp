namespace lab2.Entities;

public class MenuItem
{
    public int DishId { get; set; }
    public int MenuId { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"DishId: {DishId} \n\tMenuId: {MenuId} \n\tPrice: {Price}";
    }
}