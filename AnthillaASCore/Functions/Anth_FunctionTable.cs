using System;
using System.Collections.Generic;
using AnthillaASCore.Models;
using AnthillaASCore.Security;

namespace AnthillaASCore.Functions
{
    public class Anth_FunctionTable
    {
        public List<Anth_FunctionModel> Functions()
        {
            var table = new List<Anth_FunctionModel>();

            #region company

            var cm001 = new Anth_FunctionModel();
            cm001._Id = Guid.NewGuid().ToString();
            cm001.FunctionGuid = "AE8DA320-FBAC-44FA-A928-6F2AA274EAD3";
            cm001.StorIndexN2 = CoreSecurity.CreateRandomKey();
            cm001.StorIndexN1 = CoreSecurity.CreateRandomVector();
            cm001.FunctionAlias = "Access company page";
            cm001.FunctionValue = "/anthilla/company/getall";
            table.Add(cm001);
            var cm002 = new Anth_FunctionModel();
            cm002._Id = Guid.NewGuid().ToString();
            cm002.FunctionGuid = "5C052A98-C2A1-477A-BFEE-0D4EC6EB7888";
            cm002.StorIndexN2 = CoreSecurity.CreateRandomKey();
            cm002.StorIndexN1 = CoreSecurity.CreateRandomVector();
            cm002.FunctionAlias = "Create company";
            cm002.FunctionValue = "/anthilla/company/create";
            table.Add(cm002);
            var cm003 = new Anth_FunctionModel();
            cm003._Id = Guid.NewGuid().ToString();
            cm003.FunctionGuid = "B22A319B-897C-476A-9951-B905CFBFE154";
            cm003.StorIndexN2 = CoreSecurity.CreateRandomKey();
            cm003.StorIndexN1 = CoreSecurity.CreateRandomVector();
            cm003.FunctionAlias = "Edit company";
            cm003.FunctionValue = "/anthilla/company/edit";
            table.Add(cm003);
            var cm004 = new Anth_FunctionModel();
            cm004._Id = Guid.NewGuid().ToString();
            cm004.FunctionGuid = "40646EFF-A7B1-43AE-8EF4-D481CCDB3A79";
            cm004.StorIndexN2 = CoreSecurity.CreateRandomKey();
            cm004.StorIndexN1 = CoreSecurity.CreateRandomVector();
            cm004.FunctionAlias = "Delete company";
            cm004.FunctionValue = "/anthilla/company/delete";
            table.Add(cm004);
            var cm005 = new Anth_FunctionModel();
            cm005._Id = Guid.NewGuid().ToString();
            cm005.FunctionGuid = "4E23C6EB-4EDB-42D7-886E-3649176E01C5";
            cm005.StorIndexN2 = CoreSecurity.CreateRandomKey();
            cm005.StorIndexN1 = CoreSecurity.CreateRandomVector();
            cm005.FunctionAlias = "Detail company";
            cm005.FunctionValue = "/anthilla/company/getbyid";
            table.Add(cm005);

            #endregion company

            #region user

            var us001 = new Anth_FunctionModel();
            us001._Id = Guid.NewGuid().ToString();
            us001.FunctionGuid = "306D051C-95EA-4B6B-B25F-CC383A11F406";
            us001.StorIndexN2 = CoreSecurity.CreateRandomKey();
            us001.StorIndexN1 = CoreSecurity.CreateRandomVector();
            us001.FunctionAlias = "Access user page";
            us001.FunctionValue = "/anthilla/user/getall";
            table.Add(us001);
            var us002 = new Anth_FunctionModel();
            us002._Id = Guid.NewGuid().ToString();
            us002.FunctionGuid = "152C155E-C782-457C-BDEC-EA35D8AB663C";
            us002.StorIndexN2 = CoreSecurity.CreateRandomKey();
            us002.StorIndexN1 = CoreSecurity.CreateRandomVector();
            us002.FunctionAlias = "Create user";
            us002.FunctionValue = "/anthilla/user/create";
            table.Add(us002);
            var us003 = new Anth_FunctionModel();
            us003._Id = Guid.NewGuid().ToString();
            us003.FunctionGuid = "83CBB77A-6D72-4656-9451-0400651392F5";
            us003.StorIndexN2 = CoreSecurity.CreateRandomKey();
            us003.StorIndexN1 = CoreSecurity.CreateRandomVector();
            us003.FunctionAlias = "Edit user";
            us003.FunctionValue = "/anthilla/user/edit";
            table.Add(us003);
            var us004 = new Anth_FunctionModel();
            us004._Id = Guid.NewGuid().ToString();
            us004.FunctionGuid = "3F8002C6-62CF-485B-A940-29C28EEA2242";
            us004.StorIndexN2 = CoreSecurity.CreateRandomKey();
            us004.StorIndexN1 = CoreSecurity.CreateRandomVector();
            us004.FunctionAlias = "Delete user";
            us004.FunctionValue = "/anthilla/user/delete";
            table.Add(us004);
            var us005 = new Anth_FunctionModel();
            us005._Id = Guid.NewGuid().ToString();
            us005.FunctionGuid = "2592D016-AE4C-4A0C-8712-A9B11ABFAA0E";
            us005.StorIndexN2 = CoreSecurity.CreateRandomKey();
            us005.StorIndexN1 = CoreSecurity.CreateRandomVector();
            us005.FunctionAlias = "Detail user";
            us005.FunctionValue = "/anthilla/user/getbyid";
            table.Add(us005);

            #endregion user

            #region project

            var pj001 = new Anth_FunctionModel();
            pj001._Id = Guid.NewGuid().ToString();
            pj001.FunctionGuid = "E5F35FB8-5626-4A8B-B388-2D9AC920E226";
            pj001.StorIndexN2 = CoreSecurity.CreateRandomKey();
            pj001.StorIndexN1 = CoreSecurity.CreateRandomVector();
            pj001.FunctionAlias = "Access project page";
            pj001.FunctionValue = "/anthilla/project/getall";
            table.Add(pj001);
            var pj002 = new Anth_FunctionModel();
            pj002._Id = Guid.NewGuid().ToString();
            pj002.FunctionGuid = "6B31B36A-487F-4633-911A-5A733269E0C6";
            pj002.StorIndexN2 = CoreSecurity.CreateRandomKey();
            pj002.StorIndexN1 = CoreSecurity.CreateRandomVector();
            pj002.FunctionAlias = "Create project";
            pj002.FunctionValue = "/anthilla/project/create";
            table.Add(pj002);
            var pj003 = new Anth_FunctionModel();
            pj003._Id = Guid.NewGuid().ToString();
            pj003.FunctionGuid = "4E2B3C29-DA9A-44E8-B6A7-A1AF8B2AC6F4";
            pj003.StorIndexN2 = CoreSecurity.CreateRandomKey();
            pj003.StorIndexN1 = CoreSecurity.CreateRandomVector();
            pj003.FunctionAlias = "Edit project";
            pj003.FunctionValue = "/anthilla/project/edit";
            table.Add(pj003);
            var pj004 = new Anth_FunctionModel();
            pj004._Id = Guid.NewGuid().ToString();
            pj004.FunctionGuid = "CA0288FB-069B-4EFF-80F2-022B21953EA1";
            pj004.StorIndexN2 = CoreSecurity.CreateRandomKey();
            pj004.StorIndexN1 = CoreSecurity.CreateRandomVector();
            pj004.FunctionAlias = "Delete project";
            pj004.FunctionValue = "/anthilla/project/delete";
            table.Add(pj004);
            var pj005 = new Anth_FunctionModel();
            pj005._Id = Guid.NewGuid().ToString();
            pj005.FunctionGuid = "5DC55AEA-5B91-4646-B810-4C53BFDBBFEE";
            pj005.StorIndexN2 = CoreSecurity.CreateRandomKey();
            pj005.StorIndexN1 = CoreSecurity.CreateRandomVector();
            pj005.FunctionAlias = "Detail project";
            pj005.FunctionValue = "/anthilla/project/getbyid";
            table.Add(pj005);

            #endregion project

            return table;
        }
    }
}