﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace ESNAJ
{
    /// <summary>
    /// Lógica de interacción para nuevoJug.xaml
    /// </summary>
    public partial class nuevoJug : Window
    {
        SqlConnection con;
        SqlCommand cmm;

        Menu ventanaAnt;

        public nuevoJug(Menu ventanaMenu)
        {
            ventanaAnt = ventanaMenu;

            InitializeComponent();
            cbGrados.Items.Add("Pachón");
            cbGrados.Items.Add("Peonina");
            cbGrados.Items.Add("Bonifacio");
            cbGrados.Items.Add("Bonfil");
            cbGrados.Items.Add("Pedro");
            cbGrados.Items.Add("Anabella");
            cbGrados.Items.Add("Rey ESNAJ");
            cbGrados.SelectedIndex = 0;
            
            con = MainWindow.conectarBase();
            String query = "SELECT nombre FROM escuela";
            cmm = new SqlCommand(query, con);
            SqlDataReader lector = cmm.ExecuteReader();
            lector.Read();
            int i = 0;
            while (lector.Read())
            {
                cbEscuelas.Items.Add(lector.GetValue(i));
                i++;
            }
            cbEscuelas.SelectedIndex = 0;
            lector.Close();
        }

        private void btRegresar_Click(object sender, RoutedEventArgs e)
        {
            ventanaAnt.Show();
            this.Close();
        }

        private void btAgregar_Click(object sender, RoutedEventArgs e)
        {
            con = MainWindow.conectarBase();
            int id = ManejadorAlumnoN.nuevoId();
            
            Jugador j = new Jugador(id, tbNombre.Text, tbCorreo.Text, tbContraseña.Password, 0, int.Parse(cbEscuelas.Text), cbGrados.Text);
            String query = "INSERT INTO alumno VALUES ('" + j.id + "', '" + j.nombre + "','" + j.correo + "','" + j.contra + "','" + j.puntos + "','" +  j.escuela + "','" + j.categoria + ")";
            cmm = new SqlCommand(query,con);
            bool resp;
            if(cmm.ExecuteNonQuery() > 0)
                resp = true;
            if(resp)
                MessageBox.Show("Alta exitosa");
            else
                MessageBox.Show("No se dió de alta");
            con.Close();

        }
    }
}
