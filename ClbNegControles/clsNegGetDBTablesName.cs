using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;

namespace ClbNegControles
{
    public class clsNegGetDBTablesName
    {
        public List<clsModGetDBTablesName> GetDbTablesNames(out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsDatGetDBTablesName objClsDatGetDbTablesName = new clsDatGetDBTablesName();
            return objClsDatGetDbTablesName.getTablesDBName(out objModErrorBase);
        }
    }
}
