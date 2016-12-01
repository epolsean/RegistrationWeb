using RegistrationWeb.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationWeb.Logic.Models
{
    public class PersonTypeDTO : RegistrationThing
    {
        private string _Name = default(string);

        public override string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                IsNull(ref _Name, value);
            }
        }

        public PersonTypeDTO() : base()
        {

        }

        internal override PersonTypeDTO Create<PersonTypeDTO>()
        {
            return new PersonTypeDTO();
        } 

        private void IsNull(ref string data, string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            data = value;
        }
    }
}
