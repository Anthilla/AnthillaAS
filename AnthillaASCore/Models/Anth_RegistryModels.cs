using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaASCore.Models
{
    public class Anth_CompanyModel : Anth_DelEntity
    {
        [Key]
        public string CompanyId { get { return _Id; } set { _Id = value; } }

        public string CompanyGuid { get; set; }

        public byte[] CompanyAlias { get; set; }

        public int CompanyOwner { get; set; } //0 false; 1 true

        public byte[] CompanyRelation { get; set; } //internal / external

        public byte[] CompanyLanguage { get; set; }

        public List<string> CompanyProjectIds { get; set; }

        public IEnumerable<Anth_ProjectModel> CompanyProjects { get; set; }

        public Anth_AddressModel CompanyAddress { get; set; }
    }

    public class Anth_UserModel : Anth_DelEntity
    {
        [Key]
        public string UserId { get { return _Id; } set { _Id = value; } }

        public string UserGuid { get; set; }

        public byte[] UserFirstName { get; set; }

        public byte[] UserLastName { get; set; }

        public byte[] UserAlias { get; set; }

        [DataType(DataType.Password)]
        public byte[] UserPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public byte[] UserEmail { get; set; }

        public byte[] UserMap { get; set; }

        public byte[] UserLanguage { get; set; }

        #region relz

        public string CompanyId { get; set; }

        public List<string> ProjectIds { get; set; }

        public List<string> UGroupIds { get; set; }

        #endregion relz
    }

    public class Anth_ProjectModel : Anth_DelEntity
    {
        [Key]
        public string ProjectId { get { return _Id; } set { _Id = value; } }

        public string ProjectGuid { get; set; }

        public byte[] ProjectAlias { get; set; }

        public Anth_UserModel Leader { get; set; }

        public string LeaderId { get; set; }

        public Anth_CompanyModel CompanyOwner { get; set; }

        public string CompanyId { get; set; }

        public bool IsActive { get; set; }
    }
}