using System;
using System.Collections.Generic;

namespace Exam1
{
    public class BrowserHistory
    {
        private readonly Stack<string> _back = new();
        private readonly Stack<string> _forward = new();
        private string? _current;

        public void Navigate(string url)
        {
            if (_current != null)
                _back.Push(_current);
            _current = url;
            _forward.Clear();
        }

        public void Back()
        {
            if (_back.Count == 0)
                return;
            _forward.Push(_current!);
            _current = _back.Pop();
        }

        public void Forward()
        {
            if (_forward.Count == 0)
                return;
            _back.Push(_current!);
            _current = _forward.Pop();
        }

        public string? Current()
        {
            return _current;
        }
    }

    class Program
    {
        static void Main()
        {
            var browser = new BrowserHistory();
            browser.Navigate("https://example.com");
            Console.WriteLine(browser.Current());

            browser.Navigate("https://test.com");
            Console.WriteLine(browser.Current());

            browser.Back();
            Console.WriteLine(browser.Current());

            browser.Forward();
            Console.WriteLine(browser.Current());

    }
}