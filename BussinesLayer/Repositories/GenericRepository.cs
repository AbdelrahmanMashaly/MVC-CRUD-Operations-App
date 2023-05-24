using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(T item)
        => await _context.Set<T>().AddAsync(item);
            

        public void Delete(T item)
        =>  _context.Set<T>().Remove(item);
         

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E=>E.Department).ToListAsync();
            }
            else 
                return await _context.Set<T>().ToListAsync();
        }
       

        public async Task<T> GetById(int id)
        => await _context.Set<T>().FindAsync(id);


        public void Update(T item)
        =>  _context.Set<T>().Update(item);
         
    }
}
