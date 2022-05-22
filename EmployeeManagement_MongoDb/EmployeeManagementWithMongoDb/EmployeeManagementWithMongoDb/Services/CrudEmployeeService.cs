using EmployeeManagementWithMongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeManagementWithMongoDb.Services
{
    public interface ICrudEmployeeService
    {
        IMongoCollection<Employee> employeeCollection { get; }
        Task<List<Employee>> GetAsync();
        Task<Employee?> GetAsync(string id);
        Task CreateAsync(Employee newEmployee);
        Task UpdateAsync(string id, Employee updatednewEmployee);
        Task RemoveAsync(string id);

    }
}
