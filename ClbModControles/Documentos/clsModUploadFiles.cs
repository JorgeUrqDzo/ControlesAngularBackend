using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles.Documentos
{
   public class clsModUploadFiles
    {
        private string _Image64;

        public int IdDocumento { get; set; }
        public int TablaRelacion { get; set; }
        public string[] requisitos { get; set; }
        public string NombreArchivo { get; set; }
        public byte[] Archivo { get; set; }

        public string DocumentContent { get; set; }
        public string ExtensionArchivo { get; set; }
        public int Estatus { get; set; }
        public List<List<string[]>> Capturas { get; set; }
        public int IdRelacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public int IdRequisito { get; set; }
        public string Texto { get; set; }
        public byte[] BArchivo { get; set; }
        public string TokenFile { get; set; }
        public bool IsFisico { get; set; }
        public string FullName { get; set; }
        public string ImageTo64
        {
            get { return _Image64 = (BArchivo != null) ? Convert.ToBase64String(BArchivo) : null; }
        }
    }
}
