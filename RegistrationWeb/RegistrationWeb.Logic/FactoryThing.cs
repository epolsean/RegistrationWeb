using RegistrationWeb.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationWeb.Logic
{
    public class FactoryThing<T> where T : RegistrationThing, new()
    {
        public T Create()
        {
            var o = new T();
            return o.Create<T>();
        }
    }
}
