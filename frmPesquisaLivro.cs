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

namespace MyBookDesk
{
    public partial class frmPesquisaLivro : Form
    {
        Form frmPai;

        public frmPesquisaLivro(Form frmPai)
        {
            InitializeComponent();
            this.frmPai = frmPai;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmPai.Tag = "";
            this.Close();
        }

        private void frmPesquisaLivro_Load(object sender, EventArgs e)
        {
            CarregaGridPesquisa();
        }

        private void CarregaGridPesquisa(string campoWhere = "")
        {
            try
            {
                string sql;

                sql = "select id_livro, nome, autor, ano from livro";
                if (!String.IsNullOrEmpty(campoWhere))
                {
                    sql += " where " + campoWhere;
                }
                sql += " order by nome";

                DataTable dt = ConexaoBanco.selecionarDataTable(sql);

                dtgPesquisa.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("Livro não encontrado!",
                                        "Pesquisa de Livros",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                txtId_Livro.Clear();
                txtNome.Clear();
                txtId_Livro.Focus();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string campoWhere = "";
            if (!String.IsNullOrEmpty(txtNome.Text) && !String.IsNullOrEmpty(txtId_Livro.Text))
            {
                MessageBox.Show("Preencha somente um dos campos para pesquisar. ", "Pesquisa de Livros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId_Livro.Clear();
                txtNome.Clear();
                txtId_Livro.Focus();
            }
            else if (!String.IsNullOrEmpty(txtNome.Text))
            {
                campoWhere = "upper(nome) like '" + txtNome.Text.ToUpper() + "%'";
            }
            else if (!String.IsNullOrEmpty(txtId_Livro.Text))
            {
                campoWhere = "id_livro = " + txtId_Livro.Text;
            }
            CarregaGridPesquisa(campoWhere);
        }

        private void dtgPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = dtgPesquisa.CurrentRow.Index;

            frmPai.Tag = dtgPesquisa[0, indice].Value.ToString();

            this.Close();
        }
    }
}
