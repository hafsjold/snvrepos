﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace nsPuls3060.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("31")]
        public int BetalingsFristiDageGamleMedlemmer {
            get {
                return ((int)(this["BetalingsFristiDageGamleMedlemmer"]));
            }
            set {
                this["BetalingsFristiDageGamleMedlemmer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("61")]
        public int BetalingsFristiDageNyeMedlemmer {
            get {
                return ((int)(this["BetalingsFristiDageNyeMedlemmer"]));
            }
            set {
                this["BetalingsFristiDageNyeMedlemmer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Documents and Settings\\mha\\Dokumenter\\Medlem3060\\Databaser\\SQLCompact\\dbData30" +
            "60.sdf")]
        public string DataBasePath {
            get {
                return ((string)(this["DataBasePath"]));
            }
            set {
                this["DataBasePath"] = value;
            }
        }
    }
}