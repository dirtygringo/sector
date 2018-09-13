using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NM.Sector.Services.Identity.ViewModels;
using NM.Sector.Services.Identity.Commands;
using NM.SharedKernel.Core.Abstraction.Bus;

namespace NM.Sector.Services.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase, IDisposable
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel viewModel)
        {
            //if (!ModelState.IsValid) return BadRequest((ModelState.Errors()));

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState.Errors());

            await _bus.SendAsync(new CreateUser(viewModel.FirstName, viewModel.LastName, viewModel.Email, viewModel.Password));

            return Accepted();
        }

        #endregion

        #region Disposing

        public void Dispose()
        {
            _bus?.Dispose();
        }

        #endregion
    }
}
