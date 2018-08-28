using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LogicGate
{
    public class Program
    {
        public static Stage[] Stages =
        {
            null,
            new Stage(4, 4, 0, 0, new[]{GateTypes.AND, GateTypes.OR}),
            new Stage(4, 4, 1, 1, new[]{GateTypes.AND, GateTypes.OR, GateTypes.XOR}),
            new Stage(4, 4, 1, 1, new[]{GateTypes.AND, GateTypes.OR, GateTypes.XOR, GateTypes.NAND, GateTypes.NOR}),
            new Stage(7, 7, 1, 1, new[]{GateTypes.XOR, GateTypes.NAND, GateTypes.NOR, GateTypes.SWITCH1, GateTypes.SWITCH2}),
            new Stage(8, 8, 2, 2, new[]{GateTypes.XOR, GateTypes.NAND, GateTypes.NOR}),
        };
        private static List<Gate> Ends;
        private static List<Gate> Empties;
        public static Dictionary<String, Gate> Ids;
        private static int totalTries;
        private static int totalSolutions;
        private const int MaxStageNum = 4;
        private static StreamWriter sw;
        private static Stage currentStage;

        private static Dictionary<GateTypes, int> Used;

        static void Log(String str)
        {
            sw.WriteLine(str.ToLower());
        }

        static void Main(string[] args)
        {
            for (int i = 1; i <= MaxStageNum; i++)
            {
                sw = new StreamWriter("log" + i + ".txt");
                LoadExternalStage(i);
                TryAllPossibilities();
                //Log results.
                Log(totalTries + "/" + totalSolutions);
                Log("Ratio: " + 1.0f * totalTries / totalSolutions * 100 + "%");
                sw.Close();
                Console.WriteLine("Stage: " + i);
                Console.WriteLine(totalTries + "/" + totalSolutions);
                Console.WriteLine("Ratio: " + 1.0f * totalTries / totalSolutions * 100 + "%");
                Console.WriteLine("Done.");
            }
            Console.ReadKey();
            Console.ReadKey();
        }

        //TODO: Clean this func.
        public static void LoadExternalStage(int stageNum)
        {
            Init();
            // Require user to copy.
            ConstructDiagram("Stage" + stageNum + ".xml");
            currentStage = Stages[stageNum];
        }

        static void Init()
        {
            Ends = new List<Gate>();
            Empties = new List<Gate>();
            Ids = new Dictionary<String, Gate>();
            totalTries = 0;
            totalSolutions = 0;
            Used = new Dictionary<GateTypes, int>();
            foreach (GateTypes t in Enum.GetValues(typeof(GateTypes)))
                Used.Add(t, 0);
        }

        private static void Connect(Gate source, Gate dest, int destIdx)
        {
            dest.InGates[destIdx] = source;
            source.OutGate = dest;
        }

        private static bool isUsedValid()
        {
            if (Used[GateTypes.AND] + Used[GateTypes.NAND] > currentStage.And)
                return false;
            if (Used[GateTypes.OR] + Used[GateTypes.NOR] > currentStage.Or)
                return false;
            if (Used[GateTypes.SWITCH1] > currentStage.S1)
                return false;
            if (Used[GateTypes.SWITCH2] > currentStage.S2)
                return false;
            if (Used[GateTypes.XOR] + Used[GateTypes.SWITCH1] + Used[GateTypes.SWITCH2] > currentStage.S1 + currentStage.S2)
                return false;
            return true;
        }

        private static void ConstructDiagram(String filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            foreach (XmlNode child in doc.DocumentElement)
                //Might have multiple outputs.
                Ends.Add(_construct(null, child));
        }

        private static Gate _construct(Gate pGate, XmlNode current)
        {
            Gate gate;
            int idx = 0;
            if (!current.HasChildNodes)
                //Source.
                return new Source(Convert.ToBoolean(current.Name));
            if (current.Name == "true" || current.Name == "false")
                //Dest.
                gate = new Dest(Convert.ToBoolean(current.Name));
            else
            {
                gate = new EmptyGate(current.Name);
                if (current.Name == "empty")
                {
                    String id = current.Attributes["id"].Value;
                    gate.Id = id;
                    if (Ids.ContainsKey(id))
                    {
                        //Gate appeared before, (omitted).
                        return Ids[id];
                    }
                    //Store appeared gates.
                    Ids.Add(id, gate);
                    //Store empty gates.
                    Empties.Add(gate);
                }
            }
            //Connect with childs.
            foreach (XmlNode child in current)
            {
                Connect(_construct(gate, child), gate, idx);
                idx++;
            }
            return gate;
        }

        private static void TryAllPossibilities()
        {
            if (Ends.Count == 0)
                return;
            _try(0);
        }

        private static void _try(int depth)
        {
            if (Empties.Count == depth)
            {
                //Found a possible pattern using restrained gates.
                totalSolutions++;
                if (Check())
                {
                    //Found a solution.
                    Log("Solution " + totalSolutions + ":");
                    foreach (Gate gate in Empties)
                        Log(gate.Type.ToString());
                    totalTries++;
                }
                return;
            }
            foreach (GateTypes t in currentStage.Types)
            {
                Used[t]++;
                if (isUsedValid())
                {
                    //If there are these kind of gates left.
                    Empties[depth].Type = t;
                    _try(depth + 1);
                }
                Used[t]--;
            }
        }

        public static bool Check()
        {
            //TODO: Fix bad performance.
            //The code below will traverse every node.}
            bool pass = true;
            foreach (Gate end in Ends)
            {
                //Recursive
                try
                {
                    if (end.GetOutput() != ((Dest)end).outVal)
                        pass = false;
                }
                catch (NotImplementedException)
                {
                    pass = false;
                }
            }
            return pass;
        }
    }
}
