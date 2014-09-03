using System.Configuration;

namespace SogetiSkills.API
{
    [System.Diagnostics.DebuggerNonUserCodeAttribute]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    public static class AppSettings
    {
        public static string ApplicationReleaseProfile
        {
            get { return ConfigurationManager.AppSettings["ApplicationReleaseProfile"]; }
        }

        public static string ApplicationVersion
        {
            get { return ConfigurationManager.AppSettings["ApplicationVersion"]; }
        }
    }
}

