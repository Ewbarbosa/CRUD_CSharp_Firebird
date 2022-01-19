using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_Firebird
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            preencheGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Salvar?", "Confirmação", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Cliente cliente = new Cliente();
                cliente.ID = 0;
                cliente.Nome = txtNome.Text;
                cliente.Endereco = txtEndereco.Text;
                cliente.Telefone = txtTelefone.Text;
                cliente.Email = txtEmail.Text;

                try
                {
                    AcessoFB.fb_InserirDados(cliente);
                    preencheGrid();
                    MessageBox.Show("Dados salvos com sucesso!", "Inserir", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK);
                }
            }
            else if (dr == DialogResult.No)
            {
                // 
            }            
        }

        private void preencheGrid()
        {      
            try
            {
                dgvCientes.DataSource = AcessoFB.fbGetDados().DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK);
            }
        }

        private void preencheDados(Cliente cliente)
        {
            txtId.Text = cliente.ID.ToString();
            txtNome.Text = cliente.Nome;
            txtEndereco.Text = cliente.Endereco;
            txtTelefone.Text = cliente.Telefone;
            txtEmail.Text = cliente.Email;
        }

        private void dgvCientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // a linha abaixo seleciona o ID atravez do [e.RowIndex].Cells[0].Value            
            int codigo = 0;
            if (codigo != 0)
            {
                codigo = Convert.ToInt32(dgvCientes.Rows[e.RowIndex].Cells[0].Value);
                Cliente cliente = new Cliente();

                try
                {
                    cliente = AcessoFB.fb_ProcurarDados(codigo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK);
                }
                preencheDados(cliente);
            }            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int codigo = Convert.ToInt32(txtId.Text);
            Cliente cliente = new Cliente();
            try
            {
                cliente = AcessoFB.fb_ProcurarDados(codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK);
            }
            preencheDados(cliente);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btNovo.Enabled = true;
            foreach (Control control in groupBox.Controls)
            {
                if (control as TextBox == null)
                {

                }
                else
                {
                    ((TextBox)control).Text = "";
                }
            }
            txtId.Focus();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // se precionar a tecla ENTER o foco vai para o proximo campo
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtNome.Focus();
            }
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            // se precionar a tecla ENTER o foco vai para o proximo campo
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEndereco.Focus();
            }
        }

        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            // se precionar a tecla ENTER o foco vai para o proximo campo
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTelefone.Focus();
            }
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // se precionar a tecla ENTER o foco vai para o proximo campo
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // se precionar a tecla ENTER o foco vai para o proximo campo
            if (e.KeyChar == (char)Keys.Enter)
            {
                btNovo.Focus();
            }
        }
    }
}
