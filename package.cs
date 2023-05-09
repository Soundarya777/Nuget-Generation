namespace CSharpNugetGenerator
{
    internal class package
    {
        public Metadatas metadata { get; set; }
    }

    class Metadatas
    {
        public static string id { get; set; }
        public static string version { get; set; }
        public static string authors { get; set; }
        public static string owners { get; set; }
        public static string requireLicenseAcceptance { get; set; }
        public static string description { get; set; }
        public static string releaseNotes { get; set; }
        public static string copyright { get; set; }
        public static string[] MyAutoFillDatasProperty { get; set; }
    }
    public class Dependencies
    {
        public string id { get; set; }
    }

    internal class Files
    {

    }
}
