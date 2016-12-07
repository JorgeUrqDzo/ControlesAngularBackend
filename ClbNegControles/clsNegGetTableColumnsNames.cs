using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;

namespace ClbNegControles
{
    public class clsNegGetTableColumnsNames
    {
        public List<clsModGetTableColumnsNames> GetDbTablesNames(out clsModErrorBase objModErrorBase, clsModGetTableColumnsNames objModGetTableColumnsNames)
        {
            objModErrorBase = new clsModErrorBase();
            clsDatGetTableColumnsNames objclsDatGetTableColumnsNames = new clsDatGetTableColumnsNames();
            return objclsDatGetTableColumnsNames.getTableColumnNames(out objModErrorBase, objModGetTableColumnsNames);
        }
    }
}
