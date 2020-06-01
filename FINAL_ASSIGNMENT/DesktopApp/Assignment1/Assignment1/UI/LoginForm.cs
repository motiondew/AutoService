using Assignment1.BL;
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
    public partial class LoginForm : Form
    {
        UserService userService;

        public LoginForm()
        {
            InitializeComponent();
            userService = new UserService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool loginResponse = userService.login(textBox1.Text, textBox2.Text);

            if(loginResponse == true)
            {
                AppointmentForm appointmentForm = new AppointmentForm();
                appointmentForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Couldn't login, please enter a valid email or password!");
            }
        }

     
    }
}
