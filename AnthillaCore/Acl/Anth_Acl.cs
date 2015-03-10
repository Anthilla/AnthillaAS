using AnthillaCore.Models;
using AnthillaCore.Repository;
using System.Collections.Generic;

namespace AnthillaCore.Acl {

    public class Anth_Acl {
        private Anth_UserRepository userRepo = new Anth_UserRepository();
        private Anth_FunctionGroupRepository funcGroupRepo = new Anth_FunctionGroupRepository();
        private Anth_GroupRelationRepository groupRelRepo = new Anth_GroupRelationRepository();

        public bool CheckUser(string USERGUID, string FUNCTIONGUID) {
            bool b;
            string userGuid = USERGUID;
            string functionGuid = FUNCTIONGUID;

            List<Anth_Dump> insiderList = userRepo.GetInsiders();
            List<string> guidlist = new List<string>(); ;
            foreach (var inside in insiderList) {
                var g = inside.AnthillaGuid;
                guidlist.Add(g);
            }
            Anth_Dump user = userRepo.GetById(userGuid);
            List<string> userUserGroups = user.AnthillaUserGroupIds;
            if (!guidlist.Contains(user.AnthillaGuid)) {
                return false;
            }

            List<Anth_Dump> groupRelationList = new List<Anth_Dump>();
            foreach (string ug in userUserGroups) {
                List<Anth_Dump> groupRelations = groupRelRepo.GetByUserGroup(ug);
                foreach (Anth_Dump gr in groupRelations) {
                    groupRelationList.Add(gr);
                }
            }

            List<Anth_Dump> functionGroupList = new List<Anth_Dump>();
            foreach (var r in groupRelationList) {
                var fg = r.AnthillaFunctionGroupGuid;
                var funcGroup = funcGroupRepo.GetById(fg);
                functionGroupList.Add(funcGroup);
            }

            List<string> functionList = new List<string>();
            foreach (var fu in functionGroupList) {
                List<string> fl = fu.AnthillaFunctionGuids;
                foreach (var f in fl) {
                    functionList.Add(f);
                }
            }

            if (functionList.Contains(functionGuid)) {
                b = true;
            }
            else {
                b = false;
            }

            return b;
        }
    }
}