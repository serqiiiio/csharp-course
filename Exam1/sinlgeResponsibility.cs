using System;
using System.Collections.Generic;

public record Order(string Id, int Quantity, decimal UnitPrice, decimal Total = 0);

public class OrderValidator
{
    public bool IsValid(Order order, out string? error)
    {
        if (order.Quantity <= 0)
        {
            error = $"[{DateTime.UtcNow}] Invalid quantity for order {order.Id}";
            return false;
        }
        if (order.UnitPrice <= 0)
        {
            error = $"[{DateTime.UtcNow}] Invalid price for order {order.Id}";
            return false;
        }
        error = null;
        return true;
    }
}

public class OrderCalculator
{
    public decimal CalculateTotal(Order order)
    {
        return order.Quantity * order.UnitPrice;
    }
}

public class OrderReporter
{
    public void PrintReport(IEnumerable<string> logs)
    {
        Console.WriteLine("---- Order Report ----");
        foreach (var log in logs)
            Console.WriteLine(log);
    }
}

public class OrderService
{
    private readonly List<Order> _processedOrders = new();
    private readonly List<string> _logs = new();
    private readonly OrderValidator _validator = new();
    private readonly OrderCalculator _calculator = new();
    private readonly OrderReporter _reporter = new();

    public void ProcessOrders()
    {
        var orders = new List<Order>
        {
            new Order("A100", 2, 15.50m),
            new Order("B200", 1, 99.99m),
            new Order("C300", 5, 7.25m)
        };

        foreach (var order in orders)
        {
            if (!_validator.IsValid(order, out var error))
            {
                _logs.Add(error!);
                continue;
            }

            var total = _calculator.CalculateTotal(order);
            var processedOrder = order with { Total = total };
            _processedOrders.Add(processedOrder);

            var line = $"Order {order.Id}: {order.Quantity} × {order.UnitPrice:C} = {total:C}";
            _logs.Add(line);
        }

        _reporter.PrintReport(_logs);
        _logs.Clear();
    }
}