using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Datos;
using Entidades;

namespace WindowsMedicos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            actualizar();
        }

        private void actualizar()
        {
            gridMedicos.DataSource = AdminMedico.Listar();
            llenarCombos();
        }

        private void llenarCombos()
        {
            DataTable Especialidades1 = AdminEspecialidad.Listar();
            DataTable Especialidades2 = AdminEspecialidad.Listar();

            cbEspecialidad.DataSource = Especialidades1;
            cbEspecialidad.DisplayMember = Especialidades1.Columns["Nombre"].ToString();
            cbEspecialidad.ValueMember = Especialidades1.Columns["Id"].ToString();

            cmbTraerPorEspecialidad.DataSource = Especialidades2;
            cmbTraerPorEspecialidad.DisplayMember = Especialidades2.Columns["Nombre"].ToString();
            cmbTraerPorEspecialidad.ValueMember = Especialidades2.Columns["Id"].ToString();

            DataRow filaTotal = Especialidades2.NewRow();
            filaTotal["Id"] = 0;
            filaTotal["Nombre"] = "[TODAS]";
            Especialidades2.Rows.InsertAt(filaTotal, 0);
        }

        private void cmbTraerPorEspecialidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if ((int)cmbTraerPorEspecialidad.SelectedValue == 0) 
            {
                gridMedicos.DataSource = AdminMedico.Listar();
            }
            else gridMedicos.DataSource = AdminMedico.Listar((int)cmbTraerPorEspecialidad.SelectedValue);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Length > 0 && txtApellido.Text.Length > 0 && txtMatricula.Text.Length > 0)
            {
                int filas = AdminMedico.Insertar(new Medico(txtNombre.Text, txtApellido.Text, Convert.ToInt32(txtMatricula.Text), (int)cbEspecialidad.SelectedValue));

                if (filas > 0)
                {
                    MessageBox.Show("Correctamente guardado");
                    actualizar();
                }
            }
            else MessageBox.Show("Faltan Datos");
        }

        private void btnGuardarEspecialidad_Click(object sender, EventArgs e)
        {
            if (txtNuevaEspecialidad.Text.Length > 0)
            {
                int filas = AdminEspecialidad.Crear(txtNuevaEspecialidad.Text);
                if (filas > 0)
                {
                    MessageBox.Show("Correctamente agregado");
                    actualizar();
                }
            }
        }
    }
}
