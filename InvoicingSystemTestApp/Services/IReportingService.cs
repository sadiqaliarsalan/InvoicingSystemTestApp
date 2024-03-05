using InvoicingSystemTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingSystemTestApp.Services
{
    public interface IReportingService
    {
        IEnumerable<string> GetRepeatedGuestNames();
        IEnumerable<TravelAgentInfo> GetTotalNightsByTravelAgentFor2015();
    }
}
