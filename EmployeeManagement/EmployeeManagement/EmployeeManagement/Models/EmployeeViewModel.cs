namespace EmployeeManagement.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime TimeStartWork { get; set; }
    }
}
