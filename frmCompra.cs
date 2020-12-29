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
    public partial class frmCompra : Form
    {
        private Compra com = new Compra();
        private bool novoRegistro = false;
        private Int64 id_compra;

        public frmCompra()
        {
            InitializeComponent();
        }

        private void LimpaForm()
        {
            txtId_Compra.Clear();
            cmbCliente.SelectedIndex = -1;
            dtpDataCompra.Value = DateTime.Today;
            lblValorTotal.Text = "0,00";
        }

        private void HabilitaBotoes(bool novoRegistro)
        {
            btnNovo.Enabled = !novoRegistro;
            btnSalvar.Enabled = novoRegistro;
            btnCancelar.Enabled = novoRegistro;
            btnPesquisar.Enabled = !novoRegistro;
            btnExcluir.Enabled = !novoRegistro;
            btnVoltar.Enabled = !novoRegistro;
            txtId_Compra.Enabled = !novoRegistro;
        }

        private void HabilitaTodosBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnPesquisar.Enabled = true;
            btnExcluir.Enabled = true;
            btnVoltar.Enabled = true;
        }

        public void CarregaComboCliente()
        {
            string sql = "select id_cliente, nome from cliente order by nome";
            DataTable dt = ConexaoBanco.selecionarDataTable(sql);
            cmbCliente.ValueMember = "id_cliente";
            cmbCliente.DisplayMember = "nome";
            cmbCliente.DataSource = dt;
            cmbCliente.SelectedIndex = -1;
        }

        public void CarregaComboLivro()
        {
            string sql = "select id_livro, nome from livro order by nome";
            DataTable dt = ConexaoBanco.selecionarDataTable(sql);
            cmbLivro.ValueMember = "id_livro";
            cmbLivro.DisplayMember = "nome";
            cmbLivro.DataSource = dt;
            cmbLivro.SelectedIndex = -1;
        }

        public void CarregaGridItens(Int64 id_compra = 0)
        {
            string sql = "select id_compra, id_livro, qtd, preco from itenscompra where id_compra = "+id_compra+"";
            DataTable dt = ConexaoBanco.selecionarDataTable(sql);
            dtgLivros.DataSource = dt;
        }

        private void frmCompra_Load(object sender, EventArgs e)
        {
            CarregaComboCliente();
            CarregaComboLivro();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaForm();
            novoRegistro = true;
            HabilitaBotoes(true);
            com.Id_compra = 0;
            cmbCliente.Focus();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtId_Compra.Text))
            {
                MessageBox.Show("Digite o ID da Compra para realizar a pesquisa!",
                           "Cadastro de Compras",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Exclamation);
            }
            if (!String.IsNullOrEmpty(txtId_Compra.Text))
                CarregaCompra();
        }

        private void CarregaCompra()
        {
            try
            {
                com = Servico.PesquisarCompra(Convert.ToInt64(txtId_Compra.Text));
                if (com != null)
                {
                    txtId_Compra.Text = com.Id_compra.ToString();
                    cmbCliente.Text = com.Id_cliente.ToString();
                    dtpDataCompra.Text = com.Dtvenda.ToString();
                    lblValorTotal.Text = com.Total.ToString();
                    CarregaGridItens(Convert.ToInt64(txtId_Compra.Text));
                }
                else
                {
                    MessageBox.Show("Compra não encontrada!",
                                        "Cadastro de Compras",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    LimpaForm();
                    txtId_Compra.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao pesquisar a compra! " +
                            "\n\nMais detalhes: " + ex.Message,
                            "Cadastro de Compras",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                if (cmbCliente.SelectedIndex == -1)
                {
                    MessageBox.Show("Preencha todos os campos!", "Cadastro do Cliente",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    cmbCliente.Focus();
                    return;
                }

                com.Id_cliente = Convert.ToInt64(cmbCliente.SelectedValue);
                com.Dtvenda = Convert.ToDateTime(dtpDataCompra.Text);
                com.Total = Convert.ToDecimal(lblValorTotal.Text);

                Servico.SalvarCompra(com);
                //id_compra = nextvalue

                DialogResult resposta;
                resposta = MessageBox.Show("Compra salva com sucesso!", "Cadastro de Compras",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                //LimpaForm();
                novoRegistro = false;
                HabilitaTodosBotoes();
                cmbLivro.Focus();

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ocorreu um erro ao cadastrar os dados da compra. " + "\n\nMais detalhes: " +
                                ex.Message, "Cadastro de Compras", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaForm();
            HabilitaTodosBotoes();
            com.Id_compra = 0;
            txtId_Compra.Focus();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmHome formDestino = new frmHome();
            this.Hide();
            formDestino.Show();
        }

        private void bntDeletar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtId_Compra.Text))
                {
                    DialogResult resposta;

                    resposta = MessageBox.Show("Deseja realmente excluir a compra " + txtId_Compra.Text + "?",
                                                    "Cadastro de Compras",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if (resposta == DialogResult.Yes)
                    {
                        Int64 id_compra = Convert.ToInt64(txtId_Compra.Text);
                        Servico.ExcluirCliente(id_compra);
                        MessageBox.Show("A compra " + txtId_Compra.Text + " foi excluída com sucesso",
                                                "Cadastro de Compras",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                        LimpaForm();
                        HabilitaTodosBotoes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar excluir a compra. " +
                                    "\n\nMais detalhes: " + ex.Message,
                                    "Cadastro de Compra",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                LimpaForm();
                HabilitaTodosBotoes();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void cmbLivro_SelectedValueChanged(object sender, EventArgs e)
        {
            Int64 id_livro = Convert.ToInt64(cmbLivro.SelectedValue);
            string sql;
            sql = "select preco from livro where id_livro = "+@id_livro+"";
        }

        private void frmCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmHome destino = new frmHome();
            destino.Show();
        }

        private void frmCompra_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }
    }
}
