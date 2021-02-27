using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;

namespace WebApi.Controllers.OData
{

    [EnableCors]
    public class PartitionController : ODataController
    {

        private readonly AppDbContext _context;

        public PartitionController(DbContextOptions<AppDbContext> options)
        {
            _context = new AppDbContext(options);
        }

        [EnableQuery]
        public IActionResult Get() { return Ok(_context.Partitions); }

        /// <exception cref="T:System.ArgumentNullException"></exception>
        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_context.Partitions.AsQueryable().FirstOrDefault(c => c.Id == key));
        }

        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Partition payload)
        {
            if (payload == null)
                return BadRequest("Please put valid object in request body.");
            await _context.AddAsync(payload);
            await _context.SaveChangesAsync();
            return Created(payload);
        }

        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">A concurrency violation is encountered while saving to the database.
        ///                 A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///                 This is usually because the data in the database has been modified since it was loaded into memory.</exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">An error is encountered while saving to the database.</exception>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        [HttpPatch]
        [EnableQuery]
        public async Task<IActionResult> Patch([FromBody] Partition payload, int key)
        {
            if (payload == null || key == 0)
                return BadRequest("Please put valid object in request body.");
            var obj = await _context.Partitions.AsQueryable().SingleAsync(c => c.Id == key);
            if (obj == null)
                return BadRequest("Object with this Id was not found.");
            obj.DisplayName = payload.DisplayName; // TODO: Automaper here?
            obj.Uuid = payload.Uuid;
            await _context.SaveChangesAsync();
            return Updated(payload);
        }

    }

}
