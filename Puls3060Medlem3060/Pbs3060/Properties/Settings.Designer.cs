﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nsPbs3060.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=qynhbd9h4f.database.windows.net;Initial Catalog=dbPuls3060Medlem;Pers" +
            "ist Security Info=True;User ID=sqlUser;Password=Puls3060;MultipleActiveResultSet" +
            "s=True;Encrypt=True;TrustServerCertificate=True;Application Name=EntityFramework" +
            "")]
        public string puls3061_dk_dbConnectionString3 {
            get {
                return ((string)(this["puls3061_dk_dbConnectionString3"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=vhd50;Initial Catalog=dbPuls3060Medlem;User ID=sa;Password=Puls3060")]
        public string dbPuls3060MedlemConnectionString {
            get {
                return ((string)(this["dbPuls3060MedlemConnectionString"]));
            }
        }
    }
}
