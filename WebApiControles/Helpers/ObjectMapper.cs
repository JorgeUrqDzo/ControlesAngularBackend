using AutoMapper;

namespace WebApiControles.Helpers
{
    /// <summary>
    /// Clase que realiza el mapeo de las entidades.
    /// </summary>
    public sealed class ObjectMapper
    {
        /// <summary>
        ///Instancia especificada a nulo.
        /// </summary>
        public static readonly ObjectMapper Instance = new ObjectMapper();

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        private ObjectMapper()
        {
            this.LoadMapeos();
        }

        /// <summary>
        /// Método que realiza la configuracion del AutoMapper.
        /// </summary>
        private void LoadMapeos()
        {
            
            //Mapper.CreateMap<SeccionControlViewModel, clsModCatSecciones>();
            
            
        }

        /// <summary>
        /// Método que convierte de una entidada a otra.
        /// </summary>
        /// <typeparam name="TOrigen">Entidad que tiene el origen de los datos.</typeparam>
        /// <typeparam name="TDestino">Entidad en donde se mandaron los datos.</typeparam>
        /// <param name="valores">Informacion a ser traducida.</param>
        /// <returns>Nueva entidad con los valores.</returns>
        public TDestino Convert<TOrigen, TDestino>(TOrigen valores)
        {
            var result = Mapper.Map<TOrigen, TDestino>(valores);

            return result;
        }
    }
}
