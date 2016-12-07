using AutoMapper;
using ClbModControles;
using Nezter.System.ComboGeneral.Modelos;

namespace ClbNegControles.Helpers
{
    /// <summary>
    /// Clase que realiza el mapeo de las entidades.
    /// </summary>
    internal sealed class ObjectMapper
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
            Mapper.CreateMap<ClsModSysComboGeneral, ClsModComboCatalogos>();
            Mapper.CreateMap<ClsModComboCatalogos, ClsModSysComboGeneral>();


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
