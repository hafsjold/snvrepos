
namespace wMedlem3060.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies tblMedlemMetadata as the class
    // that carries additional metadata for the tblMedlem class.
    [MetadataTypeAttribute(typeof(tblMedlem.tblMedlemMetadata))]
    public partial class tblMedlem
    {

        // This class allows you to attach custom attributes to properties
        // of the tblMedlem class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class tblMedlemMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private tblMedlemMetadata()
            {
            }
            public int Nr { get; set; }

            [Required()]
            public string Navn { get; set; }

            public string Kaldenavn { get; set; }

            [Required()]
            public string Adresse { get; set; }

            [Required()]
            [RegularExpression("[0-9]*")]
            [StringLength(4)]
            public string Postnr { get; set; }

            [Required()]
            public string Bynavn { get; set; }

            [RegularExpression("[0-9]*")]
            [StringLength(8)]
            public string Telefon { get; set; }

            public string Email { get; set; }

            [Required()]
            [RegularExpression("M|K")]
            public string Kon { get; set; }

            public Nullable<DateTime> FodtDato { get; set; }

            public string Bank { get; set; }
        }
    }
}
