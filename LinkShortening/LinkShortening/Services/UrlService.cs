using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LinkShortening.Services;

public class UrlService
{
    private readonly ApplicationDbContext _context;
    private static readonly Random _random = new Random();
    private const string MyBase64 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_"; 

    public UrlService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateShortUrlAsync()
    {
        string shortUrl;
        do
        {
            shortUrl = new string(Enumerable.Repeat(MyBase64, 8)
                           .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        while (await _context.Urls.AnyAsync(x => x.ShortUrl == shortUrl)); 

        return $"test.com/{shortUrl}";
    }
}
