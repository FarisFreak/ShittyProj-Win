using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShittyProj
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Fnc.IsUserAdministrator() == true)
            {
                Application.Run(new frmLogin());
            }
            else
            {
                MessageBox.Show("Please Run As Administrator!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
