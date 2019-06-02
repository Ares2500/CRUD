using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Data.Entidades
{
    public class Employee
    {
        public int idPK { get; set; }
        public string idNumber { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public DateTime Birthday { get; set; }
    }
}
