using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class DirUtils
    {

        public static void EmptyDirectory(string path) {

            var tempFiles = Directory.GetFiles(path);
            if (tempFiles.Length <= 0)
                return;

            foreach (string s in tempFiles) {

                File.Delete(s);
            
            }

        
        }


        public static bool ValidDirPath(string path)
        {

            return Path.IsPathRooted(path);

        }


        public static bool CreateIfDirNotExists(string path)
        {

            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {

                    dir.Create();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;



        }

        public static void CheckCreatePath(string path)
        {
            if (!ValidDirPath(path))
            {
                throw new Exception($"Invalid path: {path}");
            }
            CreateIfDirNotExists(path);
        }


    }
}
