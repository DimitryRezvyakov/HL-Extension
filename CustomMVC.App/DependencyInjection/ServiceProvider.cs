using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.DependencyInjection
{
    public class ServiceProvider : IServiceProviderCustom
    {
        public static ServiceCollection Services { get; } = new();

        private static ServiceProvider? _instance;
        private static object _instanceLock = new object();

        public ServiceProvider()
        {
            this.AddKnowingServices();
        }

        public static ServiceProvider GetInstance()
        {
            lock (_instanceLock)
            {
                if (_instance == null) _instance = new ServiceProvider();

                return _instance;
            }
        }

        public void ClearScope()
        {
            Services.ClearScope();
        }

        public void AddScoped<TInterface, TImplimentation>() where TImplimentation : class, TInterface
        {
            var implimentation = Activator.CreateInstance(typeof(TImplimentation));

            if (implimentation == null)
                throw new NotSupportedException($"Can not create instance of {typeof(TImplimentation)}");

            Services.Scoped.Add(typeof(TInterface), implimentation);
        }

        public void AddSingleton<TInterface, TImplimentation>() where TImplimentation : class, TInterface
        {
            var implimentation = Activator.CreateInstance(typeof(TImplimentation));

            if (implimentation == null)
                throw new NotSupportedException($"Can not create instance of {typeof(TImplimentation)}");

            Services.Singleton.Add(typeof(TInterface), implimentation);
        }

        public void AddTransient<TInterface, TImplimentation>() where TImplimentation : class, TInterface
        {
            var implimentation = Activator.CreateInstance(typeof(TImplimentation));

            if (implimentation == null)
                throw new NotSupportedException($"Can not create instance of {typeof(TImplimentation)}");

            Services.Transient.Add(typeof(TInterface), implimentation);
        }

        public T GetService<T>()
        {
            return Services.GetService<T>();
        }
    }
}
