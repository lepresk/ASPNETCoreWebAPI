using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using RestApi.Models.Dto;

namespace RestApi.Controlles;

[ApiController]
[Route("api/posts")]
[Produces("application/json")]
public class PostController : ControllerBase
{

    private readonly ModelContext _context;


    public PostController(ModelContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> Get()
    {
        var posts = await _context.Posts.ToListAsync();

        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> Get(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> Post([FromBody] CreatePostDto postDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var post = new Post()
        {
            Title = postDto.Title,
            Content = postDto.Content,
            CreatedAt = DateTime.Now,
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CreatePostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return BadRequest();
        }

        post.Title = postDto.Title;
        post.Content = postDto.Content;

        _context.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific TodoItem.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
