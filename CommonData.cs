using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{

    public static class Extensions
    {
        public static string TargetExtension = ".targets";
        public static string PropsExtension = ".props";
        public static string NuConfigExtension = ".nuconfig";
        public static string NuxConfigExtension = ".nuxconfig";
        public static string NupkgExtension = ".nupkg";
        public static string NuspecExtension = ".nuspec";
        public static string FolderSeperator = "\\";
        public static string StarSeperator = "*";
    }

    public static class Props
    {
        public static string MSBuildThisFileDirectory = "$(MSBuildThisFileDirectory)";
        public static string DefaultConfig = "Default";
        public static string NewConfig = "New";
        public static string SelectProjectConfig = "-- Select Project Config--";
        public static string SelectConfig = "-- Select Configuration--";
        public static string BasicInfo = "BasicInfo";
        public static string RepoPath = "RepoPath";
        public static string NugetConfigurations_Folder = "NugetConfigurations";

        public static string DateFormat_YMD = "yyyy/MM/dd";
    }

    public static class PopUP_Messages
    {
        public static string Create_New_ProjConfig =  "Create New Project Config";
        public static string Invalid_Version = "Invalid Version";
        public static string Add_New_ProjConfig = "Add New Project Config";
        public static string Nuget_Name_Constrain = "Nuget name cannot contains special characters other than underscore or hyphen";
        public static string Enter_Valid_Target_Directory = "Enter a valid target Directory";
        public static string Specifications_Cannot_Be_Empty = "Specifications Cannot be empty";
        public static string Config_Saved = "Config Saved";

    }


}
