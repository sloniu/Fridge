using System;

namespace Template10Test.Models
{
    public sealed class Data
    {
        private static volatile Data instance;
        private static object syncRoot = new Object();



        private Data() { }

        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Data();
                    }
                }

                return instance;
            }
        }

        private static string[] _streams =
        {
            "iwilldominate",
            "totalbiscuit",
            "crytek",
            "xlook",
            "hegemonytv",
            "markiplier",
            "cohhcarnage",
            "riotgames",
            "tsm_biergsen",
            "sivhd",
            "tsm_theoddone",
            "tsm_dyrus",
            "vman7",
            //"guardsmanbob",
            "cryaotic"
        };

        public static string[] Streams
        {
            get { return _streams; }
            set { _streams = value; }
        }
    }
}
