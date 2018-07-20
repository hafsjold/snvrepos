namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsform_submissions
    {
        [Key]
        public int SubmissionId { get; set; }
        public int FormId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string UserIp { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string Lang { get; set; }
        public bool confirmed { get; set; }
    }
}
