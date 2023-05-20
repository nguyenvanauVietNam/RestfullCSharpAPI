using System.ComponentModel.DataAnnotations;

namespace Restful.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; } = 1;
        public string Code { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Sex { get; set; } = 1;
        public string Avatar { get; set; } = string.Empty;

    }
}
