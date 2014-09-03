using System.Configuration;

namespace SogetiSkills.UI
{
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static class AppSettings
    {
        public static string ApplicationName
        {
            get { return ConfigurationManager.AppSettings["ApplicationName"]; }
        }

        public static string ApplicationReleaseProfile
        {
            get { return ConfigurationManager.AppSettings["ApplicationReleaseProfile"]; }
        }

        public static string ApplicationVersion
        {
            get { return ConfigurationManager.AppSettings["ApplicationVersion"]; }
        }

        public static string ClientValidationEnabled
        {
            get { return ConfigurationManager.AppSettings["ClientValidationEnabled"]; }
        }

        public static class MvcFlashMessages
        {
            public static string InnerCssClass
            {
                get { return ConfigurationManager.AppSettings["MvcFlashMessages/InnerCssClass"]; }
            }

            public static string OuterCssClass
            {
                get { return ConfigurationManager.AppSettings["MvcFlashMessages/OuterCssClass"]; }
            }
        }

        public static string UnobtrusiveJavaScriptEnabled
        {
            get { return ConfigurationManager.AppSettings["UnobtrusiveJavaScriptEnabled"]; }
        }

        public static class Webpages
        {
            public static string Enabled
            {
                get { return ConfigurationManager.AppSettings["webpages:Enabled"]; }
            }

            public static string Version
            {
                get { return ConfigurationManager.AppSettings["webpages:Version"]; }
            }
        }
    }
}

