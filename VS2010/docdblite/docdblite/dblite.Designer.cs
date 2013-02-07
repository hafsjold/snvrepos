﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("docdbliteModel", "FK_tblData_0_0", "tbldoc", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(docdblite.tbldoc), "tblData", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(docdblite.tblData), true)]

#endregion

namespace docdblite
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class docdbliteEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new docdbliteEntities object using the connection string found in the 'docdbliteEntities' section of the application configuration file.
        /// </summary>
        public docdbliteEntities() : base("name=docdbliteEntities", "docdbliteEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new docdbliteEntities object.
        /// </summary>
        public docdbliteEntities(string connectionString) : base(connectionString, "docdbliteEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new docdbliteEntities object.
        /// </summary>
        public docdbliteEntities(EntityConnection connection) : base(connection, "docdbliteEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
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
        public ObjectSet<tblData> tblData
        {
            get
            {
                if ((_tblData == null))
                {
                    _tblData = base.CreateObjectSet<tblData>("tblData");
                }
                return _tblData;
            }
        }
        private ObjectSet<tblData> _tblData;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<tbldoc> tbldoc
        {
            get
            {
                if ((_tbldoc == null))
                {
                    _tbldoc = base.CreateObjectSet<tbldoc>("tbldoc");
                }
                return _tbldoc;
            }
        }
        private ObjectSet<tbldoc> _tbldoc;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<tblrefnr> tblrefnr
        {
            get
            {
                if ((_tblrefnr == null))
                {
                    _tblrefnr = base.CreateObjectSet<tblrefnr>("tblrefnr");
                }
                return _tblrefnr;
            }
        }
        private ObjectSet<tblrefnr> _tblrefnr;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tblData EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotblData(tblData tblData)
        {
            base.AddObject("tblData", tblData);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tbldoc EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotbldoc(tbldoc tbldoc)
        {
            base.AddObject("tbldoc", tbldoc);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tblrefnr EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotblrefnr(tblrefnr tblrefnr)
        {
            base.AddObject("tblrefnr", tblrefnr);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="docdbliteModel", Name="tblData")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class tblData : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new tblData object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        public static tblData CreatetblData(global::System.Guid id)
        {
            tblData tblData = new tblData();
            tblData.id = id;
            return tblData;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid id
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
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Guid _id;
        partial void OnidChanging(global::System.Guid value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.Byte[] data
        {
            get
            {
                return StructuralObject.GetValidValue(_data);
            }
            set
            {
                OndataChanging(value);
                ReportPropertyChanging("data");
                _data = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("data");
                OndataChanged();
            }
        }
        private global::System.Byte[] _data;
        partial void OndataChanging(global::System.Byte[] value);
        partial void OndataChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("docdbliteModel", "FK_tblData_0_0", "tbldoc")]
        public tbldoc tbldoc
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tbldoc>("docdbliteModel.FK_tblData_0_0", "tbldoc").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tbldoc>("docdbliteModel.FK_tblData_0_0", "tbldoc").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<tbldoc> tbldocReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tbldoc>("docdbliteModel.FK_tblData_0_0", "tbldoc");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<tbldoc>("docdbliteModel.FK_tblData_0_0", "tbldoc", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="docdbliteModel", Name="tbldoc")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class tbldoc : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new tbldoc object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        public static tbldoc Createtbldoc(global::System.Guid id)
        {
            tbldoc tbldoc = new tbldoc();
            tbldoc.id = id;
            return tbldoc;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid id
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
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Guid _id;
        partial void OnidChanging(global::System.Guid value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> år
        {
            get
            {
                return _år;
            }
            set
            {
                OnårChanging(value);
                ReportPropertyChanging("år");
                _år = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("år");
                OnårChanged();
            }
        }
        private Nullable<global::System.Int32> _år;
        partial void OnårChanging(Nullable<global::System.Int32> value);
        partial void OnårChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ref_nr
        {
            get
            {
                return _ref_nr;
            }
            set
            {
                Onref_nrChanging(value);
                ReportPropertyChanging("ref_nr");
                _ref_nr = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ref_nr");
                Onref_nrChanged();
            }
        }
        private Nullable<global::System.Int32> _ref_nr;
        partial void Onref_nrChanging(Nullable<global::System.Int32> value);
        partial void Onref_nrChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String virksomhed
        {
            get
            {
                return _virksomhed;
            }
            set
            {
                OnvirksomhedChanging(value);
                ReportPropertyChanging("virksomhed");
                _virksomhed = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("virksomhed");
                OnvirksomhedChanged();
            }
        }
        private global::System.String _virksomhed;
        partial void OnvirksomhedChanging(global::System.String value);
        partial void OnvirksomhedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String emne
        {
            get
            {
                return _emne;
            }
            set
            {
                OnemneChanging(value);
                ReportPropertyChanging("emne");
                _emne = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("emne");
                OnemneChanged();
            }
        }
        private global::System.String _emne;
        partial void OnemneChanging(global::System.String value);
        partial void OnemneChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String dokument_type
        {
            get
            {
                return _dokument_type;
            }
            set
            {
                Ondokument_typeChanging(value);
                ReportPropertyChanging("dokument_type");
                _dokument_type = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("dokument_type");
                Ondokument_typeChanged();
            }
        }
        private global::System.String _dokument_type;
        partial void Ondokument_typeChanging(global::System.String value);
        partial void Ondokument_typeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ekstern_kilde
        {
            get
            {
                return _ekstern_kilde;
            }
            set
            {
                Onekstern_kildeChanging(value);
                ReportPropertyChanging("ekstern_kilde");
                _ekstern_kilde = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ekstern_kilde");
                Onekstern_kildeChanged();
            }
        }
        private global::System.String _ekstern_kilde;
        partial void Onekstern_kildeChanging(global::System.String value);
        partial void Onekstern_kildeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String beskrivelse
        {
            get
            {
                return _beskrivelse;
            }
            set
            {
                OnbeskrivelseChanging(value);
                ReportPropertyChanging("beskrivelse");
                _beskrivelse = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("beskrivelse");
                OnbeskrivelseChanged();
            }
        }
        private global::System.String _beskrivelse;
        partial void OnbeskrivelseChanging(global::System.String value);
        partial void OnbeskrivelseChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String oprettes_af
        {
            get
            {
                return _oprettes_af;
            }
            set
            {
                Onoprettes_afChanging(value);
                ReportPropertyChanging("oprettes_af");
                _oprettes_af = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("oprettes_af");
                Onoprettes_afChanged();
            }
        }
        private global::System.String _oprettes_af;
        partial void Onoprettes_afChanging(global::System.String value);
        partial void Onoprettes_afChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> oprettet_dato
        {
            get
            {
                return _oprettet_dato;
            }
            set
            {
                Onoprettet_datoChanging(value);
                ReportPropertyChanging("oprettet_dato");
                _oprettet_dato = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("oprettet_dato");
                Onoprettet_datoChanged();
            }
        }
        private Nullable<global::System.DateTime> _oprettet_dato;
        partial void Onoprettet_datoChanging(Nullable<global::System.DateTime> value);
        partial void Onoprettet_datoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String kilde_sti
        {
            get
            {
                return _kilde_sti;
            }
            set
            {
                Onkilde_stiChanging(value);
                ReportPropertyChanging("kilde_sti");
                _kilde_sti = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("kilde_sti");
                Onkilde_stiChanged();
            }
        }
        private global::System.String _kilde_sti;
        partial void Onkilde_stiChanging(global::System.String value);
        partial void Onkilde_stiChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("docdbliteModel", "FK_tblData_0_0", "tblData")]
        public tblData tblData
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tblData>("docdbliteModel.FK_tblData_0_0", "tblData").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tblData>("docdbliteModel.FK_tblData_0_0", "tblData").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<tblData> tblDataReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<tblData>("docdbliteModel.FK_tblData_0_0", "tblData");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<tblData>("docdbliteModel.FK_tblData_0_0", "tblData", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="docdbliteModel", Name="tblrefnr")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class tblrefnr : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new tblrefnr object.
        /// </summary>
        /// <param name="keyname">Initial value of the keyname property.</param>
        /// <param name="nr">Initial value of the nr property.</param>
        public static tblrefnr Createtblrefnr(global::System.String keyname, global::System.Int32 nr)
        {
            tblrefnr tblrefnr = new tblrefnr();
            tblrefnr.keyname = keyname;
            tblrefnr.nr = nr;
            return tblrefnr;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String keyname
        {
            get
            {
                return _keyname;
            }
            set
            {
                if (_keyname != value)
                {
                    OnkeynameChanging(value);
                    ReportPropertyChanging("keyname");
                    _keyname = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("keyname");
                    OnkeynameChanged();
                }
            }
        }
        private global::System.String _keyname;
        partial void OnkeynameChanging(global::System.String value);
        partial void OnkeynameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 nr
        {
            get
            {
                return _nr;
            }
            set
            {
                OnnrChanging(value);
                ReportPropertyChanging("nr");
                _nr = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("nr");
                OnnrChanged();
            }
        }
        private global::System.Int32 _nr;
        partial void OnnrChanging(global::System.Int32 value);
        partial void OnnrChanged();

        #endregion
    
    }

    #endregion
    
}
