﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZaakDocumentManager.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://webbeweging.nl/wp-content/plugins/zaaksysteem-stub/service/zds/VrijeBerich" +
            "ten.php")]
        public string StandaardZaakDocumentServicesVrijBerichtService {
            get {
                return ((string)(this["StandaardZaakDocumentServicesVrijBerichtService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://webbeweging.nl/wp-content/plugins/zaaksysteem-stub/service/zds/OntvangAsyn" +
            "chroon.php")]
        public string StandaardZaakDocumentServicesOntvangAsynchroonService {
            get {
                return ((string)(this["StandaardZaakDocumentServicesOntvangAsynchroonService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://webbeweging.nl/wp-content/plugins/zaaksysteem-stub/service/zds/BeantwoordV" +
            "raag.php")]
        public string StandaardZaakDocumentServicesBeantwoordVraagService {
            get {
                return ((string)(this["StandaardZaakDocumentServicesBeantwoordVraagService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("9999")]
        public string GemeenteCode {
            get {
                return ((string)(this["GemeenteCode"]));
            }
        }
    }
}
