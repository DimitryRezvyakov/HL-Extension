using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.DependencyInjection
{
    public class ServiceCollection : IServiceCollection
    {
        public Dictionary<Type, object> Singleton { get; } = new();
        public Dictionary<Type, object> Transient { get; } = new();
        public Dictionary<Type, object> Scoped { get;  } = new();

        private Dictionary<Type, object> _scope = new();

        public static ServiceCollection Instance;
        public ServiceCollection() { Instance = this; } // for creating purpose only

        public void ClearScope()
        {
            _scope.Clear();
        }

        public T GetService<T>()
        {
            var type = typeof(T);

            if (Singleton.ContainsKey(type))
            {
                return (T)Singleton[type];
            }

            else if (Transient.ContainsKey(type))
            {
                var instancetype = Transient[type].GetType();

                try
                {
                    var instance = Activator.CreateInstance(instancetype);

                    if (instance == null)
                        throw new NotSupportedException($"Can not create a instance of type {instancetype}");

                    return (T)instance;
                }
                catch (Exception)
                {
                    //Only for development
                    throw;
                }
            }

            else if (Scoped.ContainsKey(type))
            {
                if (_scope.ContainsKey(type))
                    return (T)_scope[type];

                else
                {
                    var instancetype = Scoped[type].GetType();

                    try
                    {
                        var instance = Activator.CreateInstance(instancetype);

                        if (instance == null)
                            throw new NotSupportedException($"Can not create a instance of type {instancetype}");

                        return (T)instance;
                    }
                    catch (Exception)
                    {
                        //Only for development
                        throw;
                    }
                }
            }

            else
            {
                throw new Exception($"The dependecy of type {typeof(T)}was not registred");
            }
        }
    }
}
