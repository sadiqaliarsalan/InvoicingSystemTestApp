using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystemTestApp.Services
{
    public class Logger : ILogger
    {
        private readonly IFileWriter _writer;
        private readonly ITimeProvider _timeProvider;

        public Logger(IFileWriter writer, ITimeProvider timeProvider)
        {
            _writer = writer;
            _timeProvider = timeProvider;
            Log("Logger initialized");
        }

        public void Log(string message)
        {
            var currentTime = _timeProvider.Now;
            _writer.WriteLine($"[{currentTime:dd.MM.yy HH:mm:ss}] {message}");
        }
    }
}
