using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using System.Data;

namespace CSharpBullshitGenerator
{
    public static class BullshitContentFactory
    {
        private static Random m_random = new Random(DateTime.Now.Millisecond);

        private static string RandomChoice(List<string> lstValues)
        {
            return lstValues[m_random.Next(0, lstValues.Count)];
        }
        public static string  GenerateBullshit(string title, int length)
        {
            if (!File.Exists("data.json"))
            {
                throw new FileNotFoundException("data.json");
            }

            JsonDatas jsonData = JsonConvert.DeserializeObject<JsonDatas> (File.ReadAllText("data.json"));

            StringBuilder sbResult = new StringBuilder();
            int randomNo = 0;

            while (sbResult.Length < length)
            {
                randomNo = m_random.Next(0, 100);

                if (randomNo < 10)
                {
                    sbResult.AppendLine();
                }
                else if (randomNo < 20)
                {
                    string randomFamous = RandomChoice(jsonData.Famous).Replace("a", "{0}").Replace("b", "{1}");

                    sbResult.Append(string.Format(randomFamous,
                        RandomChoice(jsonData.Before),
                        RandomChoice(jsonData.After)));
                }
                else
                {
                    string randomBosh = RandomChoice(jsonData.Bosh).Replace("x", title);
                    sbResult.Append(randomBosh);
                }
            }

            return sbResult.ToString();           
        }
    }
}
