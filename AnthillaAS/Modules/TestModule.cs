using Nancy;

namespace AnthillaAS.Modules
{
    using System;

    public class Play
    {
        public static void Start()
        {
            Tone tone = new Tone();
            Duration duration = new Duration();

            Console.Beep(9000, 1600);
        }

        public class Tone
        {
            private int rest = 0;

            public int REST { get { return rest; } }

            private int gbelowc = 196;

            public int GbelowC { get { return gbelowc; } }

            private int a = 220;

            public int A { get { return a; } }

            private int asharp = 233;

            public int Asharp { get { return asharp; } }

            private int b = 247;

            public int B { get { return b; } }

            private int c = 262;

            public int C { get { return c; } }

            private int csharp = 277;

            public int Csharp { get { return csharp; } }

            private int d = 294;

            public int D { get { return d; } }

            private int dsharp = 311;

            public int Dsharp { get { return dsharp; } }

            private int e = 330;

            public int E { get { return e; } }

            private int f = 349;

            public int F { get { return f; } }

            private int fsharp = 370;

            public int Fsharp { get { return fsharp; } }

            private int g = 392;

            public int G { get { return g; } }

            private int gsharp = 415;

            public int Gsharp { get { return gsharp; } }
        }

        public class Duration
        {
            private int whole = 1600;

            public int WHOLE { get { return whole; } }

            public int HALF { get { return whole / 2; } }

            public int QUARTER { get { return whole / 4; } }

            public int EIGHTH { get { return whole / 8; } }

            public int SIXTEENTH { get { return whole / 16; } }
        }
    }

    public class TestModule : NancyModule
    {
        public TestModule()
            : base("/test")
        {
            Get["/playtune"] = x =>
            {
                Play.Start();
                return Response.AsXml(true);
            };
        }
    }
}