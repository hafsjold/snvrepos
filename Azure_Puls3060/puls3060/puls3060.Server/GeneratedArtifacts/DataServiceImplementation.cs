﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using global::System.Linq;

namespace LightSwitchApplication.Implementation
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class ApplicationDataDataService
        : global::Microsoft.LightSwitch.ServerGenerated.Implementation.DataService<global::LightSwitchApplication.Implementation.ApplicationData>
    {
    
        public ApplicationDataDataService() : base()
        {
        }
    
        protected override global::Microsoft.LightSwitch.IDataService GetDataService(global::Microsoft.LightSwitch.IDataWorkspace dataWorkspace)
        {
            return ((global::LightSwitchApplication.DataWorkspace)dataWorkspace).ApplicationData;
        }
    }
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class ApplicationDataServiceImplementation
        : global::Microsoft.LightSwitch.ServerGenerated.Implementation.DataServiceImplementation<global::LightSwitchApplication.Implementation.ApplicationData>
    {
        public ApplicationDataServiceImplementation(global::Microsoft.LightSwitch.IDataService dataService) : base(dataService)
        {
        }
    
    #region Queries
    #endregion

    #region Protected Methods
        protected override object CreateObject(global::System.Type type)
        {
            if (type == typeof(global::LightSwitchApplication.Implementation.tblFikBetaling))
            {
                return new global::LightSwitchApplication.Implementation.tblFikBetaling();
            }
            if (type == typeof(global::LightSwitchApplication.Implementation.tblProjekt))
            {
                return new global::LightSwitchApplication.Implementation.tblProjekt();
            }
    
            return base.CreateObject(type);
        }
    
        protected override global::LightSwitchApplication.Implementation.ApplicationData CreateObjectContext()
        {
            string assemblyName = global::System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            return new global::LightSwitchApplication.Implementation.ApplicationData(this.GetEntityConnectionString(
                "_IntrinsicData",
                "res://" + assemblyName + "/ApplicationData.csdl|res://" + assemblyName + "/ApplicationData.ssdl|res://" + assemblyName + "/ApplicationData.msl",
                "System.Data.SqlClient",
                true));
        }
    
        protected override global::Microsoft.LightSwitch.Internal.IEntityImplementation CreateEntityImplementation<T>()
        {
            if (typeof(T) == typeof(global::LightSwitchApplication.tblFikBetaling))
            {
                return new global::LightSwitchApplication.Implementation.tblFikBetaling();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.tblProjekt))
            {
                return new global::LightSwitchApplication.Implementation.tblProjekt();
            }
            return null;
        }
    
    #endregion
    
    }
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class dbPuls3060MedlemDataDataService
        : global::Microsoft.LightSwitch.ServerGenerated.Implementation.DataService<global::LightSwitchApplication.Implementation.dbPuls3060MedlemData>
    {
    
        public dbPuls3060MedlemDataDataService() : base()
        {
        }
    
        protected override global::Microsoft.LightSwitch.IDataService GetDataService(global::Microsoft.LightSwitch.IDataWorkspace dataWorkspace)
        {
            return ((global::LightSwitchApplication.DataWorkspace)dataWorkspace).dbPuls3060MedlemData;
        }
    }
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class dbPuls3060MedlemDataServiceImplementation
        : global::Microsoft.LightSwitch.ServerGenerated.Implementation.DataServiceImplementation<global::LightSwitchApplication.Implementation.dbPuls3060MedlemData>
    {
        public dbPuls3060MedlemDataServiceImplementation(global::Microsoft.LightSwitch.IDataService dataService) : base(dataService)
        {
        }
    
    #region Queries
    #endregion

    #region Protected Methods
        protected override object CreateObject(global::System.Type type)
        {
            if (type == typeof(global::LightSwitchApplication.Implementation.tblMedlem))
            {
                return new global::LightSwitchApplication.Implementation.tblMedlem();
            }
            if (type == typeof(global::LightSwitchApplication.Implementation.vMedmemLogText))
            {
                return new global::LightSwitchApplication.Implementation.vMedmemLogText();
            }
    
            return base.CreateObject(type);
        }
    
        protected override global::LightSwitchApplication.Implementation.dbPuls3060MedlemData CreateObjectContext()
        {
            string assemblyName = global::System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            return new global::LightSwitchApplication.Implementation.dbPuls3060MedlemData(this.GetEntityConnectionString(
                "dbPuls3060MedlemData",
                "res://" + assemblyName + "/dbPuls3060MedlemData.csdl|res://" + assemblyName + "/dbPuls3060MedlemData.ssdl|res://" + assemblyName + "/dbPuls3060MedlemData.msl",
                "System.Data.SqlClient",
                true));
        }
    
        protected override global::Microsoft.LightSwitch.Internal.IEntityImplementation CreateEntityImplementation<T>()
        {
            if (typeof(T) == typeof(global::LightSwitchApplication.tblMedlem))
            {
                return new global::LightSwitchApplication.Implementation.tblMedlem();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.vMedmemLogText))
            {
                return new global::LightSwitchApplication.Implementation.vMedmemLogText();
            }
            return null;
        }
    
    #endregion
    
    }
    
    #region DataServiceImplementationFactory
    [global::System.ComponentModel.Composition.PartCreationPolicy(global::System.ComponentModel.Composition.CreationPolicy.Shared)]
    [global::System.ComponentModel.Composition.Export(typeof(global::Microsoft.LightSwitch.Internal.IDataServiceFactory))]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class __DataServiceFactory :
        global::Microsoft.LightSwitch.ServerGenerated.Implementation.DataServiceFactory
    {
    
        protected override global::Microsoft.LightSwitch.IDataService CreateDataService(global::System.Type dataServiceType)
        {
            if (dataServiceType == typeof(global::LightSwitchApplication.ApplicationDataService))
            {
                return new global::LightSwitchApplication.ApplicationDataService();
            }
            if (dataServiceType == typeof(global::LightSwitchApplication.dbPuls3060MedlemDataService))
            {
                return new global::LightSwitchApplication.dbPuls3060MedlemDataService();
            }
            return base.CreateDataService(dataServiceType);
        }
    
        protected override global::Microsoft.LightSwitch.Internal.IDataServiceImplementation CreateDataServiceImplementation<TDataService>(TDataService dataService)
        {
            if (typeof(TDataService) == typeof(global::LightSwitchApplication.ApplicationDataService))
            {
                return new global::LightSwitchApplication.Implementation.ApplicationDataServiceImplementation(dataService);
            }
            if (typeof(TDataService) == typeof(global::LightSwitchApplication.dbPuls3060MedlemDataService))
            {
                return new global::LightSwitchApplication.Implementation.dbPuls3060MedlemDataServiceImplementation(dataService);
            }
            return base.CreateDataServiceImplementation(dataService);
        }
    }
    #endregion
    
    [global::System.ComponentModel.Composition.PartCreationPolicy(global::System.ComponentModel.Composition.CreationPolicy.Shared)]
    [global::System.ComponentModel.Composition.Export(typeof(global::Microsoft.LightSwitch.Internal.ITypeMappingProvider))]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class __TypeMapping
        : global::Microsoft.LightSwitch.Internal.ITypeMappingProvider
    {
        global::System.Type global::Microsoft.LightSwitch.Internal.ITypeMappingProvider.GetImplementationType(global::System.Type definitionType)
        {
            if (typeof(global::LightSwitchApplication.tblFikBetaling) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.tblFikBetaling);
            }
            if (typeof(global::LightSwitchApplication.tblProjekt) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.tblProjekt);
            }
            if (typeof(global::LightSwitchApplication.tblMedlem) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.tblMedlem);
            }
            if (typeof(global::LightSwitchApplication.vMedmemLogText) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.vMedmemLogText);
            }
            return null;
        }
    }
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class tblFikBetaling :
        global::LightSwitchApplication.tblFikBetaling.DetailsClass.IImplementation,
        global::Microsoft.LightSwitch.Internal.ICreatedModifiedPropertiesImplementation
    
    {
    
        global::Microsoft.LightSwitch.Internal.IEntityImplementation global::LightSwitchApplication.tblFikBetaling.DetailsClass.IImplementation.tblProjekt
        {
            get
            {
                return this.tblProjekt;
            }
            set
            {
                this.tblProjekt = (global::LightSwitchApplication.Implementation.tblProjekt)value;
                if (this.__host != null)
                {
                    this.__host.RaisePropertyChanged("tblProjekt");
                }
            }
        }
        
        partial void OntblFikBetaling_tblProjektChanged()
        {
            if (this.__host != null)
            {
                this.__host.RaisePropertyChanged("tblProjekt");
            }
        }
        
        #region IEntityImplementation Members
        private global::Microsoft.LightSwitch.Internal.IEntityImplementationHost __host;
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementationHost global::Microsoft.LightSwitch.Internal.IEntityImplementation.Host
        {
            get
            {
                return this.__host;
            }
        }
        
        void global::Microsoft.LightSwitch.Internal.IEntityImplementation.Initialize(global::Microsoft.LightSwitch.Internal.IEntityImplementationHost host)
        {
            this.__host = host;
        }
        
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (this.__host != null)
            {
                this.__host.RaisePropertyChanged(propertyName);
            }
        }
        #endregion
    }
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class tblProjekt :
        global::LightSwitchApplication.tblProjekt.DetailsClass.IImplementation,
        global::Microsoft.LightSwitch.Internal.ICreatedModifiedPropertiesImplementation
    
    {
    
        global::System.Collections.IEnumerable global::LightSwitchApplication.tblProjekt.DetailsClass.IImplementation.tblFikBetalings
        {
            get
            {
                return this.tblFikBetalings;
            }
        }
        
        #region IEntityImplementation Members
        private global::Microsoft.LightSwitch.Internal.IEntityImplementationHost __host;
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementationHost global::Microsoft.LightSwitch.Internal.IEntityImplementation.Host
        {
            get
            {
                return this.__host;
            }
        }
        
        void global::Microsoft.LightSwitch.Internal.IEntityImplementation.Initialize(global::Microsoft.LightSwitch.Internal.IEntityImplementationHost host)
        {
            this.__host = host;
        }
        
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (this.__host != null)
            {
                this.__host.RaisePropertyChanged(propertyName);
            }
        }
        #endregion
    }
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class tblMedlem :
        global::LightSwitchApplication.tblMedlem.DetailsClass.IImplementation
    {
    
        global::System.Collections.IEnumerable global::LightSwitchApplication.tblMedlem.DetailsClass.IImplementation.vMedmemLogTexts
        {
            get
            {
                return this.vMedmemLogTexts;
            }
        }
        
        #region IEntityImplementation Members
        private global::Microsoft.LightSwitch.Internal.IEntityImplementationHost __host;
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementationHost global::Microsoft.LightSwitch.Internal.IEntityImplementation.Host
        {
            get
            {
                return this.__host;
            }
        }
        
        void global::Microsoft.LightSwitch.Internal.IEntityImplementation.Initialize(global::Microsoft.LightSwitch.Internal.IEntityImplementationHost host)
        {
            this.__host = host;
        }
        
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (this.__host != null)
            {
                this.__host.RaisePropertyChanged(propertyName);
            }
        }
        #endregion
    }
    
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "12.1.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class vMedmemLogText :
        global::LightSwitchApplication.vMedmemLogText.DetailsClass.IImplementation
    {
    
        global::Microsoft.LightSwitch.Internal.IEntityImplementation global::LightSwitchApplication.vMedmemLogText.DetailsClass.IImplementation.tblMedlem
        {
            get
            {
                return this.tblMedlem;
            }
            set
            {
                this.tblMedlem = (global::LightSwitchApplication.Implementation.tblMedlem)value;
                if (this.__host != null)
                {
                    this.__host.RaisePropertyChanged("tblMedlem");
                }
            }
        }
        
        partial void OnNrChanged()
        {
            if (this.__host != null)
            {
                this.__host.RaisePropertyChanged("tblMedlem");
            }
        }
        
        #region IEntityImplementation Members
        private global::Microsoft.LightSwitch.Internal.IEntityImplementationHost __host;
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementationHost global::Microsoft.LightSwitch.Internal.IEntityImplementation.Host
        {
            get
            {
                return this.__host;
            }
        }
        
        void global::Microsoft.LightSwitch.Internal.IEntityImplementation.Initialize(global::Microsoft.LightSwitch.Internal.IEntityImplementationHost host)
        {
            this.__host = host;
        }
        
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (this.__host != null)
            {
                this.__host.RaisePropertyChanged(propertyName);
            }
        }
        #endregion
    }
    
}

