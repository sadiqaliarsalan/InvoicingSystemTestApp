// collection of invoices issues on a specific date
namespace InvoicingSystemTestApp.Models
{
    public class InvoiceGroup
    {
        public DateTime IssueDate { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
