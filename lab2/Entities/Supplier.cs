namespace lab2.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public override string ToString()
    {
        return Name;
    }
}