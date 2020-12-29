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
    public partial class frmCadastroCliente : Form
    {
        private Cliente cli = new Cliente();
        private bool novoRegistro = false;

        public frmCadastroCliente()
        {
            InitializeComponent();
        }

        private void LimpaForm()
        {
            txtId_Cliente.Clear();
            txtNome.Clear();
            txtCpf.Clear();
            txtNascimento.Clear();
            txtSexo.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtEstado.Clear();
            txtEmail.Clear();
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
            txtCpf.Focus();
        }

        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            HabilitaTodosBotoes();
        }

        private void frmCadastroCliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void frmCadastroCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmHome destino = new frmHome();
            destino.Show();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaForm();
            novoRegistro = true;
            HabilitaBotoes(true);
            cli.Id_cliente = 0;
            txtCpf.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtCpf.Text) || String.IsNullOrEmpty(txtNascimento.Text) ||
                     String.IsNullOrEmpty(txtSexo.Text) || String.IsNullOrEmpty(txtEndereco.Text) || String.IsNullOrEmpty(txtBairro.Text) ||
                     String.IsNullOrEmpty(txtCidade.Text) || String.IsNullOrEmpty(txtEstado.Text) || String.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("Preencha todos os campos!", "Cadastro do Cliente",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtCpf.Focus();
                    return;
                }

                cli.Nome = txtNome.Text;
                cli.Cpf = txtCpf.Text;
                cli.Dtnascimento = Convert.ToDateTime(txtNascimento.Text);
                cli.Sexo = txtSexo.Text;
                cli.Endereco = txtEndereco.Text;
                cli.Bairro = txtBairro.Text;
                cli.Cidade = txtCidade.Text;
                cli.Estado = txtEstado.Text;
                cli.Email = txtEmail.Text;
              
                Servico.SalvarUsuario(cli);

                DialogResult resposta;
                resposta = MessageBox.Show("Cliente salvo com sucesso!", "Cadastro do Cliente",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpaForm();
                novoRegistro = false;
                HabilitaTodosBotoes();
                txtCpf.Focus();

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ocorreu um erro ao cadastrar os dados do Cliente. " + "\n\nMais detalhes: " +
                                ex.Message, "Cadastro do Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaForm();
            HabilitaTodosBotoes();
            cli.Id_cliente = 0;
            txtCpf.Focus();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtCpf.Text == "   .   .   -")
            {
                frmPesquisaCliente pesq = new frmPesquisaCliente(this);
                pesq.ShowDialog();
                txtCpf.Text = this.Tag.ToString();
            }
            if(txtCpf.Text != "   .   .   -")
                CarregarDadosCliente();
        }

        private void CarregarDadosCliente()
        {
            try
            {
                cli = Servico.PesquisarUsuario(txtCpf.Text);
                if (cli != null)
                {
                    txtId_Cliente.Text = cli.Id_cliente.ToString();
                    txtNome.Text = cli.Nome;
                    txtCpf.Text = cli.Cpf;
                    txtNascimento.Text = cli.Dtnascimento.ToString();
                    txtSexo.Text = cli.Sexo;
                    txtEndereco.Text = cli.Endereco;
                    txtBairro.Text = cli.Bairro;
                    txtCidade.Text = cli.Cidade;
                    txtEstado.Text = cli.Estado;
                    txtEmail.Text = cli.Email;
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado!",
                                        "Cadastro de Clientes",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    LimpaForm();
                    txtCpf.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao pesquisar o cliente! " +
                            "\n\nMais detalhes: " + ex.Message,
                            "Cadastro de Clientes",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtId_Cliente.Text))
                {
                    DialogResult resposta;

                    resposta = MessageBox.Show("Deseja realmente excluir o cliente " + txtNome.Text + "?",
                                                    "Cadastro de Clientes",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if (resposta == DialogResult.Yes)
                    {
                        Int64 id_cliente = Convert.ToInt64(txtId_Cliente.Text);
                        Servico.ExcluirCliente(id_cliente);
                        MessageBox.Show("O cliente " + txtNome.Text + " foi excluido com sucesso",
                                                "Cadastro de Clientes",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                        LimpaForm();
                        HabilitaTodosBotoes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar excluir o cliente. " +
                                    "\n\nMais detalhes: " + ex.Message,
                                    "Cadastro de Clientes",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                LimpaForm();
                HabilitaTodosBotoes();
            }
        }

        private void txtCpf_Enter(object sender, EventArgs e)
        {
            txtCpf.SelectAll();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmHome formDestino = new frmHome();
            this.Hide();
            formDestino.Show();
        }
    }
}