using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeManagementSystem;

public class EmployeeManager
{
    private const string InvalidEmployeeTypeMessage = "Invalid employee type";
    private const string DataSavedMessage = "Data saved successfully!";
    private const string ErrorSavingFileMessage = "Error saving file: ";
    private const string ErrorLoadingFileMessage = "Error loading file: ";
    private const string EmployeeAddedMessage = "Employee {0} added successfully!";
    private const string EmployeeListHeader = "\n=== Employee List ===";
    private const string ProcessingPayrollHeader = "\n=== Processing Payroll ===";
    private const string CsvSeparator = ",";

    private List<Employee> employees = new List<Employee>();

    public void AddEmployee(EmployeeName name, EmployeeType type, Money baseSalary, Money bonus)
    {
        Employee employee = type switch
        {
            EmployeeType.FullTime => new FullTimeEmployee { Name = name, BaseSalary = baseSalary, Bonus = bonus },
            EmployeeType.PartTime => new PartTimeEmployee { Name = name, BaseSalary = baseSalary, Bonus = bonus },
            EmployeeType.Contractor => new ContractorEmployee { Name = name, BaseSalary = baseSalary, Bonus = bonus },
            _ => throw new ArgumentException(InvalidEmployeeTypeMessage)
        };
        employees.Add(employee);

        Console.WriteLine(string.Format(EmployeeAddedMessage, name));
    }

    public void SaveToFile(string fileName)
    {
        try
        {
            using (var writer = new StreamWriter(fileName))
            {
                foreach (var employee in employees)
                {
                    var type = employee switch
                    {
                        FullTimeEmployee => EmployeeType.FullTime,
                        PartTimeEmployee => EmployeeType.PartTime,
                        ContractorEmployee => EmployeeType.Contractor,
                        _ => EmployeeType.Unknown
                    };
                    writer.WriteLine($"{employee.Name}{CsvSeparator}{type}{CsvSeparator}{employee.BaseSalary.Value}{CsvSeparator}{employee.Bonus.Value}");
                }
            }
            Console.WriteLine(DataSavedMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ErrorSavingFileMessage}{ex.Message}");
        }
    }

    public void LoadFromFile(string fileName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                var lines = File.ReadAllLines(fileName);

                foreach (var line in lines)
                {
                    var parts = line.Split(CsvSeparator);

                    if (parts.Length == 4)
                    {
                        if (Enum.TryParse(parts[1], out EmployeeType type))
                        {
                            AddEmployee(
                                new EmployeeName(parts[0]),
                                type,
                                new Money(decimal.Parse(parts[2])),
                                new Money(decimal.Parse(parts[3]))
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ErrorLoadingFileMessage}{ex.Message}");
        }
    }

    public void DisplayAllEmployees()
    {
        Console.WriteLine(EmployeeListHeader);

        foreach (var employee in employees)
        {
            var type = employee switch
            {
                FullTimeEmployee => EmployeeType.FullTime,
                PartTimeEmployee => EmployeeType.PartTime,
                ContractorEmployee => EmployeeType.Contractor,
                _ => EmployeeType.Unknown
            };
            var salary = employee.CalculateSalary();

            Console.WriteLine($"Name: {employee.Name}, Type: {type}, Salary: ${salary:F2}");
        }
    }

    public void ProcessPayroll()
    {
        Console.WriteLine(ProcessingPayrollHeader);

        decimal totalPayroll = 0;

        foreach (var employee in employees)
        {
            var salary = employee.CalculateSalary();
            totalPayroll += salary;
        }
        Console.WriteLine($"Total payroll: ${totalPayroll:F2}");

    }
}