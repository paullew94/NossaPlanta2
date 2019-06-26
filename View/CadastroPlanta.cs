using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class CadastroPlanta : Form
    {
        public CadastroPlanta()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(lblID.Text=="")
            {
             Inserir();

            }
            else
            {
                Alterar();
            }
            LimparCampos();
            AtualizarTabela();

        }

        private void Inserir()
        {
            Planta planta = new Planta();
            planta.Nome = txtNome.Text;
            planta.Altura = Convert.ToDecimal(mtbAltura.Text);
            planta.Peso = Convert.ToDecimal(mtbPeso.Text);
            planta.Carnivora = rbSim.Checked;

            PlantaRepositorio repositorio = new PlantaRepositorio();
            repositorio.Inserir(planta);
        }

        private void Alterar()
        {
            Planta planta = new Planta();
            planta.Id = Convert.ToInt32(lblID.Text);
            planta.Nome = txtNome.Text;
            planta.Altura = Convert.ToDecimal(mtbAltura.Text);
            planta.Peso = Convert.ToDecimal(mtbPeso.Text);
            planta.Carnivora = rbSim.Checked;

            PlantaRepositorio repositorio = new PlantaRepositorio();
            repositorio.Alterar(planta);
        }

        private void LimparCampos()
        {
            lblID.Text = "";
            txtNome.Clear();
            mtbAltura.Clear();
            mtbPeso.Clear();
            rbSim.Checked = false;
            rbNao.Checked = false;
        }

        private void CadastroPlanta_Load(object sender, EventArgs e)
        {
            AtualizarTabela();
        }

        private void AtualizarTabela()
        {
            PlantaRepositorio repositorio = new PlantaRepositorio();
            string busca = txtBusca.Text;
            List<Planta> plantas = repositorio.ObterTodos(busca);
            dataGridView1.RowCount = 0;
            for (int i = 0; i < plantas.Count; i++)
            {
                Planta planta = plantas[i];
                dataGridView1.Rows.Add(new object[] { planta.Id, planta.Nome });

            }
        }

        private void txtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                AtualizarTabela();
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            PlantaRepositorio repositorio = new PlantaRepositorio();
            repositorio.Apagar(id);
            AtualizarTabela();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            PlantaRepositorio repositorio = new PlantaRepositorio();

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            Planta planta = repositorio.ObterPeloId(id);
            if (planta!=null)
            {
                txtNome.Text = planta.Nome;
                mtbAltura.Text = planta.Altura.ToString("0.00");
                mtbPeso.Text = planta.Peso.ToString("000.00");
                if (planta.Carnivora==true)
                {
                    rbSim.Checked = true;
                }
                else
                {
                    rbNao.Checked = false;
                }
                lblID.Text = planta.Id.ToString();
            }

        }
    }
}
