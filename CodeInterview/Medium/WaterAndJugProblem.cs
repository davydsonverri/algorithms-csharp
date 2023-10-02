using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{

    /// <summary>
    /// Find the biggest binary gap
    /// </summary>
    public class WaterAndJugProblem
    {
        bool PatternFound = false;
        Dictionary<int, List<JarState>> tree = new Dictionary<int, List<JarState>>();
        HashSet<string> visitedStates = new HashSet<string>();

        public bool Execute(int jug1Capacity, int jug2Capacity, int targetCapacity)
        {

            // Performance measure
            var PT = PerformanceTracker.StartTrack("WaterAndJugProblem", "");

            // Algorithm
            var currentLevel = 0;
            List<JarState> itemsCurrentLevel = new List<JarState>();
            itemsCurrentLevel.Add(new JarState(0, 0));
            tree.Add(currentLevel, itemsCurrentLevel);
            visitedStates.Add("0-0");


            while (tree.TryGetValue(currentLevel, out itemsCurrentLevel))
            {
                if (this.PatternFound) break;

                var newLevel = new List<JarState>();

                foreach (var itemFather in itemsCurrentLevel)
                {
                    // Fill Jar 1
                    if (itemFather.Jug1Volume < jug1Capacity)
                    {
                        var fillJar1Node = new JarState(jug1Capacity, itemFather.Jug2Volume);
                        if (visitedStates.Add(fillJar1Node.GetId()))
                        {
                            newLevel.Add(fillJar1Node);
                            if (fillJar1Node.MatchCapacity(targetCapacity))
                            {
                                this.PatternFound = true; break;
                            }
                        }
                    }

                    // Fill Jar2
                    if (itemFather.Jug2Volume < jug2Capacity)
                    {
                        var fillJar2Node = new JarState(itemFather.Jug1Volume, jug2Capacity);
                        if (visitedStates.Add(fillJar2Node.GetId()))
                        {
                            newLevel.Add(fillJar2Node);
                            if (fillJar2Node.MatchCapacity(targetCapacity))
                            {
                                this.PatternFound = true; break;
                            }
                        }
                    }

                    // Transfer Jar1 to Jar2
                    if (itemFather.Jug1Volume > 0 && itemFather.Jug2Volume < jug2Capacity)
                    {
                        var jug1NewVolume = 0;
                        var jug2NewVolume = itemFather.Jug1Volume + itemFather.Jug2Volume;
                        if (jug2NewVolume > jug2Capacity)
                        {
                            jug1NewVolume = jug2NewVolume - jug2Capacity;
                            jug2NewVolume = jug2Capacity;
                        }

                        var transferJar1ToJar2Node = new JarState(jug1NewVolume, jug2NewVolume);
                        if (visitedStates.Add(transferJar1ToJar2Node.GetId()))
                        {
                            newLevel.Add(transferJar1ToJar2Node);
                            if (transferJar1ToJar2Node.MatchCapacity(targetCapacity))
                            {
                                this.PatternFound = true; break;
                            }
                        }
                    }

                    // Transfer Jar2 to Jar1
                    if (itemFather.Jug2Volume > 0 && itemFather.Jug1Volume < jug1Capacity)
                    {
                        var jug1NewVolume = itemFather.Jug1Volume + itemFather.Jug2Volume;
                        var jug2NewVolume = 0;
                        if (jug1NewVolume > jug1Capacity)
                        {
                            jug2NewVolume = jug1NewVolume - jug1Capacity;
                            jug1NewVolume = jug1Capacity;
                        }

                        var transferJar2ToJar1Node = new JarState(jug1NewVolume, jug2NewVolume);
                        if (visitedStates.Add(transferJar2ToJar1Node.GetId()))
                        {
                            newLevel.Add(transferJar2ToJar1Node);
                            if (transferJar2ToJar1Node.MatchCapacity(targetCapacity))
                            {
                                this.PatternFound = true; break;
                            }
                        }
                    }

                    // Throw out Jar1
                    if (itemFather.Jug1Volume > 0)
                    {
                        var throwOutJar1 = new JarState(0, itemFather.Jug2Volume);
                        if (visitedStates.Add(throwOutJar1.GetId()))
                        {
                            newLevel.Add(throwOutJar1);
                            if (throwOutJar1.MatchCapacity(targetCapacity))
                            {
                                this.PatternFound = true; break;
                            }
                        }
                    }

                    // Throw out Jar2
                    if (itemFather.Jug2Volume > 0)
                    {
                        var throwOutJar2 = new JarState(itemFather.Jug1Volume, 0);
                        if (visitedStates.Add(throwOutJar2.GetId()))
                        {
                            newLevel.Add(throwOutJar2);
                            if (throwOutJar2.MatchCapacity(targetCapacity))
                            {
                                this.PatternFound = true; break;
                            }
                        }
                    }
                }
                
                currentLevel++;

                if(newLevel.Count > 0)
                {
                    tree.Add(currentLevel, newLevel);
                    tree.Remove(currentLevel - 1);
                }

            }

            // Performance measure
            PT.Stop();

            return PatternFound;
        }  
    }

    public class JarState
    {
        public int Jug1Volume { get; }
        public int Jug2Volume { get; }

        public JarState(int jug1Volume, int jug2Volume)
        {
            this.Jug1Volume = jug1Volume;
            this.Jug2Volume = jug2Volume;
        }

        public string GetId()
        {
            return Jug1Volume + "-" + Jug2Volume;
        }

        public bool MatchCapacity(int capacity)
        {
            if (this.Jug1Volume + this.Jug2Volume == capacity) return true;
            return false;
        }
    }
}
