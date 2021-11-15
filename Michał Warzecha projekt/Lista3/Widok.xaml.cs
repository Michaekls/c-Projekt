using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Lista3
{


    /// <summary>
    /// Logika interakcji dla klasy Widok.xaml
    /// </summary>
    public partial class Widok : Window
    {

        public static List<Person1> Widoki = new List<Person1>();
        public Widok()
        {
            InitializeComponent();

            string connetionString;
            SqlConnection cnn;
            connetionString = @"Server=localhost\MWMW;Database=Praco;Trusted_Connection=True";
            String sql, imie = "", nazwisko = "", pesel = "", wiek = "", miasto = "", id = "";
            cnn = new SqlConnection(connetionString);
            SqlCommand command;
            SqlDataReader dataReader;
            cnn.Open();

            sql = "SELECT * FROM PokazOsoby3";
            command = new SqlCommand(sql , cnn);

           // command.CommandType = CommandType.StoredProcedure;
            //command.CommandType = CommandType.Text;
            

            dataReader = command.ExecuteReader();
            //command.Parameters.Add("@Wiek", SqlDbType.Int).Value = (Convert.ToInt32(Wiek.Text));

            while (dataReader.Read())
            {
                id = Convert.ToString(dataReader.GetValue(0));
                imie = Convert.ToString(dataReader.GetValue(1));
                nazwisko = Convert.ToString(dataReader.GetValue(2));
                pesel = Convert.ToString(dataReader.GetValue(3));
                miasto = Convert.ToString(dataReader.GetValue(4));
                
                Widoki.Add(new Person1() { Id = id, Imie = imie, Nazwisko = nazwisko, Pesel = pesel, Miasto = miasto});

                //Output = Output + dataReader.GetValue(0) + " " + dataReader.GetValue(1) + " " + dataReader.GetValue(2) + " " + dataReader.GetValue(3);
            }

            //MessageBox.Show(Widoki.ToString());
            Listax2.ItemsSource = Widoki;
            dataReader.Close();
            command.Dispose();

            cnn.Close();
            MessageBox.Show("widok");
        }

        public class Person1
        {
            public string Id { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Pesel { get; set; }
            public string Miasto { get; set; }
            public string Wiek { get; set; }

        }

            
    }
}
