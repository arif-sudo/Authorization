using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authorization
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.loginField.AutoSize = false;
            this.usernameField.AutoSize = false;
            this.loginField.Size = new Size(this.loginField.Width, this.pictureBox1.Height);
            this.usernameField.Size = new Size(this.usernameField.Width, this.pictureBox1.Height);

            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Width, this.pictureBox2.Height);
            this.userSurnameField.AutoSize = false;
            this.userSurnameField.Size = new Size(this.userSurnameField.Width, this.pictureBox2.Height);

            this.usernameField.Text = "Enter your name";
            this.usernameField.ForeColor = Color.Gray;

            this.userSurnameField.Text = "Enter your surname";
            this.userSurnameField.ForeColor = Color.Gray;

            this.loginField.Text = "Enter your login";
            this.loginField.ForeColor = Color.Gray;

            this.passField.UseSystemPasswordChar = false;
            this.passField.Text = "Enter your password";
            this.passField.ForeColor = Color.Gray;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);    
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

       

        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.close.ForeColor = Color.Red;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.close.ForeColor= Color.White;
        }

        private void usernameField_Enter(object sender, EventArgs e)
        {
            if (usernameField.Text == "Enter your name")
            {
                usernameField.Text = "";
                usernameField.ForeColor = Color.Black;
            }
        }

        private void usernameField_Leave(object sender, EventArgs e)
        {
            if (usernameField.Text == "")
            {
                usernameField.Text = "Enter your name";
                usernameField.ForeColor = Color.Gray;
            }
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Enter your surname")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }
        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.Text = "Enter your surname";
                userSurnameField.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Enter your login")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Enter your login";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Enter your password")
            {
                passField.UseSystemPasswordChar = true;
                passField.Text = "";
                passField.ForeColor = Color.Black;
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                passField.UseSystemPasswordChar = false;
                passField.Text = "Enter your password";
                passField.ForeColor = Color.Gray;
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (usernameField.Text == "Enter your name")
            {
                MessageBox.Show("Enter your name");
                return;
            }
            if (userSurnameField.Text == "Enter your suname")
            {
                MessageBox.Show("Enter your surname");
                return;
            }
            if (loginField.Text == "Enter your login")
            {
                MessageBox.Show("Enter your login");
                return;
            }
            if (passField.Text == "Enter your password")
            {
                MessageBox.Show("Enter your password");
                return;
            }

            if (isUserExists())
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
                return;
            }
            
            Database db = new Database();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `pass`, `name`, `surname`) VALUES (NULL,@login, @pass, @name, @surname);", db.getConnection());
            cmd.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;   
            cmd.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            cmd.Parameters.Add("@name", MySqlDbType.VarString).Value = usernameField.Text;
            cmd.Parameters.Add("@surname", MySqlDbType.VarString).Value = userSurnameField.Text;

            db.openConnect();
            if (cmd.ExecuteNonQuery() == 1)
            {
                //MessageBox.Show("Account has been created");
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();
            }else
            {
                MessageBox.Show("Account was not craeted");
            }
            db.closeConnect();

        }
        public bool isUserExists()
        {
            Database db = new Database();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users`  WHERE `login` = @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                //MessageBox.Show("User login already exists");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }
    }
}
