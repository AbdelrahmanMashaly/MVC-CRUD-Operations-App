using BussinesLayer.Interfaces;
using DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IDepartmentRepository DepartmentRepository { get ; set; }
        public IEmployeeRepository employeeRepository { get; set; }

        public UnitOfWork(AppDbContext context)
        {
            DepartmentRepository= new DepartmentRepository(context);
            employeeRepository= new EmployeeRepository(context);
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
