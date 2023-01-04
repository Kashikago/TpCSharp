using MinisAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MinisAPI.Models;
using MinisAPI.Models.DTO;
using MinisAPI.Context;
using MinisAPI.Service;
using AutoMapper;
namespace MinisAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class PaintItemController : ControllerBase
{
    private readonly MinisDbContext DbContext;
    private readonly IMapper _Mapper;
    public PaintItemController(MinisDbContext dbContext, IMapper mapper)
    {

        if (dbContext != null)
            DbContext = dbContext;
        if (mapper != null)
            _Mapper = mapper;
    }

    [HttpGet("/AllPaints")]
    public async Task<IEnumerable<PaintItemDTO>> GetAll()
    {
        PaintItem[] paints = await DbContext.GetAllPaintsAsync();
        if (paints == null)
        {
            Response.StatusCode = 404;
            return null;
        }
        Response.StatusCode = 200;
        return _Mapper.Map<IEnumerable<PaintItemDTO>>(paints);

    }

    [HttpGet("{id}")]
    public async Task<PaintItemDTO> GetSpecificPaint(long id)
    {
        var paintFound = await DbContext.GetPaintItemByIdAsync(id);
        if (paintFound == null)
        {
            Response.StatusCode = 404;
        }

        Response.StatusCode = 200;

        return _Mapper.Map<PaintItemDTO>(paintFound);
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