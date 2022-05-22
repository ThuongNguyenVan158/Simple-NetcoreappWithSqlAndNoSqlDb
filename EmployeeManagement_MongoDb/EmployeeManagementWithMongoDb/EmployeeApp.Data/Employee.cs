using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.Data
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }

        public string Email { get; set; } = String.Empty;
        public string? Phone { get; set; } = String.Empty;
        public DateTime TimeStartWork { get; set; }
    }
}
