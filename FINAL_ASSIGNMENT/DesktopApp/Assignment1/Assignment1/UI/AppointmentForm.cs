using Assignment1.BL;
using assignment2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment1.UI
{
    public partial class AppointmentForm : Form
    {
        AppointmentService appointmentService;
        DataTable appointmetsTable;

        public AppointmentForm()
        {
            InitializeComponent();
            appointmentService = new AppointmentService();
            appointmetsTable = new DataTable();

            
            appointmetsTable.Columns.Add("BsonId");
            appointmetsTable.Columns.Add("Date");
            appointmetsTable.Columns.Add("Client name");
            appointmetsTable.Columns.Add("Telephone");
            appointmetsTable.Columns.Add("Car Brand");
            appointmetsTable.Columns.Add("Description");
            appointmetsTable.Columns.Add("Status");

            ConvertToDatatable(appointmentService.Get());
            dataGridView1.DataSource = appointmetsTable;
        }

        public void ConvertToDatatable(List<Appointment> appointmentList)
        {
            foreach (var appointment in appointmentList)
            {
                var row = appointmetsTable.NewRow();

                row["BsonId"] = appointment.BsonID;
                row["Date"] = appointment.date.ToString();
                row["Client name"] = appointment.clientName;
                row["Telephone"] = appointment.telephoneNo;
                row["Car Brand"] = appointment.carBrand;
                row["Description"] = appointment.description;
                row["Status"] = appointment.status;

                appointmetsTable.Rows.Add(row);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["BsonId"].FormattedValue.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Date"].FormattedValue.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Client name"].FormattedValue.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Telephone"].FormattedValue.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["Car Brand"].FormattedValue.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells["Description"].FormattedValue.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells["Status"].FormattedValue.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.BsonID = textBox1.Text;
            appointment.date = DateTime.Parse(textBox2.Text);
            appointment.clientName = textBox3.Text;
            appointment.telephoneNo = textBox4.Text;
            appointment.carBrand = textBox5.Text;
            appointment.description = textBox6.Text;
            appointment.status = int.Parse(textBox7.Text);
            appointmentService.Edit(appointment);
            appointmetsTable.Clear();
            ConvertToDatatable(appointmentService.Get());
            dataGridView1.DataSource = appointmetsTable;
        }

        private void AppointmentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
