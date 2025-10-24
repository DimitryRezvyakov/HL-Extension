using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.DependencyInjection
{
    public interface IServiceProviderCustom
    {
        public static ServiceCollection Services { get; }

        public T GetService<T>();

        public void AddTransient<TInterface, TImplimentation>() where TImplimentation : class, TInterface;
        public void AddScoped<TInterface, TImplimentation>() where TImplimentation : class, TInterface;
        public void AddSingleton<TInterface, TImplimentation>() where TImplimentation : class, TInterface;
    }
}
