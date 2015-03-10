using AnthillaCore.Functions;
using AnthillaCore.Models;
using AnthillaCore.Repository;
using System.Collections.Generic;

namespace AnthillaCore.Configuration {

    public class Anth_DefaultEntries {
        private Anth_UserGroupRepository ugrpRepo = new Anth_UserGroupRepository();
        private Anth_FunctionRepository funcRepo = new Anth_FunctionRepository();
        private Anth_FunctionGroupRepository fungrpRepo = new Anth_FunctionGroupRepository();
        private Anth_GroupRelationRepository relRepo = new Anth_GroupRelationRepository();

        private Anth_UserGroupModel[] DefaultUserGroup() {
            var ugrpList = new List<Anth_UserGroupModel>();
            var tags = "default-user-group";

            var masterGroup = ugrpRepo.Create("00000000-u000-0000-0000-000000000500", "Master", tags);
            var adminGroup = ugrpRepo.Create("00000000-u000-0000-0000-000000001000", "Administrator", tags);
            var poweruserGroup = ugrpRepo.Create("00000000-u000-0000-0000-000000001001", "PowerUser", tags);
            var userGroup = ugrpRepo.Create("00000000-u000-0000-0000-000000001002", "User", tags);
            var guestGroup = ugrpRepo.Create("00000000-u000-9999-0000-000000000000", "Guest", tags);

            ugrpList.Add(masterGroup);
            ugrpList.Add(adminGroup);
            ugrpList.Add(poweruserGroup);
            ugrpList.Add(userGroup);
            ugrpList.Add(guestGroup);

            var array = ugrpList.ToArray();
            return array;
        }

        private Anth_FunctionGroupModel[] DefaultFuncGroup() {
            var fgrpList = new List<Anth_FunctionGroupModel>();
            var tags = "default-func-group";

            var masterGroup = fungrpRepo.Create("00000000-f000-0000-0000-000000000500", "Master", tags);
            var adminGroup = fungrpRepo.Create("00000000-f000-0000-0000-000000001000", "Administrator", tags);
            var poweruserGroup = fungrpRepo.Create("00000000-f000-0000-0000-000000001001", "PowerUser", tags);
            var userGroup = fungrpRepo.Create("00000000-f000-0000-0000-000000001002", "User", tags);
            var guestGroup = fungrpRepo.Create("00000000-f000-9999-0000-000000000000", "Guest", tags);

            fgrpList.Add(masterGroup);
            fgrpList.Add(adminGroup);
            fgrpList.Add(poweruserGroup);
            fgrpList.Add(userGroup);
            fgrpList.Add(guestGroup);

            var funcTable = Anth_FunctionTable.Functions();
            var funcTableRead = funcRepo.GetAllRead();
            //var funcTableWrite = funcRepo.GetAllWrite();

            foreach (var f in funcTable) {
                fungrpRepo.AssignFunction("00000000-f000-0000-0000-000000000500", f.FunctionGuid);
            }

            foreach (var f in funcTable) {
                fungrpRepo.AssignFunction("00000000-f000-0000-0000-000000001000", f.FunctionGuid);
            }

            foreach (var f in funcTableRead) {
                fungrpRepo.AssignFunction("00000000-f000-0000-0000-000000001001", f.AnthillaGuid);
            }

            foreach (var f in funcTableRead) {
                fungrpRepo.AssignFunction("00000000-f000-0000-0000-000000001002", f.AnthillaGuid);
            }

            var array = fgrpList.ToArray();
            return array;
        }

        public void CreateDefault() {
            var ugrpTable = DefaultUserGroup();
            var fgrpTable = DefaultFuncGroup();

            for (var i = 0; i < 5; i++) {
                relRepo.CreateV(ugrpTable[i].UsersGroupGuid, fgrpTable[i].FunctionsGroupGuid);
            }
        }
    }
}