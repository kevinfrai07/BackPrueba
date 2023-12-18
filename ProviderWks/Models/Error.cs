namespace ProviderWks.Domain.Models
{
    /// <summary>
    /// Clase de tipo error para mostrar la excepcion
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Descripcion del error
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// InnerException
        /// </summary>
        public string InnerException { get; set; }
    }
}