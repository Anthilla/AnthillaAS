using AnthillaCore.Configuration;
using System;

namespace AnthillaCore {

    public class FileTools {

        public static string AdjustFileName(string fileName) {
            string newFileName = fileName.Replace(" ", "_");
            return newFileName;
        }

        public static string GetRoot() {
            string root = SelfConfig.GetAnthillaRepo();
            return root;
        }

        public static string GetSharedDir() {
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");
            string d = DateTime.Now.ToString("dd");
            string path = GetRoot() + "/Shared/" + y + "/" + m + "/" + d;
            return path;
        }

        public static string CombineSharedRoot(string fileSubPath) {
            string path = GetRoot() + "/Shared/" + fileSubPath;
            return path;
        }

        public static string GetArchiveDir() {
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");
            string d = DateTime.Now.ToString("dd");
            string path = GetRoot() + "/Archive/" + y + "/" + m + "/" + d;
            return path;
        }

        public static string CombineArchiveRoot(string fileSubPath) {
            string path = GetRoot() + "/Archive/" + fileSubPath;
            return path;
        }

        public static string GetMyfilesDir() {
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");
            string d = DateTime.Now.ToString("dd");
            string path = GetRoot() + "/MyFiles/" + y + "/" + m + "/" + d;
            return path;
        }

        public static string CombineMyFilesRoot(string fileSubPath) {
            string path = GetRoot() + "/MyFiles/" + fileSubPath;
            return path;
        }
    }
}