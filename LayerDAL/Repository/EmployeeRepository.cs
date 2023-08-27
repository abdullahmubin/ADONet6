using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LayerDAL.Entities;

using LayerDAL.Setting;
using Microsoft.Extensions.Options;

namespace LayerDAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionSetting _connection;

        public EmployeeRepository(IOptions<ConnectionSetting> connection)
        {
            _connection = connection.Value;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> ListEmployees = new List<Employee>();

            using (var connect = new SqlConnection(_connection.SQLString))
            {
                connect.Open();

                SqlCommand cmd = new SqlCommand("sp_getEmployees", connect);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ListEmployees.Add(new Employee()
                        {
                            IdEmployee = Convert.ToInt32(reader["IdEmployee"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString()
                        });
                    }

                    return ListEmployees;
                }
            }
        }
    }
}
