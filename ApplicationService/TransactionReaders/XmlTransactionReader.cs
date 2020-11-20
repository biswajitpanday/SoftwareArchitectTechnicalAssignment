using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace ApplicationService.TransactionReaders
{
    public class XmlTransactionReader : TransactionBaseReader
    {
        public XmlTransactionReader(ILogger<XmlTransactionReader> logger) : base(logger)
        {
        }

        internal override Dictionary<string, string> StatusMap => new Dictionary<string, string>
            {{"Approved", "A"}, {"Rejected", "R"}, {"Done", "D"}};

        public override string FileType => "xml";

        public override async Task<List<TransactionRawData>> ReadTransactions(string path)
        {
            var xmlFileContent = await GetXmlFileContent(path);
            var xmlTransactions = DeserializeXmlContent(xmlFileContent);
            return CopyToTransactionList(xmlTransactions);
        }

        #region Supported Methods

        private Task<string> GetXmlFileContent(string filePath)
        {
            return File.ReadAllTextAsync(filePath);
        }

        private XmlTransactions DeserializeXmlContent(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(XmlTransactions));
            using TextReader reader = new StringReader(xmlContent);
            return (XmlTransactions) serializer.Deserialize(reader);
        }

        private List<TransactionRawData> CopyToTransactionList(XmlTransactions xmlTransactions)
        {
            var transactions = xmlTransactions.XmlTransactionModel.Select(x => new TransactionRawData
            {
                TransactionId = x.TransactionId,
                TransactionDate = x.TransactionDate,
                OriginalAmount = x.PaymentDetails.OriginalAmount,
                CurrencyCode = x.PaymentDetails.CurrencyCode,
                Status = x.Status
            }).ToList();
            return transactions;
        }

        #endregion
    }

    [XmlRoot("Transactions")]
    public class XmlTransactions
    {
        [XmlElement("Transaction")] public List<XmlTransactionModel> XmlTransactionModel { get; set; }
    }

    public class XmlTransactionModel
    {
        [XmlAttribute("id")] public string TransactionId { get; set; }

        [XmlElement("TransactionDate")] public DateTime TransactionDate { get; set; }

        [XmlElement("PaymentDetails")] public PaymentDetails PaymentDetails { get; set; }

        [XmlElement("Status")] public string Status { get; set; }
    }

    public class PaymentDetails
    {
        [XmlElement("Amount")] public string OriginalAmount { get; set; }

        [XmlElement("CurrencyCode")] public string CurrencyCode { get; set; }
    }
}