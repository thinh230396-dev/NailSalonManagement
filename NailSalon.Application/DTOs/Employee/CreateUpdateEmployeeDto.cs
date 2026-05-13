namespace NailSalon.Application.DTOs.Employee;

public class CreateUpdateEmployeeDto
{
	public string FullName { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Position { get; set; } = string.Empty;
}