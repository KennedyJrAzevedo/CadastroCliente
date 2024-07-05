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

        private void resetacampos()
        {
            txtID.ResetText();
            txtNome.ResetText();
            txtEmail.ResetText();
            dataGridView1.ClearSelection();

            txtID.Visible = false;
            txtNome.Enabled = true;
            txtEmail.Enabled = true;

            btnIncluir.Text = "Salvar";
            btnIncluir.Enabled = true;

            btnExcluir.Enabled = false;
            btnReset.Visible = false;

            checkBoxAlterar.Visible = false;
            checkBoxAlterar.Checked = false;

            txtNome.Focus();

        }

        private bool verifica_campos()
        {
            bool campoPreenchido = false;
            if (!(txtNome.Text.Equals("")))
            {
                if (!(txtEmail.Text.Equals("")))
                {
                    if (txtEmail.Text.Contains("@"))
                    {

                        campoPreenchido = true;
                        return campoPreenchido;
                    }
                    else
                    {
                        MessageBox.Show("Email fornecido não é válido.", "Formulário", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return campoPreenchido;
                    }
                }
                else
                {
                    txtEmail.Focus();
                }
            }
            else
            {
                txtNome.Focus();
            }
            MessageBox.Show("Campo Vazio!", "Formulário", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }
        public void carregartabela()
        {
            dataGridView1.DataSource = bll.selecionar();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (verifica_campos())
            {
                if (btnIncluir.Text.Equals("Salvar"))
                {
                    dto.Nome = txtNome.Text;
                    dto.Email = txtEmail.Text;
                    if (bll.inserir(dto))
                    {
                        carregartabela();
                        
                        MessageBox.Show("Cadastro realizado com sucesso.");
                    };

                }
                else
                {
                    dto.Id = Convert.ToInt32(txtID.Text);
                    dto.Nome = txtNome.Text;
                    dto.Email = txtEmail.Text;
                    if (bll.alterar(dto))
                    {
                        checkBoxAlterar.Checked = false;
                        carregartabela();
                        MessageBox.Show("Alteração realizada.", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                resetacampos();
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            DialogResult confirmar = MessageBox.Show("Você quer realmente apagar esse dado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2); ;
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

                txtID.Visible = true;
                txtNome.Enabled = false;
                txtEmail.Enabled = false;
                
                checkBoxAlterar.Visible = true;
                btnReset.Visible = true;

                btnIncluir.Text = "Alterar";
                btnIncluir.Enabled = false;
                btnExcluir.Enabled = true;
            }
            else
            {
                resetacampos();
            }


        }

        private void checkBoxAlterar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlterar.Checked == true)
            {
                txtNome.Enabled = true;
                txtEmail.Enabled = true;
                btnExcluir.Enabled = false;
                btnIncluir.Enabled = true;
            }
            else
            {
                txtNome.Enabled = false;
                txtEmail.Enabled = false;
                btnExcluir.Enabled = true;
                btnIncluir.Enabled = false;
            }
        }

        private void txtPesquisarID_TextChanged(object sender, EventArgs e)
        {
            btnReset.Visible = true;
            dataGridView1.DataSource = bll.pesquisa_cliente(txtPesquisarID.Text);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetacampos();
        }
    }
}
