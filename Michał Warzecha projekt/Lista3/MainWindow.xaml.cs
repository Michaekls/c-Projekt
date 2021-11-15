using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Data.SqlTypes;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;



namespace Lista3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Person> PersonList = new List<Person>();
       
        public int odswierz=0;
        public MainWindow()
        {
            InitializeComponent();
        }

    public class Person
    {
        public string Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Pesel { get; set; }
        public string Miasto { get; set; }
        public string Wiek { get; set; }
        public string Obraz { get; set; }

    }

        private void ZapiszBaza(object oSender, EventArgs eRoutedEventArgs)
        {

            //string connetionString;
            //SqlConnection cnn;
            //connetionString = @"Server=localhost\MWMW;Database=Praco;Trusted_Connection=True";
            //cnn = new SqlConnection(connetionString);
            //SqlCommand command;
            //SqlDataReader dataReader;
            //cnn.Open();
            //command = new SqlCommand("DodajOsobe1", cnn);
  
            //command.CommandType = CommandType.StoredProcedure;
            ////command.CommandType = CommandType.Text;
            //command.Parameters.Add("@ID", SqlDbType.VarChar).Value = "32";
            //command.Parameters.Add("@Imie", SqlDbType.VarChar).Value = "Mi";
            //command.Parameters.Add("@Nazwisko", SqlDbType.VarChar).Value = "Wa";
            //command.Parameters.Add("@PESEL", SqlDbType.VarChar).Value = "1234567912";
            //command.Parameters.Add("@Miasto", SqlDbType.VarChar).Value = "da";
            //command.Parameters.Add("@Wiek", SqlDbType.VarChar).Value = "22";

            //dataReader = command.ExecuteReader();
            //dataReader.Close();
            ////command.ExecuteNonQuery();
            //command.Dispose();

            //cnn.Close();
            //MessageBox.Show("dodano osobe");
        }

        private void WczytajB(object oSender, EventArgs eRoutedEventArgs)
        {

        string connetionString;
            SqlConnection cnn;
            connetionString = @"Server=localhost\MWMW;Database=Praco;Trusted_Connection=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, imie="" ,nazwisko="",pesel="",wiek="",miasto="",id="";

            sql = "SELECT * FROM Student1";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            MainWindow.PersonList.Clear();
            while (dataReader.Read())
            {
                id = Convert.ToString(dataReader.GetValue(0));
                imie = Convert.ToString(dataReader.GetValue(1));
                nazwisko= Convert.ToString(dataReader.GetValue(2));
                pesel= Convert.ToString(dataReader.GetValue(3));
                miasto = Convert.ToString(dataReader.GetValue(4));
                wiek = Convert.ToString(dataReader.GetValue(5));
                MainWindow.PersonList.Add(new MainWindow.Person() { Id = id, Imie = imie, Nazwisko = nazwisko, Pesel = pesel, Miasto = miasto, Wiek = wiek});


                //Output = Output + dataReader.GetValue(0) + " " + dataReader.GetValue(1) + " " + dataReader.GetValue(2) + " " + dataReader.GetValue(3);
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();
            Listax.ItemsSource = null;
            Listax.ItemsSource = PersonList;
        }

        private void srWiek(object oSender, EventArgs eRoutedEventArgs)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Server=localhost\MWMW;Database=Praco;Trusted_Connection=True";
            cnn = new SqlConnection(connetionString);
            SqlCommand command;
            SqlDataReader dataReader;
            cnn.Open();

            command = new SqlCommand("SredniWiek", cnn);
            
            //dataReader = command.ExecuteReader();
            //dataReader.Read();

            MessageBox.Show("średni wiek " + command.ExecuteScalar().ToString());
            command.Dispose();

            cnn.Close();
        }


        public void Dodaj(object oSender, EventArgs eRoutedEventArgs) 
        {
            Window1 window1 = new Window1();
            window1.Show();
        }

        public void Widok(object oSender, EventArgs eRoutedEventArgs)
        {
            Widok widok = new Widok();
            widok.Show();
        }

        public void Tabelaryczna(object oSender, EventArgs eRoutedEventArgs)
        {
            Tabelaryczna tabelaryczna = new Tabelaryczna();
            tabelaryczna.Show();
        }

        public void Zmien(object oSender, EventArgs eRoutedEventArgs)
        {
            Window2 window2 = new Window2();
            window2.Show();
        }
        private void Zapisz(object oSender, RoutedEventArgs eRoutedEventArgs) 
        {

        XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
        using (Stream s = File.Create("Osoby.xml"))
            {
                serializer.Serialize(s, PersonList);
                s.Close();
            }
    }


    public void Pokazuj(object sender, EventArgs e)
    {
         
            Listax.ItemsSource = null;
            Listax.ItemsSource = PersonList;
    }


        private void Zaladuj(object sender, EventArgs e)
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            using(var stream = File.OpenRead("Osoby.xml"))
            {
                PersonList= (List<Person>)serializer.Deserialize(stream);
            }
            Listax.ItemsSource = null;
            Listax.ItemsSource = PersonList;


        }

    }
}

