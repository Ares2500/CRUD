using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ClienteModel
    {
        private int _ClienteId;
        private string _Nombre;
        private string _Apellido;
        private string _rfc;
        private string _Domicilio;
        private int _Status;

        private IClienteRepository clienteRepository;
        public EntityState State { private get; set; }
        private List<ClienteModel> listClientes;


        public int ClienteId { get => _ClienteId; set => _ClienteId = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string Apellido { get => _Apellido; set => _Apellido = value; }
        public string Rfc { get => _rfc; set => _rfc = value; }
        public string Domicilio { get => _Domicilio; set => _Domicilio = value; }
        public int Status { get => _Status; set => _Status = value; }


        public ClienteModel()
        {
            clienteRepository = new ClienteRepository();
        }


        public string saveChanges()
        {
            string message = null;
            try
            {
                var clienteDataModel = new Cliente();

                clienteDataModel.ClienteId = ClienteId;
                clienteDataModel.Nombre = Nombre;
                clienteDataModel.Apellido = Apellido;
                clienteDataModel.rfc = Rfc;
                clienteDataModel.Domicilio = Domicilio;

                switch (State)
                {
                    case EntityState.Added:
                        clienteRepository.Add(clienteDataModel);
                        message = "Se registro exitosamente";
                        break;
                    case EntityState.Deleted:
                        clienteRepository.Remove(ClienteId);
                        message = "Se borro exitosamente";
                        break;
                    case EntityState.Modified:
                        clienteRepository.Edit(clienteDataModel);
                        message = "Se modifico exitosamente";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException sqlex = ex as System.Data.SqlClient.SqlException;
                if (sqlex != null && sqlex.Number == 2627)
                    message = "Registro Duplicado";
                else
                    message = ex.ToString();
            }

            return message;
        }


        public List<ClienteModel> GetAll()
        {
            var clienteDataModel = clienteRepository.GetAll();
            listClientes = new List<ClienteModel>();
            foreach (Cliente item in clienteDataModel)
            {
                listClientes.Add(new ClienteModel
                {
                    ClienteId = item.ClienteId,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Rfc = item.rfc,
                    Domicilio = item.Domicilio,
                });
            }

            return listClientes;
        }
    }
}
