using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess.Repositories
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
            insert = "insert INTO Employee (IdNumber, Name, Mail, Birthday) VALUES (@idNumber, @name, @mail, @birthday);";
            update = "UPDATE Employee SET IdNumber = @idNumber, Name = @name, Mail =@mail, BirthDay = @birthday WHERE idPk = @idPK";
            delete = "DELETE FROM Employee WHERE idPk = @idPK";
        }

        public int Add(Employee entity)
        {

            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idNumber", entity.idNumber));
            parameters.Add(new MySqlParameter("@Name", entity.name));
            parameters.Add(new MySqlParameter("@mail", entity.mail));
            parameters.Add(new MySqlParameter("@birthday", entity.birthday));
            return ExecuteNonQuery(insert);
        }

        public int Edit(Employee entity)
        {
            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idPK", entity.idPK));
            parameters.Add(new MySqlParameter("@idNumber", entity.idNumber));
            parameters.Add(new MySqlParameter("@Name", entity.name));
            parameters.Add(new MySqlParameter("@mail", entity.mail));
            parameters.Add(new MySqlParameter("@birthday", entity.birthday));
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
                    name = item[2].ToString(),
                    mail = item[3].ToString(),
                    birthday = Convert.ToDateTime(item[4])
                });
            }

            return listEmployees;
        }

        public int Remove(int idPk)
        {
            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idPK", idPk));
            return ExecuteNonQuery(delete);
        }
    }
}
