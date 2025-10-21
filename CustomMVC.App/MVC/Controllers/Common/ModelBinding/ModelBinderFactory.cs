using CustomMVC.App.MVC.Controllers.Abstractions;
using CustomMVC.App.MVC.Controllers.Common.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Common.ModelBinding
{
    public class ModelBinderFactory
    {
        public static ModelBinderConcrete Create(Type type)
        {
            if (type.IsAssignableTo(typeof(ModelBinderConcrete)))
            {
                var instance = Activator.CreateInstance(type) as ModelBinderConcrete;

                ArgumentNullException.ThrowIfNull(instance);

                return instance;
            }
            var bodyBinder = Activator.CreateInstance(typeof(FromBodyBinder)) as ModelBinderConcrete;

            ArgumentNullException.ThrowIfNull(bodyBinder);

            return bodyBinder;
        }
    }
}
