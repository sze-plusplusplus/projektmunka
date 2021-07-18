using Microsoft.AspNetCore.Mvc;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Test app controller
    /// </summary>
    [Controller]
    [Route("test")]
    public class AppController
    {
        /// <summary>
        /// Index route
        /// </summary>
        [HttpGet]
        public string GetIndex()
        {
            return "Hello, World";
        }
    }
}