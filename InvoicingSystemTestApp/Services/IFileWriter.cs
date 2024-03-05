using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystemTestApp.Services
{
    public interface IFileWriter
    {
        void WriteLine(string message);
    }
}
