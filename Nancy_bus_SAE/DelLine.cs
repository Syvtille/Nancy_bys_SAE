﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryDB;

namespace Nancy_bus_SAE
{
    public partial class DelLine : Form
    {
        public DelLine()
        {
            InitializeComponent();
            List<string> fill = new List<string>();
            fill = BD.getNomLigne();
            foreach (string s in fill)
            {
                cboLine.Items.Add("Ligne " + s);
            }
            cmdDel.Enabled = false;
        }

        private void cmdQuit_Click(object sender, EventArgs e)
        {
            //retourne à l'acceuil sans sauvegarder
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Home);
            if (formToShow != null)
            {
                formToShow.Show();
            }
            this.Close();
        }
        //suppirme la ligne de la BD et retourne à l'acceuil
        private void cmdDel_Click(object sender, EventArgs e)
        {
            BD.delLine(BD.getNumLigne(cboLine.Text.Substring(6)));
            //unhide Home
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is Home);
            if (formToShow != null)
            {
                formToShow.Show();
            }
            this.Close();
        }

        //quand une ligne est sélectionnée permet la suppression
        private void cboLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdDel.Enabled = true;
        }
    }
}
