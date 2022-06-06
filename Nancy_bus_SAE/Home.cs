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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            BD.initConnexion();

            optAddLine.Checked = true;
            optAddStop.Checked = false;
            optDeleteLine.Checked = true;
            optDeleteStop.Checked = false;

        }

        //gère les boutons radios 
        private void optAddStop_Click(object sender, EventArgs e)
        {
            optAddLine.Checked = false;
            optAddStop.Checked = true;
        }
        
        //gère les boutons radios 
        private void optAddLine_Click(object sender, EventArgs e)
        {
            optAddLine.Checked = true;
            optAddStop.Checked = false;
        }

        //gère les boutons radios 
        private void optDeleteLine_Click(object sender, EventArgs e)
        {
            optDeleteLine.Checked = true;
            optDeleteStop.Checked = false;
        }

        //gère les boutons radios 
        private void optDeleteStop_Click(object sender, EventArgs e)
        {
            optDeleteLine.Checked = false;
            optDeleteStop.Checked = true;
        }

        //ouvre le formulaire permettant l'ajout 
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            //hide active form
            this.Hide();
            
            //d'une ligne 
            if (optAddLine.Checked)
            {
                AddLine addLine = new AddLine();
                addLine.ShowDialog();
            }
            //d'un arret
            else if (optAddStop.Checked)
            {
                AddStop addStop = new AddStop();
                addStop.ShowDialog();
            }
        }

        //ouvre le formulaire permettant la suppression
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            //hide active form
            this.Hide();
            
            //d'une ligne
            if (optDeleteLine.Checked)
            {
                DelLine deleteLine = new DelLine();
                deleteLine.ShowDialog();
            }
            //d'un arrêt
            else if (optDeleteStop.Checked)
            {
                DelStop deleteStop = new DelStop();
                deleteStop.ShowDialog();
            }
        }

        //ouvre le formulaire permettant la modification des horaires 
        private void cmdModify_Click(object sender, EventArgs e)
        {
            this.Hide();
            Modify modify = new Modify();
            modify.ShowDialog();
        }

        //appeler le form avec le numéro de ligne correspondant (requête sql)
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            this.Hide();
            string ligne = lstLine.SelectedItem.ToString();
            
            PrintLine print = new PrintLine(BD.getNumLigne(ligne.Substring(6)));
            print.ShowDialog();
        }

        //refresh de la listbox a chaque fois que home apparait
        private void Home_VisibleChanged(object sender, EventArgs e)
        {
            
            List<string> tmp = new List<string>();
            List<string> fill = new List<string>();
            fill = BD.getNomLigne();
            foreach (string d in lstLine.Items)
            {
                tmp.Add(d);
            }
            foreach (string d in tmp) {
                lstLine.Items.Remove(d);
            }
            foreach (string s in fill)
            {
                lstLine.Items.Add("Ligne " + s);
            }
            lstLine.Sorted = true;
            

        }
    }
}
