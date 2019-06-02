using Proyecto.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DataAccess.Contratos
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
