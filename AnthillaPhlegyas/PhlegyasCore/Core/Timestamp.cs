//Phlegyas
using System;

namespace AnthillaPhlegyas.Core {

    public class Timestamp {
        private static long lastTimeStamp = DateTime.UtcNow.Ticks;

        private long UtcNowTicks(int num) {
            var orig = lastTimeStamp;
            long now = DateTime.UtcNow.AddHours(num).Ticks;
            var newval = Math.Max(now, orig + 1);
            //            var uu = (Interlocked.CompareExchange(ref lastTimeStamp, newval, orig) != orig);
            return Convert.ToInt64(newval);
        }

        public long SetTimestamp() {
            var timestamp = UtcNowTicks(0) - 2;
            return timestamp;
        }

        public long SetValidity(int sum) {
            var timestamp = UtcNowTicks(sum) - 2;
            return timestamp;
        }

        public bool CheckTimestampValidity(int tsValid) {
            var tsNow = UtcNowTicks(0) - 2;
            if (tsNow > tsValid) {
                return false;
            }
            else return true;
        }
    }
}