using Microsoft.AspNetCore.Mvc;

namespace LinkShortening.Controllers;

[Route("Delete")]
public class DeleteController : Controller
{
    private readonly ApplicationDbContext _context;

    public DeleteController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index(Guid id)
    {
        var link = await _context.Urls.FindAsync(id);
        if (link == null) return NotFound();

        return View(link);
    }

    [HttpPost]
    [Route("Index")]
    public async Task<IActionResult> Confirm(Guid id)
    {
        var link = await _context.Urls.FindAsync(id);
        if (link == null) return NotFound();

        _context.Urls.Remove(link);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Url"); 
    }
}