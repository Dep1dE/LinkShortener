using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkShortening.Controllers;

public class UrlController : Controller
{
    private readonly ApplicationDbContext _context;

    public UrlController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var links = await _context.Urls.ToListAsync();
        return View(links);
    }

    [HttpGet("{shortUrl}")]
    public async Task<IActionResult> RedirectToOriginal(string shortUrl)
    {
        shortUrl = Uri.UnescapeDataString(shortUrl);
        var url = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);
        if (url == null) return NotFound();

        url.TransitionCount++; 
        await _context.SaveChangesAsync();

        Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0, private";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "-1";
        Response.Headers["Location"] = url.LongUrl;

        return Redirect(url.LongUrl);
    }
}
