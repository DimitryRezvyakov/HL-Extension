using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.DependencyInjection
{
    public interface IServiceCollection
    {
        public Dictionary<Type, object> Singleton { get;}
        public Dictionary<Type, object> Transient { get;}
        public Dictionary<Type, object> Scoped { get;}

        public T GetService<T>();
    }
}
