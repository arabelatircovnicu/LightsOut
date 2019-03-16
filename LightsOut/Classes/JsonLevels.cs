using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Classes
{
    static class JsonLevels
    {
        public struct LevelType
        {
            public string Name;
            public int Columns, Rows;
            public int[] On;
        }

        public static int Count { get { return JArray.Parse(Encoding.UTF8.GetString(Properties.Resources.levels)).Count; } }

        public static LevelType Level(int LevelNumber)
        {
            var Json = Encoding.UTF8.GetString(Properties.Resources.levels);
            JArray levels = JArray.Parse(Json);

            LevelType L = new LevelType();
            if (LevelNumber < levels.Count)
            {
                L.Name = levels[LevelNumber].ToString();
                L.Rows = (int)levels[LevelNumber]["rows"];
                L.Columns = (int)levels[LevelNumber]["columns"];
                L.On = levels[LevelNumber]["on"].Values<int>().ToArray();
            }
            return L;
        }

    }
}
