// service to verify VAT IDs using the EU VIES web service
namespace InvoicingSystemTestApp.Models
{
    public class VatVerifier
    {
        enum VerificationStatus
        {
            Valid,
            Invalid,
            // Unable to get status (e.g. service unavailable)
            Unavailable
        }

        /// <summary>
        /// Verifies the given VAT ID for the given country using the EU VIES web service.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="vatId"></param>
        /// <returns>Verification status</returns>
        // TODO: Implement Verify method
    }
}

