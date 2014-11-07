using System;

namespace AnthillaASCore
{
    public static class ConsoleTime
    {
        public static string GetTime(DateTime dt)
        {
            var str = "[";
            str += dt.ToString("MM/dd/yy");
            str += " ";
            str += dt.ToString("H:mm:ss");
            str += "] ";
            return str;
        }
    }
}