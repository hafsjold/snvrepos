namespace Pbs3060
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ecpwt_rsform_submission_values
    {
        [Key]
        public int SubmissionValueId { get; set; }
        public int FormId { get; set; }
        public int SubmissionId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}
