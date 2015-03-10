using AnthillaCore.Repository;
using System.IO;

namespace AnthillaCore.FileManagement {

    public class PackageRecap {

        private static void Write() {
            string folder = "D:/Anthilla/.tmp";
            Directory.CreateDirectory(folder);
            string file = "MyTest.txt";
            string path = Path.Combine(folder, file);
            if (!File.Exists(path)) {
                using (StreamWriter sw = File.CreateText(path)) {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
            }
        }

        public static Anth_FilePackRepository packRepo = new Anth_FilePackRepository();
        public static Anth_FileRepository fileRepo = new Anth_FileRepository();
        public static Anth_LicenseRepository licenseRepo = new Anth_LicenseRepository();

        public static void Write(string _packGuid) {
            var pack = packRepo.GetById(_packGuid);

            string recapFileName = "package_" + pack.AnthillaGuid.ToUpper() + ".txt";
            string packId = pack.AnthillaGuid;
            string packName = pack.AnthillaAlias;

            string projId = pack.AnthillaProjectId;
            var license = licenseRepo.GetByProject(projId);

            string tmpRoot = FileTools.GetRoot() + "/.tmp";
            string localdirectory = tmpRoot + "/" + packId;
            Directory.CreateDirectory(localdirectory);
            string recapFile = recapFileName;
            string path = Path.Combine(localdirectory, recapFile);

            if (File.Exists(path)) {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path)) {
                var n = " \r\n";
                //n = " \rn";
                //DOS / Windows	CR LF	rn	0x0D 0x0A
                //Mac (early)	    CR	    r	0x0D
                //Unix	        LF	    n	0x0A
                sw.WriteLine("this file is a recap and is attached to a file-package" + n + n);
                if (license == null || license.AnthillaLicenseText == "" || license.AnthillaLicenseTitle == "") {
                    sw.WriteLine("This package has no license attached" + n);
                }
                else {
                    sw.WriteLine("You have accepted and subscribed this license" + n);
                    sw.WriteLine(license.AnthillaLicenseTitle + n);
                    sw.WriteLine(license.AnthillaLicenseText + n);
                }
                sw.WriteLine("----------------------------------------------------------" + n);
                sw.WriteLine("----------------------------------------------------------" + n);
                sw.WriteLine("" + n);
                sw.WriteLine("Package Id  : {0}", packId + n);
                sw.WriteLine("Package Name: {0}", packName + n);
                sw.WriteLine("Files list  : " + n);
                sw.WriteLine("              > ------------------------------------------" + n);
                sw.WriteLine("              > <file name> - <length>" + n);
                foreach (string fileGuid in pack.AnthillaFileList) {
                    var file = fileRepo.GetById(fileGuid);
                    sw.WriteLine("              > {0} - {1}" + n, file.AnthillaOriginalAlias, file.AnthillaLenght);
                }
                sw.WriteLine("" + n);
                sw.WriteLine("              > ------------------------------------------" + n);
                sw.WriteLine("----------------------------------------------------------" + n);
            }
        }

        public static string Write2(string _packGuid) {
            var pack = packRepo.GetById(_packGuid);

            string recapFileName = "package_" + pack.AnthillaGuid.ToUpper() + ".txt";
            string packId = pack.AnthillaGuid;
            string packName = pack.AnthillaAlias;

            string tmpRoot = FileTools.GetRoot() + "/.tmp";
            string localdirectory = tmpRoot + "/" + packId;
            Directory.CreateDirectory(localdirectory);
            string recapFile = recapFileName;
            string path = Path.Combine(localdirectory, recapFile);

            if (File.Exists(path)) {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path)) {
                var n = " \n";
                sw.WriteLine("this file is a recap and is attached to a file-package" + n);
                sw.WriteLine("accept and blablabla" + n);
                sw.WriteLine("----------------------------------------------------------" + n);
                sw.WriteLine("----------------------------------------------------------" + n);
                sw.WriteLine("" + n);
                sw.WriteLine("Package Id  : {0}", packId + n);
                sw.WriteLine("Package Name: {0}", packName + n);
                sw.WriteLine("Files list  : " + n);
                sw.WriteLine("              > ------------------------------------------" + n);
                sw.WriteLine("              > <file name> - <length>" + n);
                foreach (string fileGuid in pack.AnthillaFileList) {
                    var file = fileRepo.GetById(fileGuid);
                    sw.WriteLine("              > {0} - {1}" + n, file.AnthillaAlias, file.AnthillaLenght);
                }
                sw.WriteLine("" + n);
                sw.WriteLine("              > ------------------------------------------" + n);
                sw.WriteLine("----------------------------------------------------------" + n);
            }

            return path;
        }
    }
}