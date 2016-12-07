using System.Collections.Generic;
using ClbNegControles;
using ClbModControles;
using webControles.Models;
using webControles.Helpers;

namespace webControles.Controllers.Apis
{
    public class Catalogos
    {


        /// <summary>
        /// Identificador del usuario actual.
        /// </summary>
        private int intIdUsuario;

        ///// <summary>
        ///// Instancia a la clase negocio de catálogos generales.
        ///// </summary>
        private readonly ClsNegCatalogosGenerales objNegCatalogosGenerales;

        ///// <summary>
        ///// Contrutor de la clase.
        ///// </summary>
        public Catalogos()
        {
            this.objNegCatalogosGenerales = new ClsNegCatalogosGenerales();
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoSeccion()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoTipoSeccion(intIdUsuario);
            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }
        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoControl()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoTipoControl(intIdUsuario);
            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoColumna()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoTipoColumna(intIdUsuario);
            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoGrupo(int intIdFormulario)
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoGrupo(intIdUsuario, intIdFormulario);
            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoParmetro( )
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoTipoParametro(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoEnvio()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoCatTipoEnvio(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }
        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoProceso()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoTipoProceso(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoMenu()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoTipoMenu (intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoIdMenuIcono()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoIdMenuIcono(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoAreaProceso()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoAreaProceso(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatalogoTipoFormulario()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoTipoFormulario(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }
        public IEnumerable<ComboCatalogosModel> GetCatalogoIconoSeccion()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatalogoIconoSeccion(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }
        public IEnumerable<ComboCatalogosModel> getTipoFormulario()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetTipoFormulario(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }

        public IEnumerable<ComboCatalogosModel> GetCatMenu()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.GetCatMenu(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }
        public IEnumerable<ComboCatalogosModel> getTipoConsulta()
        {
            IEnumerable<ClsModComboCatalogos> lstElements = this.objNegCatalogosGenerales.getTipoConsulta(intIdUsuario);

            return ObjectMapper.Instance.Convert<IEnumerable<ClsModComboCatalogos>, IEnumerable<ComboCatalogosModel>>(lstElements);
        }
      
    }
}
