using AnthillaCore.Models;
using AnthillaCore.Security;
using AnthillaCore.TagEngine;
using ImapX;
using System;
using System.Collections.Generic;

namespace AnthillaCore.Mapper {

    public class Anth_Mapper {

        public Anth_Dump UserToDump(Anth_UserModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "user";
            n.IsUser = true;
            n.AnthillaId = model.UserId;
            n.AnthillaGuid = model.UserGuid;
            n.AnthillaFirstName = AnthillaSecurity.Decrypt(model.UserFirstName, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaLastName = AnthillaSecurity.Decrypt(model.UserLastName, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.UserAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaEmail = AnthillaSecurity.Decrypt(model.UserEmail, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaPassword = AnthillaSecurity.Decrypt(model.UserPassword, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaTags = model.Tags;
            n.AnthillaCompanyId = model.CompanyId;
            n.AnthillaProjectIds = model.ProjectIds;
            n.AnthillaUserGroupIds = model.UGroupIds;
            n.AnthillaInsider = model.Insider;

            if (model.SmtpMailSetting != null) {
                List<string[]> list = new List<string[]>();
                foreach (var i in model.SmtpMailSetting) {
                    var s = MailSettingToDumpArray(i, model.StorIndexN2, model.StorIndexN1);
                    string[] array = new string[4] { s[0], s[1], s[2], s[3] };
                    list.Add(array);
                }
                n.AnthillaMailSettingsArray = list;
            }
            return n;
        }

        public Anth_Dump AddressToDump(Anth_AddressModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "address";
            n.IsCompany = true;
            n.AnthillaId = model.AddressId;
            n.AnthillaGuid = model.AddressGuid;
            n.AnthillaStreetName = AnthillaSecurity.Decrypt(model.StreetName, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaStreetNumber = AnthillaSecurity.Decrypt(model.StreetNumber, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaCity = AnthillaSecurity.Decrypt(model.City, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaPostalCode = AnthillaSecurity.Decrypt(model.PostalCode, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaCountry = AnthillaSecurity.Decrypt(model.Country, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaState = AnthillaSecurity.Decrypt(model.State, model.StorIndexN2, model.StorIndexN1);
            return n;
        }

        public Anth_Dump CompanyToDump(Anth_CompanyModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "company";
            n.IsCompany = true;
            n.AnthillaId = model.CompanyId;
            n.AnthillaGuid = model.CompanyGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.CompanyAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaOwner = model.CompanyOwner;
            n.AnthillaLanguage = AnthillaSecurity.Decrypt(model.CompanyLanguage, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaProjectIds = model.CompanyProjectIds;
            n.AnthillaTags = model.Tags;
            var address = model.CompanyAddress;
            if (address != null) {
                n.AnthillaStreetName = AnthillaSecurity.Decrypt(address.StreetName, address.StorIndexN2, address.StorIndexN1);
                n.AnthillaStreetNumber = AnthillaSecurity.Decrypt(address.StreetNumber, address.StorIndexN2, address.StorIndexN1);
                n.AnthillaCity = AnthillaSecurity.Decrypt(address.City, address.StorIndexN2, address.StorIndexN1);
                n.AnthillaPostalCode = AnthillaSecurity.Decrypt(address.PostalCode, address.StorIndexN2, address.StorIndexN1);
                n.AnthillaCountry = AnthillaSecurity.Decrypt(address.Country, address.StorIndexN2, address.StorIndexN1);
                n.AnthillaState = AnthillaSecurity.Decrypt(address.State, address.StorIndexN2, address.StorIndexN1);
            }
            return n;
        }

        public Anth_Dump ProjectToDump(Anth_ProjectModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "project";
            n.IsProject = true;
            n.AnthillaId = model.ProjectId;
            n.AnthillaGuid = model.ProjectGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.ProjectAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaProjectCode = AnthillaSecurity.Decrypt(model.ProjectCode, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaTags = model.Tags;
            n.AnthillaCompanyLeaderId = model.CompanyLeaderId;
            n.AnthillaLeader = model.LeaderId;
            n.AnthillaCompanyGuids = model.CompanyGuids;
            n.AnthillaLicenseGuid = model.LicenseGuid;
            n.IsActive = model.IsActive;
            return n;
        }

        public Anth_Dump LicenseToDump(Anth_LicenseModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "project";
            n.IsLicense = true;
            n.AnthillaId = model.LicenseId;
            n.AnthillaGuid = model.LicenseGuid;
            n.AnthillaLicenseGuid = model.LicenseGuid;
            n.AnthillaLicenseTitle = model.LicenseTitle;
            n.AnthillaLicenseText = model.LicenseText;
            return n;
        }

        public Anth_Dump UserGroupToDump(Anth_UserGroupModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "userGroup";
            n.IsUserGroup = true;
            n.AnthillaId = model.UsersGroupId;
            n.AnthillaGuid = model.UsersGroupGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.UsersGroupAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaHierarchyIndex = model.UsersGroupHierarchyIndex;
            n.AnthillaNestedIndex = model.UsersGroupNestedIndex;
            n.AnthillaTags = model.Tags;
            return n;
        }

        public Anth_Dump FileToDump(Anth_FileModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "file";
            n.IsFile = true;
            n.AnthillaId = model.FileId;
            n.AnthillaGuid = model.FileGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.FileAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaTrackAlias = AnthillaSecurity.Decrypt(model.FileTrackAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaOriginalAlias = AnthillaSecurity.Decrypt(model.FileOAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaPath = AnthillaSecurity.Decrypt(model.FilePath, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaLenght = AnthillaSecurity.Decrypt(model.FileLenght, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaDimension = AnthillaSecurity.Decrypt(model.FileDimension, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaExtension = AnthillaSecurity.Decrypt(model.FileType, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaCreated = model.FileCreated.ToString();
            n.AnthillaLastModified = model.FileLastModified.ToString();
            n.AnthillaFileOwner = AnthillaSecurity.Decrypt(model.FileOwner, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaType = AnthillaSecurity.Decrypt(model.FileType, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaFileContext = model.FileContext;
            n.AnthillaFilePackageId = model.FilePackage;
            n.AnthillaUserIds = model.FileUserIds;
            n.AnthillaProjectIds = model.FileProjectIds;
            n.AnthillaCompanyIds = model.FileCompanyIds;
            int i = model.FileVersion;
            string s;
            if (i.ToString().Length > 5) {
                s = i.ToString();
            }
            else {
                s = i.ToString("D4");
            }
            n.AnthillaVersion = s;
            n.AnthillaVersionInt = model.FileVersion;
            n.AnthillaTags = model.Tags;
            return n;
        }

        public Anth_Dump FilePackToDump(Anth_FilePackModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "filepack";
            n.IsFile = true;
            n.AnthillaId = model.PackageId;
            n.AnthillaGuid = model.PackageGuid;
            n.AnthillaAlias = model.PackageAlias;
            n.AnthillaFileOwner = model.PackageOwner;
            n.AnthillaFileList = model.Files;
            n.AnthillaProjectId = model.PackageProjectId;
            n.AnthillaUserIds = model.PackageUserIds;
            n.AnthillaCompanyId = model.PackageCompanyId;
            n.AnthillaType = model.PackageType;
            n.AnthillaTags = model.Tags;
            n.AnthillaPackageKey = AnthillaSecurity.Decrypt(model.PackageKey, model.StorIndexN2, model.StorIndexN1);
            int i = model.PackageVersion;
            string s;
            if (i.ToString().Length > 5) {
                s = i.ToString();
            }
            else {
                s = i.ToString("D4");
            }
            n.AnthillaVersion = s;
            n.AnthillaVersionInt = model.PackageVersion;
            n.AnthillaDescription = model.PackageMessageDescription;
            n.AnthillaPackageIsEmpty = model.isEmpty;
            return n;
        }

        public Anth_Dump EventToDump(Anth_EventModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "event";
            n.IsEvent = true;
            n.AnthillaId = model.EventId;
            n.AnthillaGuid = model.EventGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.EventAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaType = AnthillaSecurity.Decrypt(model.EventType, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaDetail = AnthillaSecurity.Decrypt(model.EventDetail, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaLenght = model.EventLenght.ToString();
            n.AnthillaDate = model.EventDate;
            n.AnthillaStart = model.EventStart;
            n.AnthillaEnd = model.EventEnd;

            n.AnthillaCompanyGuid = model.EventCompanyGuid;
            n.AnthillaProjectGuid = model.EventProjectGuid;
            n.AnthillaUserGuid = model.EventUserGuid;

            n.AnthillaTags = model.Tags;
            return n;
        }

        public Anth_Dump MailConfigToDump(Anth_MailConfig model) {
            var n = new Anth_Dump();
            n.ObjectType = "mailconfig";
            n.AnthillaId = model.MailConfigId;
            n.AnthillaGuid = model.MailConfigGuid;
            n.AnthillaUserGuid = model.UserGuid;

            List<Tuple<string, string, string, string>> list = new List<Tuple<string, string, string, string>>();
            foreach (var i in model.Setting) {
                var s = MailSettingToDumpTuple(i, model.StorIndexN2, model.StorIndexN1);
                Tuple<string, string, string, string> tuple = new Tuple<string, string, string, string>
                    (
                        s.Item1, s.Item2, s.Item3, s.Item4
                    );
                list.Add(tuple);
            }

            n.AnthillaMailSettingsTuple = list;
            return n;
        }

        public Tuple<string, string, string, string> MailSettingToDumpTuple(MailSetting model, byte[] k, byte[] v) {
            var item1 = AnthillaSecurity.Decrypt(model.Type, k, v);
            var item2 = AnthillaSecurity.Decrypt(model.ImapUrl, k, v);
            var item3 = AnthillaSecurity.Decrypt(model.Account, k, v);
            var item4 = AnthillaSecurity.Decrypt(model.Password, k, v);
            Tuple<string, string, string, string> tuple = new Tuple<string, string, string, string>(item1, item2, item3, item4);
            return tuple;
        }

        public string[] MailSettingToDumpArray(MailSetting model, byte[] k, byte[] v) {
            var item1 = AnthillaSecurity.Decrypt(model.Type, k, v);
            var item2 = AnthillaSecurity.Decrypt(model.ImapUrl, k, v);
            var item3 = AnthillaSecurity.Decrypt(model.Account, k, v);
            var item4 = AnthillaSecurity.Decrypt(model.Password, k, v);
            string[] array = new string[4] { item1, item2, item3, item4 };
            return array;
        }

        public Anth_Dump MailToDump(Anth_MailModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "mail";
            n.IsMail = true;
            n.AnthillaId = model.MailId;
            n.AnthillaGuid = model.MailGuid;
            n.AnthillaMailFrom = AnthillaSecurity.Decrypt(model.strMailFrom, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaMailBody = AnthillaSecurity.Decrypt(model.strMailBody, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaMailBodyText = AnthillaSecurity.Decrypt(model.MailBodyText, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaMailBodyParts = model.strMailBodyParts;
            n.AnthillaMailDate = model.MailDate;
            n.AnthillaMailSubject = AnthillaSecurity.Decrypt(model.MailSubject, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaMailboxGuid = model.MailboxId;
            var nMailToTable = new List<string>();

            foreach (var mail in model.strMailTo) {
                var ml = AnthillaSecurity.Decrypt(mail, model.StorIndexN2, model.StorIndexN1);
                nMailToTable.Add(ml);
            }
            n.AnthillaMailTo = nMailToTable;

            n.AnthillaTags = model.Tags;
            return n;
        }

        public Anth_Dump ImapXMailToDump(Message model) {
            var n = new Anth_Dump();
            n.ObjectType = "mailx";
            n.IsMail = true;
            n.AnthillaId = Guid.NewGuid().ToString();
            n.AnthillaGuid = Guid.NewGuid().ToString();
            n.AnthillaMailMessageUId = model.UId;
            n.AnthillaMailFrom = model.From.Address;
            n.AnthillaMailBody = model.Body.Text;
            n.AnthillaMailDate = model.Date;
            n.AnthillaMailSubject = model.Subject;

            var ii = model.Headers;

            var nMailToTable = new List<string>();
            foreach (var mail in model.To) {
                var ml = mail.Address;
                nMailToTable.Add(ml);
            }
            n.AnthillaMailTo = nMailToTable;

            n.AnthillaTags = new List<string>() { };
            return n;
        }

        public Anth_Dump MailboxToDump(Anth_MailboxModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "mailbox";
            n.IsMailbox = true;
            n.AnthillaId = model.MailboxId;
            n.AnthillaGuid = model.MailboxGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.MailboxAlias, model.StorIndexN2, model.StorIndexN1);

            n.AnthillaTags = model.Tags;
            return n;
        }

        public Anth_Dump GmailMailboxToDump(Folder model) {
            var n = new Anth_Dump();
            n.ObjectType = "mailbox";
            n.IsMailbox = true;
            n.AnthillaId = Guid.NewGuid().ToString();
            n.AnthillaGuid = Guid.NewGuid().ToString();
            n.AnthillaMailboxAlias = model.Name;
            var hc = model.HasChildren;
            if (hc == true) {
                n.AnthillaMailboxHasChild = model.HasChildren;
                var nl = new List<string>() { };
                foreach (var f in model.SubFolders) {
                    var nm = f.Name;
                    nl.Add(nm.ToString());
                }
                n.AnthillaMailboxSubfolders = nl;
            }

            n.AnthillaTags = new List<string>() { };
            return n;
        }

        public Anth_Dump ContactToDump(Contact model) {
            var n = new Anth_Dump();
            n.ObjectType = "contact";
            n.IsMailbox = true;
            n.AnthillaId = model.ContactId;
            n.AnthillaGuid = model.ContactGuid;
            n.AnthillaFirstName = AnthillaSecurity.Decrypt(model.ContactFirstName, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaLastName = AnthillaSecurity.Decrypt(model.ContactLastName, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaEmail = AnthillaSecurity.Decrypt(model.ContactEmail, model.StorIndexN2, model.StorIndexN1);
            return n;
        }

        public Anth_Dump VerificationUrlToDump(Anth_VerificationUrlModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "verifyurl";
            n.IsMailbox = true;
            n.AnthillaId = model.VerificationUrlId;
            n.AnthillaGuid = model.VerificationUrlGuid;
            n.AnthillaUserGuid = model.UserGuid;
            n.AnthillaEmail = model.EmailToAddress;
            n.AnthillaVerificationUrl = model.VerificationUrl;
            n.AnthillaVerificationUrlCreated = model.UrlCreation;
            n.AnthillaVerificationUrlVal = model.UrlValidity;
            return n;
        }

        public Anth_Dump AclToDump(Anth_AclModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "acl";
            n.IsAcl = true;
            n.AnthillaId = model.AclId;
            n.AnthillaGuid = model.AclGuid;
            n.AnthillaUserGroupGuid = model.AclUserGroupGuid;
            n.AnthillaFunctionGroupGuid = model.AclFunctionGroupGuid;

            return n;
        }

        public Anth_Dump TagToDump(Anth_TagModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "tag";
            n.IsTag = true;
            n.AnthillaId = model.TagId;
            n.AnthillaGuid = model.TagGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.TagAlias, model.StorIndexN2, model.StorIndexN1);

            return n;
        }

        public Anth_Dump TagPresetToDump(Anth_TagPresetModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "tagcollection";
            n.IsTagPreset = true;
            n.AnthillaId = model.TagPresetId;
            n.AnthillaGuid = model.TagPresetGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.TagPresetAlias, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaTagItem = AnthillaSecurity.Decrypt(model.TagPresetItem, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaTagModel = AnthillaSecurity.Decrypt(model.TagPresetModel, model.StorIndexN2, model.StorIndexN1);
            n.AnthillaTagType = AnthillaSecurity.Decrypt(model.TagPresetType, model.StorIndexN2, model.StorIndexN1);
            var newList = new List<string>();
            if (model.TagPreset != null) {
                foreach (Anth_TagModel t in model.TagPreset) {
                    Anth_TagRepository repo = new Anth_TagRepository();
                    var itemTag = repo.GetById(t.TagGuid);
                    newList.Add(itemTag.AnthillaAlias);
                }
            }
            else {
                var tt = "";
                newList.Add(tt);
            }
            n.AnthillaTagList = newList;
            return n;
        }

        public Anth_Dump FunctionToDump(Anth_FunctionModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "function";
            n.IsFunction = true;
            n.AnthillaGuid = model.FunctionGuid;
            n.AnthillaAlias = model.FunctionAlias;
            n.AnthillaValue = model.FunctionValue;
            n.AnthillaType = model.FunctionType;
            return n;
        }

        public Anth_Dump FunctionGroupToDump(Anth_FunctionGroupModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "functiongroup";
            n.IsFunction = true;
            n.AnthillaGuid = model.FunctionsGroupGuid;
            n.AnthillaAlias = AnthillaSecurity.Decrypt(model.FunctionsGroupAlias, model.StorIndexN2, model.StorIndexN1);
            var list = new List<string>();
            if (model.FunctionList != null) {
                foreach (var s in model.FunctionList) {
                    list.Add(s);
                }
            }
            n.AnthillaFunctionGuids = list;
            n.AnthillaTags = model.Tags;
            return n;
        }

        public Anth_Dump GroupRelationToDump(Anth_GroupRelationModel model) {
            var n = new Anth_Dump();
            n.ObjectType = "grouprelation";
            n.IsFunction = false;
            n.AnthillaGuid = model.GroupRelationGuid;
            n.AnthillaUserGroupId = model.UsersRelGuid;
            n.AnthillaFunctionGroupGuid = model.FunctionsRelGuid;

            return n;
        }
    }
}