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
    public partial class frmPesquisaEditora : Form
    {
        Form frmPai;
        public frmPesquisaEditora(Form frmPai)
        {
            InitializeComponent();
            this.frmPai = frmPai;
        }

        private void frmPesquisaEditora_Load(object sender, EventArgs e)
        {
            CarregaGridPesquisa();
        }

        private void CarregaGridPesquisa(string campoWhere = "")
        {
            try
            {
                string sql;

                sql = "select id_editora, nome, vendedor, telefone from editora";
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
                MessageBox.Show("Editora não encontrada!",
                                        "Pesquisa de Editora",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                txtId_Editora.Clear();
                txtNome.Clear();
                txtId_Editora.Focus();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string campoWhere = "";
            if (!String.IsNullOrEmpty(txtNome.Text) && !String.IsNullOrEmpty(txtId_Editora.Text))
            {
                MessageBox.Show("Preencha somente um dos campos para pesquisar. ", "Pesquisa de Editora", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId_Editora.Clear();
                txtNome.Clear();
                txtId_Editora.Focus();
            }
            else if (!String.IsNullOrEmpty(txtNome.Text))
            {
                campoWhere = "upper(nome) like '" + txtNome.Text.ToUpper() + "%'";
            }
            else if (!String.IsNullOrEmpty(txtId_Editora.Text))
            {
                campoWhere = "id_editora = " + txtId_Editora.Text;
            }
            CarregaGridPesquisa(campoWhere);
        }

        private void dtgPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = dtgPesquisa.CurrentRow.Index;

            frmPai.Tag = dtgPesquisa[3, indice].Value.ToString();

            this.Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmPai.Tag = "";
            this.Close();
        }

        private void txtId_Editora_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
