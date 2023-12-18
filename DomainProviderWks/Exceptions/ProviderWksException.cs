namespace ProviderWks.Domain.Exceptions
{
    /// <summary>
    ///
    /// </summary>
    public class ProviderWksException : Exception
    {
        public ProviderWksException() : base()
        {
        }

        public ProviderWksException(string message) : base(message)
        {
        }

        public ProviderWksException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}