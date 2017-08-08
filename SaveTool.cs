using System;
using System.Security.Cryptography;
using System.Text;

namespace CTR
{
    public class SaveTool
    {
        private readonly AesEngine engine;
        public SaveTool(AesEngine e) => engine = e;

        private void SetCTR(string message)
        {
            byte[] arrbBasePath = Encoding.Unicode.GetBytes(message);
            byte[] terminator = { 0, 0 };

            SHA256 sha = SHA256.Create();
            sha.TransformBlock(arrbBasePath, 0, arrbBasePath.Length, null, 0);
            sha.TransformFinalBlock(terminator, 0, terminator.Length);
            byte[] hash = sha.Hash;

            byte[] ctr = new byte[16];
            for (int i = 0; i < ctr.Length; ++i)
                ctr[i] = (byte)(hash[i] ^ hash[i + 16]);
            engine.SetCTR(ctr);

            Console.WriteLine("Save decryption CTR: {0}", ctr.ToHexString());
        }
        private static void LeftShiftAes128Key(ref byte[] K)
        {
            // left shift by one
            for (int i = 0; i < 15; ++i)
                K[i] = (byte)((K[i] << 1) | (K[i + 1] >> 7));
            K[15] = (byte)(K[15] << 1);
        }
        private byte[] SignHash(ref byte[] hash)
        {
            engine.SelectKeyslot(0x30);
            engine.SetMode(AesMode.ECB);

            // Naive fixed-length input (32 bytes) CMAC implementation, adapted from OpenBSD
            byte[] block = new byte[16];

            // Normally, we would have this do blockwise processing until the final block here,
            // but the first block is the penultimate block already.
            {
                for (int j = 0; j < 16; ++j)
                    block[j] ^= hash[j];
                block = engine.Encrypt(block);
            }

            byte[] subkey = engine.Encrypt(new byte[16]);

            bool carry = (subkey[0] & 0x80) != 0;
            LeftShiftAes128Key(ref subkey);
            if (carry)
                subkey[15] ^= 0x87;

            for (int i = 0; i < 16; ++i)
            {
                hash[i + 16] ^= subkey[i];
                block[i] ^= (byte)(hash[i + 16] ^ subkey[i]);
            }

            return engine.Encrypt(block);
        }

        public byte[] GetCMAC(byte[] dec, bool isCart, ulong SaveID)
        {
            SHA256 sha = SHA256.Create();
            sha.TransformBlock(Encoding.ASCII.GetBytes("CTR-SAV0"), 0, 8, null, 0);
            // DISA table @ 0x100, size 0x100
            sha.TransformFinalBlock(dec, 0x100, 0x100);
            byte[] headerHash = sha.Hash;

            sha = SHA256.Create();
            sha.TransformBlock(Encoding.ASCII.GetBytes(isCart ? "CTR-NOR0" : "CTR-SIGN"), 0, 8, null, 0);
            // Save ID; only for SD saves
            if (!isCart)
                sha.TransformBlock(BitConverter.GetBytes(SaveID), 0, 8, null, 0);
            sha.TransformFinalBlock(headerHash, 0, headerHash.Length);

            byte[] totalHash = sha.Hash;
            return SignHash(ref totalHash);
        }
        public byte[] DecryptDigitalSave(byte[] encryptedData, string szBasePath)
        {
            engine.SelectKeyslot(0x34);
            engine.SetMode(AesMode.CTR);

            SetCTR(szBasePath);
            return engine.Decrypt(encryptedData);
        }
    }
}