using System;
using System.IO;

namespace CTR
{
    public static class AesUtil
    {
        public static AesEngine GetEngine(string pathOTP, string pathBootROM, string pathMovableSed)
        {
            var engine = new AesEngine();

            var boot = File.ReadAllBytes(pathBootROM);
            var otp = File.ReadAllBytes(pathOTP);
            engine.LoadKeysFromBootromFile(boot);
            engine.SetOTP(otp);

            FileStream fs = File.Open(pathMovableSed, FileMode.Open);
            byte[] movableKey = new byte[0x10];
            fs.Seek(0x110, SeekOrigin.Begin);
            fs.Read(movableKey, 0, 0x10);
            fs.Close();

            Console.WriteLine("Movable key: {0}", movableKey.ToHexString());
            engine.SetKeyY(0x30, movableKey);
            engine.SetKeyY(0x34, movableKey);

            return engine;
        }
    }
}
