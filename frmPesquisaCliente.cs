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
    public partial class frmPesquisaCliente : Form
    {
        Form frmPai;
        public frmPesquisaCliente(Form frmPai)
        {
            InitializeComponent();
            this.frmPai = frmPai;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmPai.Tag = "";
            this.Close();
        }

        private void frmPesquisaCliente_Load(object sender, EventArgs e)
        {
            CarregaGridPesquisa();
        }

        private void CarregaGridPesquisa(string campoWhere = "")
        {
            try
            {
                string sql;

                sql = "select id_cliente, nome, cpf, email from cliente";
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
                MessageBox.Show("Cliente não encontrado!",
                                        "Pesquisa de Clientes",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                txtId_Cliente.Clear();
                txtNome.Clear();
                txtId_Cliente.Focus();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string campoWhere = "";
            if (!String.IsNullOrEmpty(txtNome.Text) && !String.IsNullOrEmpty(txtId_Cliente.Text))
            {
                MessageBox.Show("Preencha somente um dos campos para pesquisar. ", "Pesquisa de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtId_Cliente.Clear();
                txtNome.Clear();
                txtId_Cliente.Focus();
            }
            else if(!String.IsNullOrEmpty(txtNome.Text))
            {
                campoWhere = "upper(nome) like '" + txtNome.Text.ToUpper() + "%'";
            }
            else if(!String.IsNullOrEmpty(txtId_Cliente.Text))
            {
                campoWhere = "id_cliente = " + txtId_Cliente.Text;
            }
            CarregaGridPesquisa(campoWhere);
        }

        private void dtgPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = dtgPesquisa.CurrentRow.Index;

            frmPai.Tag = dtgPesquisa[2, indice].Value.ToString();

            this.Close();
        }
    }
}
