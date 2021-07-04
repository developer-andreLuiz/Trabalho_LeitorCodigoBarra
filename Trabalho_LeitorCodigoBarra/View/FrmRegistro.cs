using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabalho_LeitorCodigoBarra.Model;

namespace Trabalho_LeitorCodigoBarra.View
{
    public partial class FrmRegistro : Form
    {
        #region Variaveis

        #endregion

        #region Funções
        void CarregarGidCompleto()
        {
            var list = Banco.Tb_registro.RetornoCompleto();
            if (list.Count>0)
            {
                dataGridView.DataSource = list;
            }
            else
            {
                dataGridView.DataSource = new List<Tb_registro_Model>();
            }

            dataGridView.Columns[0].Width = 50;
            dataGridView.Columns[1].Width = 90;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 90;
            dataGridView.Columns[4].Width = 150;



            lblLinhas.Text = list.Count.ToString();
        }
        void CarregarGid()
        {
            string data = dateTimePicker1.Text;
            var list = Banco.Tb_registro.RetornoCompleto();

            List<Tb_registro_Model> listaGrid = new List<Tb_registro_Model>();

            foreach (var item in list)
            {
                if (item.data.Equals(data))
                {
                    listaGrid.Add(item);
                }
            }

            if (listaGrid.Count > 0)
            {
                dataGridView.DataSource = listaGrid;
            }
            else
            {
                dataGridView.DataSource = new List<Tb_registro_Model>();
            }

            dataGridView.Columns[0].Width = 50;
            dataGridView.Columns[1].Width = 90;
            dataGridView.Columns[2].Width = 70;
            dataGridView.Columns[3].Width = 90;
            dataGridView.Columns[4].Width = 150;



            lblLinhas.Text = listaGrid.Count.ToString();
        }
        #endregion

        #region Eventos
        public FrmRegistro()
        {
            InitializeComponent();
            CarregarGidCompleto();
        }

        private void btnDeletarTudo_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Deseja Apagar Todos os Registros ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                Banco.Tb_registro.Truncate();
                CarregarGidCompleto();
            }
        }

        #endregion

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CarregarGid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarregarGidCompleto();
        }
    }
}
