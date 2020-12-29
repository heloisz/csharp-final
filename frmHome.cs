using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBookDesk
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_FormClosed(object sender, FormClosedEventArgs e)
        {
           this.Hide();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            frmCadastroCliente formDestino = new frmCadastroCliente();
            this.Hide();
            formDestino.Show();
        }

        private void btnEditora_Click(object sender, EventArgs e)
        {
            frmCadastroEditora formDestino = new frmCadastroEditora();
            this.Hide();
            formDestino.Show();
        }

        private void btnLivro_Click(object sender, EventArgs e)
        {
            frmCadastroLivro formDestino = new frmCadastroLivro();
            this.Hide();
            formDestino.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

        }
    }
}
