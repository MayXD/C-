using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEW_SOFTWARE
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 Form1 = new Form1();
            Form1.ShowDialog();
            if (Form1.closeflag == false)
            {
                Application.Run(new Form2());
            }
            /// local git test
            /// vs git test
        }
    }
}
