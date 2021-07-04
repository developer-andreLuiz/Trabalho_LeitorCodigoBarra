using Kennedy.ManagedHooks;
using Microsoft.Win32;
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
using Trabalho_LeitorCodigoBarra.View;

namespace Trabalho_LeitorCodigoBarra
{
    public partial class FrmMain : Form
    {
        #region Informações 
        //Limpar Dados
        //HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
        //HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce
        //HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run
        //HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\RunOnce
        #endregion
    
        #region Variaveis
        private KeyboardHook keyboardHook = null;
        string Texto = string.Empty;
        string palavraChave = string.Empty;
        int timeMinimizar = 9;
        List<Tb_produto_Model> ListaProdutos = new List<Tb_produto_Model>();
        #endregion

        #region Funções
        void Formatar(string txt)
        {
            

            if (txt.Equals("D0"))
            {
                txt = "0";
                Texto += txt;
            }
            if (txt.Equals("D1"))
            {
                txt = "1";
                Texto += txt;
            }
            if (txt.Equals("D2"))
            {
                txt = "2";
                Texto += txt;
            }
            if (txt.Equals("D3"))
            {
                txt = "3";
                Texto += txt;
            }
            if (txt.Equals("D4"))
            {
                txt = "4";
                Texto += txt;
            }
            if (txt.Equals("D5"))
            {
                txt = "5";
                Texto += txt;
            }
            if (txt.Equals("D6"))
            {
                txt = "6";
                Texto += txt;
            }
            if (txt.Equals("D7"))
            {
                txt = "7";
                Texto += txt;
            }
            if (txt.Equals("D8"))
            {
                txt = "8";
                Texto += txt;
            }
            if (txt.Equals("D9"))
            {
                txt = "9";
                Texto += txt;
            }


          
        }
        void FormatarPalavraChave(string txt)
        {
            palavraChave += txt;
        }
        void Cancelar()
        {
            btnCancelar.Text = "Esconder";
            btnCancelar.BackColor = Color.Green;
            lblSegundos.Text = "10";
            timeMinimizar = 9;
            timer.Enabled = false;
            groupBox1.Visible = false;
        }
        #endregion

        #region Eventos
        public FrmMain()
        {
            InitializeComponent();
           
            //Teclado
            keyboardHook = new Kennedy.ManagedHooks.KeyboardHook();
            keyboardHook.KeyboardEvent += new Kennedy.ManagedHooks.KeyboardHook.KeyboardEventHandler(keyboardHook_KeyboardEvent);
            keyboardHook.InstallHook();
           
            //Porta Serial
            try { serialPort.Dispose(); } catch { }
            try { serialPort.Close(); } catch { }
            try { if (serialPort.IsOpen == false){ serialPort.Open(); } } catch { }
           
            //Iniciar com o windows
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                reg.SetValue("Leitor Codigo Barra", Application.ExecutablePath.ToString());
            }
            catch { }

        }
        private void keyboardHook_KeyboardEvent(KeyboardEvents kEvent, Keys key)
        {
            if (kEvent.ToString().Equals("KeyUp") == false)
            {
                Formatar(key.ToString());
                FormatarPalavraChave(key.ToString());
                if (key.ToString().Equals("Return"))
                {
                    bool cadastrado = false;
                    foreach (var item in ListaProdutos)
                    {
                        if (item.codigo.Equals(Texto))
                        {
                            cadastrado = true;
                            try { serialPort.Write("Evento"); } catch { }
                            try
                            {
                                Tb_registro_Model registro = new Tb_registro_Model();
                                registro.codigo = item.codigo;
                                registro.nome = item.nome;
                                registro.data = DateTime.Now.ToShortDateString();
                                registro.hora = DateTime.Now.ToString("HH:mm:ss");
                                Banco.Tb_registro.Inserir(registro);
                            }
                            catch { }
                            break;
                           
                        }
                    }




                    if (Texto.Length > 7 && cadastrado==false)
                    {
                        try { serialPort.Write("Evento");} catch { }
                    }
                    
                  
                    
                    
                    
                    
                    if (palavraChave.Equals("APARECERReturn"))
                    {
                        this.Show();
                        btnfocus.Focus();
                        Cancelar();
                    }
                    Texto = string.Empty;
                    palavraChave = string.Empty;

                }
                
            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            lblSegundos.Text = timeMinimizar.ToString();
            timeMinimizar--;
            if (timeMinimizar==-1)
            {
                this.Hide();
                timer.Enabled = false;
            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (btnCancelar.Text.Equals("Cancelar"))
            {
                Cancelar();

            }
            else
            {
                btnCancelar.Text = "Cancelar";
                btnCancelar.BackColor = Color.Red;
                this.Hide();
              





            }
        }
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            Cancelar();

            FrmRegistro f = new FrmRegistro();
            f.ShowDialog();
        }
        private void btnProduto_Click(object sender, EventArgs e)
        {
            Cancelar();

            FrmProduto f = new FrmProduto();
            f.ShowDialog();
        }
        private void btnfocus_Click(object sender, EventArgs e)
        {
           

        }

        private void FrmMain_Activated(object sender, EventArgs e)
        {
            ListaProdutos = Banco.Tb_produto.RetornoCompleto();
        }


        #endregion


    }
}
