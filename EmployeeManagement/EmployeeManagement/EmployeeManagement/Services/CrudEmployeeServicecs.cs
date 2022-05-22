using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public interface ICrudEmployeeService
    {
        List<EmployeeViewModel> GetAsync();
        Task<EmployeeViewModel?> GetAsync(Guid id);
        Task CreateAsync(EmployeeViewModel newEmployee);
        Task UpdateAsync(Guid id, EmployeeViewModel updatednewEmployee);
        Task RemoveAsync(Guid id);

    }
    public class CrudEmployeeService : ICrudEmployeeService
    {
        private readonly EmployeeDbContext _context;
        public CrudEmployeeService(EmployeeDbContext context)
        {
            _context = context;
        }
        public List<EmployeeViewModel> GetAsync()
        {
            //var listEmployee = _context.Employee.ToList();
            var listEmployee = (from x in _context.Employee
                               select new EmployeeViewModel()
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   BirthDate = x.BirthDate,
                                   Email = x.Email,
                                   Phone = x.Phone,
                                   TimeStartWork = x.TimeStartWork
                               }
                               ).ToList();  
            return listEmployee;
        }

        public async Task<EmployeeViewModel?> GetAsync(Guid id)
        {
            var employee = await(from x in _context.Employee
                            where x.Id == id
                            select  new EmployeeViewModel()
                            {
                                Id=x.Id,
                                Name=x.Name,
                                BirthDate=x.BirthDate,
                                Phone = x.Phone,
                                Email = x.Email,
                                TimeStartWork=x.TimeStartWork
                            }).FirstOrDefaultAsync();
            return employee;
        }
        public async Task CreateAsync(EmployeeViewModel newEmployee)
        {
            var employee = new Employee()
            {
                Id = newEmployee.Id,
                Name =newEmployee.Name,
                BirthDate = newEmployee.BirthDate.ToUniversalTime(),
                Email = newEmployee.Email,
                Phone = newEmployee.Phone,
                TimeStartWork = newEmployee.TimeStartWork.ToUniversalTime()
            };
            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
        } 

        public async Task UpdateAsync(Guid id, EmployeeViewModel updatednewEmployee)
        {
            var employee = await (from x in _context.Employee
                                  where x.Id == id
                                  select x).FirstOrDefaultAsync();
            if (employee != null)
            {
                //employee.BirthDate = updatednewEmployee.BirthDate;
                employee.Name = updatednewEmployee.Name;
                employee.Email = updatednewEmployee.Email;
                employee.Phone = updatednewEmployee.Phone;
            }
            await _context.SaveChangesAsync();
        }
           

        public async Task RemoveAsync(Guid id)
        {
            var employee = await(from x in _context.Employee
                            where x.Id == id
                            select x).FirstOrDefaultAsync();
            if (employee != null)
               _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
