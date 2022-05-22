using EmployeeManagementWithMongoDb.Models;
using EmployeeManagementWithMongoDb.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EmployeeManagementWithMongoDb.Data
{
    public class EmployeeDbContext: ICrudEmployeeService
    {
        public readonly IMongoDatabase _db;
        public EmployeeDbContext(IOptions<EmployeeDbSetting> employeeDbSettings)
        {
            var mongoClient = new MongoClient(
                employeeDbSettings.Value.ConnectionString);

            _db = mongoClient.GetDatabase(
                employeeDbSettings.Value.DatabaseName);
        }
        public IMongoCollection<Employee> employeeCollection =>
            _db.GetCollection<Employee>("Employee");
        public async Task<List<Employee>> GetAsync()
        {
            return await employeeCollection.Find(a => true).ToListAsync();
        }
        public async Task<Employee?> GetAsync(string id)
        {
            return await employeeCollection.Find(x => x._id == id).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Employee newEmployee)
        {
            await employeeCollection.InsertOneAsync(newEmployee);
        }
        public async Task UpdateAsync(string id, Employee updatednewEmployee)
        {
            var filter = Builders<Employee>.Filter.Eq(c => c._id, id);
            var update = Builders<Employee>.Update
                .Set("Name", updatednewEmployee.Name)
                .Set("BirthDate", updatednewEmployee.BirthDate)
                .Set("Email", updatednewEmployee.Email)
                .Set("Phone", updatednewEmployee.Phone)
                .Set("TimeStartWork", updatednewEmployee.TimeStartWork);
           await employeeCollection.UpdateOneAsync(filter, update);
        }
        public async Task RemoveAsync(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(c => c._id, id);
            await employeeCollection.DeleteOneAsync(filter);
        }

    }
}
