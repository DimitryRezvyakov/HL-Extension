using CustomMVC.App.Core.Http.HttpMethods.Attributes;
using CustomMVC.App.MVC.Controllers.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.Test.Samples
{
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public async Task Index()
        {
            await Context.Response.WriteAsync("Hello");
        }
    }
}
