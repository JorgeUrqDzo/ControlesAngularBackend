using ClbModControles.Documentos;
using ClbDatControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;

namespace ClbNegControles
{
    public class clsNegUploadFiles
    {
        public int Agregar(int strTablaRelacion, int intIdRelacion, clsModUploadFiles objBasEncDocumentos)
        {
            int IdDocumento = 0;

            try
            {
                clsDatUploadFiles obj = new clsDatUploadFiles();
                IdDocumento = obj.Agregar(strTablaRelacion, intIdRelacion, objBasEncDocumentos);
                return IdDocumento;
            }
            catch
            {
                return 0;
            }
        }
        public int AgregarLargeFile(int strTablaRelacion, int intIdRelacion, clsModUploadFiles objBasEncDocumentos)
        {
            int IdDocumento = 0;

            try
            {
                clsDatUploadFiles obj = new clsDatUploadFiles();
                IdDocumento = obj.AgregarLargeFile(strTablaRelacion, intIdRelacion, objBasEncDocumentos);
                return IdDocumento;
            }
            catch
            {
                return 0;
            }
        }

        public List<clsModUploadFiles> CargarDocumentosByIdNormal(int IdTablaRelacion, int intIdRelacion, int intId, int IdUsuario)
        {
            try
            {
                List<clsModUploadFiles> lstModUploadFiles = new List<clsModUploadFiles>();
                clsDatUploadFiles obj = new clsDatUploadFiles();
                lstModUploadFiles = obj.CargarDocumentosByIdNormal(IdTablaRelacion, intIdRelacion, intId, IdUsuario);
                return lstModUploadFiles;
            }
            catch
            {
                return null;
            }
        }

        public List<clsModUploadFiles> CargarDocumentos(int IdTablaRelacion, int intIdRelacion, int intId, int IdUsuario)
        {
            try
            {
                List<clsModUploadFiles> lstModUploadFiles = new List<clsModUploadFiles>();
                clsDatUploadFiles obj = new clsDatUploadFiles();
                lstModUploadFiles = obj.CargarDocumentos(IdTablaRelacion, intIdRelacion, intId, IdUsuario);
                return lstModUploadFiles;
            }
            catch
            {
                return null;
            }
        }

    }
}
