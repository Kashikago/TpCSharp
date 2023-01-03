using System.Net;
using MinisAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MinisAPI.Models;
using MinisAPI.Context;
using MinisAPI.Service;
namespace MinisAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PaintItemController : ControllerBase
{
    private readonly MinisDbContext DbContext;
    public PaintItemController(MinisDbContext dbContext)
    {
        if (dbContext != null)
            DbContext = dbContext;
    }

    [HttpGet("/AllPaints")]
    public PaintItem[] GetAll()
    {
        var paints = DbContext.GetAllPaints();
        if (paints == null)
        {
            Response.StatusCode = 404;
            return new PaintItem[0];
        }
        Response.StatusCode = 200;
        return paints;

    }

    [HttpGet("{id}")]
    public PaintItem GetSpecificPaint(long id)
    {

        PaintItem paintFound = DbContext.GetPaintItemById(id);
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

        DbContext.CreateNewPaint(name, hexCode, brand);
    }

    [HttpPut("{id:int}")]
    public void Put(long id, string name, string hexCode, string brand)
    {

        PaintItem paintFound = DbContext.GetPaintItemById(id);
        if (paintFound == null)
        {
            Response.StatusCode = 404;
        }
        if (!hexCode.IsHexcolor())
        {
            Response.StatusCode = 406;
        }
        DbContext.ModifyPaint(paintFound, name, hexCode, brand);
        Response.StatusCode = 200;
    }

    [HttpDelete("{id:int}")]
    public HttpResponseMessage Delete(long id)
    {

        PaintItem? paintFound = DbContext.GetPaintItemById(id);
        if (paintFound == null)
        {
            return new HttpResponseMessage(HttpStatusCode.NotAcceptable);
        }

        DbContext.DeletePaint(paintFound);
        return new HttpResponseMessage(HttpStatusCode.OK);

    }
}