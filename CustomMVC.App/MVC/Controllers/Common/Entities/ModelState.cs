using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Common.Entities
{
    public class ModelState
    {
        public object[]? Parameters { get; set; }
        public bool IsValid { get; set; }

        public ModelState() { }

        private ModelState(object[]? parameters, bool isValid)
        {

        }
        public static ModelState FromFailure(Exception? exception = null)
        {
            return new ModelState(null, false);
        }

        public static ModelState FromSuccess(object[]? parameters = null)
        {
            return new ModelState(parameters, true);
        }
    }
}
