﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace touchIMAGE.Graphs.Panels.Navigation
{
    public partial class PNL_filtreModify_format : UserControl
    {
        public PNL_filtreModify_format()
        {
            InitializeComponent();
        }

        private List<string> formats;

        private void txt_Value_TextChanged(object sender, EventArgs e)
        {
            cmd_Add.Enabled = (lst_NotSelected.Items.Count != 0 ? true : false);
        }

        private void cmd_Add_Click(object sender, EventArgs e)
        {
            AddItems();
        }

        private void PNL_FiltreModify_Load(object sender, EventArgs e)
        {
            cmd_Del.Enabled = (lst_Selected.Items.Count != 0 ? true : false);
            FillList();
        }

        private void cmd_Del_Click(object sender, EventArgs e)
        {
            RemoveItems();           
        }

        private void lst_Selected_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Enable disable button delete is the list is empty
            cmd_Del.Enabled = (lst_Selected.Items.Count != 0 ? true : false);
        }

        /// <summary>
        /// Syncronise the list width the main Program Data
        /// </summary>

        private void FillList()
        {
            foreach (string f in ProgramData.filter_Format)
            {
                lst_Selected.Items.Add(f.Substring(2));
            }
        }

        /// <summary>
        /// Load all data in the Main Program Data
        /// </summary>

        public void LoadData()
        {
            formats = new List<string>();

            foreach (string f in lst_Selected.Items)
                formats.Add("*." + f);

            ProgramData.filter_Format =  formats.ToArray();
            Program.MainForm.RefreshImageList();
        }

        private void txt_Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)(Keys.Enter)) && (cmd_Add.Enabled))
            {
                AddItems();
            }
        }

        private void lst_Selected_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) && (cmd_Del.Enabled))
                RemoveItems();
        }

        private void RefreshViewImages()
        {
            LoadData();
        }

        private void AddItems()
        {
            lst_Selected.Items.Add(lst_NotSelected.SelectedItem);
            lst_NotSelected.Items.Remove(lst_NotSelected.SelectedItem);
            RefreshViewImages();
        }

        private void RemoveItems()
        {
            lst_NotSelected.Items.Add(lst_Selected.SelectedItem);
            lst_Selected.Items.Remove(lst_Selected.SelectedItem);
            //Verify if the list is Empty
            //if (lst_Selected.Items.Count == 0)
              //  FillList();
            RefreshViewImages();
        }

        private void lst_NotSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd_Add.Enabled = (lst_NotSelected.Items.Count != 0 ? true : false);
        }
    }
}

