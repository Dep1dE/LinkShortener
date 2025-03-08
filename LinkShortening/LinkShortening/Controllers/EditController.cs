using LinkShortening.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortening.Controllers;

[Route("Edit")]
public class EditController : Controller
{
    private readonly ApplicationDbContext _context;

    public EditController(ApplicationDbContext context)
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
    public async Task<IActionResult> Index(URL model)
    {

        if (!Uri.IsWellFormedUriString(model.LongUrl, UriKind.Absolute))
        {
            ModelState.AddModelError("LongUrl", "Некорректный URL!");
            return View("Index", model);
        }

        var link = await _context.Urls.FindAsync(model.Id);
        if (link == null) return NotFound();

        link.LongUrl = model.LongUrl;
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Url"); 
    }
}
