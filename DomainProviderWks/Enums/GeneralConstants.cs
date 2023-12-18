namespace ProviderWks.Domain.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public static class GeneralConstants
    {
        private static string GetSufixEnvironment()
        {
            return (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production") ?
                $"-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Substring(0, 3).ToLower()}"
                : string.Empty;
        }
        /**Variables AzureInsight*/

        public static string GenesysSodexoAPP = $"SDX_GENESYS_APP{GetSufixEnvironment()}";
        public static string PseSodexoLog = $"SDX_GENESYS_NAMETABLELOG{GetSufixEnvironment()}";
        public static string PseSodexoKeyInsight = $"SDXGENESYSKEYAPPINSIGHTS{GetSufixEnvironment()}";

        public static string SdxAccount = $"SDX_ACCOUNT{GetSufixEnvironment()}";
        public static string SdxStorageKey = $"SDXSTORAGEKEY{GetSufixEnvironment()}";
    }
}
