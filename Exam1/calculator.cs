using System;
using System.Collections.Generic;

public class SimpleCalculator
{
    private readonly List<object> _expression = new();

    public void EnterNumber(double x)
    {
        _expression.Add(x);
    }

    public void EnterOperator(char op)
    {
        if (op != '+' && op != '-' && op != '*' && op != '/')
            throw new ArgumentException("Operator must be one of: +, -, *, /");
        _expression.Add(op);
    }

    public void Undo()
    {
        if (_expression.Count > 0)
            _expression.RemoveAt(_expression.Count - 1);
    }

    public double Evaluate()
    {
        if (_expression.Count == 0)
            return 0;

        var output = new List<object>();
        var ops = new Stack<char>();
        foreach (var token in _expression)
        {
            if (token is double d)
            {
                output.Add(d);
            }
            else if (token is char op)
            {
                while (ops.Count > 0 && Precedence(ops.Peek()) >= Precedence(op))
                    output.Add(ops.Pop());
                ops.Push(op);
            }
        }
        while (ops.Count > 0)
            output.Add(ops.Pop());

        var stack = new Stack<double>();
        foreach (var token in output)
        {
            if (token is double d)
            {
                stack.Push(d);
            }
            else if (token is char op)
            {
                double b = stack.Pop();
                double a = stack.Pop();
                stack.Push(op switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' => b != 0 ? a / b : throw new DivideByZeroException(),
                    _ => throw new InvalidOperationException("Unknown operator")
                });
            }
        }
        return stack.Count == 1 ? stack.Pop() : 0;
    }

    private int Precedence(char op) => op switch
    {
        '+' or '-' => 1,
        '*' or '/' => 2,
        _ => 0
    };
}

class Program
{
    static void Main()
    {
        var calc = new SimpleCalculator();

        calc.EnterNumber(5);
        calc.EnterOperator('+');
        calc.EnterNumber(3);
        calc.EnterOperator('*');
        calc.EnterNumber(2);
        Console.WriteLine("Result: " + calc.Evaluate());

        calc.Undo();
        Console.WriteLine("Result: " + calc.Evaluate());

        calc.EnterNumber(4);
        Console.WriteLine("Result: " + calc.Evaluate());
    }
}