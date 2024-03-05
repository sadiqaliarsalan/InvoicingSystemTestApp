// service to verify VAT IDs using the EU VIES web service

using System.Text;
using System.Xml.Linq;

namespace InvoicingSystemTestApp.Models
{
    public class VatVerifier
    {
        public enum VerificationStatus
        {
            Valid,
            Invalid,
            Unavailable
        }

        public async Task<VerificationStatus> Verify(string countryCode, string vatId)
        {
            var soapRequest = $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ec.europa.eu:taxud:vies:services:checkVat:types"">
                <soapenv:Header/>
                <soapenv:Body>
                    <urn:checkVat>
                        <urn:countryCode>{countryCode}</urn:countryCode>
                        <urn:vatNumber>{vatId}</urn:vatNumber>
                    </urn:checkVat>
                </soapenv:Body>
            </soapenv:Envelope>";

            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://ec.europa.eu/taxation_customs/vies/services/checkVatService")
                    {
                        Content = new StringContent(soapRequest, Encoding.UTF8, "text/xml")
                    };
                    request.Headers.Add("SOAPAction", "urn:ec.europa.eu:taxud:vies:services:checkVat/checkVat");

                    var response = await client.SendAsync(request);
                    if (!response.IsSuccessStatusCode)
                    {
                        return VerificationStatus.Unavailable;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    return ParseResponse(content);
                }
            }
            catch
            {
                return VerificationStatus.Unavailable;
            }
        }

        private VerificationStatus ParseResponse(string soapResponse)
        {
            var xDoc = XDocument.Parse(soapResponse);
            XNamespace ns = "urn:ec.europa.eu:taxud:vies:services:checkVat:types";

            var validNode = xDoc.Descendants(ns + "valid");
            if (validNode.Any() && bool.TryParse(validNode.First().Value, out bool isValid))
            {
                return isValid ? VerificationStatus.Valid : VerificationStatus.Invalid;
            }

            return VerificationStatus.Unavailable;
        }
    }
}

