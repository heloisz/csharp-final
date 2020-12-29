using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using MyBookDesk;
using MyBookDesk.Entidades;

namespace MyBookDesk
{
    public partial class frmLogin : Form
    {
        private Usuario usu = new Usuario();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                usu = Servico.Login(txtLogin.Text, txtSenha.Text);
                if (usu != null)
                {
                    frmHome formDestino = new frmHome();
                    this.Hide();
                    formDestino.Show();
                }
                else
                {
                    MessageBox.Show("Login e/ou senha incorreto(s)!",
                                        "Login",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    txtLogin.Text = "";
                    txtSenha.Text = "";
                    txtLogin.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar logar! " +
                            "\n\nMais detalhes: " + ex.Message,
                            "Login",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            }
        }
    }
}
