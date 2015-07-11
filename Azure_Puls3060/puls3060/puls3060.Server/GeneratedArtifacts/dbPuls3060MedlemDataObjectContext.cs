﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("LightSwitchApplication", "tblMedlem_vMedmemLogText", "tblMedlem", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(LightSwitchApplication.Implementation.tblMedlem), "vMedmemLogText", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(LightSwitchApplication.Implementation.vMedmemLogText), true)]

#endregion

namespace LightSwitchApplication.Implementation
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class dbPuls3060MedlemData : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new dbPuls3060MedlemData object using the connection string found in the 'dbPuls3060MedlemData' section of the application configuration file.
        /// </summary>
        public dbPuls3060MedlemData() : base("name=dbPuls3060MedlemData", "dbPuls3060MedlemData")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbPuls3060MedlemData object.
        /// </summary>
        public dbPuls3060MedlemData(string connectionString) : base(connectionString, "dbPuls3060MedlemData")
        {
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new dbPuls3060MedlemData object.
        /// </summary>
        public dbPuls3060MedlemData(EntityConnection connection) : base(connection, "dbPuls3060MedlemData")
        {
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<tblMedlem> tblMedlems
        {
            get
            {
                if ((_tblMedlems == null))
                {
                    _tblMedlems = base.CreateObjectSet<tblMedlem>("tblMedlems");
                }
                return _tblMedlems;
            }
        }
        private ObjectSet<tblMedlem> _tblMedlems;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<vMedmemLogText> vMedmemLogTexts
        {
            get
            {
                if ((_vMedmemLogTexts == null))
                {
                    _vMedmemLogTexts = base.CreateObjectSet<vMedmemLogText>("vMedmemLogTexts");
                }
                return _vMedmemLogTexts;
            }
        }
        private ObjectSet<vMedmemLogText> _vMedmemLogTexts;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tblMedlems EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotblMedlems(tblMedlem tblMedlem)
        {
            base.AddObject("tblMedlems", tblMedlem);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the vMedmemLogTexts EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTovMedmemLogTexts(vMedmemLogText vMedmemLogText)
        {
            base.AddObject("vMedmemLogTexts", vMedmemLogText);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="LightSwitchApplication", Name="tblMedlem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class tblMedlem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new tblMedlem object.
        /// </summary>
        /// <param name="nr">Initial value of the Nr property.</param>
        public static tblMedlem CreatetblMedlem(global::System.Int32 nr)
        {
            tblMedlem tblMedlem = new tblMedlem();
            tblMedlem.Nr = nr;
            return tblMedlem;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Navn
        {
            get
            {
                return _Navn;
            }
            set
            {
                OnNavnChanging(value);
                ReportPropertyChanging("Navn");
                _Navn = value;
                ReportPropertyChanged("Navn");
                OnNavnChanged();
            }
        }
        private global::System.String _Navn;
        partial void OnNavnChanging(global::System.String value);
        partial void OnNavnChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Kaldenavn
        {
            get
            {
                return _Kaldenavn;
            }
            set
            {
                OnKaldenavnChanging(value);
                ReportPropertyChanging("Kaldenavn");
                _Kaldenavn = value;
                ReportPropertyChanged("Kaldenavn");
                OnKaldenavnChanged();
            }
        }
        private global::System.String _Kaldenavn;
        partial void OnKaldenavnChanging(global::System.String value);
        partial void OnKaldenavnChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Adresse
        {
            get
            {
                return _Adresse;
            }
            set
            {
                OnAdresseChanging(value);
                ReportPropertyChanging("Adresse");
                _Adresse = value;
                ReportPropertyChanged("Adresse");
                OnAdresseChanged();
            }
        }
        private global::System.String _Adresse;
        partial void OnAdresseChanging(global::System.String value);
        partial void OnAdresseChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Postnr
        {
            get
            {
                return _Postnr;
            }
            set
            {
                OnPostnrChanging(value);
                ReportPropertyChanging("Postnr");
                _Postnr = value;
                ReportPropertyChanged("Postnr");
                OnPostnrChanged();
            }
        }
        private global::System.String _Postnr;
        partial void OnPostnrChanging(global::System.String value);
        partial void OnPostnrChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Bynavn
        {
            get
            {
                return _Bynavn;
            }
            set
            {
                OnBynavnChanging(value);
                ReportPropertyChanging("Bynavn");
                _Bynavn = value;
                ReportPropertyChanged("Bynavn");
                OnBynavnChanged();
            }
        }
        private global::System.String _Bynavn;
        partial void OnBynavnChanging(global::System.String value);
        partial void OnBynavnChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Telefon
        {
            get
            {
                return _Telefon;
            }
            set
            {
                OnTelefonChanging(value);
                ReportPropertyChanging("Telefon");
                _Telefon = value;
                ReportPropertyChanged("Telefon");
                OnTelefonChanged();
            }
        }
        private global::System.String _Telefon;
        partial void OnTelefonChanging(global::System.String value);
        partial void OnTelefonChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                OnEmailChanging(value);
                ReportPropertyChanging("Email");
                _Email = value;
                ReportPropertyChanged("Email");
                OnEmailChanged();
            }
        }
        private global::System.String _Email;
        partial void OnEmailChanging(global::System.String value);
        partial void OnEmailChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> Status
        {
            get
            {
                return _Status;
            }
            set
            {
                OnStatusChanging(value);
                ReportPropertyChanging("Status");
                _Status = value;
                ReportPropertyChanged("Status");
                OnStatusChanged();
            }
        }
        private Nullable<global::System.Int32> _Status;
        partial void OnStatusChanging(Nullable<global::System.Int32> value);
        partial void OnStatusChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Nr
        {
            get
            {
                return _Nr;
            }
            set
            {
                if (_Nr != value)
                {
                    OnNrChanging(value);
                    ReportPropertyChanging("Nr");
                    _Nr = value;
                    ReportPropertyChanged("Nr");
                    OnNrChanged();
                }
            }
        }
        private global::System.Int32 _Nr;
        partial void OnNrChanging(global::System.Int32 value);
        partial void OnNrChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Kon
        {
            get
            {
                return _Kon;
            }
            set
            {
                OnKonChanging(value);
                ReportPropertyChanging("Kon");
                _Kon = value;
                ReportPropertyChanged("Kon");
                OnKonChanged();
            }
        }
        private global::System.String _Kon;
        partial void OnKonChanging(global::System.String value);
        partial void OnKonChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> FodtDato
        {
            get
            {
                return _FodtDato;
            }
            set
            {
                OnFodtDatoChanging(value);
                ReportPropertyChanging("FodtDato");
                _FodtDato = value;
                ReportPropertyChanged("FodtDato");
                OnFodtDatoChanged();
            }
        }
        private Nullable<global::System.DateTime> _FodtDato;
        partial void OnFodtDatoChanging(Nullable<global::System.DateTime> value);
        partial void OnFodtDatoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Bank
        {
            get
            {
                return _Bank;
            }
            set
            {
                OnBankChanging(value);
                ReportPropertyChanging("Bank");
                _Bank = value;
                ReportPropertyChanged("Bank");
                OnBankChanged();
            }
        }
        private global::System.String _Bank;
        partial void OnBankChanging(global::System.String value);
        partial void OnBankChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("LightSwitchApplication", "tblMedlem_vMedmemLogText", "vMedmemLogText")]
        public EntityCollection<vMedmemLogText> vMedmemLogTexts
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<vMedmemLogText>("LightSwitchApplication.tblMedlem_vMedmemLogText", "vMedmemLogText");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<vMedmemLogText>("LightSwitchApplication.tblMedlem_vMedmemLogText", "vMedmemLogText", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="LightSwitchApplication", Name="vMedmemLogText")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class vMedmemLogText : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new vMedmemLogText object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        public static vMedmemLogText CreatevMedmemLogText(global::System.Int32 id)
        {
            vMedmemLogText vMedmemLogText = new vMedmemLogText();
            vMedmemLogText.id = id;
            return vMedmemLogText;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = value;
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> Nr
        {
            get
            {
                return _Nr;
            }
            set
            {
                OnNrChanging(value);
                ReportPropertyChanging("Nr");
                _Nr = value;
                ReportPropertyChanged("Nr");
                OnNrChanged();
            }
        }
        private Nullable<global::System.Int32> _Nr;
        partial void OnNrChanging(Nullable<global::System.Int32> value);
        partial void OnNrChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> logdato
        {
            get
            {
                return _logdato;
            }
            set
            {
                OnlogdatoChanging(value);
                ReportPropertyChanging("logdato");
                _logdato = value;
                ReportPropertyChanged("logdato");
                OnlogdatoChanged();
            }
        }
        private Nullable<global::System.DateTime> _logdato;
        partial void OnlogdatoChanging(Nullable<global::System.DateTime> value);
        partial void OnlogdatoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Aktivitet
        {
            get
            {
                return _Aktivitet;
            }
            set
            {
                OnAktivitetChanging(value);
                ReportPropertyChanging("Aktivitet");
                _Aktivitet = value;
                ReportPropertyChanged("Aktivitet");
                OnAktivitetChanged();
            }
        }
        private global::System.String _Aktivitet;
        partial void OnAktivitetChanging(global::System.String value);
        partial void OnAktivitetChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> Dato
        {
            get
            {
                return _Dato;
            }
            set
            {
                OnDatoChanging(value);
                ReportPropertyChanging("Dato");
                _Dato = value;
                ReportPropertyChanged("Dato");
                OnDatoChanged();
            }
        }
        private Nullable<global::System.DateTime> _Dato;
        partial void OnDatoChanging(Nullable<global::System.DateTime> value);
        partial void OnDatoChanged();

        #endregion

    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("LightSwitchApplication", "tblMedlem_vMedmemLogText", "tblMedlem")]
        public tblMedlem tblMedlem
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tblMedlem>("LightSwitchApplication.tblMedlem_vMedmemLogText", "tblMedlem").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tblMedlem>("LightSwitchApplication.tblMedlem_vMedmemLogText", "tblMedlem").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<tblMedlem> tblMedlemReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tblMedlem>("LightSwitchApplication.tblMedlem_vMedmemLogText", "tblMedlem");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<tblMedlem>("LightSwitchApplication.tblMedlem_vMedmemLogText", "tblMedlem", value);
                }
            }
        }

        #endregion

    }

    #endregion

    
}
