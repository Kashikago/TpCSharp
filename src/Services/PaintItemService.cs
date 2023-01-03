using MinisAPI.Context;
using MinisAPI.Models;
namespace MinisAPI.Service;
public static class PaintItemService
{
    public static async Task<PaintItem[]> GetAllPaints(this MinisDbContext dbContext)
    {
        return dbContext.PaintItems.ToArray();
    }

    public static async Task<PaintItem> GetPaintItemById(this MinisDbContext dbContext, long id)
    {
        return dbContext?.PaintItems.Find(id);
    }

    public static async void CreateNewPaint(this MinisDbContext dbContext, string name, string hexCode, string brand)
    {
        PaintItem newPaintItem = new PaintItem(name, hexCode, brand);
        dbContext?.PaintItems.Add(newPaintItem);
        dbContext?.SaveChanges();
    }

    public static async void ModifyPaint(this MinisDbContext dbContext, PaintItem paintItem, string name, string hexCode, string brand)
    {
        paintItem.Name = name;
        paintItem.HexCode = hexCode;
        paintItem.Brand = brand;
        dbContext.SaveChanges();
    }

    public static async void DeletePaint(this MinisDbContext dbContext, PaintItem paintItem)
    {
        dbContext.PaintItems.Remove(paintItem);
        dbContext.SaveChanges();
    }
}