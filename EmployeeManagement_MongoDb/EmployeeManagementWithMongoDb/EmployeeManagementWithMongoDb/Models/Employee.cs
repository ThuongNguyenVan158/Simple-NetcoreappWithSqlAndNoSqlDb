using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementWithMongoDb.Models

{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }

        public string Email { get; set; } = String.Empty;
        public string? Phone { get; set; } = String.Empty;
        public DateTime TimeStartWork { get; set; }
    }
}
