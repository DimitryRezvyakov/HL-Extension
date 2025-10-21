using CustomMVC.App.Core.Http.HttpMethods.Abstractions;
using CustomMVC.App.MVC.Controllers.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Common
{
    /// <summary>
    /// Provides all descriptor for all controllers actions
    /// </summary>
    public class ActionDescriptorProvider
    {
        private static readonly ControllersProvider _controllersProvider = ControllersProvider.Instance;
        private static Dictionary<string, ActionDescriptor> _descriptors = new();
        private static object lockObj = new();
        private static ActionDescriptorProvider? _instance;
        public static ActionDescriptorProvider Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (_instance == null) _instance = new ActionDescriptorProvider();
                    return _instance;
                }
            }
        }

        public ActionDescriptor GetDescriptor(string name)
        {
            return _descriptors[name];
        }

        private ActionDescriptorProvider()
        {
            var controllers = _controllersProvider.GetControllersTypes();

            foreach (var controllerType in controllers)
            {
                foreach (var method in controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                                        .Where(m => !m.IsSpecialName))
                {
                    _descriptors.Add(method.Name, new ActionDescriptor(method));
                }
            }
        }
    }
}
