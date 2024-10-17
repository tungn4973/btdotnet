using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        String email_test = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        String password_test = @"^(?=.*[!@#$%^&*(),.?""{}|<>])[A-Za-z\d!@#$%^&*(),.?""{}|<>]{8,}$";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tb_email.Text, email_test))
            {
                MessageBox.Show("Email không hợp lệ");
                return;
            }
            if (tb_password.Text.Length < 8 || tb_password.Text.Length > 24)
            {
                MessageBox.Show("Mật khẩu phải chứa ít nhất 8 - 24 ký tự");
                return;
            } 
            if (!Regex.IsMatch(tb_password.Text, password_test))
            {
                MessageBox.Show("Mật khẩu không hợp lệ. Yêu cầu tối thiểu 8 ký tự và phải bao gồm ít nhất 1 ký tự đặc biệt.");
                return;
            }
            MessageBox.Show("Đăng ký thành công");
        }
    }
}
