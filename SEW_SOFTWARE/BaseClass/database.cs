using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SEW_SOFTWARE.BaseClass
{
    class database
    {
        public static SqlConnection myCon()
        {
            return new SqlConnection("server=localhost;database=SEW;uid=wuyue;pwd=Kylin123.");
        }
    }
}
