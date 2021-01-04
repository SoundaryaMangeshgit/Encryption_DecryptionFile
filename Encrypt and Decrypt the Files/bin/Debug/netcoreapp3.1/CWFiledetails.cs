using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using encry_decry1;
using System.Diagnostics;

namespace EncryptZipFiles
{
    public class CSFiledeatils

    {
        // file properties
        public string EncryptedChecksum { get; set; }  
        public string OriginalChecksum { get; set; }
        public string DecryptedChecksum { get; set; }
        public string FileName { get; set; }

        public string RawFilePath { get; set; }
        public string Filesize { get; set; }
        public long Time_ToChecksum_Original { get; set; }
        public long Time_ToChecksum_Encrypted { get; set; }
        public long Time_ToChecksum_Decrypted { get; set; }
        // function that encrypts and decrypts and generates the checksum
        public void ProcessFile(FileInfo file,CWEncryptFilesManager obj1)

        {
            CWEncryptFilesManager verifyobj = new CWEncryptFilesManager();
            Console.WriteLine("adding stop watch to time the stop watch for checksum for the original file ");
            Stopwatch time = new Stopwatch();
            time.Start();
            // checksum for original file
            String  checksumOriginal = ComputeHash.GetHashValue(file.FullName);
            time.Stop();
            Time_ToChecksum_Original = time.ElapsedMilliseconds;
            Console.WriteLine(" checksum of the original :. Time Taken : " + time.ElapsedMilliseconds + " ms");
            Console.WriteLine("------------------------------------------------");
            time.Reset();
            // encryption of the files
            FileEncryptDecrypt.FileCryptoHandler(file.FullName, obj1.EncryptedArchivePath, obj1.Password); //to encrypt 
                                                                                                                     // Console.log(file.Fullname);
            Console.WriteLine(file.FullName);
            Console.WriteLine(obj1.EncryptedArchivePath);

            String pathEncryp = obj1.EncryptedArchivePath + '\\' + file.Name;
            Console.WriteLine("adding stop watch to time the stop watch for checksum for the encryted file ");
            //  timer and generate the checksum for the encrypted file
            time.Start();
            String checksumEncryp = ComputeHash.GetHashValue(pathEncryp);
            time.Stop();
            Time_ToChecksum_Encrypted = time.ElapsedMilliseconds;
            Console.WriteLine(" checksum of the encryted file :. Time Taken : " + time.ElapsedMilliseconds + " ms");
            Console.WriteLine("------------------------------------------------");
            time.Reset();
            // function to decrypt the encrypted files
            Console.WriteLine("Decryption started ");
            FileEncryptDecrypt.FileCryptoHandler1(pathEncryp, obj1.DecryptedArchivePathstore, obj1.Password);
            String pathDecryp = obj1.DecryptedArchivePathstore + '\\' + file.Name;
            Console.WriteLine("adding stop watch to time the stop watch for checksum for the decrypted file ");
            // timer and generate the checksum for the decrypted file 
            time.Start();
            String checksumDecryp = ComputeHash.GetHashValue(pathDecryp);
            time.Stop();
            Time_ToChecksum_Decrypted = time.ElapsedMilliseconds;
            Console.WriteLine(" checksum of the encryted file :. Time Taken : " + time.ElapsedMilliseconds + " ms");
            Console.WriteLine("------------------------------------------------");
            // set all the properties value
            this.FileName = file.Name;
            this.Filesize = (file.Length).ToString();
            this.EncryptedChecksum = checksumEncryp;
            this.DecryptedChecksum = checksumDecryp;
            this.RawFilePath = verifyobj.ArchivePath;
            this.OriginalChecksum= checksumOriginal;
            this.Time_ToChecksum_Original = Time_ToChecksum_Original;
            this.Time_ToChecksum_Decrypted = Time_ToChecksum_Decrypted;
            this.Time_ToChecksum_Encrypted = Time_ToChecksum_Encrypted;
            // generate  the logger obj and write all the values
            CWLogger logobj = new CWLogger("log.txt");
            logobj.debug("----------------------------------------");
            logobj.debug(FileName);
            logobj.debug(EncryptedChecksum);
            logobj.debug(DecryptedChecksum);
            logobj.debug("Encryption decrytion successfull");
            logobj.debug("----------------------------------------");
            Console.WriteLine(FileName);
            ValidateChecksum(checksumOriginal, checksumDecryp);
        }
        public  void ValidateChecksum(string checksumOriginal, string decryptedChecksum)
        {
            if (checksumOriginal != decryptedChecksum)
            {
                Console.WriteLine("Checksums do not match");
            }
        }

    } // CWfile deatils
}
