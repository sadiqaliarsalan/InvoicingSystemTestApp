using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystemTestApp.Services
{
    public class FileWriter : IFileWriter
    {
        private readonly StreamWriter _writer;

        public FileWriter(string path)
        {
            _writer = new StreamWriter(File.Open(path, FileMode.Append)) { AutoFlush = true };
        }

        public void WriteLine(string message)
        {
            _writer.WriteLine(message);
        }
    }

}
