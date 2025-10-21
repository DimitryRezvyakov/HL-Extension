using CustomMVC.App.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Common.Entities
{
    public abstract class ControllerBase
    {
        public HttpContext Context { get; set; } = null!;
        public ModelState ModelState { get; set; } = null!;
    }
}
