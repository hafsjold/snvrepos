namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tblpaypal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblpaypal()
        {
            this.tblbankkonto = new HashSet<tblbankkonto>();
        }
        [Key]
        public int pid { get; set; }
        public Nullable<bool> Imported { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Time_Zone { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> Gross { get; set; }
        public Nullable<decimal> Fee { get; set; }
        public Nullable<decimal> Net { get; set; }
        public string From_Email_Address { get; set; }
        public string To_Email_Address { get; set; }
        public string Transaction_ID { get; set; }
        public string Counterparty_Status { get; set; }
        public string Address_Status { get; set; }
        public string Item_Title { get; set; }
        public string Item_ID { get; set; }
        public Nullable<decimal> Shipping_and_Handling_Amount { get; set; }
        public Nullable<decimal> Insurance_Amount { get; set; }
        public Nullable<decimal> Sales_Tax { get; set; }
        public string Option_1_Name { get; set; }
        public string Option_1_Value { get; set; }
        public string Option_2_Name { get; set; }
        public string Option_2_Value { get; set; }
        public string Auction_Site { get; set; }
        public string Buyer_ID { get; set; }
        public string Item_URL { get; set; }
        public Nullable<System.DateTime> Closing_Date { get; set; }
        public string Escrow_Id { get; set; }
        public string Invoice_Id { get; set; }
        public string Reference_Txn_ID { get; set; }
        public string Invoice_Number { get; set; }
        public string Custom_Number { get; set; }
        public string Quantity { get; set; }
        public string Receipt_ID { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }
        public string Contact_Phone_Number { get; set; }
        public Nullable<bool> Bogfoert { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblbankkonto> tblbankkonto { get; set; }
    }
}
