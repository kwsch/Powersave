using System;
using System.IO;
using System.Windows.Forms;

namespace Powersaves
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private PowersaveBackup bak;
        private void SaveBackup(string path) => File.WriteAllBytes(path, bak.Write());
        private void LoadBackup(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            if (!PowersaveBackup.IsValid(data.Length))
            {
                MessageBox.Show($"Size is not valid: {data.Length:X5}");
                return;
            }

            TB_Path.Text = path;
            bak = new PowersaveBackup(data);
            L_Edited.Text = $"Edited: {bak.Edited}" + Environment.NewLine + bak.BackupName;
            L_Edited.Visible = B_Reset.Enabled = B_Export.Enabled = true;
        }

        private void OpenBrowse(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                LoadBackup(openFileDialog1.FileName);
        }
        private void ExportBackup(object sender, EventArgs e)
        {
            var savename = Path.GetFileNameWithoutExtension(TB_Path.Text);
            saveFileDialog1.FileName = savename + " - [Fixed].bin";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                SaveBackup(saveFileDialog1.FileName);
        }
        private void ResetBackup(object sender, EventArgs e)
        {
            // FF the entire cart savedata
            bak.Reset();
        }
    }
}
