using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics
{
    public class PrintServer
    {
        private readonly Queue<string> _waitingJobs = new();
        private readonly Dictionary<string, string?> _printers = new();

        public PrintServer(IEnumerable<string> printerIds)
        {
            foreach (var id in printerIds)
                _printers[id] = null;
        }

        public void EnqueueJob(string jobId)
        {
            _waitingJobs.Enqueue(jobId);
        }

        public void AssignNext()
        {
            if (_waitingJobs.Count == 0)
                return;

            var idlePrinter = _printers.FirstOrDefault(p => p.Value == null);
            if (idlePrinter.Key != null && idlePrinter.Value == null)
            {
                var jobId = _waitingJobs.Dequeue();
                _printers[idlePrinter.Key] = jobId;
            }
        }

        public void CompleteJob(string printerId, string jobId)
        {
            if (_printers.TryGetValue(printerId, out var currentJob) && currentJob == jobId)
            {
                _printers[printerId] = null;
            }
        }

        public void Status()
        {
            Console.WriteLine("Waiting jobs: " + string.Join(", ", _waitingJobs));
            foreach (var printer in _printers)
            {
                var status = printer.Value ?? "idle";
                Console.WriteLine($"Printer {printer.Key}: {status}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var server = new PrintServer(new[] { "Printer1", "Printer2" });

            server.EnqueueJob("JobA");
            server.EnqueueJob("JobB");
            server.EnqueueJob("JobC");

            server.AssignNext();
            server.AssignNext();
            server.AssignNext();

            server.Status();

            server.CompleteJob("Printer1", "JobA");

            server.AssignNext(); 
            server.Status();
        }
    }
}