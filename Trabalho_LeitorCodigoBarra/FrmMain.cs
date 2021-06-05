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

namespace Trabalho_LeitorCodigoBarra
{
    public partial class FrmMain : Form
    {
        private KeyboardHook keyboardHook = null;
        string Texto = string.Empty;
        //Limpar Dados
        //HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
        //HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce
        //HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run
        //HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\RunOnce
        public FrmMain()
        {
            InitializeComponent();
            keyboardHook = new Kennedy.ManagedHooks.KeyboardHook();
            keyboardHook.KeyboardEvent += new Kennedy.ManagedHooks.KeyboardHook.KeyboardEventHandler(keyboardHook_KeyboardEvent);
            keyboardHook.InstallHook();
            try
            {
                serialPort.Dispose();
            }
            catch { }
            try
            {
                serialPort.Close();
            }
            catch { }
            try
            {
                if (serialPort.IsOpen == false)
                {
                    serialPort.Open();
                }
            }
            catch { }
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                reg.SetValue("Leitor Codigo Barra", Application.ExecutablePath.ToString());
            }
            catch { }

        }
        string Formatar(string txt)
        {
            string retornoLocal = string.Empty;

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


            return retornoLocal;
        }
        private void keyboardHook_KeyboardEvent(KeyboardEvents kEvent, Keys key)
        {
            if (kEvent.ToString().Equals("KeyUp") == false)
            {
                Formatar(key.ToString());
                if (key.ToString().Equals("Return"))
                {
                    if (Texto.Length > 8)
                    {
                        try
                        {
                            serialPort.Write("Evento");
                        }
                        catch { }
                    }
                    Texto = string.Empty;

                }

            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            this.Hide();
            timer.Enabled = false;
        }
    }
}
