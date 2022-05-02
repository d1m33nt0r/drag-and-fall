namespace Common
{
    public static class Constants
    {
        public static class Platform
        {
            public const int COUNT_SEGMENTS = 12;
        }

        public static class Concentration
        {
            public const int CONCENTRATION_THRESHOLD = 10;
        }

        public class TAGS
        {
            public const string PLAYER = "Player";
        }

        public class TimerValues
        {
            public static readonly string[] values = 
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "11",
                "12",
                "13",
                "14",
                "15",
                "16",
                "17",
                "18",
                "19",
                "20"
            };
        }

        public class GainScoreValues
        {
            private static readonly string[] values = 
            {
                "+1",
                "+2",
                "+4",
                "+8",
                "+16",
                "+32",
                "+64",
                "+128",
                "+256",
                "+512",
                "+1024"
            };

            public static string GetString(int value)
            {
                switch (value)
                {
                    case 1: return values[0];
                    case 2: return values[1];
                    case 4: return values[2];
                    case 8: return values[3];
                    case 16: return values[4];
                    case 32: return values[5];
                    case 64: return values[6];
                    case 128: return values[7];
                    case 256: return values[8];
                    case 512: return values[9];
                    case 1024: return values[10];
                    default: return values[0];
                }
            }
        }
    }
}