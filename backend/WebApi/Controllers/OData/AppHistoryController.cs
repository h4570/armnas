using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.OData
{

    [EnableCors]
    public class AppHistoryController : ODataController
    {

        private readonly AppDbContext _context;

        public AppHistoryController(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        [EnableQuery]
        public IActionResult Get() { return Ok(_context.AppHistory); }

    }
}
