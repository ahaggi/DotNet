using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace MVC.Controllers {

    [Route("")]
    [Route("[controller]")]
    public class TestController : Controller {
        // GET: /<controller>/
        [HttpGet("")]
        [HttpGet("Index")]
        public string Index() {
            return "viewwwwwasdsadwww  w";
        }
    }
}
