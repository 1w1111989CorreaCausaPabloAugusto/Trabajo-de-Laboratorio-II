using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace LABORATORIO
{
    public partial class Cine : Form
    {
        private bool mouseDown;
        private Point lastLocation;
        ConsultasxEnunciados CE = new ConsultasxEnunciados();
        Datos oBD = new Datos(@"Data Source=DESKTOP-B8TK374;Initial Catalog=CINE;Integrated Security=True");
        
        enum consultas
        {
            temporada,
            actores,
            reservas,
            comprobantes,
            genero,
            pagos,
            tickets,
            periodo
        }
        consultas miConsulta;
        public Cine()
        {
            InitializeComponent();
            miConsulta = consultas.actores;
          
        }
        public void habilitarCampos(bool x)
        {
            txtGenero.Enabled = x;
            txtNacActor.Enabled = x;
            txtNacDirector.Enabled = x;
        }
        public void limpiarCampos()
        {
            txtNacActor.Clear();
            txtNacDirector.Clear();
            txtGenero.Clear();
            txtImporteMin.Clear();
        }

        private void btnTemporada_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnTemporada.Height;
            panelLeft.Top = btnTemporada.Top;
            miConsulta = consultas.temporada;
            rchEnunciado.Text = CE.enunciadoTemporada();
            limpiarCampos();
        }

        private void btnActores_Click(object sender, EventArgs e)
        {
            txtNacActor.Enabled = true;
            txtNacDirector.Enabled = true;
            panelLeft.Height = btnActores.Height;
            panelLeft.Top = btnActores.Top;
            miConsulta = consultas.actores;
            rchEnunciado.Text = CE.enunciadoActores();
            txtGenero.Enabled = false;
            limpiarCampos();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnReservas.Height;
            panelLeft.Top = btnReservas.Top;
            miConsulta = consultas.reservas;
            rchEnunciado.Text = CE.enunciadoReservas();
            limpiarCampos();
        }

        private void btnComprobante_Click(object sender, EventArgs e)
        {
            panelLeft.Height = btnComprobante.Height;
            panelLeft.Top = btnComprobante.Top;
            miConsulta = consultas.comprobantes;
            rchEnunciado.Text = CE.enunciadoComprobantes();
            limpiarCampos();
        }

        private void btnGenero_Click(object sender, EventArgs e)
        {
            panelRight.Height = btnGenero.Height;
            panelRight.Top = btnGenero.Top;
            miConsulta = consultas.genero;
            CE.pGen = txtGenero.Text;
            rchEnunciado.Text = CE.enunciadoGeneros();
            txtNacActor.Enabled = false;
            txtNacDirector.Enabled = false;
            txtGenero.Enabled = true;
            txtImporteMin.Enabled = false;
            limpiarCampos();
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            panelRight.Height = btnPagos.Height;
            panelRight.Top = btnPagos.Top;
            miConsulta = consultas.pagos;
            rchEnunciado.Text = CE.enunciadoPagos();
            txtImporteMin.Enabled = true;
        }

        private void btnTickets_Click(object sender, EventArgs e)
        {
            panelRight.Height = btnTickets.Height;
            panelRight.Top = btnTickets.Top;
            miConsulta = consultas.tickets;
            rchEnunciado.Text = CE.enunciadoTickets();
            limpiarCampos();
            txtGenero.Enabled = false;
        }

        private void btnPeriodo_Click(object sender, EventArgs e)
        {
            panelRight.Height = btnPeriodo.Height;
            panelRight.Top = btnPeriodo.Top;
            miConsulta = consultas.periodo;
            rchEnunciado.Text = CE.enunciadoPeriodo();
            limpiarCampos();
        }

        private void Cine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?", "Saliendo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cine_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Cine_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Cine_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (miConsulta == consultas.actores)
            {
                
                CE.pNacDirector = txtNacDirector.Text;
                CE.pNacActor = txtNacActor.Text;

                
                dgvTabla.DataSource = oBD.consultarTabla(CE.consultaActores());
                rchEnunciado.Text = CE.enunciadoActores();
                
            }
            if (miConsulta == consultas.reservas)
            {
                dgvTabla.DataSource = oBD.consultarTabla(CE.consultaReservas());
                
            }
            if(miConsulta == consultas.temporada)
            {
                dgvTabla.DataSource = oBD.consultarTabla(CE.consultaTemporada());
                
            }
            if (miConsulta == consultas.genero)
            {
                CE.pGen = txtGenero.Text;
                dgvTabla.DataSource = oBD.consultarTabla(CE.consultaGeneros());
                rchEnunciado.Text = CE.enunciadoGeneros();

            }
            if (miConsulta == consultas.periodo)
            {
                CE.pFecha1 = dtpFecha1.Value;
                CE.pFecha2 = dtpFecha2.Value;
                if(string.IsNullOrEmpty(txtImporteMin.Text))
                    MessageBox.Show("Debe ingresar un monto minimo", "Monto Minimo", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                else
                    CE.pImporteMin = Convert.ToDouble(txtImporteMin.Text);




                dgvTabla.DataSource = oBD.consultarTabla(CE.consultaPeriodo());
                 rchEnunciado.Text = CE.enunciadoPeriodo();
                
                
            }
            if(miConsulta == consultas.tickets)
            {
                dgvTabla.DataSource = oBD.consultarTabla(CE.consultarTickets());
                
            }

            if(miConsulta == consultas.comprobantes)
            {
                dgvTabla.DataSource = oBD.consultarTabla(CE.consultaComprobantes());
            }

            if(miConsulta == consultas.pagos)
            {
                CE.pImporteMin =Convert.ToDouble (txtImporteMin.Text);
                dgvTabla.DataSource = oBD.consultarTabla(CE.consultaPagos());
            }


        }

        private void Cine_Load(object sender, EventArgs e)
        {
            habilitarCampos(false);
            dtpFecha1.Format = DateTimePickerFormat.Custom;
            dtpFecha1.CustomFormat = "MM-dd-yyy";
            dtpFecha2.Format = DateTimePickerFormat.Custom;
            dtpFecha2.CustomFormat = "MM-dd-yyy";

        }

        private void txtImporteMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch != 8 && ch !=46)
            {
                e.Handled = true;
            }
        }
    }

}
