using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_LeitorCodigoBarra.Model;

namespace Trabalho_LeitorCodigoBarra
{
    class Banco
    {
        protected static SQLiteConnection conexao { get; } = new SQLiteConnection("Data Source=c:\\Leitor\\Banco.db;Version=3");
        public static void AbrirConexao()
        {
            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }
        }
        public class Tb_registro
        {
            /// <summary>
            /// Retorna Todos os registros da Tabela
            /// </summary>
            /// <returns></returns>
            public static List<Tb_registro_Model> RetornoCompleto()
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand("select * from tb_registro order by id asc", conexao);
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
                DataTable dtLista = new DataTable();
                sQLiteDataAdapter.Fill(dtLista);
                List<Tb_registro_Model> ltFinal = new List<Tb_registro_Model>();
                foreach (DataRow dataRow in dtLista.Rows)
                {
                    Tb_registro_Model newTb_reconhecimento_Model = new Tb_registro_Model();
                    newTb_reconhecimento_Model.id = Convert.ToInt32(dataRow["id"]);
                    newTb_reconhecimento_Model.data = Convert.ToString(dataRow["data"]);
                    newTb_reconhecimento_Model.hora = Convert.ToString(dataRow["hora"]);
                    newTb_reconhecimento_Model.codigo = Convert.ToString(dataRow["codigo"]);
                    newTb_reconhecimento_Model.nome = Convert.ToString(dataRow["nome"]);

                    ltFinal.Add(newTb_reconhecimento_Model);
                }
                return ltFinal;
            }
       
            /// <summary>
            /// Insere registros na tabela
            /// </summary>
            /// <param name="objLocal"></param>
            public static void Inserir(Tb_registro_Model objLocal)
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand("insert into tb_registro (data, hora, codigo, nome) values (@data, @hora, @codigo, @nome)", conexao);
                cmd.Parameters.AddWithValue("data", objLocal.data);
                cmd.Parameters.AddWithValue("hora", objLocal.hora);
                cmd.Parameters.AddWithValue("codigo", objLocal.codigo);
                cmd.Parameters.AddWithValue("nome", objLocal.nome);
                cmd.ExecuteNonQuery();
            }
            /// <summary>
            /// Apaga Todos os Registros da Tabela... Muito Cuidado ao utilizar esta Função
            /// </summary>
            public static void Truncate()
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand("delete from tb_registro", conexao);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from sqlite_sequence where name='tb_registro'";
                cmd.ExecuteNonQuery();
            }
        }
        public class Tb_produto 
        {
            /// <summary>
            /// Retorna Todos os registros da Tabela
            /// </summary>
            /// <returns></returns>
            public static List<Tb_produto_Model> RetornoCompleto()
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand("select * from tb_produto order by nome asc", conexao);
                SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
                DataTable dtLista = new DataTable();
                sQLiteDataAdapter.Fill(dtLista);
                List<Tb_produto_Model> ltFinal = new List<Tb_produto_Model>();
                foreach (DataRow dataRow in dtLista.Rows)
                {
                    Tb_produto_Model objLocal = new Tb_produto_Model();
                    objLocal.id = Convert.ToInt32(dataRow["id"]);
                    objLocal.codigo = Convert.ToString(dataRow["codigo"]);
                    objLocal.nome = Convert.ToString(dataRow["nome"]);

                    ltFinal.Add(objLocal);
                }
                return ltFinal;
            }
            /// <summary>
            /// Insere registros na tabela
            /// </summary>
            /// <param name="objLocal"></param>
            public static void Inserir(Tb_produto_Model objLocal)
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand("insert into tb_produto (codigo, nome) values (@codigo, @nome)", conexao);
                cmd.Parameters.AddWithValue("codigo", objLocal.codigo);
                cmd.Parameters.AddWithValue("nome", objLocal.nome);
                cmd.ExecuteNonQuery();
            }
            /// <summary>
            /// Atualiza registros na tabela
            /// </summary>
            /// <param name="objLocal"></param>
            public static void Atualizar(Tb_produto_Model objLocal)
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }

                SQLiteCommand cmd = new SQLiteCommand("update tb_produto set codigo = @codigo, nome = @nome where id = @id", conexao);
                cmd.Parameters.AddWithValue("id", objLocal.id);
                cmd.Parameters.AddWithValue("codigo", objLocal.codigo);
                cmd.Parameters.AddWithValue("nome", objLocal.nome);
                cmd.ExecuteNonQuery();
            }
            /// <summary>
            /// Deleta registros na tabela
            /// </summary>
            /// <param name="objLocal"></param>
            public static void Deletar(Tb_produto_Model objLocal)
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand("delete from tb_produto where id = @id", conexao);
                cmd.Parameters.AddWithValue("id", objLocal.id);
                cmd.ExecuteNonQuery();
            }

            /// <summary>
            /// Apaga Todos os Registros da Tabela... Muito Cuidado ao utilizar esta Função
            /// </summary>
            public static void Truncate()
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                SQLiteCommand cmd = new SQLiteCommand("delete from tb_produto", conexao);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from sqlite_sequence where name='tb_produto'";
                cmd.ExecuteNonQuery();
            }
        }

    }
}
