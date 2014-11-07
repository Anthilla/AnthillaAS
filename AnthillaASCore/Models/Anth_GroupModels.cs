using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnthillaASCore.Models
{
    public class Anth_FunctionGroupModel : Anth_DelEntity
    {
        [Key]
        public string FunctionsGroupId { get { return _Id; } set { _Id = value; } }

        public string FunctionsGroupGuid { get; set; }

        public byte[] FunctionsGroupAlias { get; set; }

        public List<string> FunctionList { get; set; }
    }

    public class Anth_UserGroupModel : Anth_DelEntity
    {
        [Key]
        public string UsersGroupId { get { return _Id; } set { _Id = value; } }

        public string UsersGroupGuid { get; set; }

        public byte[] UsersGroupAlias { get; set; }

        public string UsersGroupEvent { get; set; }

        public string UsersGroupTrigger { get; set; }

        public int UsersGroupHierarchyIndex { get; set; }

        public int UsersGroupNestedIndex { get; set; }
    }

    public class Anth_AclModel : Anth_DelEntity
    {
        [Key]
        public string AclId { get { return _Id; } set { _Id = value; } }

        public byte[] AclAlias { get; set; }

        public string AclGuid { get; set; }

        public string AclUserGroupGuid { get; set; }

        public string AclFunctionGroupGuid { get; set; }
    }

    public class Anth_GroupRelationModel : Anth_DelEntity
    {
        [Key]
        public string GroupRelationId { get { return _Id; } set { _Id = value; } }

        public string GroupRelationGuid { get; set; }

        public string FunctionsRelGuid { get; set; }

        public string UsersRelGuid { get; set; }
    }
}