using System.Net;
using MinisAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MinisAPI.Models;
using MinisAPI.Context;
namespace MinisAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PaintItemController : ControllerBase
{
    private readonly MinisDbContext DbContext;
    public PaintItemController(MinisDbContext dbContext)
    {
        DbContext = dbContext;
    }

    [HttpGet("/AllPaints")]
    public PaintItem[] GetAll()
    {
        var paints = DbContext.PaintItems.ToArray();
        Response.StatusCode = 200;
        return paints;

    }

    [HttpGet("{id}")]
    public PaintItem GetSpecificPaint(long id)
    {

        PaintItem paintFound = DbContext.PaintItems.Find(id);
        if (paintFound == null)
        {
            Response.StatusCode = 404;
        }

        Response.StatusCode = 200;
        return paintFound;
    }
    [HttpPost()]
    public void Post(string name, string hexCode, string brand)
    {
        Console.WriteLine($"Post Received: {name} - {hexCode}");

        if (!hexCode.IsHexcolor())
        {
            Response.StatusCode = 406;
        }

        PaintItem newPaintItem = new PaintItem(name, hexCode, brand);
        DbContext.PaintItems.Add(newPaintItem);
        DbContext.SaveChanges();

    }

    [HttpPut("{id:int}")]
    public void Put(long id, string name, string hexCode)
    {

        PaintItem paintFound = DbContext.PaintItems.Find(id);
        if (paintFound == null)
        {
            Response.StatusCode = 404;
        }
        if (!hexCode.IsHexcolor())
        {
            Response.StatusCode = 406;
        }

        paintFound.Name = name;
        paintFound.HexCode = hexCode;
        DbContext.SaveChanges();
        Response.StatusCode = 200;


    }

    [HttpDelete("{id:int}")]
    public HttpResponseMessage Delete(long id)
    {

        PaintItem? paintFound = DbContext.PaintItems.ToList().Find(elem => elem.Id == id);
        if (paintFound == null)
        {
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }

        DbContext.PaintItems.Remove(paintFound);
        DbContext.SaveChanges();
        return new HttpResponseMessage(HttpStatusCode.OK);

    }
}