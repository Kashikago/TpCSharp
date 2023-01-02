using System.Net;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MinisAPI.Models;
using MinisAPI.Context;
namespace MinisAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PaintItemController : ControllerBase
{

    [HttpGet("/AllPaints")]
    public PaintItem[] GetAll()
    {
        using (MinisDbContext db = new MinisDbContext())
        {
            var paints = db.PaintItems.ToArray();
            return paints;
        }

    }

    [HttpGet("{id}")]
    public PaintItem GetSpecificPaint(long id)
    {
        using (MinisDbContext db = new MinisDbContext())
        {
            PaintItem paintFound = db.PaintItems.Find(id);
            if (paintFound == null)
            {
                return new PaintItem();
            }

            return paintFound;
        }
    }
    [HttpPost()]
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

    [HttpPut("{id:int}")]
    public void Put(long id, string name, string hexCode)
    {

    }

    [HttpDelete("{id:int}")]
    public HttpResponseMessage Delete(long id)
    {
        using (MinisDbContext db = new MinisDbContext())
        {
            PaintItem? paintFound = db.PaintItems.ToList().Find(elem => elem.Id == id);
            if (paintFound == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            }

            db.PaintItems.Remove(paintFound);
            db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}