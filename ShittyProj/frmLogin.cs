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

    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
            
        }
        private void StartFrmMain()
        {
            this.Hide();
            var frmmain = new frmMain();
            frmmain.Closed += (s, args) => this.Close();
            frmmain.Show();
        }
        private void LoginAction(object sender, DoWorkEventArgs e)
        {
            
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            string jsonAcc = Fnc.PHP(InjectData.JSonData, "POST", "username=" + txtUsername.Text + "&password=" + txtPassword.Text + "&hdsn=" + ShittyFunc.GetHDSN());
            data dat = new data();
            dat = ShittyFunc.ser.Deserialize<data>(jsonAcc);
            if (txtUsername.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show("Please fill username & password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLogin.Enabled = true;
            }
            else
            {
                if (dat.available == 1)
                {
                    MessageBox.Show("Login Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //
                    ShittyFunc.username = txtUsername.Text;
                    ShittyFunc.password = txtPassword.Text;
                    string jsonGame = Fnc.PHP(InjectData.JSonGame, "POST", "username=" + txtUsername.Text + "&password=" + txtPassword.Text);
                    data.Rootobject game = new data.Rootobject();
                    game = ShittyFunc.ser.Deserialize<data.Rootobject>(jsonAcc);
                    //
                    ShittyFunc.res = jsonAcc;
                    ShittyFunc.res2 = jsonGame;
                    StartFrmMain();
                }
                else if (dat.available == 0)
                {
                    MessageBox.Show("Wrong Username / Password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnLogin.Enabled = true;
                }
            }
        }
    }
}



