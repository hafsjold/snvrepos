namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_users
    {
        //[Key]
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public sbyte block { get; set; }
        public Nullable<sbyte> sendEmail { get; set; }
        public System.DateTime registerDate { get; set; }
        public System.DateTime lastvisitDate { get; set; }
        public string activation { get; set; }
        public string @params { get; set; }
        public System.DateTime lastResetTime { get; set; }
        public int resetCount { get; set; }
        public string otpKey { get; set; }
        public string otep { get; set; }
        public sbyte requireReset { get; set; }
    }
}
