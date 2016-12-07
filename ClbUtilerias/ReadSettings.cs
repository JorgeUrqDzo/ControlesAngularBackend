using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ClbUtilerias
{
        public class ReadSettings
        {
            /// <summary>
            /// Instancia de la clase.
            /// </summary>
            public static ReadSettings Instance = new ReadSettings();

            /// <summary>
            /// Constructor de la clase.
            /// </summary>
            internal ReadSettings()
            {
            }

            /// <summary>
            /// Obtiene la url de destino del sitio de webforms en caso de generarse un error al validar el editor de documentos.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetUrlEditorNoValidValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["ED.TargetUrlNoValid"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el dominio del sitio donde se encuentra el editor de documentos.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetDomainEditorValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["ED.TargetDomainEditor"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el dominio del sitio hacia donde va a direccionar al terminar de trabajar con el editor de documentos.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetDomainBackValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["NS.TargetDomainBack"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el dominio del sitio hacia donde va a direccionar a abrir el pdf de la solicitud.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetDomainPdfValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["RS.TargetDomainPDF"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el dominio del sitio hacia donde va a direccionar a abrir el pdf de la recibo.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetDomainReciboPdfValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["RR.TargetDomainReciboPdf"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el dominio del sitio hacia donde va a direccionar a abrir el pdf de la factura.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetDomainFacturaPdfValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["RF.TargetDomainFacturaPdf"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el dominio del sitio hacia donde va a direccionar a abrir el pdf de la cotización.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetDomainCotizacionPdfValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["CR.TargetDomainCotizacionPdf"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            public string GetTargetDomainFormAdicionalPdfValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["FR.TargetDomainFormAdicionalPdf"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            public string GetTargetDomainEntrevistaKycPdfValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["FR.TargetDomainEntrevistaKycPdf"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el valor configurado para el tiempo de vida en minutos del uuid tojen del editor de documentos.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public int GetValidMinutesTokenValue()
            {
                try
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["ED.ValidMinutesToken"]);
                }
                catch
                {
                    return 30;
                }
            }

            /// <summary>
            /// Obtiene el dominio del sitio hacia donde va a direccionar a guardar el pdf de la solicitud.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetTargetDomainPdfHandlerValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["RS.TargetDomainPDFHandler"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el nombre de la base de datos de documentos.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetDocumentsDataBaseNameValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["DB.DocumentsName"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el email del remitente.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetEmailRemitenteValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.EmailRemitente"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el nombre del remitente.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetNombreRemitenteValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.NombreRemitente"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el password del remitente.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetPasswordRemitenteValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.PasswordRemitente"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el puerto del email del remitente.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public int GetEmailPuertoRemitenteValue()
            {
                try
                {
                    string strId = ConfigurationManager.AppSettings["EM.PuertoRemitente"];
                    int intId;
                    int.TryParse(strId, out intId);
                    return intId;
                }
                catch
                {
                    return 0;
                }
            }

            /// <summary>
            /// Obtiene el host del email del remitente.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetEmailHostRemitenteValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.HostRemitente"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el valor si se usa SSl en el envío de correo.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public bool GetEmailUseSslRemitenteValue()
            {
                try
                {
                    string strSsl = ConfigurationManager.AppSettings["EM.UseSSL"];
                    bool bolSsl;
                    bool.TryParse(strSsl, out bolSsl);
                    return bolSsl;
                }
                catch
                {
                    return true;
                }
            }

            /// <summary>
            /// Obtiene los emails de los destinatarios.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetEmailsDestinatarioValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.EmailsDestinatario"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene los emails de los destinatarios.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetEmailsDestinatarioFacturaValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.EmailsDestinatarioFactura"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene los emails de copias de los destinatarios.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetEmailsCopiaDestinatarioValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.EmailsCopiaDestinatario"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el sominio del email.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetEmailDominioValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["EM.EmailsDominio"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el id de servidor de correo.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public int GetIdServidorCorreoEmailValue()
            {
                try
                {
                    string strId = ConfigurationManager.AppSettings["EM.IdServidorCorreo"];
                    int intId;
                    int.TryParse(strId, out intId);
                    return intId;
                }
                catch
                {
                    return 1;
                }
            }

            /// <summary>
            /// Obtiene el nombre del ecenario.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetEcenarioValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["BI.Ecenario"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene el nombre del Usuario.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetUsuarioBIValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["BI.Usuario"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene la Contraseña del usuario.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetContraseñaBIValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["BI.Contraseña"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// Obtiene la URL del BI.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetURLValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["BI.URL"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// indica si se ve el menu .
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetVerMenuValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["BI.VerMenu"];
                }
                catch
                {
                    return string.Empty;
                }
            }

            /// <summary>
            /// indica la ruta del BI.
            /// </summary>
            /// <returns>Valor obtenido.</returns>
            public string GetRutaBIValue()
            {
                try
                {
                    return ConfigurationManager.AppSettings["BI.RutaBI"];
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
