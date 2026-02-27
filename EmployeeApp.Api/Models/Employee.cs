using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Api.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }
}