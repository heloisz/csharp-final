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
    public partial class frmCadastroLivro : Form
    {
        private Livro liv = new Livro();
        private bool novoRegistro = false;

        public frmCadastroLivro()
        {
            InitializeComponent();
        }

        private void LimpaForm()
        {
            txtId_Livro.Clear();
            txtNome.Clear();
            txtAno.Clear();
            txtGenero.Clear();
            txtAutor.Clear();
            txtEditora.Clear();
            txtPreco.Clear();
        }

        private void HabilitaBotoes(bool novoRegistro)
        {
            btnNovo.Enabled = !novoRegistro;
            btnSalvar.Enabled = novoRegistro;
            btnCancelar.Enabled = novoRegistro;
            btnPesquisar.Enabled = !novoRegistro;
            btnExcluir.Enabled = !novoRegistro;
            btnVoltar.Enabled = !novoRegistro;
            txtId_Livro.Enabled = !novoRegistro;
        }

        private void HabilitaTodosBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnPesquisar.Enabled = true;
            btnExcluir.Enabled = true;
            btnVoltar.Enabled = true;
            txtId_Livro.Enabled = true;
            txtId_Livro.Focus();
        }

        private void frmCadastroLivro_Load(object sender, EventArgs e)
        {
            HabilitaTodosBotoes();
        }

        private void frmCadastroLivro_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void frmCadastroLivro_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmHome destino = new frmHome();
            destino.Show();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimpaForm();
            novoRegistro = true;
            HabilitaBotoes(true);
            liv.Id_livro = 0;
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtAno.Text) || String.IsNullOrEmpty(txtAutor.Text) ||
                     String.IsNullOrEmpty(txtEditora.Text) || String.IsNullOrEmpty(txtGenero.Text) || String.IsNullOrEmpty(txtPreco.Text))
                {
                    MessageBox.Show("Preencha todos os campos!", "Cadastro de Livros",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtNome.Focus();
                    return;
                }

                liv.Nome = txtNome.Text;
                liv.Ano = Convert.ToDecimal(txtAno.Text);
                liv.Genero = txtGenero.Text;
                liv.Autor = txtAutor.Text;
                liv.Id_editora = Convert.ToInt64(txtEditora.Text);
                liv.Preco = Convert.ToDecimal(txtPreco.Text);

                Servico.SalvarLivro(liv);

                DialogResult resposta;
                resposta = MessageBox.Show("Livro salvo com sucesso!", "Cadastro de Livros",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpaForm();
                novoRegistro = false;
                HabilitaTodosBotoes();
                txtId_Livro.Focus();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar os dados do livro. " + "\n\nMais detalhes: " +
                                ex.Message, "Cadastro de Livros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtId_Livro.Text == "")
            {
                frmPesquisaLivro pesq = new frmPesquisaLivro(this);
                pesq.ShowDialog();
                txtId_Livro.Text = this.Tag.ToString();
            }
            if (txtId_Livro.Text != "")
                CarregarDadosLivro();
        }

        private void CarregarDadosLivro()
        {
            try
            {
                liv = Servico.PesquisarLivro(Convert.ToInt64(txtId_Livro.Text));
                if (liv != null)
                {
                    txtId_Livro.Text = liv.Id_livro.ToString();
                    txtNome.Text = liv.Nome;
                    txtAno.Text = liv.Ano.ToString();
                    txtGenero.Text = liv.Genero;
                    txtAutor.Text = liv.Autor;
                    txtEditora.Text = liv.Id_editora.ToString();
                    txtPreco.Text = liv.Preco.ToString();
                }
                else
                {
                    MessageBox.Show("Livro não encontrado!",
                                        "Cadastro de Livros",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
                    LimpaForm();
                    txtId_Livro.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao pesquisar o livro! " +
                            "\n\nMais detalhes: " + ex.Message,
                            "Cadastro de Livros",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpaForm();
            HabilitaTodosBotoes();
            liv.Id_livro = 0;
            txtId_Livro.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtId_Livro.Text))
                {
                    DialogResult resposta;

                    resposta = MessageBox.Show("Deseja realmente excluir o livro" + txtNome.Text + "?",
                                                    "Cadastro de Livros",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if (resposta == DialogResult.Yes)
                    {
                        Int64 id_livro = Convert.ToInt64(txtId_Livro.Text);
                        Servico.ExcluirLivro(id_livro);
                        MessageBox.Show("O livro " + txtNome.Text + " foi excluido com sucesso!",
                                                "Cadastro de Livros",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                        LimpaForm();
                        HabilitaTodosBotoes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar excluir o livro. " +
                                    "\n\nMais detalhes: " + ex.Message,
                                    "Cadastro de Livros",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                LimpaForm();
                HabilitaTodosBotoes();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmHome formDestino = new frmHome();
            this.Hide();
            formDestino.Show();
        }
    }
}
