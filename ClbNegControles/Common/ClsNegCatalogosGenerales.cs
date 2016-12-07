using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nezter.System.ComboGeneral.Modelos;
using Nezter.System.ComboGeneral.Negocio;
using ClbDatControles.Common;
using ClbModControles;
using ClbModControles.Enums;
using ClbNegControles.Helpers;

namespace ClbNegControles
{
    /// <summary>
    /// Clase negocio para las operaciones de consulta de catálogos generales.
    /// </summary>
    public class ClsNegCatalogosGenerales
    {
        /// <summary>
        /// Clase que expone la cadena de conexión de base de datos.
        /// </summary>
        private readonly DataBaseConnection objExportConnection;

        /// <summary>
        /// Constructor interno de la clase.
        /// </summary>
        public ClsNegCatalogosGenerales()
        {
            this.objExportConnection = DataBaseConnection.Instance;
        }

        /// <summary>
        /// Obtiene el catálogo de tipos de persona.
        /// </summary>
        /// <param name="intIdUsuario">Identificador del usuario que realiza la consulta.</param>
        /// <returns></returns>
        public IEnumerable<ClsModComboCatalogos> GetCatalogoTipoControl(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
                Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoControl), "IdTipoControl", "TipoControl", intIdUsuario);
            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }

        /// <summary>
        /// Obtiene el catálogo de tipos de persona.
        /// </summary>
        /// <param name="intIdUsuario">Identificador del usuario que realiza la consulta.</param>
        /// <returns></returns>
        public IEnumerable<ClsModComboCatalogos> GetCatalogoTipoColumna(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
                Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoColumna), "IdTipoColumna", "TipoColumna", intIdUsuario);
            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }
        /// <summary>
        /// Obtiene el catálogo de tipos de persona.
        /// </summary>
        /// <param name="intIdUsuario">Identificador del usuario que realiza la consulta.</param>
        /// <returns></returns>
        public IEnumerable<ClsModComboCatalogos> GetCatalogoGrupo(int intIdUsuario, int intIdForumario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
                Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatGrupo), "IdGrupo", "Grupo","IdFormulario = "+ intIdForumario, intIdUsuario);
            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }
        
        /// <summary>
        /// Obtiene el catálogo de tipos de persona.
        /// </summary>
        /// <param name="intIdUsuario">Identificador del usuario que realiza la consulta.</param>
        /// <returns></returns>
        public IEnumerable<ClsModComboCatalogos> GetCatalogoTipoSeccion(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
                Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoSeccion), "IdTipoSeccion", "TipoSeccion", intIdUsuario);
            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }

        public IEnumerable<ClsModComboCatalogos> GetCatalogoTipoParametro(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(), 
                Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoParametro), "IdTipoParametro", "TipoParametro", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }

        public IEnumerable<ClsModComboCatalogos> GetCatalogoCatTipoEnvio(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(), 
                Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoEnvio), "IdTipoEnvio", "TipoEnvio", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }

        public IEnumerable<ClsModComboCatalogos> GetCatalogoTipoProceso(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoProceso), "IdTipoProceso", "TipoProceso", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }

        public IEnumerable<ClsModComboCatalogos> GetCatalogoTipoMenu(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoMenu), "IdTipoMenu", "TipoMenu", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }
        public IEnumerable<ClsModComboCatalogos> GetCatalogoIdMenuIcono(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatIdMenuIcono), "IdMenuIcono", "Icono", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }

        public IEnumerable<ClsModComboCatalogos> GetCatalogoAreaProceso(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatAreaProceso), "IdAreaProceso", "AreaProceso", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }


        public IEnumerable<ClsModComboCatalogos> GetCatalogoTipoFormulario(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoFormulario), "IdTipoFormulario", "TipoFormulario", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }
        public IEnumerable<ClsModComboCatalogos> GetCatalogoIconoSeccion(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatIconos), "Id", "Icono", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }

        //public IEnumerable<ClsModComboCatalogos> GetCatalogoIconoSeccion(int intIdUsuario)
        //{
        //    ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
        //       Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatSeccionIcono), "IdSeccionIcono", "Icono", intIdUsuario);

        //    return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        //}

        public IEnumerable<ClsModComboCatalogos> GetTipoFormulario(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatFormulario), "IdFormulario", "Nombre", "IdTipoFormulario = 2", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }
        public IEnumerable<ClsModComboCatalogos> GetCatMenu(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatMenu), "IdMenu", "Menu", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }
        public IEnumerable<ClsModComboCatalogos> getTipoConsulta(int intIdUsuario)
        {
            ICollection<ClsModSysComboGeneral> lstSysCombo = ClsNegSysComboGeneral.Cargar(this.objExportConnection.GetConnectionString(),
               Enum.GetName(typeof(TablaCatalogo), TablaCatalogo.CatTipoConsulta), "IdTipoConsulta", "tipo", intIdUsuario);

            return ObjectMapper.Instance.Convert<ICollection<ClsModSysComboGeneral>, ICollection<ClsModComboCatalogos>>(lstSysCombo);
        }
    }
}
