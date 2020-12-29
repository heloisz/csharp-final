using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookDesk.Entidades
{
    public class Usuario
    {
        private string login;
        private string senha;

        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ApplicationException("O login do usuário deve ser informado!");
                else
                    login = value;
            }
        }
        public string Senha
        {
            get
            {
                return senha;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ApplicationException("A senha do usuário deve ser informado!");
                else
                    senha = value;
            }
        }
    }

    public class Cliente
    {
        private Int64 id_cliente;
        private string nome;
        private string cpf;
        private DateTime dtnascimento;
        private string sexo;
        private string endereco;
        private string bairro;
        private string cidade;
        private string estado;
        private string email;

        public long Id_cliente { get => id_cliente; set => id_cliente = value; }
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ApplicationException("O nome do cliente deve ser informado!");
                else
                    nome = value;
            }
        }
        public string Cpf { get => cpf; set => cpf = value; }
        public DateTime Dtnascimento { get => dtnascimento; set => dtnascimento = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Email { get => email; set => email = value; }
    }

    public class Editora
    {
        private Int64 id_editora;
        private string nome;
        private string vendedor;
        private string endereco;
        private string bairro;
        private string cidade;
        private string estado;
        private string telefone;

        public long Id_editora { get => id_editora; set => id_editora = value; }
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ApplicationException("O nome da editora deve ser informado!");
                else
                    nome = value;
            }
        }
        public string Vendedor { get => vendedor; set => vendedor = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Cidade { get => cidade; set => cidade = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Telefone { get => telefone; set => telefone = value; }
    }

    public class Livro
    {
        private Int64 id_livro;
        private string nome;
        private Decimal ano;
        private string genero;
        private string autor;
        private Int64 id_editora;
        private Decimal preco;

        public long Id_livro { get => id_livro; set => id_livro = value; }
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ApplicationException("O nome do livro deve ser informado!");
                else
                    nome = value;
            }
        }
        public Decimal Ano { get => ano; set => ano = value; }
        public string Genero { get => genero; set => genero = value; }
        public string Autor { get => autor; set => autor = value; }
        public long Id_editora { get => id_editora; set => id_editora = value; }
        public Decimal Preco { get => preco; set => preco = value; }
    }

    public class Compra
    {
        private Int64 id_compra;
        private Int64 id_cliente;
        private DateTime dtvenda;
        private Decimal total;

        public long Id_compra { get => id_compra; set => id_compra = value; }
        public long Id_cliente { get => id_cliente; set => id_cliente = value; }
        public DateTime Dtvenda { get => dtvenda; set => dtvenda = value; }
        public Decimal Total { get => total; set => total = value; }
    }

    public class Itens_Compra
    {
        private Int64 id_compra;
        private Int64 id_livro;
        private int qtd;
        private Decimal preco;

        public long Id_compra { get => id_compra; set => id_compra = value; }
        public long Id_livro { get => id_livro; set => id_livro = value; }
        public int Qtd { get => qtd; set => qtd = value; }
        public Decimal Preco { get => preco; set => preco = value; }
    }
}