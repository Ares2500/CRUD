using DataAccess.Repositorios;
using Proyecto.Data.Contratos;
using Proyecto.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Data.Repositorios
{
    public class EmployeeRepository : MasterRepository, IEmployeeRepository
    {

        private string selectAll;
        private string insert;
        private string update;
        private string delete;


        public EmployeeRepository()
        {
            selectAll = "SELECT * FROM Employee";
            insert = "INSERT INTO Employee VALUES(@idNumber, @name, @mail, @birthday)";
            update = "UPDATE Employee SET IdNumber = @idNumber, Name = @name, Mail =@mail, BirthDay = @birthday";
            delete = "DELETE FROM Employee WHERE idPk = @idPK"
        }

        public int Add(Employee entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idNumber", entity.idNumber));
            parameters.Add(new SqlParameter("@Name", entity.Name));
            parameters.Add(new SqlParameter("@mail", entity.Mail));
            parameters.Add(new SqlParameter("@birthday", entity.Birthday));
            return ExecuteNonQuery(insert)
        }

        public int Edit(Employee entity)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idNumber", entity.idNumber));
            parameters.Add(new SqlParameter("@Name", entity.Name));
            parameters.Add(new SqlParameter("@mail", entity.Mail));
            parameters.Add(new SqlParameter("@birthday", entity.Birthday));
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Employee> GetAll()
        {
            var tableResult = ExecuteReader(selectAll);
            var listEmployees = new List<Employee>();
            foreach (DataRow item in tableResult.Rows)
            {
                listEmployees.Add(new Employee
                {
                    idPK = Convert.ToInt32(item[0]),
                    idNumber = item[1].ToString(),
                    Name = item[2].ToString(),
                    Mail = item[3].ToString(),
                    Birthday = Convert.ToDateTime(item[4])
                });
            }

            return listEmployees;
        }

        public int Remove(int idPk)
        {
            parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@idPK", idPk));
            return ExecuteNonQuery(delete);
        }
    }
}
