using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace Lista3
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private string imie;
        private string nazwisko;
        private string pesel;
        private string miasto;
        private string wiek;
        private string obraz;
        private string id;
        private int blad;
        public BitmapImage obrazek { get; set; }

        public Window1()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, EventArgs e) 
        {
            blad = 0;
            imie = Imie.Text;
            nazwisko = Nazwisko.Text;
            pesel = Pesel.Text;
            miasto = Miasto.Text;
            wiek = Wiek.Text;

            Regex rgx1 = new Regex("^[a-zA-Z]+$");

            if ( rgx1.IsMatch(imie) == false)
            {
                blad = 1;
                imiee.Content = "Wpisz same litery";
            }
            else
            {
                imiee.Content = "";
            }

            if (rgx1.IsMatch(nazwisko) == false)
            {
                blad = 1;
                nazwiskoo.Content = "Wpisz same litery";
            }
            else
            {
                nazwiskoo.Content = "";
            }

            if (rgx1.IsMatch(miasto) == false)
            {
                blad = 1;
                miastoo.Content = "Wpisz same litery";
            }
            else 
            {
                miastoo.Content = "";
            }

            Regex rgx = new Regex("^[0-9]+$");

            if (rgx.IsMatch(wiek) == false)
            {
                blad = 1;
                wiekk.Content = "Wpisz same cyfry";
             }
            else 
            {
                wiekk.Content = "";
            }

            if (rgx.IsMatch(pesel) == false)
            {
                blad = 1;
                pesell.Content = "Wpisz same cyfry";
            }
            else if (pesel.Length != 11)
            {
                blad = 1;
                pesell.Content = "Wpisz równo 11 cyfrw Peselu";
            }
            else
            {
                pesell.Content = "";
            }


            id = Convert.ToString(MainWindow.PersonList.Count);
            if (blad == 0)
            {
                MainWindow.PersonList.Add(new MainWindow.Person() { Id = id, Imie = imie, Nazwisko = nazwisko, Pesel = pesel, Miasto = miasto, Wiek = wiek, Obraz = obraz });

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Server=localhost\MWMW;Database=Praco;Trusted_Connection=True";
                cnn = new SqlConnection(connetionString);
                SqlCommand command;
                SqlDataReader dataReader;
                cnn.Open();

                command = new SqlCommand("DodajOsobe2", cnn);

                command.CommandType = CommandType.StoredProcedure;
                //command.CommandType = CommandType.Text;
                command.Parameters.Add("@ID", SqlDbType.VarChar).Value = id.ToString();
                command.Parameters.Add("@Imie", SqlDbType.VarChar).Value = Imie.Text;
                command.Parameters.Add("@Nazwisko", SqlDbType.VarChar).Value = Nazwisko.Text;
                command.Parameters.Add("@PESEL", SqlDbType.VarChar).Value = Pesel.Text;
                command.Parameters.Add("@Miasto", SqlDbType.VarChar).Value = Miasto.Text;
                command.Parameters.Add("@Wiek", SqlDbType.VarChar).Value = Convert.ToInt32(Wiek.Text);

                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();

                cnn.Close();
                MessageBox.Show("dodano osobe");

                Imie.Text = "";
                Nazwisko.Text = "";
                Pesel.Text = "";
                Miasto.Text = "";
                Wiek.Text = "";
                obrazz.Source = null;
                imiee.Content = "";
                nazwiskoo.Content = "";
                miastoo.Content = "";
                pesell.Content = ""; 
                wiekk.Content = "";

            }
        }

        private void Zdjecie(object sender, EventArgs e) 
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg,*.jpg)|*.png;*.jpeg,*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                obrazek = new BitmapImage();
                obrazek.BeginInit();
                Uri fileUri = new Uri(openFileDialog.FileName);
                
                obraz = Convert.ToString(fileUri);

                obrazek.UriSource = new Uri(obraz, UriKind.Absolute);
                obrazek.EndInit();

                obrazz.Source = null;
                obrazz.Source = obrazek;

            }

        }

    }
}
