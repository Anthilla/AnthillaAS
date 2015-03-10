using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaCore.Models {

    public class Anth_FileModel : Anth_DelEntity {

        [Key]
        public string FileId { get { return _Id; } set { _Id = value; } }

        public string FileGuid { get; set; }

        public string FileContext { get; set; }

        public byte[] FileType { get; set; }

        public byte[] FileAlias { get; set; }

        public byte[] FileTrackAlias { get; set; }

        public byte[] FileOwner { get; set; }

        public byte[] FileOAlias { get; set; }

        public byte[] FilePath { get; set; }

        public byte[] FileDimension { get; set; }

        public byte[] FileExtension { get; set; }

        public byte[] FileLenght { get; set; }

        public byte[] FilePosition { get; set; }

        public DateTime FileCreated { get; set; }

        public DateTime FileLastModified { get; set; }

        public int FileVersion { get; set; }

        #region Relations

        public string FilePackage { get; set; }

        public List<string> FileUserIds { get; set; }

        public List<string> FileCompanyIds { get; set; }

        public List<string> FileProjectIds { get; set; }

        #endregion Relations
    }

    public class Anth_DirectoryModel : Anth_DelEntity {

        [Key]
        public string DirectoryId { get { return _Id; } set { _Id = value; } }

        public string DirectoryGuid { get; set; }

        public string DirectoryAlias { get; set; }

        public string DirectoryPath { get; set; }
    }

    public class Anth_FilePackModel : Anth_DelEntity {

        [Key]
        public string PackageId { get { return _Id; } set { _Id = value; } }

        [Key]
        public string PackageGuid { get; set; }

        public string PackageAlias { get; set; }

        public string PackageOwner { get; set; }

        public int PackageVersion { get; set; }

        public string PackageType { get; set; }

        public string PackageMessageDescription { get; set; }

        public byte[] PackageKey { get; set; }

        public List<string> Files { get; set; }

        public bool isEmpty { get; set; }

        #region Relations

        public string PackageProjectId { get; set; }

        public List<string> PackageUserIds { get; set; }

        public string PackageCompanyId { get; set; }

        #endregion Relations
    }
}