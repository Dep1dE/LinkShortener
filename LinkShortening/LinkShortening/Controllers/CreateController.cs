using LinkShortening.Models;
using LinkShortening.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortening.Controllers;

[Route("Create")]
public class CreateController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UrlService _urlService;

    public CreateController(ApplicationDbContext context, UrlService urlService)
    {
        _context = context;
        _urlService = urlService;
    }

    [HttpGet]
    [Route("Index")]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("Index")]
    public async Task<IActionResult> CreateShortLink(URL model)
    {
        if (!Uri.IsWellFormedUriString(model.LongUrl, UriKind.Absolute))
        {
            ModelState.AddModelError("LongUrl", "Некорректный URL!");
            return View("Index", model);
        }

        model.ShortUrl = await _urlService.GenerateShortUrlAsync();
        model.CreatedAt = DateTime.UtcNow.ToLocalTime();
        _context.Urls.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Url"); 
    }
}
