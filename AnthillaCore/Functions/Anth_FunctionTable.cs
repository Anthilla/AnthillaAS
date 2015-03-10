using AnthillaCore.Models;
using System;
using System.Collections.Generic;

namespace AnthillaCore.Functions {

    public class Anth_FunctionTable {

        public static List<Anth_FunctionModel> Functions() {
            var table = new List<Anth_FunctionModel>();

            #region AddressModule

            var a001 = new Anth_FunctionModel();
            a001._Id = Guid.NewGuid().ToString();
            a001.FunctionGuid = "EgBwiI0900";
            a001.FunctionAlias = "Address Get All";
            a001.FunctionType = "r";
            table.Add(a001);
            var a002 = new Anth_FunctionModel();
            a002._Id = Guid.NewGuid().ToString();
            a002.FunctionGuid = "EgBwiI0500";
            a002.FunctionAlias = "Address Create";
            a002.FunctionType = "w";
            table.Add(a002);
            var a003 = new Anth_FunctionModel();
            a003._Id = Guid.NewGuid().ToString();
            a003.FunctionGuid = "EgBwiI0800";
            a003.FunctionAlias = "Address Edit";
            a003.FunctionType = "w";
            table.Add(a003);
            var a004 = new Anth_FunctionModel();
            a004._Id = Guid.NewGuid().ToString();
            a004.FunctionGuid = "EgBwiI0700";
            a004.FunctionAlias = "Address Delete";
            a004.FunctionType = "w";
            table.Add(a004);

            #endregion AddressModule

            #region CompanyModule

            var a005 = new Anth_FunctionModel();
            a005._Id = Guid.NewGuid().ToString();
            a005.FunctionGuid = "shnyGU0900";
            a005.FunctionAlias = "Company Get All";
            a005.FunctionType = "r";
            table.Add(a005);
            var a006 = new Anth_FunctionModel();
            a006._Id = Guid.NewGuid().ToString();
            a006.FunctionGuid = "shnyGU0912";
            a006.FunctionAlias = "Company Get By Id";
            a006.FunctionType = "r";
            table.Add(a006);
            var a007 = new Anth_FunctionModel();
            a007._Id = Guid.NewGuid().ToString();
            a007.FunctionGuid = "shnyGU0201";
            a007.FunctionAlias = "Company Check Alias Exist";
            a007.FunctionType = "r";
            table.Add(a007);
            var a008 = new Anth_FunctionModel();
            a008._Id = Guid.NewGuid().ToString();
            a008.FunctionGuid = "shnyGU0500";
            a008.FunctionAlias = "Company Create";
            a008.FunctionType = "w";
            table.Add(a008);
            var a009 = new Anth_FunctionModel();
            a009._Id = Guid.NewGuid().ToString();
            a009.FunctionGuid = "shnyGU0907";
            a009.FunctionAlias = "Company Get By Alias";
            a009.FunctionType = "r";
            table.Add(a009);
            var a010 = new Anth_FunctionModel();
            a010._Id = Guid.NewGuid().ToString();
            a010.FunctionGuid = "shnyGU0102";
            a010.FunctionAlias = "Company Assign Address";
            a010.FunctionType = "w";
            table.Add(a010);
            var a011 = new Anth_FunctionModel();
            a011._Id = Guid.NewGuid().ToString();
            a011.FunctionGuid = "shnyGU1003";
            a011.FunctionAlias = "Company Make Owner";
            a011.FunctionType = "w";
            table.Add(a011);
            var a012 = new Anth_FunctionModel();
            a012._Id = Guid.NewGuid().ToString();
            a012.FunctionGuid = "shnyGU0926";
            a012.FunctionAlias = "Company Get Owner";
            a012.FunctionType = "r";
            table.Add(a012);
            var a013 = new Anth_FunctionModel();
            a013._Id = Guid.NewGuid().ToString();
            a013.FunctionGuid = "shnyGU0800";
            a013.FunctionAlias = "Company Edit";
            a013.FunctionType = "w";
            table.Add(a013);
            var a014 = new Anth_FunctionModel();
            a014._Id = Guid.NewGuid().ToString();
            a014.FunctionGuid = "shnyGU0700";
            a014.FunctionAlias = "Company Delete";
            a014.FunctionType = "w";
            table.Add(a014);

            #endregion CompanyModule

            #region FeedbackModule

            var a015 = new Anth_FunctionModel();
            a015._Id = Guid.NewGuid().ToString();
            a015.FunctionGuid = "gFwhS0900";
            a015.FunctionAlias = "Feedback Get All";
            a015.FunctionType = "r";
            table.Add(a015);
            var a016 = new Anth_FunctionModel();
            a016._Id = Guid.NewGuid().ToString();
            a016.FunctionGuid = "gFwhS0912";
            a016.FunctionAlias = "Feedback Get By Id";
            a016.FunctionType = "r";
            table.Add(a016);
            var a017 = new Anth_FunctionModel();
            a017._Id = Guid.NewGuid().ToString();
            a017.FunctionGuid = "gFwhS0500";
            a017.FunctionAlias = "Feedback Create";
            a017.FunctionType = "w";
            table.Add(a017);
            var a018 = new Anth_FunctionModel();
            a018._Id = Guid.NewGuid().ToString();
            a018.FunctionGuid = "gFwhS0700";
            a018.FunctionAlias = "Feedback Delete";
            a018.FunctionType = "w";
            table.Add(a018);

            #endregion FeedbackModule

            #region FileModule

            var a019 = new Anth_FunctionModel();
            a019._Id = Guid.NewGuid().ToString();
            a019.FunctionGuid = "Y8hkyG0900";
            a019.FunctionAlias = "File Get All";
            a019.FunctionType = "r";
            table.Add(a019);
            var a020 = new Anth_FunctionModel();
            a020._Id = Guid.NewGuid().ToString();
            a020.FunctionGuid = "Y8hkyG0912";
            a020.FunctionAlias = "File Get By Id";
            a020.FunctionType = "r";
            table.Add(a020);
            var a021 = new Anth_FunctionModel();
            a021._Id = Guid.NewGuid().ToString();
            a021.FunctionGuid = "Y8hkyG0907";
            a021.FunctionAlias = "File Get By Alias";
            a021.FunctionType = "r";
            table.Add(a021);
            var a022 = new Anth_FunctionModel();
            a022._Id = Guid.NewGuid().ToString();
            a022.FunctionGuid = "Y8hkyG0920";
            a022.FunctionAlias = "File Get Db Name";
            a022.FunctionType = "r";
            table.Add(a022);
            var a023 = new Anth_FunctionModel();
            a023._Id = Guid.NewGuid().ToString();
            a023.FunctionGuid = "Y8hkyG0927";
            a023.FunctionAlias = "File Get SubPath";
            a023.FunctionType = "r";
            table.Add(a023);
            var a024 = new Anth_FunctionModel();
            a024._Id = Guid.NewGuid().ToString();
            a024.FunctionGuid = "Y8hkyG0922";
            a024.FunctionAlias = "File Get Key";
            a024.FunctionType = "r";
            table.Add(a024);
            var a025 = new Anth_FunctionModel();
            a025._Id = Guid.NewGuid().ToString();
            a025.FunctionGuid = "Y8hkyG0931";
            a025.FunctionAlias = "File Get Vector";
            a025.FunctionType = "r";
            table.Add(a025);
            var a026 = new Anth_FunctionModel();
            a026._Id = Guid.NewGuid().ToString();
            a026.FunctionGuid = "Y8hkyG0101";
            a026.FunctionAlias = "File Add Tags";
            a026.FunctionType = "w";
            table.Add(a026);
            var a027 = new Anth_FunctionModel();
            a027._Id = Guid.NewGuid().ToString();
            a027.FunctionGuid = "Y8hkyG0500";
            a027.FunctionAlias = "File Create";
            a027.FunctionType = "w";
            table.Add(a027);
            var a028 = new Anth_FunctionModel();
            a028._Id = Guid.NewGuid().ToString();
            a028.FunctionGuid = "Y8hkyG0700";
            a028.FunctionAlias = "File Delete";
            a028.FunctionType = "w";
            table.Add(a028);
            var a029 = new Anth_FunctionModel();
            a029._Id = Guid.NewGuid().ToString();
            a029.FunctionGuid = "Y8hkyG0103";
            a029.FunctionAlias = "File Assign Company";
            a029.FunctionType = "w";
            table.Add(a029);
            var a030 = new Anth_FunctionModel();
            a030._Id = Guid.NewGuid().ToString();
            a030.FunctionGuid = "Y8hkyG0107";
            a030.FunctionAlias = "File Assign Project";
            a030.FunctionType = "w";
            table.Add(a030);
            var a031 = new Anth_FunctionModel();
            a031._Id = Guid.NewGuid().ToString();
            a031.FunctionGuid = "Y8hkyG0109";
            a031.FunctionAlias = "File Assign User";
            a031.FunctionType = "w";
            table.Add(a031);

            #endregion FileModule

            #region FilePackModule

            var a032 = new Anth_FunctionModel();
            a032._Id = Guid.NewGuid().ToString();
            a032.FunctionGuid = "EhkSGb0900";
            a032.FunctionAlias = "File Package Get All";
            a032.FunctionType = "r";
            table.Add(a032);
            var a033 = new Anth_FunctionModel();
            a033._Id = Guid.NewGuid().ToString();
            a033.FunctionGuid = "EhkSGb0912";
            a033.FunctionAlias = "File Package Get By Id";
            a033.FunctionType = "r";
            table.Add(a033);
            var a034 = new Anth_FunctionModel();
            a034._Id = Guid.NewGuid().ToString();
            a034.FunctionGuid = "EhkSGb0500";
            a034.FunctionAlias = "File Package Create";
            a034.FunctionType = "w";
            table.Add(a034);
            var a035 = new Anth_FunctionModel();
            a035._Id = Guid.NewGuid().ToString();
            a035.FunctionGuid = "EhkSGb0103";
            a035.FunctionAlias = "File Package Assign Company";
            a035.FunctionType = "w";
            table.Add(a035);
            var a036 = new Anth_FunctionModel();
            a036._Id = Guid.NewGuid().ToString();
            a036.FunctionGuid = "EhkSGb0107";
            a036.FunctionAlias = "File Package Assign Project";
            a036.FunctionType = "w";
            table.Add(a036);
            var a037 = new Anth_FunctionModel();
            a037._Id = Guid.NewGuid().ToString();
            a037.FunctionGuid = "EhkSGb0109";
            a037.FunctionAlias = "File Package Assign User";
            a037.FunctionType = "w";
            table.Add(a037);
            var a038 = new Anth_FunctionModel();
            a038._Id = Guid.NewGuid().ToString();
            a038.FunctionGuid = "EhkSGb0109";
            a038.FunctionAlias = "File Package Add File To";
            a038.FunctionType = "w";
            table.Add(a038);
            var a039 = new Anth_FunctionModel();
            a039._Id = Guid.NewGuid().ToString();
            a039.FunctionGuid = "EhkSGb0109";
            a039.FunctionAlias = "File Package Remove File";
            a039.FunctionType = "w";
            table.Add(a039);

            #endregion FilePackModule

            #region FuncGroupModule

            var a040 = new Anth_FunctionModel();
            a040._Id = Guid.NewGuid().ToString();
            a040.FunctionGuid = "0hnKGI0900";
            a040.FunctionAlias = "Function Group Get All";
            a040.FunctionType = "r";
            table.Add(a040);
            var a041 = new Anth_FunctionModel();
            a041._Id = Guid.NewGuid().ToString();
            a041.FunctionGuid = "0hnKGI0912";
            a041.FunctionAlias = "Function Group Get By Id";
            a041.FunctionType = "r";
            table.Add(a041);
            var a042 = new Anth_FunctionModel();
            a042._Id = Guid.NewGuid().ToString();
            a042.FunctionGuid = "0hnKGI0500";
            a042.FunctionAlias = "Function Group Create";
            a042.FunctionType = "w";
            table.Add(a042);
            var a043 = new Anth_FunctionModel();
            a043._Id = Guid.NewGuid().ToString();
            a043.FunctionGuid = "0hnKGI0800";
            a043.FunctionAlias = "Function Group Edit";
            a043.FunctionType = "w";
            table.Add(a043);
            var a044 = new Anth_FunctionModel();
            a044._Id = Guid.NewGuid().ToString();
            a044.FunctionGuid = "0hnKGI0700";
            a044.FunctionAlias = "Function Group Delete";
            a044.FunctionType = "w";
            table.Add(a044);
            var a045 = new Anth_FunctionModel();
            a045._Id = Guid.NewGuid().ToString();
            a045.FunctionGuid = "0hnKGI0700";
            a045.FunctionAlias = "Function Group Assign Function";
            a045.FunctionType = "w";
            table.Add(a045);

            #endregion FuncGroupModule

            #region FuncModule

            var a046 = new Anth_FunctionModel();
            a046._Id = Guid.NewGuid().ToString();
            a046.FunctionGuid = "lSGbIZ0900";
            a046.FunctionAlias = "Function Get All";
            a046.FunctionType = "r";
            table.Add(a046);
            var a047 = new Anth_FunctionModel();
            a047._Id = Guid.NewGuid().ToString();
            a047.FunctionGuid = "lSGbIZ0912";
            a047.FunctionAlias = "Function Get By Id";
            a047.FunctionType = "r";
            table.Add(a047);
            var a048 = new Anth_FunctionModel();
            a048._Id = Guid.NewGuid().ToString();
            a048.FunctionGuid = "lSGbIZ0912";
            a048.FunctionAlias = "Function Get Value";
            a048.FunctionType = "r";
            table.Add(a048);

            #endregion FuncModule

            #region GroupRelationModule

            var a049 = new Anth_FunctionModel();
            a049._Id = Guid.NewGuid().ToString();
            a049.FunctionGuid = "0hlKGI0900";
            a049.FunctionAlias = "Group Relation Get All";
            a049.FunctionType = "r";
            table.Add(a049);
            var a050 = new Anth_FunctionModel();
            a050._Id = Guid.NewGuid().ToString();
            a050.FunctionGuid = "0hlKGI0912";
            a050.FunctionAlias = "Group Relation Get By Id";
            a050.FunctionType = "r";
            table.Add(a050);
            var a051 = new Anth_FunctionModel();
            a051._Id = Guid.NewGuid().ToString();
            a051.FunctionGuid = "0hlKGI0500";
            a051.FunctionAlias = "Group Relation Create";
            a051.FunctionType = "w";
            table.Add(a051);
            var a052 = new Anth_FunctionModel();
            a052._Id = Guid.NewGuid().ToString();
            a052.FunctionGuid = "0hlKGI0800";
            a052.FunctionAlias = "Group Relation Edit";
            a052.FunctionType = "w";
            table.Add(a052);
            var a053 = new Anth_FunctionModel();
            a053._Id = Guid.NewGuid().ToString();
            a053.FunctionGuid = "0hlKGI0700";
            a053.FunctionAlias = "Group Relation Delete";
            a053.FunctionType = "w";
            table.Add(a053);

            #endregion GroupRelationModule

            #region LoggingModule

            var a054 = new Anth_FunctionModel();
            a054._Id = Guid.NewGuid().ToString();
            a054.FunctionGuid = "chnSAf0900";
            a054.FunctionAlias = "Logs Get All";
            a054.FunctionType = "r";
            table.Add(a054);
            var a055 = new Anth_FunctionModel();
            a055._Id = Guid.NewGuid().ToString();
            a055.FunctionGuid = "chnSAf0912";
            a055.FunctionAlias = "Logs Get By Id";
            a055.FunctionType = "r";
            table.Add(a055);

            #endregion LoggingModule

            #region ProjectModule

            var a056 = new Anth_FunctionModel();
            a056._Id = Guid.NewGuid().ToString();
            a056.FunctionGuid = "IoYChi0900";
            a056.FunctionAlias = "Project Get All";
            a056.FunctionType = "r";
            table.Add(a056);
            var a057 = new Anth_FunctionModel();
            a057._Id = Guid.NewGuid().ToString();
            a057.FunctionGuid = "IoYChi0912";
            a057.FunctionAlias = "Project Get By Id";
            a057.FunctionType = "r";
            table.Add(a057);
            var a058 = new Anth_FunctionModel();
            a058._Id = Guid.NewGuid().ToString();
            a058.FunctionGuid = "IoYChi0907";
            a058.FunctionAlias = "Project Get By Alias";
            a058.FunctionType = "r";
            table.Add(a058);
            var a059 = new Anth_FunctionModel();
            a059._Id = Guid.NewGuid().ToString();
            a059.FunctionGuid = "IoYChi0201";
            a059.FunctionAlias = "Project Check Alias Exist";
            a059.FunctionType = "r";
            table.Add(a059);
            var a060 = new Anth_FunctionModel();
            a060._Id = Guid.NewGuid().ToString();
            a060.FunctionGuid = "IoYChi0500";
            a060.FunctionAlias = "Project Create";
            a060.FunctionType = "w";
            table.Add(a060);
            var a061 = new Anth_FunctionModel();
            a061._Id = Guid.NewGuid().ToString();
            a061.FunctionGuid = "IoYChi0800";
            a061.FunctionAlias = "Project Edit";
            a061.FunctionType = "w";
            table.Add(a061);
            var a062 = new Anth_FunctionModel();
            a062._Id = Guid.NewGuid().ToString();
            a062.FunctionGuid = "IoYChi0700";
            a062.FunctionAlias = "Project Delete";
            a062.FunctionType = "w";
            table.Add(a062);
            var a063 = new Anth_FunctionModel();
            a063._Id = Guid.NewGuid().ToString();
            a063.FunctionGuid = "IoYChi0700";
            a063.FunctionAlias = "Project Assign Project Leader";
            a063.FunctionType = "w";
            table.Add(a063);

            #endregion ProjectModule

            #region TagModule

            var a064 = new Anth_FunctionModel();
            a064._Id = Guid.NewGuid().ToString();
            a064.FunctionGuid = "YUhkSG0900";
            a064.FunctionAlias = "Tag Get All";
            a064.FunctionType = "r";
            table.Add(a064);

            #endregion TagModule

            #region TagPresetModule

            var a065 = new Anth_FunctionModel();
            a065._Id = Guid.NewGuid().ToString();
            a065.FunctionGuid = "SGUoBv0900";
            a065.FunctionAlias = "Tag Preset Get All";
            a065.FunctionType = "r";
            table.Add(a065);
            var a066 = new Anth_FunctionModel();
            a066._Id = Guid.NewGuid().ToString();
            a066.FunctionGuid = "SGUoBv0913";
            a066.FunctionAlias = "Tag Preset Get By Model";
            a066.FunctionType = "r";
            table.Add(a066);
            var a067 = new Anth_FunctionModel();
            a067._Id = Guid.NewGuid().ToString();
            a067.FunctionGuid = "SGUoBv0912";
            a067.FunctionAlias = "Tag Preset Get By Id";
            a067.FunctionType = "r";
            table.Add(a067);
            var a068 = new Anth_FunctionModel();
            a068._Id = Guid.NewGuid().ToString();
            a068.FunctionGuid = "SGUoBv0502";
            a068.FunctionAlias = "Tag Preset Create Model";
            a068.FunctionType = "w";
            table.Add(a068);

            #endregion TagPresetModule

            #region TimingModule

            var a069 = new Anth_FunctionModel();
            a069._Id = Guid.NewGuid().ToString();
            a069.FunctionGuid = "SgD4jy0900";
            a069.FunctionAlias = "Event Get All";
            a069.FunctionType = "r";
            table.Add(a069);
            var a070 = new Anth_FunctionModel();
            a070._Id = Guid.NewGuid().ToString();
            a070.FunctionGuid = "SgD4jy0912";
            a070.FunctionAlias = "Event Get By Id";
            a070.FunctionType = "r";
            table.Add(a070);
            var a071 = new Anth_FunctionModel();
            a071._Id = Guid.NewGuid().ToString();
            a071.FunctionGuid = "SgD4jy0909";
            a071.FunctionAlias = "Event Get By Date";
            a071.FunctionType = "r";
            table.Add(a071);
            var a072 = new Anth_FunctionModel();
            a072._Id = Guid.NewGuid().ToString();
            a072.FunctionGuid = "SgD4jy0910";
            a072.FunctionAlias = "Event Get By Date Hour";
            a072.FunctionType = "r";
            table.Add(a072);
            var a073 = new Anth_FunctionModel();
            a073._Id = Guid.NewGuid().ToString();
            a073.FunctionGuid = "SgD4jy0500";
            a073.FunctionAlias = "Event Create";
            a073.FunctionType = "w";
            table.Add(a073);
            var a074 = new Anth_FunctionModel();
            a074._Id = Guid.NewGuid().ToString();
            a074.FunctionGuid = "SgD4jy0800";
            a074.FunctionAlias = "Event Edit";
            a074.FunctionType = "w";
            table.Add(a074);
            var a075 = new Anth_FunctionModel();
            a075._Id = Guid.NewGuid().ToString();
            a075.FunctionGuid = "SgD4jy0700";
            a075.FunctionAlias = "Event Delete";
            a075.FunctionType = "w";
            table.Add(a075);

            #endregion TimingModule

            #region UserGroupModule

            var a076 = new Anth_FunctionModel();
            a076._Id = Guid.NewGuid().ToString();
            a076.FunctionGuid = "CYD6SL0900";
            a076.FunctionAlias = "User Group Get All";
            a076.FunctionType = "r";
            table.Add(a076);
            var a077 = new Anth_FunctionModel();
            a077._Id = Guid.NewGuid().ToString();
            a077.FunctionGuid = "CYD6SL0912";
            a077.FunctionAlias = "User Group Get By Id";
            a077.FunctionType = "r";
            table.Add(a077);
            var a078 = new Anth_FunctionModel();
            a078._Id = Guid.NewGuid().ToString();
            a078.FunctionGuid = "CYD6SL0500";
            a078.FunctionAlias = "User Group Create";
            a078.FunctionType = "w";
            table.Add(a078);
            var a079 = new Anth_FunctionModel();
            a079._Id = Guid.NewGuid().ToString();
            a079.FunctionGuid = "CYD6SL0800";
            a079.FunctionAlias = "User Group Edit";
            a079.FunctionType = "w";
            table.Add(a079);
            var a080 = new Anth_FunctionModel();
            a080._Id = Guid.NewGuid().ToString();
            a080.FunctionGuid = "CYD6SL0700";
            a080.FunctionAlias = "User Group Delete";
            a080.FunctionType = "w";
            table.Add(a080);

            #endregion UserGroupModule

            #region UserModule

            var a081 = new Anth_FunctionModel();
            a081._Id = Guid.NewGuid().ToString();
            a081.FunctionGuid = "2IoZUh0900";
            a081.FunctionAlias = "User Get All";
            a081.FunctionType = "r";
            table.Add(a081);
            var a082 = new Anth_FunctionModel();
            a082._Id = Guid.NewGuid().ToString();
            a082.FunctionGuid = "2IoZUh0912";
            a082.FunctionAlias = "User Get By Id";
            a082.FunctionType = "r";
            table.Add(a082);
            var a083 = new Anth_FunctionModel();
            a083._Id = Guid.NewGuid().ToString();
            a083.FunctionGuid = "2IoZUh0201";
            a083.FunctionAlias = "User Check Alias Exist";
            a083.FunctionType = "r";
            table.Add(a083);
            var a084 = new Anth_FunctionModel();
            a084._Id = Guid.NewGuid().ToString();
            a084.FunctionGuid = "2IoZUh0500";
            a084.FunctionAlias = "User Create";
            a084.FunctionType = "w";
            table.Add(a084);
            var a085 = new Anth_FunctionModel();
            a085._Id = Guid.NewGuid().ToString();
            a085.FunctionGuid = "2IoZUh0914";
            a085.FunctionAlias = "User Get By Name";
            a085.FunctionType = "r";
            table.Add(a085);
            var a086 = new Anth_FunctionModel();
            a086._Id = Guid.NewGuid().ToString();
            a086.FunctionGuid = "2IoZUh0800";
            a086.FunctionAlias = "User Edit";
            a086.FunctionType = "w";
            table.Add(a086);
            var a087 = new Anth_FunctionModel();
            a087._Id = Guid.NewGuid().ToString();
            a087.FunctionGuid = "2IoZUh0700";
            a087.FunctionAlias = "User Delete";
            a087.FunctionType = "w";
            table.Add(a087);
            var a088 = new Anth_FunctionModel();
            a088._Id = Guid.NewGuid().ToString();
            a088.FunctionGuid = "2IoZUh0103";
            a088.FunctionAlias = "User Assign Company";
            a088.FunctionType = "w";
            table.Add(a088);
            var a089 = new Anth_FunctionModel();
            a089._Id = Guid.NewGuid().ToString();
            a089.FunctionGuid = "2IoZUh0105";
            a089.FunctionAlias = "User Assign Group";
            a089.FunctionType = "w";
            table.Add(a089);
            var a090 = new Anth_FunctionModel();
            a090._Id = Guid.NewGuid().ToString();
            a090.FunctionGuid = "2IoZUh0107";
            a090.FunctionAlias = "User Assign Project";
            a090.FunctionType = "w";
            table.Add(a090);
            var a091 = new Anth_FunctionModel();
            a091._Id = Guid.NewGuid().ToString();
            a091.FunctionGuid = "2IoZUh1005";
            a091.FunctionAlias = "User Reset Password";
            a091.FunctionType = "w";
            table.Add(a091);
            var a092 = new Anth_FunctionModel();
            a092._Id = Guid.NewGuid().ToString();
            a092.FunctionGuid = "2IoZUh0300";
            a092.FunctionAlias = "User Config Mail Setting";
            a092.FunctionType = "w";
            table.Add(a092);
            var a093 = new Anth_FunctionModel();
            a093._Id = Guid.NewGuid().ToString();
            a093.FunctionGuid = "2IoZUh0925";
            a093.FunctionAlias = "User Get Mail Setting By Id";
            a093.FunctionType = "r";
            table.Add(a093);

            #endregion UserModule

            #region SystemModule

            var a094 = new Anth_FunctionModel();
            a094._Id = Guid.NewGuid().ToString();
            a094.FunctionGuid = "A4hKGD1011";
            a094.FunctionAlias = "System Set Root";
            a094.FunctionType = "w";
            table.Add(a094);
            var a095 = new Anth_FunctionModel();
            a095._Id = Guid.NewGuid().ToString();
            a095.FunctionGuid = "EwkoMA2008";
            a095.FunctionAlias = "System Get Config";
            a095.FunctionType = "r";
            table.Add(a095);
            var a096 = new Anth_FunctionModel();
            a096._Id = Guid.NewGuid().ToString();
            a096.FunctionGuid = "EwkoMA2003";
            a096.FunctionAlias = "System Set Config";
            a096.FunctionType = "w";
            table.Add(a096);
            var a097 = new Anth_FunctionModel();
            a097._Id = Guid.NewGuid().ToString();
            a097.FunctionGuid = "EwkoMA2004";
            a097.FunctionAlias = "System Set Back port";
            a097.FunctionType = "w";
            table.Add(a097);
            var a098 = new Anth_FunctionModel();
            a098._Id = Guid.NewGuid().ToString();
            a098.FunctionGuid = "EwkoMA2005";
            a098.FunctionAlias = "System Get Back port";
            a098.FunctionType = "r";
            table.Add(a098);
            var a099 = new Anth_FunctionModel();
            a099._Id = Guid.NewGuid().ToString();
            a099.FunctionGuid = "EwkoMA2006";
            a099.FunctionAlias = "System Set Front port";
            a099.FunctionType = "w";
            table.Add(a099);
            var a100 = new Anth_FunctionModel();
            a100._Id = Guid.NewGuid().ToString();
            a100.FunctionGuid = "EwkoMA2007";
            a100.FunctionAlias = "System Get Front port";
            a100.FunctionType = "r";
            table.Add(a100);

            #endregion SystemModule

            #region NamingModule

            var a101 = new Anth_FunctionModel();
            a101._Id = Guid.NewGuid().ToString();
            a101.FunctionGuid = "GXIBNL0900";
            a101.FunctionAlias = "Naming Get All";
            a101.FunctionType = "r";
            table.Add(a101);
            var a102 = new Anth_FunctionModel();
            a102._Id = Guid.NewGuid().ToString();
            a102.FunctionGuid = "GXIBNL0932";
            a102.FunctionAlias = "Naming Get By Type";
            a102.FunctionType = "r";
            table.Add(a102);
            var a103 = new Anth_FunctionModel();
            a103._Id = Guid.NewGuid().ToString();
            a103.FunctionGuid = "GXIBNL1012";
            a103.FunctionAlias = "Naming Set For Company";
            a103.FunctionType = "w";
            table.Add(a103);
            var a104 = new Anth_FunctionModel();
            a104._Id = Guid.NewGuid().ToString();
            a104.FunctionGuid = "GXIBNL1013";
            a104.FunctionAlias = "Naming Set For User";
            a104.FunctionType = "w";
            table.Add(a104);
            var a105 = new Anth_FunctionModel();
            a105._Id = Guid.NewGuid().ToString();
            a105.FunctionGuid = "GXIBNL1014";
            a105.FunctionAlias = "Naming Set For Project";
            a105.FunctionType = "w";
            table.Add(a105);
            var a106 = new Anth_FunctionModel();
            a106._Id = Guid.NewGuid().ToString();
            a106.FunctionGuid = "GXIBNL1015";
            a106.FunctionAlias = "Naming Set For Group";
            a106.FunctionType = "w";
            table.Add(a106);
            var a107 = new Anth_FunctionModel();
            a107._Id = Guid.NewGuid().ToString();
            a107.FunctionGuid = "GXIBNL1016";
            a107.FunctionAlias = "Naming Set For Tag";
            a107.FunctionType = "w";
            table.Add(a107);
            var a108 = new Anth_FunctionModel();
            a108._Id = Guid.NewGuid().ToString();
            a108.FunctionGuid = "GXIBNL1017";
            a108.FunctionAlias = "Naming Set For File";
            a108.FunctionType = "w";
            table.Add(a108);
            var a108a = new Anth_FunctionModel();
            a108a._Id = Guid.NewGuid().ToString();
            a108a.FunctionGuid = "GXIBNL1018";
            a108a.FunctionAlias = "Naming Set";
            a108a.FunctionType = "w";
            table.Add(a108a);
            var a109 = new Anth_FunctionModel();
            a109._Id = Guid.NewGuid().ToString();
            a109.FunctionGuid = "GXIBNL0700";
            a109.FunctionAlias = "Naming Delete";
            a109.FunctionType = "w";
            table.Add(a109);
            var a110 = new Anth_FunctionModel();
            a110._Id = Guid.NewGuid().ToString();
            a110.FunctionGuid = "GXIBNL1018";
            a110.FunctionAlias = "Naming Create User Alias";
            a110.FunctionType = "w";
            table.Add(a110);

            #endregion NamingModule

            #region MailModule

            var a111 = new Anth_FunctionModel();
            a111._Id = Guid.NewGuid().ToString();
            a111.FunctionGuid = "U2JIZC0900";
            a111.FunctionAlias = "Mail Config Get All";
            a111.FunctionType = "r";
            table.Add(a111);
            var a112 = new Anth_FunctionModel();
            a112._Id = Guid.NewGuid().ToString();
            a112.FunctionGuid = "U2JIZC0912";
            a112.FunctionAlias = "Mail Config Get By Id";
            a112.FunctionType = "r";
            table.Add(a112);
            var a113 = new Anth_FunctionModel();
            a113._Id = Guid.NewGuid().ToString();
            a113.FunctionGuid = "U2JIZC0500";
            a113.FunctionAlias = "Mail Config Create";
            a113.FunctionType = "w";
            table.Add(a113);
            var a114 = new Anth_FunctionModel();
            a114._Id = Guid.NewGuid().ToString();
            a114.FunctionGuid = "U2JIZC0503";
            a114.FunctionAlias = "Mail Config Create Setting";
            a114.FunctionType = "w";
            table.Add(a114);
            var a115 = new Anth_FunctionModel();
            a115._Id = Guid.NewGuid().ToString();
            a115.FunctionGuid = "U2JIZC0700";
            a115.FunctionAlias = "Mail Config Delete";
            a115.FunctionType = "w";
            table.Add(a115);
            var a116 = new Anth_FunctionModel();
            a116._Id = Guid.NewGuid().ToString();
            a116.FunctionGuid = "U2JIZC3000";
            a116.FunctionAlias = "Mail Imap Download";
            a116.FunctionType = "w";
            table.Add(a116);
            var a117 = new Anth_FunctionModel();
            a117._Id = Guid.NewGuid().ToString();
            a117.FunctionGuid = "U2JIZC3001";
            a117.FunctionAlias = "Mail Imap Login";
            a117.FunctionType = "w";
            table.Add(a117);
            var a118 = new Anth_FunctionModel();
            a118._Id = Guid.NewGuid().ToString();
            a118.FunctionGuid = "U2JIZC3002";
            a118.FunctionAlias = "Mail Imap Get All Mail Mapped";
            a118.FunctionType = "r";
            table.Add(a118);
            var a119 = new Anth_FunctionModel();
            a119._Id = Guid.NewGuid().ToString();
            a119.FunctionGuid = "U2JIZC3003";
            a119.FunctionAlias = "Mail Imap Get Mailbox List";
            a119.FunctionType = "r";
            table.Add(a119);
            var a120 = new Anth_FunctionModel();
            a120._Id = Guid.NewGuid().ToString();
            a120.FunctionGuid = "U2JIZC3004";
            a120.FunctionAlias = "Mail Imap Get Mail In Mailbox Mapped";
            a120.FunctionType = "r";
            table.Add(a120);
            var a121 = new Anth_FunctionModel();
            a121._Id = Guid.NewGuid().ToString();
            a121.FunctionGuid = "U2JIZC3005";
            a121.FunctionAlias = "Mail Imap Get All";
            a121.FunctionType = "r";
            table.Add(a121);
            var a122 = new Anth_FunctionModel();
            a122._Id = Guid.NewGuid().ToString();
            a122.FunctionGuid = "U2JIZC3006";
            a122.FunctionAlias = "Mail Smtp Send Mail";
            a122.FunctionType = "w";
            table.Add(a122);

            #endregion MailModule

            return table;
        }
    }
}