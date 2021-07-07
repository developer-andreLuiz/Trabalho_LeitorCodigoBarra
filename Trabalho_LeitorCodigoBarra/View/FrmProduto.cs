using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabalho_LeitorCodigoBarra.Model;

namespace Trabalho_LeitorCodigoBarra.View
{
    public partial class FrmProduto : Form
    {
        #region Variaveis
        bool novo = false;
        List<Tb_produto_Model> Lista = new List<Tb_produto_Model>();
        #endregion

        #region Funções
        //interface
        void Limpar()
        {
            txtId.Text = "0";
            txtCodigo.Text = string.Empty;
            txtNome.Text = string.Empty;
        }
        void InterfaceAbrirGravarCancelarDeletar()
        {
            btnNovo.Enabled = true;
            btnAtualizar.Enabled = true;
            btnGravar.Enabled = false;
            btnCancelar.Enabled = false;
            btnExcluir.Enabled = true;
            btnApagarTudo.Enabled = true;

            txtCodigo.Enabled = false;
            txtNome.Enabled = false;
        }
        void InterfaceNovoAtualizar()
        {
            btnNovo.Enabled = false;
            btnAtualizar.Enabled = false;
            btnGravar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = false;
            btnApagarTudo.Enabled = true;

            txtCodigo.Enabled = true;
            txtNome.Enabled = true;
        }

        //Objeto
        bool Gravar(Tb_produto_Model objLocal)
        {
            bool retorno = true;
            objLocal.id = int.Parse(txtId.Text);
            if (txtCodigo.Text.Length>0)
            {
                objLocal.codigo = txtCodigo.Text;
                if (txtNome.Text.Length>0)
                {
                    objLocal.nome = txtNome.Text;
                }
                else
                {
                    retorno = false;
                    MessageBox.Show("Nome do Produto Invalido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                retorno = false;
                MessageBox.Show("Código do Produto Invalido","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            return retorno;
            
        }
        void Exibir(Tb_produto_Model objLocal)
        {
            txtId.Text = objLocal.id.ToString();
            txtCodigo.Text = objLocal.codigo;
            txtNome.Text = objLocal.nome;
        }

        //Grid
        void CarregarGrid()
        {
            if (Lista.Count > 0)
            {
                dataGridView.DataSource = Lista;
            }
            else
            {
                dataGridView.DataSource = new List<Tb_produto_Model>();
            }

            dataGridView.Columns[0].Width = 50;
            dataGridView.Columns[1].Width = 150;
            dataGridView.Columns[2].Width = 250;
          



            lblProdutos.Text = Lista.Count.ToString();
        }

        #endregion

        #region Eventos
        public FrmProduto()
        {
            InitializeComponent();
            Lista = Banco.Tb_produto.RetornoCompleto();
            InterfaceAbrirGravarCancelarDeletar();
            CarregarGrid();
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            Limpar();
            InterfaceNovoAtualizar();
            novo = true;

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Equals("0")==false)
            {
                InterfaceNovoAtualizar();
                novo = false;
            }
            else
            {
                MessageBox.Show("Id do Produto Invalido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Tb_produto_Model p = new Tb_produto_Model();
            if (Gravar(p))
            {
                if (novo)
                {
                    bool continuar = true;

                    foreach (var item in Lista)
                    {
                        if (item.codigo.Equals(p.codigo))
                        {
                            continuar = false;
                            break;
                        }
                    }
                    if (continuar)
                    {
                        Banco.Tb_produto.Inserir(p);
                        Lista = Banco.Tb_produto.RetornoCompleto();
                        Limpar();
                        InterfaceAbrirGravarCancelarDeletar();
                        CarregarGrid();
                      
                    }
                    else
                    {
                        MessageBox.Show("Codigo do Produto ja Cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Banco.Tb_produto.Atualizar(p);
                    Lista = Banco.Tb_produto.RetornoCompleto();
                    InterfaceAbrirGravarCancelarDeletar();
                    CarregarGrid();
                }
              
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Deseja APAGAR este Produto?","Atenção",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Tb_produto_Model p = new Tb_produto_Model();
                if (Gravar(p))
                {
                    Banco.Tb_produto.Deletar(p);
                    Lista = Banco.Tb_produto.RetornoCompleto();
                    Limpar();
                    InterfaceAbrirGravarCancelarDeletar();
                    CarregarGrid();
                }
            } 
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //Limpar();
            //InterfaceAbrirGravarCancelarDeletar();

            #region Função de Importar
            //List<string> ListaNome = new List<string>();
            //List<string> ListaCodigo = new List<string>();
            //String line;
            //try
            //{
            //    //Pass the file path and file name to the StreamReader constructor
            //    StreamReader sr = new StreamReader("C:\\Prov\\Nome.txt");
            //    //Read the first line of text
            //    line = sr.ReadLine();
            //    //Continue to read until you reach end of file
            //    while (line != null)
            //    {
            //        string[] txt = line.Split(' ');
            //        ListaNome.Add(txt[0]);
            //        //Read the next line
            //        line = sr.ReadLine();
            //    }
            //    //close the file
            //    sr.Close();
            //    Console.ReadLine();
            //}
            //catch { }
            //try
            //{
            //    //Pass the file path and file name to the StreamReader constructor
            //    StreamReader sr = new StreamReader("C:\\Prov\\Codigo.txt");
            //    //Read the first line of text
            //    line = sr.ReadLine();
            //    //Continue to read until you reach end of file
            //    while (line != null)
            //    {
            //        string txt = "00" + line;
            //        ListaCodigo.Add(txt);
            //        //Read the next line
            //        line = sr.ReadLine();
            //    }
            //    //close the file
            //    sr.Close();
            //    Console.ReadLine();
            //}
            //catch { }

            //if (ListaNome.Count == ListaCodigo.Count)
            //{
            //    for (int i = 0; i < ListaNome.Count; i++)
            //    {
            //        Tb_produto_Model produto = new Tb_produto_Model();
            //        produto.codigo = ListaCodigo[i];
            //        produto.nome = ListaNome[i];
            //        Banco.Tb_produto.Inserir(produto);



            //    }

            //    MessageBox.Show("Finalizado");




            //}
            #endregion
        }
        private void btnApagarTudo_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("CUIDADO! DESEJA APAGAR TODOS OS PRODUTOS? NÃO TEM COMO RECUPERAR", "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                Banco.Tb_produto.Truncate();
                Lista = Banco.Tb_produto.RetornoCompleto();
                Limpar();
                InterfaceAbrirGravarCancelarDeletar();
                CarregarGrid();
            }
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Tb_produto_Model p = new Tb_produto_Model();

            p.id = int.Parse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            p.codigo = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            p.nome = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            Exibir(p);
        }


        #endregion

       
    }
}
