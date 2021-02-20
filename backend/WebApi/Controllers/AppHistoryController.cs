using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("app-history")]
    [ApiController]
    [EnableCors]
    public class AppHistoryController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AppHistoryController(IOptions<ConfigEnvironment> config, DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        [HttpGet]
        [Route("table/{tableId}")]
        public async Task<ActionResult<List<AppHistory>>> GetAll(AppTable tableId)
        {
            return await _context
                .AppHistory
                .AsQueryable()
                .Where(c => c.TableId == tableId)
                .ToListAsync();
        }

        [HttpGet]
        [Route("table/{tableId}/element/{elementId}")]
        public async Task<ActionResult<List<AppHistory>>> GetAll(AppTable tableId, int elementId)
        {
            return await _context
                .AppHistory
                .AsQueryable()
                .Where(c => c.TableId == tableId && c.ElementId == elementId)
                .ToListAsync();
        }

    }
}
