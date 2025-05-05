using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace LoginPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        LINQConnectionDataContext db = new LINQConnectionDataContext(Properties.Settings.Default.LINQloginConnectionString);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string enteredUserId = user_txt.Text.Trim();
                string enteredPassword = pass_txt.Text.Trim(); // if you use TextBox for password

                if (string.IsNullOrEmpty(enteredUserId) || string.IsNullOrEmpty(enteredPassword))
                {
                    MessageBox.Show("Please enter both User ID and Password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Search user in the users_Tables
                var user = db.users_Tables
                             .Where(u => u.UserID == enteredUserId && u.UserPass == enteredPassword)
                             .FirstOrDefault();

                if (user != null)
                {
                    MessageBox.Show($"Welcome {user.UserName}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);


                    if (user.UserRoles == "admin")
                    {
                       AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();   
                    }
                    else if (user.UserRoles == "student")
                    {
                        
                        StudentWindow studentWindow = new StudentWindow();
                        studentWindow.Show();
                        this.Close();


                    }

                   
                }
                else
                {
                    MessageBox.Show("Invalid User ID or Password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }







        //private void login_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    string userId = user_txt.Text.Trim();
        //    string password = pass_txt.Text.Trim();

        //    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
        //    {
        //        MessageBox.Show("Please enter both User ID and Password.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    string storedPassword = getPassword(userId);

        //    if (storedPassword == null)
        //    {
        //        MessageBox.Show("User not Found", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    else if (passComparison(password, storedPassword))
        //    {
        //        MessageBox.Show("Login successful", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid User ID or Password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private string getPassword(string userId)
        //{
        //    var user = (from u in db.users_Tables
        //                where u.UserID == userId
        //                select u).FirstOrDefault();

        //    if (user != null)
        //    {
        //        return user.UserPass;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //private bool passComparison(string inputPass, string storedPass)
        //{
        //    return inputPass == storedPass;
        //}



    }

}
