namespace ProviderWks.Domain.Exceptions
{
    /// <summary>
    /// Clase de tipo exception personalizada
    /// </summary>
    public class ProviderWksException : Exception
    {
        /// <summary>
        ///Constructores para excepcion
        /// </summary>
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