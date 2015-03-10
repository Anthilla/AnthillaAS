using AnthillaCore.Models;
using AnthillaCore.Security;
using Nancy;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AnthillaCore.FileManagement {

    public static class FileUpload {

        public static async Task UploadMyFiles(string _guid, HttpFile _file, HttpClient _client, Anth_Dump _user, string _server, string _extension, string _newfilename) {
            HttpFile file = _file;
            HttpClient client = _client;
            string server = _server;
            string modelGuid = _guid;
            string bKey = Guid.NewGuid().ToString();
            //byte[] key = AnthillaSecurity.AnthillaKey(bKey);
            string bVector = Guid.NewGuid().ToString();
            //byte[] vector = AnthillaSecurity.AnthillaVector(bVector);
            byte[] key = AnthillaSecurity.AnthillaKey("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");
            byte[] vector = AnthillaSecurity.AnthillaVector("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");
            string guidName = Guid.NewGuid().ToString();
            string dbFileName = FileTools.AdjustFileName(_newfilename);
            string extension = _extension;
            string newFileName = guidName + ".anthfile";
            string realPath = FileTools.GetMyfilesDir();
            string dimension = file.Value.Length.ToString();
            if (!Directory.Exists(realPath)) { Directory.CreateDirectory(realPath); }
            string fullFileName = Path.Combine(realPath, newFileName);
            if (file != null && file.Value.Length > 0) {
                using (RijndaelManaged aes = new RijndaelManaged()) {
                    using (FileStream fileStream = new FileStream(fullFileName, FileMode.Create)) {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, vector)) {
                            using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write)) {
                                using (MemoryStream memoryStream = new MemoryStream()) {
                                    file.Value.CopyTo(cryptoStream);
                                    Anth_Dump user = _user;
                                    var uri = server + "file/" + modelGuid + "/myfiles/" + user.AnthillaGuid + "/" + newFileName + "/" + extension + "/" + dbFileName + "/" + dimension + "/" + bKey + "/" + bVector;
                                    await client.GetStringAsync(uri);
                                    cryptoStream.Flush();
                                    cryptoStream.Close();
                                    memoryStream.Flush();
                                    memoryStream.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        public static async Task UploadSharing(string _guid, HttpFile _file, HttpClient _client, Anth_Dump _user, string _server, string _newfilename, string _extension) {
            HttpFile file = _file;
            HttpClient client = _client;
            string server = _server;
            string modelGuid = _guid;
            string newfilename = _newfilename;
            string bKey = Guid.NewGuid().ToString();
            //byte[] key = AnthillaSecurity.AnthillaKey(bKey);
            string bVector = Guid.NewGuid().ToString();
            //byte[] vector = AnthillaSecurity.AnthillaVector(bVector);
            byte[] key = AnthillaSecurity.AnthillaKey("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");
            byte[] vector = AnthillaSecurity.AnthillaVector("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");
            string guidName = Guid.NewGuid().ToString();
            //qui si tolgono gli spazi dal nome -> regole della nomenclatura
            string fileName = FileTools.AdjustFileName(newfilename);
            string extension = _extension;
            string newFileName = guidName + ".anthfile";
            string realPath = FileTools.GetSharedDir();
            string dimension = file.Value.Length.ToString();
            string type = file.ContentType;
            if (!Directory.Exists(realPath)) { Directory.CreateDirectory(realPath); }
            string fullFileName = Path.Combine(realPath, newFileName);
            if (file != null && file.Value.Length > 0) {
                using (RijndaelManaged aes = new RijndaelManaged()) {
                    using (FileStream fileStream = new FileStream(fullFileName, FileMode.Create)) {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, vector)) {
                            using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write)) {
                                using (MemoryStream memoryStream = new MemoryStream()) {
                                    file.Value.CopyTo(cryptoStream);
                                    Anth_Dump user = _user;
                                    var uri = server + "file/" + modelGuid + "/sharing/" + user.AnthillaGuid + "/" + newFileName + "/" + extension + "/" + fileName + "/" + dimension + "/" + bKey + "/" + bVector;
                                    await client.GetStringAsync(uri);
                                    cryptoStream.Flush();
                                    cryptoStream.Close();
                                    memoryStream.Flush();
                                    memoryStream.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        public static async Task UploadArchive(string _guid, HttpFile _file, HttpClient _client, Anth_Dump _user, string _server, string _newfilename, string _extension) {
            HttpFile file = _file;
            HttpClient client = _client;
            string server = _server;
            string modelGuid = _guid;
            string newfilename = _newfilename;
            string bKey = Guid.NewGuid().ToString();
            //byte[] key = AnthillaSecurity.AnthillaKey(bKey);
            string bVector = Guid.NewGuid().ToString();
            //byte[] vector = AnthillaSecurity.AnthillaVector(bVector);
            byte[] key = AnthillaSecurity.AnthillaKey("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");
            byte[] vector = AnthillaSecurity.AnthillaVector("D2F28F3A-4AEE-4B39-B8D8-F3D72CAD19D3");
            string guidName = Guid.NewGuid().ToString();
            //qui si tolgono gli spazi dal nome -> regole della nomenclatura
            string fileName = FileTools.AdjustFileName(newfilename);
            string extension = _extension;
            string newFileName = guidName + ".anthfile";
            string realPath = FileTools.GetSharedDir();
            string dimension = file.Value.Length.ToString();
            string type = file.ContentType;
            if (!Directory.Exists(realPath)) { Directory.CreateDirectory(realPath); }
            string fullFileName = Path.Combine(realPath, newFileName);
            if (file != null && file.Value.Length > 0) {
                using (RijndaelManaged aes = new RijndaelManaged()) {
                    using (FileStream fileStream = new FileStream(fullFileName, FileMode.Create)) {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(key, vector)) {
                            using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write)) {
                                using (MemoryStream memoryStream = new MemoryStream()) {
                                    file.Value.CopyTo(cryptoStream);
                                    Anth_Dump user = _user;
                                    var uri = server + "file/" + modelGuid + "/archive/" + user.AnthillaGuid + "/" + newFileName + "/" + extension + "/" + fileName + "/" + dimension + "/" + bKey + "/" + bVector;
                                    await client.GetStringAsync(uri);
                                    cryptoStream.Flush();
                                    cryptoStream.Close();
                                    memoryStream.Flush();
                                    memoryStream.Close();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}