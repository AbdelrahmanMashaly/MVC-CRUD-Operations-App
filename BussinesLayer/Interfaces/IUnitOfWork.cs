using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository employeeRepository { get; set; }

        Task<int> Complete();
        
    }
}
