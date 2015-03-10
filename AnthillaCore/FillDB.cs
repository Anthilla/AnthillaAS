using AnthillaCore.Functions;
using AnthillaCore.Repository;
using System;

namespace AnthillaCore {

    public class FillDBWith : FillDBCore {
        private Anth_UserRepository userRepo = new Anth_UserRepository();
        private Anth_CompanyRepository compRepo = new Anth_CompanyRepository();
        private Anth_ProjectRepository projRepo = new Anth_ProjectRepository();
        private Anth_FunctionRepository funpRepo = new Anth_FunctionRepository();
        private Anth_FunctionGroupRepository fungrpRepo = new Anth_FunctionGroupRepository();
        private Anth_GroupRelationRepository relRepo = new Anth_GroupRelationRepository();

        public void ALL() {
            var imps = Impressionisti();
            var libs = Liberty();

            var gams = Giocattoli();
            var choc = Cioccolato();

            var plns = AereiX();
            var invs = Invenzioni();

            var grps = Gruppi();

            var frps = FunctionGroups();
            var funcTable = Anth_FunctionTable.Functions();
            var funcTableRead = funpRepo.GetAllRead();
            var funcTableWrite = funpRepo.GetAllWrite();

            #region datacreation

            Random rnd = new Random();
            foreach (var impUser in imps) {
                userRepo.AssignCompany(impUser.UserGuid, gams[rnd.Next(0, gams.Length)].CompanyGuid);
                userRepo.AssignProject(impUser.UserGuid, plns[rnd.Next(0, plns.Length)].ProjectGuid);
                userRepo.AssignProject(impUser.UserGuid, plns[rnd.Next(0, plns.Length)].ProjectGuid);
                userRepo.AssignGroup(impUser.UserGuid, grps[rnd.Next(0, grps.Length)].UsersGroupGuid);
            }

            foreach (var libUser in libs) {
                userRepo.AssignCompany(libUser.UserGuid, choc[rnd.Next(0, choc.Length)].CompanyGuid);
                userRepo.AssignProject(libUser.UserGuid, invs[rnd.Next(0, invs.Length)].ProjectGuid);
                userRepo.AssignProject(libUser.UserGuid, invs[rnd.Next(0, invs.Length)].ProjectGuid);
                userRepo.AssignGroup(libUser.UserGuid, grps[rnd.Next(0, grps.Length)].UsersGroupGuid);
            }

            foreach (var pl in plns) {
                projRepo.AssignPLeader(pl.ProjectGuid, imps[rnd.Next(0, imps.Length)].UserGuid);
            }

            foreach (var pl in invs) {
                projRepo.AssignPLeader(pl.ProjectGuid, libs[rnd.Next(0, libs.Length)].UserGuid);
            }

            var f = frps[0];
            var fguid = f.FunctionsGroupGuid;
            foreach (var ff in funcTable) {
                fungrpRepo.AssignFunction(fguid, ff.FunctionGuid);
            }

            var fa = frps[1];
            var faguid = fa.FunctionsGroupGuid;
            foreach (var fff in funcTable) {
                fungrpRepo.AssignFunction(faguid, fff.FunctionGuid);
            }

            var fs = frps[2];
            var fsguid = fs.FunctionsGroupGuid;
            foreach (var ffff in funcTableWrite) {
                fungrpRepo.AssignFunction(faguid, ffff.AnthillaGuid);
            }

            var fg = frps[3];
            var fgguid = fg.FunctionsGroupGuid;
            foreach (var ffff in funcTableRead) {
                fungrpRepo.AssignFunction(faguid, ffff.AnthillaGuid);
            }

            #endregion datacreation

            relRepo.Create(grps[0].UsersGroupGuid, frps[0].FunctionsGroupGuid);
            relRepo.Create(grps[1].UsersGroupGuid, frps[1].FunctionsGroupGuid);
            relRepo.Create(grps[2].UsersGroupGuid, frps[2].FunctionsGroupGuid);
            relRepo.Create(grps[3].UsersGroupGuid, frps[3].FunctionsGroupGuid);
            relRepo.Create(grps[4].UsersGroupGuid, frps[3].FunctionsGroupGuid);
        }

        public bool CheckFDb() {
            return compRepo.CheckAliasExist("Venchi");
        }
    }
}