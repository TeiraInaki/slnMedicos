using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Datos;
using Entidades;

namespace WebApp
{
    public partial class VistaMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            MostrarMedicos();
            LlenarCombo();
        }

        private void MostrarMedicos()
        {
            gridMedico.DataSource = AdminMedico.Listar();
            gridMedico.DataBind();
        }

        private void LlenarCombo()
        {
            DataTable dt = AdminEspecialidad.Listar();
            dlEspecialidad.DataSource = dt;
            dlEspecialidad.DataTextField = dt.Columns["Nombre"].ToString();
            dlEspecialidad.DataValueField = dt.Columns["Id"].ToString();
            dlEspecialidad.DataBind();            
        }

        protected void dlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Medico medico = new Medico()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                NroMatricula = Convert.ToInt32(txtNroMatricula.Text),
                EspecialidadId = Convert.ToInt32(dlEspecialidad.SelectedValue)
            };

            int filasAfectadas = AdminMedico.Insertar(medico);

            if (filasAfectadas > 0)
            {
                Refresh();
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Medico medico = new Medico()
            {
                Id = Convert.ToInt32(txtId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                NroMatricula = Convert.ToInt32(txtNroMatricula.Text),
                EspecialidadId = Convert.ToInt32(dlEspecialidad.SelectedValue)
            };

            int filasAfectadas = AdminMedico.Modificar(medico);

            if (filasAfectadas > 0)
            {
                Refresh();
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int filasAfectadas = AdminMedico.Eliminar((Convert.ToInt32(txtId.Text)));

            if (filasAfectadas > 0)
            {
                Refresh();
            }
        }
    }
}