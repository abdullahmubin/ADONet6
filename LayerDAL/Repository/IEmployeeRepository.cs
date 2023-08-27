using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LayerDAL.Entities;

namespace LayerDAL.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
    }
}
