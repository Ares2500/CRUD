using DataAccess.Contracts;
using DataAccess.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ClienteRepository : MasterRepository, IClienteRepository
    {

        private string selectAll;
        private string insert;
        private string update;
        private string delete;

        public ClienteRepository()
        {
            selectAll = "SELECT * FROM cliente";
            insert = "INSERT INTO cliente (Nombre, Apellido, rfc, Domicilio) VALUES (@nombre, @apellido, @rfc, @domicilio);";
            update = "UPDATE cliente SET Nombre = @nombre, Apellido = @apellido, rfc =@rfc, Domicilio = @domicilio WHERE ClienteId = @clienteId";
            delete = "DELETE FROM cliente WHERE ClienteId = @idPK";
        }

        public int Add(Cliente entity)
        {
            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@nombre", entity.Nombre));
            parameters.Add(new MySqlParameter("@apellido", entity.Apellido));
            parameters.Add(new MySqlParameter("@rfc", entity.rfc));
            parameters.Add(new MySqlParameter("@domicilio", entity.Domicilio));
            return ExecuteNonQuery(insert);
        }

        public int Edit(Cliente entity)
        {
            parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@clienteId", entity.ClienteId));
            parameters.Add(new MySqlParameter("@nombre", entity.Nombre));
            parameters.Add(new MySqlParameter("@apellido", entity.Apellido));
            parameters.Add(new MySqlParameter("@rfc", entity.rfc));
            parameters.Add(new MySqlParameter("@domicilio", entity.Domicilio));
            return ExecuteNonQuery(update);
        }

        public IEnumerable<Cliente> GetAll()
        {
            var tableResult = ExecuteReader(selectAll);
            var listClientes = new List<Cliente>();
            foreach (DataRow item in tableResult.Rows)
            {
                listClientes.Add(new Cliente
                {
                    ClienteId = Convert.ToInt32(item[0]),
                    Nombre = item[1].ToString(),
                    Apellido = item[2].ToString(),
                    rfc = item[3].ToString(),
                    Domicilio = item[4].ToString(),
                });
            }

            return listClientes;
        }

        public int Remove(int idPk)
        {
            throw new NotImplementedException();
        }

        
    }
}
