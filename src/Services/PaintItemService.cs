using MinisAPI.Context;
using MinisAPI.Models;
namespace MinisAPI.Service;
public static class PaintItemService
{
    public static async Task<PaintItem[]> GetAllPaintsAsync(this MinisDbContext dbContext)
    {
        return dbContext.PaintItems.ToArray();
    }

    public static async Task<PaintItem> GetPaintItemByIdAsync(this MinisDbContext dbContext, long id)
    {
        return dbContext?.PaintItems.Find(id);
    }

    public static async void CreateNewPaintAsync(this MinisDbContext dbContext, string name, string hexCode, string brand)
    {
        PaintItem newPaintItem = new PaintItem(name, hexCode, brand);
        dbContext?.PaintItems?.Add(newPaintItem);
        await dbContext.SaveChangesAsync();
    }

    public static async void ModifyPaintAsync(this MinisDbContext dbContext, PaintItem paintItem, string name, string hexCode, string brand)
    {
        paintItem.Name = name;
        paintItem.HexCode = hexCode;
        paintItem.Brand = brand;
        await dbContext.SaveChangesAsync();
    }

    public static async void DeletePaintAsync(this MinisDbContext dbContext, PaintItem paintItem)
    {
        dbContext?.PaintItems?.Remove(paintItem);
        await dbContext.SaveChangesAsync();
    }
}