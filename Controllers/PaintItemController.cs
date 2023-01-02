using Microsoft.AspNetCore.Mvc;
using MinisAPI.Models;
using MinisAPI.Context;
namespace MinisAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PaintItemController : ControllerBase
{
    [HttpGet(Name = "GetPaintItem")]
    public PaintItem[] Get()
    {
        using (MinisDbContext db = new MinisDbContext())
        {
            var paints = db.PaintItems.ToArray();
            return paints;
        }

    }
    [HttpPost(Name = "PostPaintItem")]
    public void Post(string name, string hexCode, string brand)
    {
        Console.WriteLine($"Post Received: {name} - {hexCode}");
        using (MinisDbContext db = new MinisDbContext())
        {
            PaintItem newPaintItem = new PaintItem(name, hexCode, brand);
            db.PaintItems.Add(newPaintItem);
            db.SaveChanges();
        }
    }

    [HttpPut(Name = "PutPaintItem")]
    public void Put(long id, string name, string hexCode)
    {

    }
}