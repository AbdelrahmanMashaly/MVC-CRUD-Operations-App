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
    public class DepartmentRepository :GenericRepository<Department> , IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context): base(context) 
        {

        }

    }
}
