using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contracts;
using DataAccess.Entities;
using DataAccess.Repositories;
using Domain.ValueObjects;

namespace Domain.Models
{
    public class EmployeeModel : IDisposable
    {
        private int idPK;
        private string idNumber;
        private string name;
        private string mail;
        private DateTime birthday;
        private int age;


        private IEmployeeRepository employeeRepository;
        public EntityState State { private get; set; }
        private List<EmployeeModel> listEmployees;


        //Propiedades /MODELOS DE VISTA /VALIDAR DATOS
        public int IdPK { get => idPK; set => idPK = value; }
        [Required(ErrorMessage = "El campo numero es requerido")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo se permiten numeros")]
        [StringLength(maximumLength:10,MinimumLength =10,ErrorMessage ="Solo se permiten 10 digitos")]
        public string IdNumber { get => idNumber; set => idNumber = value; }
        [Required]
        [RegularExpression("^[a-zA-Z]+$",ErrorMessage = "Solo se permiten letras")]
        [StringLength(maximumLength:100,MinimumLength =3)]
        public string Name { get => name; set => name = value; }
        [Required]
        [EmailAddress]
        public string Mail { get => mail; set => mail = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public int Age { get => age; private set => age = value; }

        public EmployeeModel()
        {
            employeeRepository = new EmployeeRepository();
        }

        public string saveChanges()
        {
            string message = null;
            try
            {
                var employeeDataModel = new Employee();
                employeeDataModel.idPK = IdPK;
                employeeDataModel.idNumber = idNumber;
                employeeDataModel.name = name;
                employeeDataModel.mail = mail;
                employeeDataModel.birthday = birthday;

                switch (State)
                {
                    case EntityState.Added:
                        employeeRepository.Add(employeeDataModel);
                        message = "Se registro exitosamente";
                        break;
                    case EntityState.Deleted:
                        employeeRepository.Remove(IdPK);
                        message = "Se borro exitosamente";
                        break;
                    case EntityState.Modified:
                        employeeRepository.Edit(employeeDataModel);
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

        public List<EmployeeModel> GetAll()
        {
            var employeeDataModel = employeeRepository.GetAll();
            listEmployees = new List<EmployeeModel>();
            foreach (Employee item in employeeDataModel)
            {
                var birthDate = item.birthday;
                listEmployees.Add(new EmployeeModel
                {
                    idPK = item.idPK,
                    IdNumber = item.idNumber,
                    Name = item.name,
                    mail = item.mail,
                    birthday = item.birthday,
                    age = CalculateAge(birthDate)
                });
            }

            return listEmployees;
        }


        public IEnumerable<EmployeeModel> FindById(string filter)
        {
            return listEmployees.FindAll(e => e.idNumber.Contains(filter) || e.name.Contains(filter) );
        }

        private int CalculateAge(DateTime date)
        {
            DateTime dateNow = DateTime.Now;
            return dateNow.Year - date.Year;
        }

        public void Dispose()
        {
            
        }
    }
}
