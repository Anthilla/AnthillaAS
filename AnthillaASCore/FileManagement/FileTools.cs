using System;

namespace AnthillaASCore
{
    public class FileTools
    {
        public static string AdjustFileName(string fileName)
        {
            string newFileName = fileName.Replace(" ", "_");
            return newFileName;
        }

        public static string GetRoot()
        {
            //return "/framework/anthilla/AnthillaDatabase/AnthFile";
            return @"D:\Uber\AnthillaDatabase\AnthFile";
        }

        public static string GetSharedDir()
        {
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");
            string d = DateTime.Now.ToString("dd");
            //string path = "/framework/anthilla/AnthillaDatabase/AnthFile/Shared/" + y + "/" + m + "/" + d;
            string path = @"D:\Uber\AnthillaDatabase\AnthFile\Shared\" + y + @"\" + m + @"\" + d;
            return path;
        }

        public static string CombineSharedRoot(string fileSubPath)
        {
            //string path = "/framework/anthilla/AnthillaDatabase/AnthFile/Shared/" + fileSubPath;
            string path = @"D:\Uber\AnthillaDatabase\AnthFile\Shared\" + fileSubPath;
            return path;
        }

        public static string GetArchiveDir()
        {
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");
            string d = DateTime.Now.ToString("dd");
            //string path = "/framework/anthilla/AnthillaDatabase/AnthFile/Archive/" + y + "/" + m + "/" + d;
            string path = @"D:\Uber\AnthillaDatabase\AnthFile\Archive\" + y + @"\" + m + @"\" + d;
            return path;
        }

        public static string CombineArchiveRoot(string fileSubPath)
        {
            //string path = "/framework/anthilla/AnthillaDatabase/AnthFile/Archive/" + fileSubPath;
            string path = @"D:\Uber\AnthillaDatabase\AnthFile\Archive\" + fileSubPath;
            return path;
        }

        public static string GetMyfilesDir()
        {
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");
            string d = DateTime.Now.ToString("dd");
            //string path = "/framework/anthilla/AnthillaDatabase/AnthFile/MyFiles/" + y + "/" + m + "/" + d;
            string path = @"D:\Uber\AnthillaDatabase\AnthFile\MyFiles\" + y + @"\" + m + @"\" + d;
            return path;
        }

        public static string CombineMyFilesRoot(string fileSubPath)
        {
            //string path = "/framework/anthilla/AnthillaDatabase/AnthFile/MyFiles/" + fileSubPath;
            string path = @"D:\Uber\AnthillaDatabase\AnthFile\MyFiles\" + fileSubPath;
            return path;
        }
    }
}