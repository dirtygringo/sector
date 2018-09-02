using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NM.Sector.Services.Identity.Contract.Commands;
using NM.SharedKernel.Infrastructure.Bus;

namespace NM.Sector.Services.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields

        private readonly IBusClient _bus;

        #endregion

        #region Constructor

        public AccountController(IBusClient bus)
        {
            _bus = bus;
        }

        #endregion

        #region Actions

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/account/string
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] string value)
        {
            await _bus.SendAsync(new CreateUser("Pera", "Lazic", "pera.lazic@gmail.com", "peralazic"));
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion
    }
}
