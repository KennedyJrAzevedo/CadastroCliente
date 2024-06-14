using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto3Camadas.CODE.BLL;
using Projeto3Camadas.CODE.DTO;

namespace Projeto3Camadas
{
    public partial class FrmCadastroCliente : Form
    {
        ClienteBLL bll = new ClienteBLL();
        ClienteDTO dto = new ClienteDTO();
        public FrmCadastroCliente()
        {
            InitializeComponent();
        }

        public void resetacampos()
        {
            txtID.ResetText() ;
            txtNome.ResetText();
            txtEmail.ResetText();
            dataGridView1.ClearSelection();

            labelID.Visible = false;
            txtID.Visible = false;
            checkBoxAlterar.Visible = false;
            btnReset.Visible = false;

            txtNome.Enabled = true;
            txtEmail.Enabled = true;
            btnIncluir.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            checkBoxAlterar.Checked = false;

        }
        public void carregartabela()
        {
            dataGridView1.DataSource = bll.selecionar();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            dto.Nome = txtNome.Text;
            dto.Email = txtEmail.Text;
            if (bll.inserir(dto)) {
                carregartabela();
                resetacampos();
                MessageBox.Show("Cadastro realizado com sucesso.");
            };
            
            
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            dto.Id = Convert.ToInt32(txtID.Text);
            dto.Nome = txtNome.Text;
            dto.Email = txtEmail.Text;
            if (bll.alterar(dto))
            {
                checkBoxAlterar.Checked = false;
                carregartabela();
                MessageBox.Show("Alteração realizada.","Alteração",MessageBoxButtons.OK,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            
            DialogResult confirmar = MessageBox.Show("Você quer realmente apagar esse dado?", "Confirmação", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2); ;
            if (confirmar == DialogResult.Yes)
            {
                dto.Id = Convert.ToInt32(txtID.Text);
                dto.Nome = txtNome.Text;
                dto.Email = txtEmail.Text;
                bll.excluir(dto);
                resetacampos();
                carregartabela();
            }
        }

        private void FrmCadastroCliente_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bll.selecionar();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["id_cliente"].Value.ToString();
                txtNome.Text = row.Cells["nome"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();

                txtNome.Enabled = false;
                txtEmail.Enabled = false;
                btnIncluir.Enabled = false;

                btnExcluir.Enabled = true;

                checkBoxAlterar.Visible = true;
                btnReset.Visible = true;
                labelID.Visible = true;
                txtID.Visible = true;
            }
            else
            {
                resetacampos();
            }


        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            resetacampos();
        }

        private void checkBoxAlterar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlterar.Checked == true)
            {
                btnAlterar.Enabled = true;
                txtNome.Enabled = true;
                txtEmail.Enabled = true;
            }
            else
            {
                btnAlterar.Enabled = false;
                txtNome.Enabled = false;
                txtEmail.Enabled = false;
            }
        }
    }
}
