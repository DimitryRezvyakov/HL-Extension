using CustomMVC.App.Common.Extensions;
using CustomMVC.App.MVC.Controllers.Common.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Common
{
    public class ControllersProvider
    {
        private static ControllersProvider? _instance;
        private static object lockObj = new object();
        private readonly ConcurrentDictionary<string, Type> _controllers = new();
        public static ControllersProvider Instance
        {
            get
            {
                lock(lockObj)
                {
                    if (_instance == null) _instance = new ControllersProvider();
                    return _instance;
                }
            }
        }

        public Type GetController(string name)
        {
            return _controllers[name.ToLower()];
        }

        public string[] GetControllersNames()
        {
            return _controllers.Keys.ToArray();
        }

        public Type[] GetControllersTypes()
        {
            return _controllers.Values.ToArray();
        }

        private ControllersProvider()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

            var controllers = assembly.GetTypes().Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract).ToList();

            foreach (var controller in controllers)
            {
                string name = controller.Name.RawControllerName();

                _controllers.TryAdd(name, controller);
            }
        }
    }
}
