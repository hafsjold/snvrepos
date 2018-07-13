namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsmembership_memberships
    {
        [Key]
        public int id { get; set; }
        public int category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int term_id { get; set; }
        public string thumb { get; set; }
        public int thumb_w { get; set; }
        public string sku { get; set; }
        public decimal price { get; set; }
        public bool use_renewal_price { get; set; }
        public decimal renewal_price { get; set; }
        public bool recurring { get; set; }
        public int recurring_times { get; set; }
        public string share_redirect { get; set; }
        public int period { get; set; }
        public string period_type { get; set; }
        public bool use_trial_period { get; set; }
        public int trial_period { get; set; }
        public string trial_period_type { get; set; }
        public decimal trial_price { get; set; }
        public bool unique { get; set; }
        public bool no_renew { get; set; }
        public int stock { get; set; }
        public bool activation { get; set; }
        public bool action { get; set; }
        public string thankyou { get; set; }
        public string redirect { get; set; }
        public bool user_email_use_global { get; set; }
        public bool user_email_mode { get; set; }
        public string user_email_from { get; set; }
        public string user_email_from_addr { get; set; }
        public string user_email_new_subject { get; set; }
        public string user_email_new_text { get; set; }
        public string user_email_approved_subject { get; set; }
        public string user_email_approved_text { get; set; }
        public string user_email_denied_subject { get; set; }
        public string user_email_denied_text { get; set; }
        public string user_email_renew_subject { get; set; }
        public string user_email_renew_text { get; set; }
        public string user_email_upgrade_subject { get; set; }
        public string user_email_upgrade_text { get; set; }
        public string user_email_addextra_subject { get; set; }
        public string user_email_addextra_text { get; set; }
        public string user_email_expire_subject { get; set; }
        public string user_email_expire_text { get; set; }
        public int expire_notify_interval { get; set; }
        public bool admin_email_mode { get; set; }
        public string admin_email_from_addr { get; set; }
        public string admin_email_to_addr { get; set; }
        public string admin_email_new_subject { get; set; }
        public string admin_email_new_text { get; set; }
        public string admin_email_approved_subject { get; set; }
        public string admin_email_approved_text { get; set; }
        public string admin_email_denied_subject { get; set; }
        public string admin_email_denied_text { get; set; }
        public string admin_email_renew_subject { get; set; }
        public string admin_email_renew_text { get; set; }
        public string admin_email_upgrade_subject { get; set; }
        public string admin_email_upgrade_text { get; set; }
        public string admin_email_addextra_subject { get; set; }
        public string admin_email_addextra_text { get; set; }
        public string admin_email_expire_subject { get; set; }
        public string admin_email_expire_text { get; set; }
        public string custom_code_transaction { get; set; }
        public string custom_code { get; set; }
        public bool gid_enable { get; set; }
        public string gid_subscribe { get; set; }
        public string gid_expire { get; set; }
        public bool disable_expired_account { get; set; }
        public bool fixed_expiry { get; set; }
        public int fixed_day { get; set; }
        public sbyte fixed_month { get; set; }
        public short fixed_year { get; set; }
        public bool published { get; set; }
        public int ordering { get; set; }
    }
}
