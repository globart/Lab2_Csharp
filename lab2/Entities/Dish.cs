namespace lab2.Entities;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Name: {Name}";
    }
}