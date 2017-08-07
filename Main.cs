using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CTR;

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

        private void ReadDecrypted(string path)
        {
            var data = File.ReadAllBytes(path);
            var sav = new SaveContainer(data);
        }

        private static void ApplyXORPad(byte[] dest, byte[] xorpad)
        {
            if (dest.Length != xorpad.Length)
                throw new ArgumentException($"{nameof(dest)} length ({dest.Length}) != {nameof(xorpad)} length ({xorpad.Length})");

            for (int i = 0; i < dest.Length; i++)
                dest[i] ^= xorpad[i];
        }
    }
}
