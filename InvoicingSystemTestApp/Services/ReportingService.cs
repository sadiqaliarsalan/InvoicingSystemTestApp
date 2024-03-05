using InvoicingSystemTestApp.Models;
using System.Collections.Generic;

namespace InvoicingSystemTestApp.Services
{
    public class ReportingService : IReportingService
    {
        private readonly List<InvoiceGroup> _invoiceGroups;

        public ReportingService(List<InvoiceGroup> invoiceGroups)
        {
            _invoiceGroups = invoiceGroups;
        }

        public IEnumerable<string> GetRepeatedGuestNames()
        {
            if (_invoiceGroups == null) 
            {
                return Enumerable.Empty<string>();
            }

            IEnumerable<string> repeatedGuestNames = _invoiceGroups
                .SelectMany(group => group?.Invoices ?? new List<Invoice>())
                .SelectMany(invoice => invoice?.Observations ?? new List<Observation>())
                .GroupBy(observation => observation?.GuestName)
                .Where(group => group != null && group.Count() > 1)
                .Select(group => group.Key);

            return repeatedGuestNames;
        }


        public IEnumerable<TravelAgentInfo> GetTotalNightsByTravelAgentFor2015()
        {
            if (_invoiceGroups == null)
            {
                return Enumerable.Empty<TravelAgentInfo>();
            }

            IEnumerable<TravelAgentInfo> numberOfNightsByTravelAgent = _invoiceGroups
                .Where(group => group?.IssueDate.Year == 2015)
                .SelectMany(group => group?.Invoices ?? new List<Invoice>())
                .SelectMany(invoice => invoice?.Observations ?? new List<Observation>())
                .GroupBy(observation => observation?.TravelAgent)
                .Where(group => group != null)
                .Select(group => new TravelAgentInfo
                {
                    TravelAgent = group.Key,
                    TotalNumberOfNights = group.Sum(observation => observation?.NumberOfNights ?? 0)
                });

            return numberOfNightsByTravelAgent;
        }

    }
}