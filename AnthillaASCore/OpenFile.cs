using System;
using System.Diagnostics;
using System.IO;

namespace AnthillaASCore
{
    public class OpenFile
    {
        public static void Open()
        {
            string folder = @"D:\Anthilla\AnthillaAS\AnthillaASCore\";
            string fileTxt = "file.txt";
            string[] path = new string[] { Path.Combine(folder, fileTxt) };

            using (FileStream stream = File.Open(path[0], FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(Stopwatch.GetTimestamp().ToString());
            }
            using (FileStream stream = File.Open(path[0], FileMode.Open))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                string a = reader.ReadString();

                Console.WriteLine(a);
            }
        }

        private Note[] Mary =
        {
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.GbelowC, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.B, Duration.HALF),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.A, Duration.QUARTER),
            new Note(Tone.A, Duration.HALF),
            new Note(Tone.B, Duration.QUARTER),
            new Note(Tone.D, Duration.QUARTER),
            new Note(Tone.D, Duration.HALF)
        };

        protected enum Tone
        {
            REST = 0,
            GbelowC = 196,
            A = 220,
            Asharp = 233,
            B = 247,
            C = 262,
            Csharp = 277,
            D = 294,
            Dsharp = 311,
            E = 330,
            F = 349,
            Fsharp = 370,
            G = 392,
            Gsharp = 415,
        }

        protected enum Duration
        {
            WHOLE = 1600,
            HALF = WHOLE / 2,
            QUARTER = HALF / 2,
            EIGHTH = QUARTER / 2,
            SIXTEENTH = EIGHTH / 2,
        }

        protected struct Note
        {
            private Tone toneVal;
            private Duration durVal;

            public Note(Tone frequency, Duration time)
            {
                toneVal = frequency;
                durVal = time;
            }

            public Tone NoteTone { get { return toneVal; } }

            public Duration NoteDuration { get { return durVal; } }
        }
    }
}