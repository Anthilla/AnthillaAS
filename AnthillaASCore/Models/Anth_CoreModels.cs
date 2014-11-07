using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaASCore.Models
{
    public class Anth_Entity
    {
        [Key]
        public string _Id { get; set; }

        public byte[] StorIndexN1 { get; set; }

        public byte[] StorIndexN2 { get; set; }

        #region Attributes

        public string Attribute001 { get; set; }

        public string Attribute002 { get; set; }

        public string Attribute003 { get; set; }

        public string Attribute004 { get; set; }

        public string Attribute005 { get; set; }

        public string Attribute006 { get; set; }

        public string Attribute007 { get; set; }

        public string Attribute008 { get; set; }

        public string Attribute009 { get; set; }

        public string Attribute010 { get; set; }

        #endregion Attributes
    }

    public interface Anth_IDel
    {
        bool IsDeleted { get; set; }
    }

    public class Anth_DelEntity : Anth_Entity, Anth_IDel
    {
        public bool IsDeleted { get; set; }

        public string TagInput { get; set; }

        public List<string> Tags { get; set; } //che saranno i valori del modello Tag

        public string ARelGuid { get; set; }

        public string ADt { get; set; } // = DateTime.Now.ToString();

        public string Aned { get; set; } // = "e"; o = "d"; o = "n";

        public string AnthId
        {
            get { return Aned + "_" + ARelGuid + "_" + ADt; }
        }
    }

    public class Anth_Object
    {
        private bool f = false;

        #region Object Definition

        public string ObjectType { get; set; }

        public bool IsUser { get { return f; } set { f = value; } }

        public bool IsCompany { get { return f; } set { f = value; } }

        public bool IsProject { get { return f; } set { f = value; } }

        public bool IsUserGroup { get { return f; } set { f = value; } }

        public bool IsActive { get { return f; } set { f = value; } }

        public bool IsFile { get { return f; } set { f = value; } }

        public bool IsDay { get { return f; } set { f = value; } }

        public bool IsEvent { get { return f; } set { f = value; } }

        public bool IsMail { get { return f; } set { f = value; } }

        public bool IsMailbox { get { return f; } set { f = value; } }

        public bool IsAcl { get { return f; } set { f = value; } }

        public bool IsTag { get { return f; } set { f = value; } }

        public bool IsTagPreset { get { return f; } set { f = value; } }

        public bool IsFunction { get { return f; } set { f = value; } }

        #endregion Object Definition
    }

    public class Anth_Dump : Anth_Object
    {
        #region Core Model Attributes

        public string AnthillaId { get; set; }

        public string AnthillaGuid { get; set; }

        public string AnthillaAlias { get; set; }

        #endregion Core Model Attributes

        #region Basic Attributes

        public string AnthillaFirstName { get; set; }

        public string AnthillaLastName { get; set; }

        public int AnthillaOwner { get; set; }

        public string AnthillaRelation { get; set; }

        public string AnthillaLanguage { get; set; }

        public string AnthillaPassword { get; set; }

        public string AnthillaEmail { get; set; }

        public string AnthillaMap { get; set; }

        public string AnthillaLeader { get; set; }

        #endregion Basic Attributes

        #region Single Relation Attributes

        public string AnthillaUser { get; set; }

        public string AnthillaProject { get; set; }

        public string AnthillaCompany { get; set; }

        public string AnthillaUserGroup { get; set; }

        public string AnthillaUserId { get; set; }

        public string AnthillaProjectId { get; set; }

        public string AnthillaCompanyId { get; set; }

        public string AnthillaUserGroupId { get; set; }

        public string AnthillaUserGuid { get; set; }

        public string AnthillaProjectGuid { get; set; }

        public string AnthillaCompanyGuid { get; set; }

        public string AnthillaUserGroupGuid { get; set; }

        public string AnthillaUserAlias { get; set; }

        public string AnthillaProjectAlias { get; set; }

        public string AnthillaCompanyAlias { get; set; }

        public string AnthillaUserGroupAlias { get; set; }

        public string AnthillaFunctionGroupGuid { get; set; }

        public int AnthillaHierarchyIndex { get; set; }

        public int AnthillaNestedIndex { get; set; }

        #endregion Single Relation Attributes

        #region Many-to-many Relation Attributes

        public List<string> AnthillaUserIds { get; set; }

        public List<string> AnthillaProjectIds { get; set; }

        public List<string> AnthillaCompanyIds { get; set; }

        public List<string> AnthillaUserGroupIds { get; set; }

        public List<string> AnthillaUserGuids { get; set; }

        public List<string> AnthillaProjectGuids { get; set; }

        public List<string> AnthillaCompanyGuids { get; set; }

        public List<string> AnthillaUserGroupGuids { get; set; }

        public List<string> AnthillaFunctionGuids { get; set; }

        #endregion Many-to-many Relation Attributes

        #region System Attributes

        public string AnthillaComment { get; set; }

        public string AnthillaLog { get; set; }

        public string AnthillaLicensing { get; set; }

        public string AnthillaNaming { get; set; }

        public string AnthillaError { get; set; }

        public List<string> AnthillaTags { get; set; }

        public string AnthillaTagInput { get; set; }

        public string AnthillaTagItem { get; set; }

        public string AnthillaTagModel { get; set; }

        public string AnthillaTagType { get; set; }

        public List<string> AnthillaTagList { get; set; }

        public string AnthillaValue { get; set; }

        public string AnthillaPrefix { get; set; }

        public string AnthillaSuffix { get; set; }

        #endregion System Attributes

        #region Custom String Attributes

        public string AnthillaParN1 { get; set; }

        public string AnthillaParN2 { get; set; }

        public string AnthillaParN3 { get; set; }

        public string AnthillaParN4 { get; set; }

        public string AnthillaParN5 { get; set; }

        #endregion Custom String Attributes

        #region Custom Group Attributes

        public IEnumerable<string> AnthillaListN1 { get; set; }

        public IEnumerable<string> AnthillaListN2 { get; set; }

        public IEnumerable<string> AnthillaListN3 { get; set; }

        public IEnumerable<string> AnthillaListN4 { get; set; }

        public IEnumerable<string> AnthillaListN5 { get; set; }

        #endregion Custom Group Attributes

        #region File Attributes

        public string AnthillaPath { get; set; }

        public string AnthillaDimension { get; set; }

        public string AnthillaExtension { get; set; }

        public string AnthillaLenght { get; set; }

        public string AnthillaCreated { get; set; }

        public string AnthillaLastModified { get; set; }

        public string AnthillaOriginalAlias { get; set; }

        public string AnthillaFileOwner { get; set; }

        public string AnthillaFileContext { get; set; }

        #endregion File Attributes

        #region Timing Attributes

        public string AnthillaDate { get; set; }

        public string AnthillaStart { get; set; }

        public string AnthillaEnd { get; set; }

        public string AnthillaType { get; set; }

        public string AnthillaDetail { get; set; }

        public string AnthillaDayName { get; set; }

        public string AnthillaDayInt { get; set; }

        public string AnthillaMonthName { get; set; }

        public string AnthillaMonthInt { get; set; }

        public string AnthillaYear { get; set; }

        #endregion Timing Attributes

        #region Mail Attributes

        public string AnthillaMailFrom { get; set; }

        public IEnumerable<string> AnthillaMailTo { get; set; }

        public string AnthillaMailBody { get; set; }

        public string AnthillaMailBodyText { get; set; }

        public string[] AnthillaMailBodyParts { get; set; }

        public DateTime? AnthillaMailDate { get; set; }

        public string AnthillaMailSubject { get; set; }

        public string AnthillaMailboxGuid { get; set; }

        public string AnthillaMailboxAlias { get; set; }

        public List<Tuple<string, string, string, string>> AnthillaMailSettingsTuple { get; set; }

        public List<string[]> AnthillaMailSettingsArray { get; set; }

        #endregion Mail Attributes

        #region Address Attributes

        public string AnthillaStreetName { get; set; }

        public string AnthillaStreetNumber { get; set; }

        public string AnthillaCity { get; set; }

        public string AnthillaPostalCode { get; set; }

        public string AnthillaCountry { get; set; }

        public string AnthillaState { get; set; }

        #endregion Address Attributes
    }
}