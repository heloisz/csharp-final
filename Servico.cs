using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBookDesk.Entidades;
using Npgsql;

namespace MyBookDesk
{
    public static class Servico
    {
        //-----------------------------------------------------Usuario-----------------------------------------------------
        private static Usuario objLogin(ref NpgsqlDataReader dr)
        {
            Usuario usu = new Usuario();
            usu.Login = dr["login"].ToString();
            usu.Senha = dr["senha"].ToString();
            return usu;
        }

        public static Usuario Login(string login, string senha)
        {
            Usuario usu = new Usuario();
            string sql = "Select * from usuario " +
                                "where login = @1 and senha = @2";

            List<object> param = new List<object>();
            param.Add(login);
            param.Add(senha);

            NpgsqlDataReader dr = ConexaoBanco.selecionar(sql, param);
            if (dr.Read())
            {
                usu = objLogin(ref dr);
            }
            else
            {
                usu = null;
            }
            dr.Close();
            return usu;
        }

        //-----------------------------------------------------Cliente-----------------------------------------------------
        private static Cliente objUsuario(ref NpgsqlDataReader dr)
        {
            Cliente cli = new Cliente();
            cli.Id_cliente = Convert.ToInt64(dr["id_cliente"]);
            cli.Nome = dr["nome"].ToString();
            cli.Cpf = dr["cpf"].ToString();
            cli.Dtnascimento = Convert.ToDateTime(dr["dtnascimento"]);
            cli.Sexo = dr["sexo"].ToString();
            cli.Endereco = dr["endereco"].ToString();
            cli.Bairro = dr["bairro"].ToString();
            cli.Cidade = dr["cidade"].ToString();
            cli.Estado = dr["estado"].ToString();
            cli.Email = dr["email"].ToString();
            return cli;
        }

        public static Cliente PesquisarUsuario(string cpf)
        {
            Cliente cli = new Cliente();
            string sql = "Select * from cliente " +
                                "where cpf = @1";

            List<object> param = new List<object>();
            param.Add(cpf);

            NpgsqlDataReader dr = ConexaoBanco.selecionar(sql, param);
            if (dr.Read())
            {
                cli = objUsuario(ref dr);
            }
            else
            {
                cli = null;
            }
            dr.Close();
            return cli;
        }

        public static Cliente PesquisarUsuarioEmail(string email)
        {
            Cliente cli = new Cliente();
            string sql = "Select * from cliente " +
                                "where email = @1";

            List<object> param = new List<object>();
            param.Add(email);

            NpgsqlDataReader dr = ConexaoBanco.selecionar(sql, param);
            if (dr.Read())
            {
                cli = objUsuario(ref dr);
            }
            else
            {
                cli = null;
            }
            dr.Close();
            return cli;
        }

        public static void SalvarUsuario(Cliente cli)
        {
            string sql;

            List<object> param = new List<object>();
            param.Add(cli.Nome);
            param.Add(cli.Cpf);
            param.Add(cli.Dtnascimento);
            param.Add(cli.Sexo);
            param.Add(cli.Endereco);
            param.Add(cli.Bairro);
            param.Add(cli.Cidade);
            param.Add(cli.Estado);
            param.Add(cli.Email);

            if (cli.Id_cliente == 0)
                sql = "insert into cliente (nome, cpf, dtnascimento, sexo, endereco, bairro, cidade, estado, email) values (@1, @2, @3, @4, @5, @6, @7, @8, @9)";
            else
            {
                sql = "update cliente set " +
                            "nome  = @1, " +
                            "cpf   = @2, " +
                            "dtnascimento   = @3, " +
                            "sexo   = @4, " +
                            "endereco   = @5, " +
                            "bairro   = @6, " +
                            "cidade   = @7, " +
                            "estado = @8, " +
                            "email = @9 " +
                        "where id_cliente = @10";
                param.Add(Convert.ToInt64(cli.Id_cliente));
            }

            ConexaoBanco.executar(sql, param);
        }

