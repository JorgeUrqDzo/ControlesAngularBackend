
namespace webControles.Models
{
    public class MensajeModel
    {

        public MensajeModel() { }

        public MensajeModel(bool bolError, string strMensaje) {
            Error = bolError;
            Mensaje = strMensaje;
        }
        public MensajeModel(bool bolError, string strMensaje, int intId)
        {
            Error = bolError;
            Mensaje = strMensaje;
            Id = intId;
        }

        public bool Error { get; set; }
        public string Mensaje { get; set; }

        public int Id { get; set; }

    }
}
