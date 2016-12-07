using AutoMapper;
using webControles;
using webControles.Models;
using ClbModControles;
using webControles.Models.Views;

namespace webControles.Helpers
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
            #region Mapeo entre FormularioModel y clsModCatFormularios
            Mapper.CreateMap<FormularioModel, clsModCatFormularios>();
            Mapper.CreateMap<clsModCatFormularios, FormularioModel>();
            Mapper.CreateMap<clsModCatSecciones, SeccionesModel>();
            #endregion

            //Mapeo entre BusquedaBandejaModelo y clsModCatBandejas
            Mapper.CreateMap<BusquedaBandejaModel, clsModCatBandejas>();
            Mapper.CreateMap<clsModCatBandejas, BusquedaBandejaModel>();

            Mapper.CreateMap<MenuIconoModel, clsModCatMenuIcono>();
            Mapper.CreateMap<clsModCatMenuIcono, MenuIconoModel>();

            #region Mapeo entre SeccionModel y clsModCatSecciones
            Mapper.CreateMap<SeccionesModel, clsModCatSecciones>();
            Mapper.CreateMap<clsModCatSecciones, SeccionesModel>();
            #endregion

            Mapper.CreateMap<clsModCatSeccionControl, SeccionControlModel>();
            Mapper.CreateMap<SeccionControlModel, clsModCatSeccionControl>();
            Mapper.CreateMap<clsModCatSecciones, SeccionControlViewModel>();
            Mapper.CreateMap<SeccionControlViewModel, clsModCatSecciones>();


            Mapper.CreateMap<clsModCatBandejaColumna, BandejaColumnaModel>();
            Mapper.CreateMap<BandejaColumnaModel, clsModCatBandejaColumna>();

            Mapper.CreateMap<clsModCatBandejaFiltros, BandejaFiltrosModel>();
            Mapper.CreateMap<BandejaFiltrosModel, clsModCatBandejaFiltros>();


            #region Mapeo entre BandejaModel y clsModEncConfBandeja
            Mapper.CreateMap<clsModEncConfBandeja, BandejaModel>();
            Mapper.CreateMap<BandejaModel, clsModEncConfBandeja>();
            #endregion

            Mapper.CreateMap<clsModCatActividad, ActividadModel>();
            Mapper.CreateMap<ActividadModel, clsModCatActividad>();

            Mapper.CreateMap<clsModCatMenu, MenuModel>();
            Mapper.CreateMap<MenuModel, clsModCatMenu>();

            #region Mapeo entre clsModDetConfBandejaLinkParametro y ParametrosModel
            Mapper.CreateMap<clsModDetConfBandejaLinkParametro, ParametrosModel>();
            Mapper.CreateMap<ParametrosModel, clsModDetConfBandejaLinkParametro>();

            #endregion

            #region Mapeo entre clsModCatAreaProceso y AreaProcesoModel
            Mapper.CreateMap<clsModCatAreaProceso, AreaProcesoModel>();
            Mapper.CreateMap<AreaProcesoModel, clsModCatAreaProceso>();
            #endregion

            #region Mapeo entre clsModEncProceso y EncProcesoModel
            Mapper.CreateMap<clsModEncProceso, EncProcesoModel>();
            Mapper.CreateMap<EncProcesoModel, clsModEncProceso>();
            #endregion

            #region Mapeo entre DetActividadModel y clsModDetActividad
            Mapper.CreateMap<clsModDetActividad, DetActividadModel>();
            Mapper.CreateMap<DetActividadModel, clsModDetActividad>();

            #endregion

            #region Mapeo entre TipoProcesoModel y clsModCatTipoProceso
            Mapper.CreateMap<TipoProcesoModel, clsModCatTipoProceso>();
            Mapper.CreateMap<clsModCatTipoProceso, TipoProcesoModel>();
            #endregion

            Mapper.CreateMap<ClsModComboCatalogos, ComboCatalogosModel>();

            Mapper.CreateMap<clsModEncDataSource, EncDatasourceModel>();
            Mapper.CreateMap<EncDatasourceModel, clsModEncDataSource>();

            Mapper.CreateMap<clsModCatTipoProceso, TipoProcesoModel>();
            Mapper.CreateMap<TipoProcesoModel, clsModCatTipoProceso>();

            #region Mapeo entre clsModGetDBTablesName y getDBTablesNameModel
            Mapper.CreateMap<clsModGetDBTablesName, getDBTablesNameModel>();
            Mapper.CreateMap<getDBTablesNameModel, clsModGetDBTablesName>();
            #endregion

            #region Mapeo entre clsModGetTableColumnsNames y getTableColumnsNamesModel
            Mapper.CreateMap<clsModGetTableColumnsNames, getTableColumnsNamesModel>();
            Mapper.CreateMap<getTableColumnsNamesModel, clsModGetTableColumnsNames>();
            #endregion

            #region Mapeo entre clsModControlesGenerados y GenerarControlesModel

            Mapper.CreateMap<clsModControlesGenerados, GenerarControlesModel>();
            Mapper.CreateMap<GenerarControlesModel, clsModControlesGenerados>();

            #endregion

            #region Mapeo entre clsModRelSecciones y RelSeccionesModel

            Mapper.CreateMap<clsModRelSecciones, RelSeccionesModel>();
            Mapper.CreateMap<RelSeccionesModel, clsModRelSecciones>();

            #endregion


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
