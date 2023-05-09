using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Utilities
{
    public class MetaData
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Version { get; set; }
        public string Authors { get; set; }
        public string Owners { get; set; }
        public string RequireLicenseAcceptance { get; set; } = "false";
        public string Description { get; set; }
        public string ReleaseNotes { get; set; }
        public string Copyright { get; set; } = "Copyright © 2023. All rights reserved";
        public string TargetFramework { get; set; }
        public string OutputPath { get; set; }
        public string WorkingDirectory { get; set; }
        public string ConfigPath { get; set; }
        public DataTable RepoPathTable { get; set; }

        public Dictionary<string, ProjectConfig> ProjectConfigs { get; set; }

        public MetaData()
        {
            ProjectConfigs = new Dictionary<string, ProjectConfig>();
            RepoPathTable = new DataTable();

        }
    }



    public class ProjectConfig
    {

        public DataTable RuntimeDataTable { get; set; }
        public DataTable ReferenceDataTable { get; set; }
        public DataTable ObfucationRuntimeDataTable { get; set; }

        public TextBox Obproj_File { get; set; }
        public TextBox Obproj_DLL_Directory { get; set; }
        public TextBox ObProj_OutPut_Directory { get; set; }

        public ProjectConfig()
        {
            RuntimeDataTable = new DataTable();
            ReferenceDataTable = new DataTable();
            ObfucationRuntimeDataTable = new DataTable();
            Obproj_File = new TextBox();
            Obproj_DLL_Directory = new TextBox();
            ObProj_OutPut_Directory = new TextBox();
        }
    }

}
