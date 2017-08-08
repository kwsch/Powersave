using System;
using System.IO;
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

        /// <remarks>
        /// Expects the following files IN THE CURRENT WORKING DIRECTORY:
        /// - movable.sed (from nand:/private/movable.sed)
        /// - 00000001.sav (from sd:/Nintendo 3DS/&lt;id0&gt;/&lt;id1&gt;/title/00040000/0011c500/data/00000001.sav, i.e. Alpha Sapphire)
        /// - boot9.bin (dumped from boot9strap or the Interwebz)
        /// </remarks>
        public static void Test()
        {
            const string boot9 = "boot9.bin";
            const string otp = "otp.bin";
            const string mov = "movable.sed";
            var engine = AesUtil.GetEngine(boot9, otp, mov);
            var tool = new SaveTool(engine);

            uint dir = 0x00040000;
            uint gameID = 0x0011c500; // alpha sapphire
            ulong SaveID = dir << 32 | gameID;
            var name = "00000001.sav";
            bool isCart = false;

            byte[] encryptedData = File.ReadAllBytes(name);
            string basePath = $"/title/{dir:X8}/{gameID:X8}/data/{name}";
            byte[] dec = tool.DecryptDigitalSave(encryptedData, basePath);

            var MAC = tool.GetCMAC(dec, isCart, SaveID);
            Console.WriteLine(MAC.ToHexString());

            var sav = new SaveContainer(dec);
        }
    }
}
