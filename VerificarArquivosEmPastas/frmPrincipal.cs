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

namespace VerificarArquivosEmPastas
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();

            #region CUSTOMIZAÇÃO DO DATAGRIDVIEW

            // linhas alternadas
            listaPasta1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(184,153,126);
            listaPasta2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(184, 153, 126);
            listaEstaoSoNa1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(184, 153, 126);
            listaEstaoSoNa2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(184, 153, 126);
            listaDataDiferente.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(184, 153, 126);

            // linha selecionada
            listaPasta1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230,125,33);
            listaPasta1.DefaultCellStyle.SelectionForeColor = Color.Black;
            listaEstaoSoNa1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            listaEstaoSoNa1.DefaultCellStyle.SelectionForeColor = Color.Black;
            listaEstaoSoNa2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            listaEstaoSoNa2.DefaultCellStyle.SelectionForeColor = Color.Black;
            listaDataDiferente.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            listaDataDiferente.DefaultCellStyle.SelectionForeColor = Color.Black;
            listaPasta2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            listaPasta2.DefaultCellStyle.SelectionForeColor = Color.Black;

            // fonte
            //dataGridView2.DefaultCellStyle.Font = new Font("Century Gothic",8);

            // bordas
            listaPasta1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            listaPasta2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            listaEstaoSoNa1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            listaEstaoSoNa2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            listaDataDiferente.CellBorderStyle = DataGridViewCellBorderStyle.None;

            // cabeçalho
            listaPasta1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            listaPasta2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            listaEstaoSoNa1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            listaEstaoSoNa2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
            listaDataDiferente.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);

            listaPasta1.EnableHeadersVisualStyles = false; // habilita a edição do cabeçalho
            listaPasta2.EnableHeadersVisualStyles = false;
            listaEstaoSoNa1.EnableHeadersVisualStyles = false;
            listaEstaoSoNa2.EnableHeadersVisualStyles = false;
            listaDataDiferente.EnableHeadersVisualStyles = false;

            listaPasta1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 68, 41);
            listaPasta2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 68 ,41);
            listaEstaoSoNa1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 68 ,41);
            listaEstaoSoNa2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 68 ,41);
            listaDataDiferente.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 68 ,41);
            listaPasta1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            listaPasta2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            listaEstaoSoNa1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            listaEstaoSoNa2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            listaDataDiferente.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            #endregion

        }

        private void btnPasta1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                listaPasta1.Rows.Clear();
                DirectoryInfo d = new DirectoryInfo(folder.SelectedPath);
                foreach (var arquivo in d.GetFiles())
                {
                    listaPasta1.Rows.Add(arquivo.Name,arquivo.LastWriteTime);
                }

                txtPasta1.Text = folder.SelectedPath;
            }
            
        }

        private void btnPasta2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                listaPasta2.Rows.Clear();
                DirectoryInfo d = new DirectoryInfo(folder.SelectedPath);
                foreach (var arquivo in d.GetFiles())
                {
                    listaPasta2.Rows.Add(arquivo.Name,arquivo.LastWriteTime);
                }

                txtPasta2.Text = folder.SelectedPath;
            }
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            if (listaPasta1.Rows.Count == 0 || listaPasta2.Rows.Count == 0)
            {
                MessageBox.Show("Carregue os arquivos de duas pastas.","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            listaDataDiferente.Rows.Clear();
            listaEstaoSoNa1.Rows.Clear();
            listaEstaoSoNa2.Rows.Clear();

            List<ItensIguais> itensIguais = new List<ItensIguais>(); // usado para verificar a data
            List<ItensIguais> itensIguais2 = new List<ItensIguais>();

            #region VERIFICAÇÃO DA PRIMEIRA PASTA
            
            foreach (DataGridViewRow arquivo in listaPasta1.Rows)
            {
                string nomedoarquivo = arquivo.Cells[0].Value.ToString();

                int contador = 0;
                foreach (DataGridViewRow file in listaPasta2.Rows)
                {
                    string arquivocompara = file.Cells[0].Value.ToString();
                    string dataModificacao = file.Cells[1].Value.ToString();
                    if (arquivocompara == nomedoarquivo)
                    {
                        contador++;
                        itensIguais.Add(new ItensIguais(arquivocompara,dataModificacao));
                    }
                }

                if (contador == 0)
                {
                    listaEstaoSoNa1.Rows.Add(nomedoarquivo);
                }
            }
            lblTotal1.Text = "Total: " + listaEstaoSoNa1.Rows.Count.ToString();
            #endregion

            #region VERIFICAÇÃO DA SEGUNDA PASTA
            foreach (DataGridViewRow arquivo in listaPasta2.Rows)
            {
                string nomedoarquivo = arquivo.Cells[0].Value.ToString();

                int contador = 0;
                foreach (DataGridViewRow file in listaPasta1.Rows)
                {
                    string arquivocompara = file.Cells[0].Value.ToString();
                    string dataModificacao = file.Cells[1].Value.ToString();
                    if (arquivocompara == nomedoarquivo)
                    {
                        contador++;
                        itensIguais2.Add(new ItensIguais(arquivocompara, dataModificacao));
                    }
                }

                if (contador == 0)
                {
                    listaEstaoSoNa2.Rows.Add(nomedoarquivo);
                }
            }
            lblTotal2.Text = "Total: " + listaEstaoSoNa2.Rows.Count.ToString();

            #endregion

            #region VERIFICAÇÃO DA DATA DE MODIFICACAO

            int i = 0;
            foreach (ItensIguais item in itensIguais)
            {
                string dataModificacao1 = item.Data.ToString();
                string dataModificacao2 = itensIguais2[i].Data.ToString();
                i++;

                if (dataModificacao1 != dataModificacao2)
                {
                    listaDataDiferente.Rows.Add(item.Nome);
                }
            }
            lblTotal3.Text = "Total: " + listaDataDiferente.Rows.Count.ToString();
            #endregion
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
