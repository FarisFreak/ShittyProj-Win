using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;
using System.Net;
using System.Web.Script.Serialization;
using System.Management;


namespace ShittyProj
{
    public partial class frmMain : Form
    {
        data dat;
        data.Rootobject game = new data.Rootobject();
        public frmMain()
        {
            InitializeComponent();
        }
        private void logs(string s)
        {
            var jam = DateTime.Now.ToString("HH:mm:ss");
            rtbLog.Text += "\n[" + jam + "] " + s;
        }
        private void loadInfo()
        {
            game = ShittyFunc.ser.Deserialize<data.Rootobject>(ShittyFunc.res2);
            dat = ShittyFunc.ser.Deserialize<data>(ShittyFunc.res);
            rtbLog.Text = "Status";
            rtbLog.Text += "\n=========================";
            lblUsername.Text = dat.username;
            lblName.Text = dat.nama;
            lblLevel.Text = dat.level;
            if (dat.expiry >= 0) {
                lblExpiry.Text = "Remaining " + dat.expiry + " day(s)";
            } else if (dat.expiry >= -98 && dat.expiry <= -1) {
                lblExpiry.Text = "Expired";
                logs("Expired license. Please contact admin.");
            } else if (dat.expiry == -99) {
                lblExpiry.Text = "-";
                logs("Not registered PC. Please contact admin.");
            }
            if (dat.aktif == 99) {
                lblHWIDStatus.Text = "Not Registered";
                logs("Your HWID : " + ShittyFunc.GetHDSN() + ". Please contact admin.");
            } else if (dat.aktif == 0) {
                lblHWIDStatus.Text = "Not Active";
            } else if (dat.aktif == 1) {
                lblHWIDStatus.Text = "Active";
                logs("Welcome :)");
            }
            lblSaldo.Text = "$" + dat.saldo;
            lblPoint.Text = dat.point;
            //
            lblIPAddress.Text = PCInfo.IP();
            lblMacAddress.Text = PCInfo.MAC();
            lblHWID.Text = PCInfo.HDSN();
        }
        private void GameList()
        {
            if (game.data.Length > 0)
            {
                int i = 0;
                while (i < game.data.Length)
                {
                    cmbGameList.Items.Add(game.data[i].game);
                    i++;
                }
            }
            cmbGameList.SelectedIndex = 0;
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            //frmLogin frmlog = new frmLogin();
            //Task task = Task.Run((Action) loadInfo);
            loadInfo();
            GameList();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Convert.ToString(cmbGameList.SelectedIndex));
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            MessageBox.Show(game.data[cmbGameList.SelectedIndex].path);
        }
    }
}
