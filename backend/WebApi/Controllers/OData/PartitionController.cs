using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Internal;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OSCommander;
using WebApi.Services;

namespace WebApi.Controllers.OData
{

    [EnableCors]
    public class PartitionController : ODataController
    {

        private readonly AppDbContext _context;
        private readonly PartitionService _service;

        public PartitionController(
            DbContextOptions<AppDbContext> options,
            ILogger<SystemInformationController> logger,
            IConfiguration config,
            IOptions<ConfigEnvironment> envOpt
            )
        {
            _context = new AppDbContext(options);
            var env = envOpt.Value;
            SystemInformation sysInfo;
            if (env.Ssh != null)
                sysInfo = new SystemInformation(logger,
                    new OSCommander.Dtos.SshCredentials(env.Ssh.Host, env.Ssh.Username, config["Ssh:RootPass"]));
            else
                sysInfo = new SystemInformation(logger);
            _service = new PartitionService(sysInfo, _context);
        }

        [EnableQuery]
        public IActionResult Get() { return Ok(_context.Partitions); }

        /// <exception cref="T:System.ArgumentNullException"></exception>
        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_context.Partitions.AsQueryable().FirstOrDefault(c => c.Id == key));
        }

        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateException">An error is encountered while saving to the database.</exception>
        /// <exception cref="T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">A concurrency violation is encountered while saving to the database.
        ///                 A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///                 This is usually because the data in the database has been modified since it was loaded into memory.</exception>
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

        [HttpPost("/partition/mount/{uuid}")]
        public async Task<IActionResult> Mount(string uuid)
        {
            var res = await _service.Mount(uuid);
            return res.Succeed ? Ok(res.Result) : StatusCode(res.StatusCode, res.ErrorMessage);
        }

        [HttpPost("/partition/unmount/{uuid}")]
        public async Task<IActionResult> Unmount(string uuid)
        {
            var res = await _service.Unmount(uuid);
            return res.Succeed ? Ok(res.Result) : StatusCode(res.StatusCode, res.ErrorMessage);
        }

    }

}
