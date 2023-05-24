using BussinesLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> ,IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {

        }

        public IQueryable<Employee> GetEmployeeByName(string name) 
           => _context.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));

        
    }
}
