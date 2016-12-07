using System;
namespace ClbUtilerias
{
    public class ClsGeneral
    {

        public static DateTime FormatoFecha(string strFecha)
        {
            DateTime FechaIni;
            if (strFecha != "")
            {
                string strFechaIni = "";

                string[] FechaInicio = strFecha.Split('/');

                if (FechaInicio[0].Length > 2)
                {
                    strFechaIni = FechaInicio[0] + "/" + FechaInicio[1] + "/" + FechaInicio[2];
                }
                else
                {
                    strFechaIni = FechaInicio[2] + "/" + FechaInicio[1] + "/" + FechaInicio[0];
                }

                FechaIni = DateTime.Parse(strFechaIni);
            }
            else
            {
                FechaIni = DateTime.Parse("1900/01/01");
            }
            return FechaIni;
        }

        public static string FechayyyyMMdd(DateTime dtFecha) {

            string strFecha = "19000101";

            if (dtFecha != null) {
                if (dtFecha.Year > 1900) {

                    strFecha = dtFecha.ToString("yyyyMMdd");
                }
            }

            return strFecha;
        }


    }

}
