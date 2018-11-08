﻿using System;
using System.Linq;
using System.Windows.Forms;
using ZiLib;

namespace NextionFontEditor {

    public partial class FormFontEditor : Form {

        public FormFontEditor() {
            InitializeComponent();
        }

        private ZiFont ziFont;

        private void FormFontEditor_Load(object sender, EventArgs e) {
            cmbZoom.Items.AddRange(Enumerable.Range(1, 30).Select(x => $"{x}x").ToArray());
            cmbZoom.SelectedIndex = 9;
        }

        private void charEditor1_Click(object sender, EventArgs e) {
            pPreview.Image = charEditor1.CharImage;
        }

        private void numChar_ValueChanged(object sender, EventArgs e) {
            charEditor1.CharImage = ziFont.CharBitmaps.Skip((int) numChar.Value).First();
            pPreview.Image = charEditor1.CharImage;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void btnOpenFont_Click(object sender, EventArgs e) {
            var res = ofd.ShowDialog();

            if (res == DialogResult.OK) {
                ziFont = ZiFont.FromFile(ofd.FileName);

                numChar.Maximum = ziFont.CodePage.CharacterCount - 1;

                charEditor1.CharImage = ziFont.CharBitmaps.Skip(1).First();
                numChar.Value = 1;
            }
        }

        private void panel1_Resize(object sender, EventArgs e) {
            charEditor1.Left = (panel1.Width / 2) - (charEditor1.Width / 2);
            charEditor1.Top = (panel1.Height / 2) - (charEditor1.Height / 2);
        }

        private void FormFontEditor_Shown(object sender, EventArgs e) {
            panel1_Resize(sender, e);
        }

        private void cmbZoom_TextChanged(object sender, EventArgs e) {
        }

        private void cmbZoom_SelectedIndexChanged(object sender, EventArgs e) {
            charEditor1.Zoom = int.Parse(cmbZoom.Text.Replace("x", ""));
            panel1_Resize(sender, e);
        }

        private void btnShowGrid_Click(object sender, EventArgs e) {
            charEditor1.ShowGrid = btnShowGrid.Checked;
        }
    }
}