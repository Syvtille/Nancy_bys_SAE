﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LibraryDB
{
    public class BD
    {
        private static MySqlConnection cnx;

        public static void initConnexion()
        {            
            String server = "10.1.139.236";
            String login = "c2";
            String passwd = "mdp";
            String BD = "basec2";
            string connexion = $"SERVER={server};DATABASE={BD};UID={login};PASSWORD={passwd};";

            cnx = new MySqlConnection(connexion);

            try
            {
                cnx.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        /// <summary>
        /// sous-programme qui renvoie dans une liste de chaine de caractères le nom de toute les lignes
        /// </summary>
        /// <returns>une liste de chaine de caractéres représentant le nom des lignes de bus</returns>
        public static List<String> getNomLigne() //retourne le nom de toutes les lignes
        {
            MySqlDataReader dr;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "select nomLigne from Ligne";
            dr = cmd.ExecuteReader(); 
            List<String> test = new List<String>();
            while (dr.Read())
            {
                test.Add(Convert.ToString(dr["nomLigne"]));
            }
            dr.Close();
            return test;
        }

        public static String getNomLigne(int id) //retourne le nom de la ligne correspondant au numéro
        {
            MySqlDataReader dr;
            MySqlCommand cmd = new MySqlCommand();
            String res = "null";
            cmd.Connection = cnx;
            cmd.CommandText = $"select nomLigne from Ligne where nLigne={id}";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                res = Convert.ToString(dr["nomLigne"]);
            }
            dr.Close();
            return res;
        }

        public static int getNumLigne(string nomLigne) //retourne le numéro de la ligne dont le nom est nomLigne
        {
            MySqlDataReader dr;
            MySqlCommand cmd = new MySqlCommand();
            int res = -1;
            cmd.Connection = cnx;
            cmd.CommandText = $"select nLigne from Ligne where nomLigne = '{nomLigne}'";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                res = Convert.ToInt32(dr["nLigne"]);
            }
            dr.Close();
            return res;
        }

        public static List<String> getHeureDepLigne(int id)
        {
            MySqlDataReader dr;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = $"select horaireDepart from HoraireDepart where nLigne = {id}";
            dr = cmd.ExecuteReader();
            List<String> test = new List<String>();
            while (dr.Read())
            {
                test.Add(Convert.ToString(dr["horaireDepart"]));
            }
            dr.Close();
            return test;
        }

        public static int getNbLigne()
        {
            int res = -1;
            MySqlDataReader dr;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "select count(*) from Ligne";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                res = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return res;
        }

        public static List<String> getArretInterLigne(int nLine)
        {
            List<String> res = new List<String>();
            MySqlDataReader dr;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = $"select Arret1.nomArret, Arret2.nomArret, intervalleTemps from TempsTrajet, Arret as Arret1, Arret as Arret2, Trajet where TempsTrajet.nArretA = Arret1.nArret and TempsTrajet.nArretB = Arret2.nArret and Trajet.nArretA = Arret1.nArret and Trajet.nArretB = Arret2.nArret and nLigne = {nLine};";
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                res.Add(Convert.ToString(dr["nomArret"]));
            }
            dr.Close();
            return res;
        }

        public static void closeConnexion()
        {
            try
            {
                cnx.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}