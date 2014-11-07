using System;

namespace AnthillaASCore.Logging
{
    public class Anth_Log
    {
        //configurano i parametri
        //private static bool traceEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["LogEnable"]);
        //private const string errorCategory = "Error Category";
        //private const string debugCategory = "Debug Category";
        //private const string functionalCategory = "Functional Category";
        //private const string clientPerfCategory = "Client Performance Category";
        //private const string debugTraceEnabledPropname = "DebugTraceEnabled";

        static Anth_Log()
        {
        }

        public static void TraceEvent(string activity, string typeOfLevel, string keyword, string message)
        {
            try
            {
                //StackFrame frame = new StackFrame(1, true);
                //StackTrace stackTrace = new StackTrace(frame);
                //MethodBase method = frame.GetMethod();
                //Exception exception = e;
                //Anth_Dump user = new Anth_Dump();

                var AnthID = "Anthilla System Id"; //Anthilla System Id
                var Level = typeOfLevel;// +exception.InnerException.ToString();
                var Source = "method.ToString()";
                var EventId = "Event Id"; //Event Id
                var Activity = activity;
                var Keyword = keyword;
                var User = "user"; //user.AnthillaAlias.ToString();
                var OperativeCode = "Operative Code"; //Operative Code
                var Reg = "Registry"; //Registro
                var SessionID = "Session Id"; //Session Id
                var RelationID = "Relation Id"; //Relation Id
                var Message = message;
                var EventsSourceName = "frame.GetFileName().ToString()";

                WriteLog(AnthID, Level, Source, EventId, Activity, Keyword, User, OperativeCode, Reg, SessionID, RelationID, EventsSourceName, Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private static void WriteLog(string anthId, string level, string source, string eventID, string act,
            string kw, string user, string oc, string reg, string sId, string rId, string esn, string msg)
        {
            try
            {
                Anth_LogModel logItem = new Anth_LogModel();
                logItem.LogGuid = Guid.NewGuid().ToString();
                logItem.AnthillaID = anthId;
                logItem.DateTime = DateTime.Now;
                logItem.Level = level;
                logItem.Source = source;
                logItem.EventID = eventID;
                logItem.Activity = act;
                logItem.Keyword = kw;
                logItem.User = user;
                logItem.OperativeCode = oc;
                logItem.Reg = reg;
                logItem.SessionID = sId;
                logItem.RelationID = rId;
                logItem.EventsSourceName = esn;
                logItem.Message = msg;

                DeNSo.Session.New.Set(logItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static double ConvertToTimestamp(DateTime date)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Math.Floor((date - dateTime).TotalSeconds);
        }
    }
}