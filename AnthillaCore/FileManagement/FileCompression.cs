using AnthillaCore;
using AnthillaCore.Repository;
using AnthillaCore.Security;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace UbearCore {

    public class Zip {

        #region methods

        public static void DoZipFile() {
            string startPath = "d:/example/start";
            Directory.CreateDirectory(startPath);
            string zipPath = "d:/example/result.7z";
            string extractPath = "d:/example/extract";

            ZipFile.CreateFromDirectory(startPath, zipPath);

            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }

        private static void CompressGZIP(FileInfo fileToCompress) {
            using (FileStream originalFileStream = fileToCompress.OpenRead()) {
                if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".7z") {
                    using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".7z")) {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress)) {
                            originalFileStream.CopyTo(compressionStream);
                            //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            //    fileToCompress.Name, fileToCompress.Length.ToString(), compressedFileStream.Length.ToString());
                        }
                    }
                }
            }
        }

        private static void DecompressGZIP(FileInfo fileToDecompress) {
            using (FileStream originalFileStream = fileToDecompress.OpenRead()) {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName)) {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress)) {
                        decompressionStream.CopyTo(decompressedFileStream);
                        //Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }

        #endregion methods

        private Anth_FilePackRepository fpRepo = new Anth_FilePackRepository();
        private Anth_FileRepository fileRepo = new Anth_FileRepository();

        public void ZipFilesToDownload(string _packGuid) {
            string packGuid = _packGuid;
            byte[] key = AnthillaSecurity.AnthillaKey("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");
            byte[] vector = AnthillaSecurity.AnthillaVector("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");

            List<FileInfo> fileList = new List<FileInfo>();
            var fileModels = fpRepo.GetFileInPackage(packGuid);
            foreach (var file in fileModels) {
                var subpath = fileRepo.GetSubPath(file.AnthillaGuid);
                var fileRoot = FileTools.CombineArchiveRoot(subpath);
                var path = Path.Combine(fileRoot, file + ".anthfile");
                if (File.Exists(path)) {
                    var fileInfo = new FileInfo(path);
                    fileList.Add(fileInfo);
                }
            }

            string tmpRoot = FileTools.GetRoot() + "/.tmp";
            Directory.CreateDirectory(tmpRoot);
            string dirCompression = tmpRoot + packGuid;
            Directory.CreateDirectory(dirCompression);
            foreach (var file in fileList) {
                var fullFileName = file.FullName;
                var fileName = file.Name;
                var toPath = Path.Combine(dirCompression, fileName);
                if (File.Exists(fullFileName)) {
                    File.Copy(fullFileName, toPath, true);
                }
            }

            string[] filesToDecrypt = Directory.GetFiles(dirCompression);
            foreach (string file in filesToDecrypt) {
                var fullFileName = file;
                using (RijndaelManaged aes = new RijndaelManaged()) {
                    using (FileStream fileOpenStream = File.OpenRead(fullFileName)) {
                        using (ICryptoTransform decryptor = aes.CreateDecryptor(key, vector)) {
                            using (FileStream destinationStream = new FileStream(fullFileName, FileMode.Create)) {
                                using (CryptoStream cryptoStream = new CryptoStream(destinationStream, decryptor, CryptoStreamMode.Write)) {
                                    fileOpenStream.CopyTo(cryptoStream);
                                    int data;
                                    while ((data = cryptoStream.ReadByte()) != -1) {
                                        cryptoStream.WriteByte((byte)data);
                                        destinationStream.WriteByte((byte)data);
                                        cryptoStream.FlushFinalBlock();
                                        cryptoStream.Close();
                                        destinationStream.Flush();
                                        destinationStream.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string zipPath = tmpRoot + packGuid + ".7z";
            ZipFile.CreateFromDirectory(dirCompression, zipPath);

            Directory.Delete(dirCompression, true);
            File.Delete(zipPath);
        }

        public static void Create() {
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile()) {
                zip.Password = "ciao";

                string directory = "D:/Uber/test/altro";

                DirectoryInfo directorySelected = new DirectoryInfo(directory);
                var files = directorySelected.GetFiles();

                foreach (FileInfo file in files) {
                    var f = file.FullName;
                    zip.AddFile(f);
                }

                zip.Save("test.7z");
                //zip.Save(outputStream);
            }
        }
    }
}