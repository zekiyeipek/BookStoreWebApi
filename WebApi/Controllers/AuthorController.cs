using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Abstract;


[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _authorService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _authorService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
    {
        await _authorService.AddAsync(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAuthorDto dto)
    {
        await _authorService.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _authorService.DeleteAsync(id);
        return Ok();
    }
}
