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
    public async Task<PaintItem[]> GetAll()
    {
        PaintItem[] paints = await DbContext.GetAllPaintsAsync();
        if (paints == null)
        {
            Response.StatusCode = 404;
            return null;
        }
        Response.StatusCode = 200;
        return paints;

    }

    [HttpGet("{id}")]
    public async Task<PaintItem> GetSpecificPaint(long id)
    {

        var paintFound = await DbContext.GetPaintItemByIdAsync(id);
        if (paintFound == null)
        {
            Response.StatusCode = 404;
        }

        Response.StatusCode = 200;
        return paintFound;
    }
    [HttpPost()]
    public async void Post(string name, string hexCode, string brand)
    {
        Console.WriteLine($"Post Received: {name} - {hexCode}");

        if (!hexCode.IsHexcolor())
        {
            Response.StatusCode = 406;
        }

        DbContext.CreateNewPaintAsync(name, hexCode, brand);
    }

    [HttpPut("{id:int}")]
    public async void Put(long id, string name, string hexCode, string brand)
    {

        PaintItem paintFound = await DbContext.GetPaintItemByIdAsync(id);
        if (paintFound == null)
        {
            Response.StatusCode = 404;
        }
        if (!hexCode.IsHexcolor())
        {
            Response.StatusCode = 406;
        }
        DbContext.ModifyPaintAsync(paintFound, name, hexCode, brand);
        Response.StatusCode = 200;
    }

    [HttpDelete("{id:int}")]
    public async void Delete(long id)
    {

        PaintItem? paintFound = await DbContext.GetPaintItemByIdAsync(id);
        if (paintFound == null)
        {
            Response.StatusCode = 404;
        }

        DbContext.DeletePaintAsync(paintFound);
        Response.StatusCode = 200;

    }
}