using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Test app controller
    /// </summary>
    [Controller]
    [Route("test")]
    [AllowAnonymous]
    public class AppController : ControllerBase
    {
        /// <summary>
        /// Index route
        /// </summary>
        [HttpGet]
        public string GetIndex()
        {
            return "<h1>MeetHut</h1>";
        }
    }
}