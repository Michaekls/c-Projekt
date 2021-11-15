using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace Lista3
{
    /// <summary>
    /// Logika interakcji dla klasy Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        private string imie;
        private string nazwisko;
        private string pesel;
        private string miasto;
        private string wiek;
        public string obraz;
        private string id;
        private int blad;
        public BitmapImage obrazek { get; set; }

        private int wybierzosobe;

        public Window2()
        {
            InitializeComponent();
        }

        private void zdjecie(object sender, EventArgs e)
        {
            //myBitmapImage.BeginInit();

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

            private void Button_Click(object sender, EventArgs e)
        {
            blad = 0;
            try
            {
                wybierzosobe = Convert.ToInt32(WybierzOsobe.Text);
            }
            catch
            {
                MessageBox.Show("Nie wybrano osobyb");
            }

            if (Imie.Text != "")
                imie = Imie.Text;
            else
                imie = MainWindow.PersonList[wybierzosobe].Imie;
            if (Nazwisko.Text != "")
                nazwisko = Nazwisko.Text;
            else
                nazwisko = MainWindow.PersonList[wybierzosobe].Nazwisko;
            if (Pesel.Text != "")
                pesel = Pesel.Text;
            else
                pesel = MainWindow.PersonList[wybierzosobe].Pesel;
            if (Miasto.Text != "")
                miasto = Miasto.Text;
            else
                miasto = MainWindow.PersonList[wybierzosobe].Miasto;
            if (Wiek.Text != "")
                wiek = Wiek.Text;
            else
                wiek = MainWindow.PersonList[wybierzosobe].Wiek;
            if (obraz == null)
                obraz= MainWindow.PersonList[wybierzosobe].Obraz;


            Regex rgx1 = new Regex("^[a-zA-Z]+$");

            if (rgx1.IsMatch(imie) == false)
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

            id = MainWindow.PersonList[wybierzosobe].Id;
            if (blad==0)
            {
                MainWindow.PersonList[wybierzosobe] = new MainWindow.Person() {Id = id, Imie = imie, Nazwisko = nazwisko, Pesel = pesel, Miasto = miasto, Wiek = wiek, Obraz = obraz };

                string connetionString;
                SqlConnection cnn;
                connetionString = @"Server=localhost\MWMW;Database=Praco;Trusted_Connection=True";
                cnn = new SqlConnection(connetionString);
                SqlCommand command;
                SqlDataReader dataReader;
                cnn.Open();
                command = new SqlCommand("ZmienOsobe", cnn);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID", SqlDbType.VarChar).Value = id.ToString();
                command.Parameters.Add("@Imie", SqlDbType.VarChar).Value = Imie.Text;
                command.Parameters.Add("@Nazwisko", SqlDbType.VarChar).Value = Nazwisko.Text;
                command.Parameters.Add("@PESEL", SqlDbType.VarChar).Value = Pesel.Text;
                command.Parameters.Add("@Miasto", SqlDbType.VarChar).Value = Miasto.Text;
                command.Parameters.Add("@Wiek", SqlDbType.VarChar).Value = Wiek.Text;

                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();

                cnn.Close();
                MessageBox.Show("Zmienono osobe");

                Imie.Text = "";
                Nazwisko.Text = "";
                Pesel.Text = "";
                Miasto.Text = "";
                Wiek.Text = "";
                WybierzOsobe.Text = "";

                obrazz.Source = null;
                imiee.Content = "";
                nazwiskoo.Content = "";
                miastoo.Content = "";
                pesell.Content = "";
                wiekk.Content = "";
            }


 


        }
    }
}
