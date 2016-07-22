using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelyCMD
{
    class SquirrelyUtils
    {

        public static List<FileInfo> files = new List<FileInfo>();
        public static List<DirectoryInfo> folders = new List<DirectoryInfo>();

        public static void FullDirList(String directory) {
            DirectoryInfo dir = new DirectoryInfo(directory);
            try {
                foreach (FileInfo f in dir.GetFiles()) {
                    files.Add(f);
                }
            } catch {
            }

            foreach (DirectoryInfo d in dir.GetDirectories()) {
                folders.Add(d);
            }
        }
        
    }
}
