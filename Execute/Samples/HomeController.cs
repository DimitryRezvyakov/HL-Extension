using CustomMVC.App.Core.Http.HttpMethods.Attributes;
using CustomMVC.App.MVC.Controllers.Common.Entities;
using CustomMVC.App.MVC.Controllers.Common.ModelBinding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.Test.Samples
{
    public class IndexPostModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class HomeController : ControllerBase
    {

        [HttpGet]
        public async Task Index()
        {
            await Context.Response.WriteAsync("Index");
        }

        [HttpPost]
        public async Task IndexPost(IndexPostModel model)
        {
            Console.WriteLine(model.Name);
            await Context.Response.WriteAsync("IndexPost");
        }
    }
}
