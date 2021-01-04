using System;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;

namespace EncryptZipFiles
{
    class FileEncryptDecrypt
    {
        const int bufferLen = 1048576; // 1 MB
        internal static readonly bool hasher;

        // Encryption Function
        public static void Decrypt(string fileIn, string fileOut, string Password)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x76, 0x01, 0xee, 0x1a, 0x13, 0xfe });
            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            CryptoStream cs = new CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write);
            byte[] buffer = new byte[bufferLen];
            int bytesRead;
            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLen);
                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);
            cs.Close(); // this will also close the unrelying fsOut stream
            fsIn.Close();
        }

        public static void FileCryptoHandler1(string inFilePath, string outFilePath, string password)
        {


            string filepath = Path.GetFileNameWithoutExtension(inFilePath);
            string filext = Path.GetExtension(inFilePath);

            // Create Decrypted File Paths

            string decrypted_FilePath = outFilePath + "\\" + filepath + filext;

            try
            {
                Console.WriteLine("Decryption Started");
                Stopwatch time = new Stopwatch();
                time.Start();
                Decrypt(inFilePath, decrypted_FilePath, password);
                time.Stop();
                Console.WriteLine("Decryption Ends. Time Taken : " + time.ElapsedMilliseconds + " ms");
                Console.WriteLine("------------------------------------------------");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Decryption -- Exception: {0}\r\n{1}", ex.Message, ex.StackTrace);
                return;
            }
            Console.WriteLine("------------------------------------------------");
        }
        public static void Encrypt(string fileIn, string fileOut, string Password)
        {
            FileStream fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
            FileStream fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                      new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76, 0x01, 0xee, 0x1a, 0x13, 0xfe });
            ulong bytestotal = 0;
            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);


            CryptoStream cs = new CryptoStream(fsOut, alg.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] buffer = new byte[bufferLen];
            int bytesRead;
            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLen);
                cs.Write(buffer, 0, bytesRead);
                bytestotal += (ulong)bytesRead;
            } while (bytesRead != 0);
            cs.Close();
            fsIn.Close();
        }

        public static void FileCryptoHandler(string inFilePath, string outFilePath, string password)
        {


            string filepath = Path.GetFileNameWithoutExtension(inFilePath);
            string filext = Path.GetExtension(inFilePath);

            // Create Encrypted File Paths

            string encrypted_FilePath = outFilePath + "\\" + filepath + filext;

            try
            {
                Console.WriteLine("Encryption Started");
                Stopwatch time = new Stopwatch();
                time.Start();
                Encrypt(inFilePath, encrypted_FilePath, password);
                time.Stop();
                Console.WriteLine("Encryption Ends. Time Taken : " + time.ElapsedMilliseconds + " ms");
                Console.WriteLine("------------------------------------------------");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Encryption -- Exception: {0}\r\n{1}", ex.Message, ex.StackTrace);
                return;
            }
            Console.WriteLine("------------------------------------------------");
        }
    }
}