        public static void ExcluirCliente(Int64 id_cliente)
        {
            try
            {
                string sql = "delete from cliente " +
                                       "where id_cliente = @1";

                List<object> param = new List<object>();
                param.Add(id_cliente);
                ConexaoBanco.executar(sql, param);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        //-----------------------------------------------------Editora-----------------------------------------------------
        private static Editora objEditora(ref NpgsqlDataReader dr)
        {
            Editora edi = new Editora();
            edi.Id_editora = Convert.ToInt64(dr["id_editora"]);
            edi.Nome = dr["nome"].ToString();
            edi.Vendedor = dr["vendedor"].ToString();
            edi.Endereco = dr["endereco"].ToString();
            edi.Bairro = dr["bairro"].ToString();
            edi.Cidade = dr["cidade"].ToString();
            edi.Estado = dr["estado"].ToString();
            edi.Telefone = dr["telefone"].ToString();
            return edi;
        }

        public static void SalvarEditora(Editora edi)
        {
            string sql;

            List<object> param = new List<object>();
            param.Add(edi.Nome);
            param.Add(edi.Vendedor);
            param.Add(edi.Endereco);
            param.Add(edi.Bairro);
            param.Add(edi.Cidade);
            param.Add(edi.Estado);
            param.Add(edi.Telefone);

            if (edi.Id_editora == 0)
                sql = "insert into editora (nome, vendedor, endereco, bairro, cidade, estado, telefone) values (@1, @2, @3, @4, @5, @6, @7)";
            else
            {
                sql = "update editora set " +
                            "nome  = @1, " +
                            "vendedor   = @2, " +
                            "endereco   = @3, " +
                            "bairro   = @4, " +
                            "cidade   = @5, " +
                            "estado = @6, " +
                            "telefone = @7 " +
                        "where id_editora = @8";
                param.Add(Convert.ToInt64(edi.Id_editora));
            }

            ConexaoBanco.executar(sql, param);
        }

        public static Editora PesquisarEditora(string telefone)
        {
            Editora edi = new Editora();
            string sql = "Select * from editora " +
                                "where telefone = @1";

            List<object> param = new List<object>();
            param.Add(telefone);

            NpgsqlDataReader dr = ConexaoBanco.selecionar(sql, param);
            if (dr.Read())
            {
                edi = objEditora(ref dr);
            }
            else
            {
                edi = null;
            }
            dr.Close();
            return edi;
        }

        public static void ExcluirEditora(Int64 id_editora)
        {
            try
            {
                string sql = "delete from editora " +
                                       "where id_editora = @1";

                List<object> param = new List<object>();
                param.Add(id_editora);
                ConexaoBanco.executar(sql, param);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        //-----------------------------------------------------Livro-----------------------------------------------------
        private static Livro objLivro(ref NpgsqlDataReader dr)
        {
            Livro liv = new Livro();
            liv.Id_livro = Convert.ToInt64(dr["id_livro"]);
            liv.Nome = dr["nome"].ToString();
            liv.Ano = Convert.ToDecimal(dr["ano"]);
            liv.Genero = dr["genero"].ToString();
            liv.Autor = dr["autor"].ToString();
            liv.Id_editora = Convert.ToInt64(dr["id_editora"]);
            liv.Preco = Convert.ToDecimal(dr["preco"]);
            return liv;
        }

        public static void SalvarLivro(Livro liv)
        {
            string sql;

            List<object> param = new List<object>();
            param.Add(liv.Nome);
            param.Add(liv.Ano);
            param.Add(liv.Genero);
            param.Add(liv.Autor);
            param.Add(liv.Id_editora);
            param.Add(liv.Preco);

            if (liv.Id_livro == 0)
                sql = "insert into livro (nome, ano, genero, autor, id_editora, preco) values (@1, @2, @3, @4, @5, @6)";
            else
            {
                sql = "update livro set " +
                            "nome  = @1, " +
                            "ano   = @2, " +
                            "genero   = @3, " +
                            "autor   = @4, " +
                            "id_editora   = @5, " +
                            "preco = @6 " +
                        "where id_editora = @7";
                param.Add(Convert.ToInt64(liv.Id_livro));
            }

            ConexaoBanco.executar(sql, param);
        }

        public static Livro PesquisarLivro(Int64 id_livro)
        {
            Livro liv = new Livro();
            string sql = "Select * from livro " +
                                "where id_livro = @1";

            List<object> param = new List<object>();
            param.Add(id_livro);

            NpgsqlDataReader dr = ConexaoBanco.selecionar(sql, param);
            if (dr.Read())
            {
                liv = objLivro(ref dr);
            }
            else
            {
                liv = null;
            }
            dr.Close();
            return liv;
        }

        public static void ExcluirLivro(Int64 id_livro)
        {
            try
            {
                string sql = "delete from livro " +
                                       "where id_livro = @1";

                List<object> param = new List<object>();
                param.Add(id_livro);
                ConexaoBanco.executar(sql, param);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        //-----------------------------------------------------Compra-----------------------------------------------------
        private static Compra objCompra(ref NpgsqlDataReader dr)
        {
            Compra com = new Compra();
            com.Id_compra = Convert.ToInt64(dr["id_compra"]);
            com.Id_cliente = Convert.ToInt64(dr["id_cliente"]);
            com.Dtvenda = Convert.ToDateTime(dr["dtvenda"]);
            com.Total = Convert.ToDecimal(dr["total"]);
            return com;
        }

        public static void SalvarCompra(Compra com)
        {
            string sql;

            List<object> param = new List<object>();
            param.Add(com.Id_cliente);
            param.Add(com.Dtvenda);
            param.Add(com.Total);

            if (com.Id_compra == 0)
                sql = "insert into compra (id_cliente, dtvenda, total) values (@1, @2, @3)";
            else
            {
                sql = "update compra set " +
                            "id_cliente  = @1, " +
                            "dtvenda   = @2, " +
                            "total   = @3 " +
                        "where id_compra = @4";
                param.Add(Convert.ToInt64(com.Id_compra));
            }

            ConexaoBanco.executar(sql, param);
        }

        public static Compra PesquisarCompra(Int64 id_compra)
        {
            Compra com = new Compra();
            string sql = "Select * from compra " +
                                "where id_compra = @1";

            List<object> param = new List<object>();
            param.Add(id_compra);

            NpgsqlDataReader dr = ConexaoBanco.selecionar(sql, param);
            if (dr.Read())
            {
                com = objCompra(ref dr);
            }
            else
            {
                com = null;
            }
            dr.Close();
            return com;
        }

        public static void ExcluirCompra(Int64 id_compra)
        {
            try
            {
                string sql = "delete from compra " +
                                       "where id_compra = @1";

                List<object> param = new List<object>();
                param.Add(id_compra);
                ConexaoBanco.executar(sql, param);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        //-----------------------------------------------------Itens da Compra-----------------------------------------------------
        private static Itens_Compra objItensCompra(ref NpgsqlDataReader dr)
        {
            Itens_Compra itc = new Itens_Compra();
            itc.Id_compra = Convert.ToInt64(dr["id_compra"]);
            itc.Id_livro = Convert.ToInt64(dr["id_livro"]);
            itc.Qtd = Convert.ToInt32(dr["qtd"]);
            itc.Preco = Convert.ToDecimal(dr["preco"]);
            return itc;
        }
    }
}
