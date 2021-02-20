using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors]
    public class ShelfController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ILogger<ShelfController> _logger;

        public ShelfController(DbContextOptions<AppDbContext> options, ILogger<ShelfController> logger)
        {
            _context = new AppDbContext(options);
            _logger = logger;
        }

        [Produces("application/json")]
        [HttpGet]
        [Route("/shelfs")]
        public async Task<ActionResult<List<Shelf>>> GetAll()
        {
            _logger.LogError("Hello!");
            return await _context.Shelfs.AsQueryable().ToListAsync();
        }

    }
}
