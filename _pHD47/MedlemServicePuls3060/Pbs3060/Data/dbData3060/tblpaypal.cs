namespace Pbs3060
{
    using System;
    using System.Collections.Generic;

    public partial class Tblpaypal
    {
        public Tblpaypal()
        {
            this.Tblbankkonto = new HashSet<Tblbankkonto>();
        }
        public int Pid { get; set; }
        public bool? Imported { get; set; }
        public DateTime Date { get; set; }
        public string TimeZone { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Fee { get; set; }
        public decimal? Net { get; set; }
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string TransactionId { get; set; }
        public string CounterpartyStatus { get; set; }
        public string AddressStatus { get; set; }
        public string ItemTitle { get; set; }
        public string ItemId { get; set; }
        public decimal? ShippingAndHandlingAmount { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public decimal? SalesTax { get; set; }
        public string Option1Name { get; set; }
        public string Option1Value { get; set; }
        public string Option2Name { get; set; }
        public string Option2Value { get; set; }
        public string AuctionSite { get; set; }
        public string BuyerId { get; set; }
        public string ItemUrl { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string EscrowId { get; set; }
        public string InvoiceId { get; set; }
        public string ReferenceTxnId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomNumber { get; set; }
        public string Quantity { get; set; }
        public string ReceiptId { get; set; }
        public decimal? Balance { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string ContactPhoneNumber { get; set; }
        public bool? Bogfoert { get; set; }
    
        public ICollection<Tblbankkonto> Tblbankkonto { get; set; }
    }
}
