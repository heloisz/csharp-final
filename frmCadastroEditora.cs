using MyBookDesk.Entidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyBookDesk;

namespace MyBookDesk
{
    public partial class frmCadastroEditora : Form
    {
        private Editora edi = new Editora();
        private bool novoRegistro = false;
       
        public frmCadastroEditora()
        {
            InitializeComponent();
        }

        private void LimpaForm()
        {
            txtId_Editora.Clear();
            txtNome.Clear();
            txtVendedor.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtEstado.Clear();
            txtTelefone.Clear();
        }

        private void HabilitaBotoes(bool novoRegistro)
        {
            btnNovo.Enabled = !novoRegistro;
            btnSalvar.Enabled = novoRegistro;
            btnCancelar.Enabled = novoRegistro;
            btnPesquisar.Enabled = !novoRegistro;
            btnExcluir.Enabled = !novoRegistro;
            btnVoltar.Enabled = !novoRegistro;
        }

        private void HabilitaTodosBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnPesquisar.Enabled = true;
            btnExcluir.Enabled = true;
            btnVoltar.Enabled = true;
            txtNome.Focus();
        }

        private void frmCadastroEditora_Load(object sender, EventArgs e)
        {
            HabilitaTodosBotoes();
        }

        private void frmCadastroEditora_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void frmCadastroEditora_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmHome destino = new frmHome();
            destino.Show();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaForm();
            novoRegistro = true;
            HabilitaBotoes(true);
            edi.Id_editora = 0;
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtVendedor.Text) || String.IsNullOrEmpty(txtTelefone.Text) ||
                     String.IsNullOrEmpty(txtEndereco.Text) || String.IsNullOrEmpty(txtBairro.Text) || String.IsNullOrEmpty(txtCidade.Text) ||
                     String.IsNullOrEmpty(txtEstado.Text))
                {
                    MessageBox.Show("Preencha todos os campos!", "Cadastro da Editora",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtNome.Focus();
                    return;
                }

                edi.Nome = txtNome.Text;
                edi.Vendedor = txtVendedor.Text;
                edi.Endereco = txtEndereco.Text;
                edi.Bairro = txtBairro.Text;
                edi.Cidade = txtCidade.Text;
                edi.Estado = txtEstado.Text;
                edi.Telefone = txtTelefone.Text;

                Servico.SalvarEditora(edi);

                DialogResult resposta;
                resposta = MessageBox.Show("Editora salva com sucesso!", "Cadastro da Editora",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpaForm();
                novoRegistro = false;
                HabilitaTodosBotoes();
                txtNome.Focus();

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ocorreu um erro ao cadastrar os dados da editora. " + "\n\nMais detalhes: " +
                                ex.Message, "Cadastro de Editora", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaForm();
            HabilitaTodosBotoes();
            edi.Id_editora = 0;
            txtNome.Focus();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtTelefone.Text == "(  )      -")
            {
                frmPesquisaEditora pesq = new frmPesquisaEditora(this);
                pesq.ShowDialog();
                txtTelefone.Text = this.Tag.ToString();
            }
            if (txtTelefone.Text != "(  )      -")
                CarregarDadosCliente();
        }

        private void CarregarDadosCliente()
        {
            try
            {
                edi = Servico.PesquisarEditora(txtTelefone.Text);
                if (edi != null)
                { 
                    txtId_Editora.Text = edi.Id_editora.ToString();
                    txtNome.Text = edi.Nome;
                    txtVendedor.Text = edi.Vendedor;
                    txtEndereco.Text = edi.Endereco;
                    txtBairro.Text = edi.Bairro;
                    txtCidade.Text = edi.Cidade;
                    txtEstado.Text = edi.Estado;
                    txtTelefone.Text = edi.Telefone;
                }
                else
                {
                    MessageBox.Show("Editora não encontrada!",
                                        "Cadastro de Editora",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    LimpaForm();
                    txtNome.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao pesquisar a editora! " +
                            "\n\nMais detalhes: " + ex.Message,
                            "Cadastro de Editora",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtId_Editora.Text))
                {
                    DialogResult resposta;

                    resposta = MessageBox.Show("Deseja realmente excluir a editora " + txtNome.Text + "?",
                                                    "Cadastro de Editoras",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if (resposta == DialogResult.Yes)
                    {
                        Int64 id_editora = Convert.ToInt64(txtId_Editora.Text);
                        Servico.ExcluirEditora(id_editora);
                        MessageBox.Show("A editora " + txtNome.Text + " foi excluido com sucesso",
                                                "Cadastro de Editoras",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                        LimpaForm();
                        HabilitaTodosBotoes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar excluir a editora. " +
                                    "\n\nMais detalhes: " + ex.Message,
                                    "Cadastro de Editoras",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                LimpaForm();
                HabilitaTodosBotoes();
            }
        }
        private void txtTelefone_Enter(object sender, EventArgs e)
        {
            txtTelefone.SelectAll();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmHome formDestino = new frmHome();
            this.Hide();
            formDestino.Show();
        }
    }
}
