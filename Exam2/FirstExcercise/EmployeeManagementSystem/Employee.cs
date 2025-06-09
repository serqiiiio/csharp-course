namespace EmployeeManagementSystem;

public abstract class Employee
{
    public EmployeeName Name { get; set; }
    public Money BaseSalary { get; set; }
    public Money Bonus { get; set; }
    public abstract decimal CalculateSalary();
}

public class FullTimeEmployee : Employee
{
    public override decimal CalculateSalary() => BaseSalary + Bonus;
}

public class PartTimeEmployee : Employee
{
    private const decimal PartTimeSalaryMultiplier = 0.8m;
    public override decimal CalculateSalary() => BaseSalary * PartTimeSalaryMultiplier + Bonus;
}

public class ContractorEmployee : Employee
{
    public override decimal CalculateSalary() => BaseSalary;
}