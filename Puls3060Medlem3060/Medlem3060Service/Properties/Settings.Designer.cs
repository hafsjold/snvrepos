﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nsMedlem3060Service.Properties {
    
    
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
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=qynhbd9h4f.database.windows.net;Initial Catalog=dbPuls3060Medlem;Inte" +
            "grated Security=False;Persist Security Info=True;User ID=sqlUser;Encrypt=True")]
        public string puls3061_dk_dbConnectionString_Test {
            get {
                return ((string)(this["puls3061_dk_dbConnectionString_Test"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=qynhbd9h4f.database.windows.net;Initial Catalog=dbPuls3060Medlem;Inte" +
            "grated Security=False;Persist Security Info=True;User ID=sqlUser;Encrypt=True")]
        public string puls3061_dk_dbConnectionString_Prod {
            get {
                return ((string)(this["puls3061_dk_dbConnectionString_Prod"]));
            }
        }
    }
}
