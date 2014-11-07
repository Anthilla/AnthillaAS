using System;
using System.Collections.Generic;
using System.Linq;
using AnthillaASCore.Logging;
using AnthillaASCore.Security;

namespace AnthillaASCore.Configuration
{
    public class Anth_PathConfig
    {
        public void SetRoot(string path)
        {
            root root = new root();
            root.ADt = DateTime.Now.ToString();
            root.Aned = "n";
            root.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            root.IsDeleted = false;
            root.StorIndexN2 = CoreSecurity.CreateRandomKey();
            root.StorIndexN1 = CoreSecurity.CreateRandomVector();
            root.RootId = Guid.NewGuid().ToString();
            root.RootGuid = "root";
            root.RootAlias = "root";
            //root.RootPath = "D:\\Home\\zandam\\Documents\\Visual Studio 2012\\Projects\\Anthilla\\AnthillaTest\\App_Data\\AnthillaFileRepository\\";
            root.RootPath = path;

            #region Nulls

            root.Tags = new List<string>() { };
            root.TagInput = "";
            root.Attribute001 = "";
            root.Attribute002 = "";
            root.Attribute003 = "";
            root.Attribute004 = "";
            root.Attribute005 = "";
            root.Attribute006 = "";
            root.Attribute007 = "";
            root.Attribute008 = "";
            root.Attribute009 = "";
            root.Attribute010 = "";

            #endregion Nulls

            Anth_Log.TraceEvent("File Root", "Information", "tmp", "Generic root created");
            DeNSo.Session.New.Set(root);
        }

        public void SetArchive(string path)
        {
            root root = new root();
            root.ADt = DateTime.Now.ToString();
            root.Aned = "n";
            root.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            root.IsDeleted = false;
            root.StorIndexN2 = CoreSecurity.CreateRandomKey();
            root.StorIndexN1 = CoreSecurity.CreateRandomVector();
            root.RootId = Guid.NewGuid().ToString();
            root.RootGuid = "archive";
            root.RootAlias = "archive";
            //root.RootPath = "D:\\Home\\zandam\\Documents\\Visual Studio 2012\\Projects\\Anthilla\\AnthillaTest\\App_Data\\AnthillaFileRepository\\Archive\\";
            root.RootPath = path;

            #region Nulls

            root.Tags = new List<string>() { };
            root.TagInput = "";
            root.Attribute001 = "";
            root.Attribute002 = "";
            root.Attribute003 = "";
            root.Attribute004 = "";
            root.Attribute005 = "";
            root.Attribute006 = "";
            root.Attribute007 = "";
            root.Attribute008 = "";
            root.Attribute009 = "";
            root.Attribute010 = "";

            #endregion Nulls

            Anth_Log.TraceEvent("File Root", "Information", "tmp", "Archive root created");
            DeNSo.Session.New.Set(root);
        }

        public void SetMyFiles(string path)
        {
            root root = new root();
            root.ADt = DateTime.Now.ToString();
            root.Aned = "n";
            root.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            root.IsDeleted = false;
            root.StorIndexN2 = CoreSecurity.CreateRandomKey();
            root.StorIndexN1 = CoreSecurity.CreateRandomVector();
            root.RootId = Guid.NewGuid().ToString();
            root.RootGuid = "myfiles";
            root.RootAlias = "myfiles";
            //root.RootPath = "D:\\Home\\zandam\\Documents\\Visual Studio 2012\\Projects\\Anthilla\\AnthillaTest\\App_Data\\AnthillaFileRepository\\MyFiles\\";
            root.RootPath = path;

            #region Nulls

            root.Tags = new List<string>() { };
            root.TagInput = "";
            root.Attribute001 = "";
            root.Attribute002 = "";
            root.Attribute003 = "";
            root.Attribute004 = "";
            root.Attribute005 = "";
            root.Attribute006 = "";
            root.Attribute007 = "";
            root.Attribute008 = "";
            root.Attribute009 = "";
            root.Attribute010 = "";

            #endregion Nulls

            Anth_Log.TraceEvent("File Root", "Information", "tmp", "MyFiles root created");
            DeNSo.Session.New.Set(root);
        }

        public void SetDownload(string path)
        {
            root root = new root();
            root.ADt = DateTime.Now.ToString();
            root.Aned = "n";
            root.ARelGuid = Guid.NewGuid().ToString().Substring(0, 8);
            root.IsDeleted = false;
            root.StorIndexN2 = CoreSecurity.CreateRandomKey();
            root.StorIndexN1 = CoreSecurity.CreateRandomVector();
            root.RootId = Guid.NewGuid().ToString();
            root.RootGuid = "download";
            root.RootAlias = "download";
            //root.RootPath = "C:\\AnthillaDownload\\";
            root.RootPath = path;

            #region Nulls

            root.Tags = new List<string>() { };
            root.TagInput = "";
            root.Attribute001 = "";
            root.Attribute002 = "";
            root.Attribute003 = "";
            root.Attribute004 = "";
            root.Attribute005 = "";
            root.Attribute006 = "";
            root.Attribute007 = "";
            root.Attribute008 = "";
            root.Attribute009 = "";
            root.Attribute010 = "";

            #endregion Nulls

            Anth_Log.TraceEvent("File Root", "Information", "tmp", "Download root created");
            DeNSo.Session.New.Set(root);
        }

        public bool ChechRoot()
        {
            var sConfig = DeNSo.Session.New.Get<root>(i => i.RootGuid == "root").FirstOrDefault();
            if (sConfig == null)
            {
                return false;
            }
            else if (sConfig != null)
            {
                return true;
            }
            else return false;
        }

        public bool ChechArchive()
        {
            var sConfig = DeNSo.Session.New.Get<root>(i => i.RootGuid == "archive").FirstOrDefault();
            if (sConfig == null)
            {
                return false;
            }
            else if (sConfig != null)
            {
                return true;
            }
            else return false;
        }

        public bool ChechMyFiles()
        {
            var sConfig = DeNSo.Session.New.Get<root>(i => i.RootGuid == "myfiles").FirstOrDefault();
            if (sConfig == null)
            {
                return false;
            }
            else if (sConfig != null)
            {
                return true;
            }
            else return false;
        }

        public bool ChechDownload()
        {
            var sConfig = DeNSo.Session.New.Get<root>(i => i.RootGuid == "download").FirstOrDefault();
            if (sConfig == null)
            {
                return false;
            }
            else if (sConfig != null)
            {
                return true;
            }
            else return false;
        }
    }
}