using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationWeb.Logic.Interfaces
{
    public abstract class RegistrationThing : IThing
    {
        public Guid Id { get; }

        public virtual int AppId { get; set; }

        public virtual string Name { get; set; }

        public DateTime Creation { get; }

        internal RegistrationThing()
        {
            Id = Guid.NewGuid();
            Name = "none";
            Creation = DateTime.Now;
        }

        internal abstract T Create<T>() where T : new();
    }
}
