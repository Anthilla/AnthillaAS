using AnthillaCore.Mail;
using AnthillaCore.Models;
using AnthillaCore.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace AnthillaCore.FileManagement {

    public class GenerateMessage {
        public static Anth_FilePackRepository packRepo = new Anth_FilePackRepository();
        public static Anth_FileRepository fileRepo = new Anth_FileRepository();
        public static Anth_MailRepository mailRepo = new Anth_MailRepository();

        public static void Write(string _packGuid) {
            var pack = packRepo.GetById(_packGuid);
            var fileGuidList = pack.AnthillaFileList;
            var fileList = new List<Anth_Dump>();
            foreach (string fg in fileGuidList) {
                var f = fileRepo.GetById(fg);
                fileList.Add(f);
            }
            var path = PackageRecap.Write2(_packGuid);
            var text = File.ReadAllText(path);

            //var message = new Anth_MailModel();
            var from = pack.AnthillaFileOwner;
            List<string> to = pack.AnthillaUserIds;
            var body = text;
            var subject = _packGuid + "_" + pack.AnthillaAlias;
            var box = "File-Notification";
            List<string> taglist = pack.AnthillaTags;
            string tags = "";
            foreach (var t in taglist) {
                tags += t + ", ";
            }
            tags += "file-shared-notification";
            string guid = Guid.NewGuid().ToString();
            mailRepo.Create2(guid, from, to, body, subject, box, tags);
        }
    }
}