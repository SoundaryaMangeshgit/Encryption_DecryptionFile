using System;
using System.Collections.Generic;
using System.IO;

using encry_decry1;
namespace EncryptZipFiles
{
    public class CWEncryptFilesManager
    {
        public string Password;
        // properties of the class
        public string ArchivePath { get; set; }
        public string EncryptedArchivePath { get; set; }
        public string ArchivePathforDec { get; set; }
        public string DecryptedArchivePathstore { get; set; }
        public string EncryptedFilePath { get; set; }
        public string DecryptedFilePath { get; set; }
      
        // fetch the file directory and run processfile
        public IList<CSFiledeatils> EncryptArchive(CWEncryptFilesManager obj)
        {
            CSFiledeatils fileobj = new CSFiledeatils();
           DirectoryInfo d = new DirectoryInfo(obj.ArchivePath);
            FileInfo[] Files = d.GetFiles("*.zip"); //Getting zip files
            IList<CWEncryptFilesManager> archiveInfo = new List<CWEncryptFilesManager>();
            IList<CSFiledeatils> fileinfo = new List<CSFiledeatils>();
            foreach (FileInfo file in Files)
            {

                fileobj.ProcessFile(file,obj);
                fileinfo.Add(fileobj);
                archiveInfo.Add(obj);
                Console.WriteLine(fileobj);
            }
            Console.WriteLine(" printing archive info");
            PrintInfo(fileobj,obj);
            return fileinfo;
        }
        public override string ToString()
        {
          
            
            return $"{EncryptedFilePath}" + System.Environment.NewLine
                 + $"{DecryptedFilePath}" + System.Environment.NewLine;           
        }
        // print all the properties
        public void PrintInfo(CSFiledeatils fileobj1, CWEncryptFilesManager cwobj )

        {

            Console.WriteLine(fileobj1.DecryptedChecksum);
              Console.WriteLine(fileobj1.EncryptedChecksum);
              Console.WriteLine(fileobj1.FileName);
            Console.WriteLine(fileobj1.OriginalChecksum);
            Console.WriteLine(cwobj.ArchivePath);
              Console.WriteLine(cwobj.ArchivePathforDec);
            
            Console.WriteLine(fileobj1.ToString());                
            Console.WriteLine(cwobj.ToString());
        }
 

        public static void Main()
        {
         // function that inputs all the user values    
            InputPathforEncryp();


        }
        public static void InputPathforEncryp()
        {
            CWEncryptFilesManager CWobj = new CWEncryptFilesManager();
            //a.temp();
            Console.WriteLine("Folder path of the archieves to encrypt: ");
            CWobj.ArchivePath = Console.ReadLine();
            Console.WriteLine("Password to encrypt: ");
            CWobj.Password = Console.ReadLine();
            Console.WriteLine("Folder path to store encrypted archieves: ");
            CWobj.EncryptedArchivePath = Console.ReadLine();
            Console.WriteLine("Path of the archieve to decrypt: ");
           
            CWobj.ArchivePathforDec = Console.ReadLine();
            Console.WriteLine("Folder path to store decrypted archieves: ");
            CWobj.DecryptedArchivePathstore = Console.ReadLine();
            CWobj.EncryptArchive(CWobj);
            Console.WriteLine("Program finished execution");


        }


    } //class CW
}
   
 // namespace

    // logger class that adds the file properties into temp.txt
class CWLogger
{
String fileName = "temp.txt";
    public CWLogger(String fileName)
    {
       
        File.CreateText(fileName);
    }

    public void debug(String logMessage)
    {
        using (StreamWriter sw = File.AppendText(fileName))
        {
            sw.WriteLine(logMessage);
        }
    }
}

