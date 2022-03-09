using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassFileRenamer.Controller
{
    public class FileController
    {
        public List<string> ReadFiles(string folderPath, string extension)
        {
            List<string> filelist = new List<string>();
            string[] files = Directory.GetFiles($@"{folderPath}", $"*.{extension}");

            foreach (var file in files)
            {
                int length = file.Length;
                string filename = file.Substring(length);
                filelist.Add(filename.ToLower());
            }
            return filelist;
        }

        public List<string> ReadCSVFile(string csvFilePath)
        {

            List<string> namelist = new List<string>();
            string file = $"{csvFilePath}";

            using (StreamReader sr = new StreamReader(file))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(";");
                    namelist.Add($"{parts[0].ToLower()}_{parts[1]}");
                    line = sr.ReadLine();
                }
            }

            return namelist;
        }

        public void RenameFiles(string folderPath, string extension, string csvFilePath, string fromPath, string toPath)
        {
            foreach (string file in ReadFiles(folderPath, extension))
            {
                int i = file.IndexOf("_");
                string shortfilename = file.Substring(0, i);

                foreach (string name in ReadCSVFile(csvFilePath))
                {
                    int j = name.IndexOf("_");
                    string oldname = name.Substring(0, j);
                    string newname = name.Substring(j + 1);

                    if (shortfilename == oldname)
                    {
                        System.IO.File.Move($@"{fromPath}{file}", $@"{toPath}{newname}{file.Substring(i)}", true);
                    }
                }
            }
        }


    }
}
