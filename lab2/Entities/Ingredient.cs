namespace lab2.Entities;

public class Ingredient
{
    public int DishId { get; set; }
    public int ProductId { get; set; }
    public double Grams { get; set; }

    public override string ToString()
    {
        return $"DishId: {DishId} \n\tProductId: {ProductId} \n\tGrams: {Grams}";
    }
}