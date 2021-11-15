using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Lista3
{
    /// <summary>
    /// Logika interakcji dla klasy Tabelaryczna.xaml
    /// </summary>
    public partial class Tabelaryczna : Window
    {
        public static List<Person2> Tabela = new List<Person2>();
        public Tabelaryczna()
        {
            InitializeComponent();


        }

        private void Listax2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public class Person2
        {
            public string Id { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Pesel { get; set; }
            public string Miasto { get; set; }
            public string Wiek { get; set; }

        }

        private void Wiekk(object sender, RoutedEventArgs e)
        {
            Tabela.Clear();
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Server=localhost\MWMW;Database=Praco;Trusted_Connection=True";
            String sql, imie = "", nazwisko = "", pesel = "", wiek = "", miasto = "", id = "";
            cnn = new SqlConnection(connetionString);
            SqlCommand command;
            SqlDataReader dataReader;
            cnn.Open();

            sql = "Tabelaryczna";
            command = new SqlCommand(sql, cnn);

            command.CommandType = CommandType.StoredProcedure;
            //command.CommandType = CommandType.Text;

            command.Parameters.Add("@Wiek", SqlDbType.Int).Value = (Convert.ToInt32(Wiek.Text));
            dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                id = Convert.ToString(dataReader.GetValue(0));
                imie = Convert.ToString(dataReader.GetValue(1));
                nazwisko = Convert.ToString(dataReader.GetValue(2));
                pesel = Convert.ToString(dataReader.GetValue(3));
                miasto = Convert.ToString(dataReader.GetValue(4));
                wiek = Convert.ToString(dataReader.GetValue(5));
                Tabela.Add(new Person2() { Id = id, Imie = imie, Nazwisko = nazwisko, Wiek=wiek, Pesel = pesel, Miasto = miasto });

                //Output = Output + dataReader.GetValue(0) + " " + dataReader.GetValue(1) + " " + dataReader.GetValue(2) + " " + dataReader.GetValue(3);
            }

            //MessageBox.Show(Widoki.ToString());
            Listax3.ItemsSource = null;
            Listax3.ItemsSource = Tabela;
            dataReader.Close();
            command.Dispose();

            cnn.Close();
            MessageBox.Show("Tabelaryczna");
        }
    }
}
