namespace EmployeeManagementSystem;

public class Program
{
    public static void Main()
    {
        var manager = new EmployeeManager();

        manager.AddEmployee(
          new EmployeeName("John Doe"),
          EmployeeType.FullTime,
          new Money(5000),
          new Money(500)
        );
        manager.AddEmployee(
          new EmployeeName("Jane Smith"),
          EmployeeType.PartTime,
          new Money(3000),
          new Money(200)
        );
        manager.AddEmployee(
          new EmployeeName("Bob Johnson"),
          EmployeeType.Contractor,
          new Money(4000),
          new Money(0)
        );

        manager.DisplayAllEmployees();
        manager.ProcessPayroll();
        manager.SaveToFile("employees.txt");
    }
}