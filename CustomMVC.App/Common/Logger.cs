using CustomMVC.App.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Common
{
    public class Logger<T> : Ilogger<T>
    {
        public Type type => typeof(T);

        public void LogError(Exception ex)
        {
            Console.WriteLine($"Error: {ex} in {type}");
        }

        public void LogFatal(string message, Exception ex)
        {
            Console.WriteLine($"Fatal: {message} in {type}", ex);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine($"Info: {message} in {type}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"Warning: {message} in {type}");
        }

        public void LogDebug(string message)
        {

        }
    }
}
