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

        public static class Aspnet
        {
            public static string UseTaskFriendlySynchronizationContext
            {
                get { return ConfigurationManager.AppSettings["aspnet:UseTaskFriendlySynchronizationContext"]; }
            }
        }

        public static string ClientValidationEnabled
        {
            get { return ConfigurationManager.AppSettings["ClientValidationEnabled"]; }
        }

        public static string DefaultTheme
        {
            get { return ConfigurationManager.AppSettings["DefaultTheme"]; }
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

        public static string ThemesEnabled
        {
            get { return ConfigurationManager.AppSettings["ThemesEnabled"]; }
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

