using Microsoft.EntityFrameworkCore;
namespace MinisAPI.Models;

public class PaintItem
{
    public PaintItem() { }
    public PaintItem(string name, string hexcode, string brand)
    {
        Name = name;
        HexCode = hexcode;
        Brand = brand;
    }
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? HexCode { get; set; }
    public string? Brand { get; set; }
    public List<int> Ratings { get; set; } = new List<int>();
}