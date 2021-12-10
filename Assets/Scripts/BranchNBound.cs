// using Data;
// using Priority_Queue;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace PawasakaSkillCalc
// {
//     public class BranchNBound
//     {
//         public List<SkillObj> skillObjs;
//         public int[] remainPoints;
//         public SimplePriorityQueue<Node, float> pq = new SimplePriorityQueue<Node, float>();
//         public Node solution;

//         public BranchNBound(List<SkillObj> skillObjs, InputPlayer inputPlayer)
//         {
//             this.skillObjs = skillObjs;
//             remainPoints = new int[] { inputPlayer.Strength, inputPlayer.Agility, inputPlayer.Skill, inputPlayer.Spirit };
//         }

//         public List<SkillObj> Run()
//         {
//             // root node
//             var rootNode = new Node(null, -1, false, remainPoints.ToArray(), 0f);
//             solution = rootNode;
//             rootNode.EvaluateChildren(skillObjs, ref solution, pq);

//             // start loop
//             while (pq.Count > 0 && pq.Count < 20000000)
//             // while (pq.Count > 0)
//             {
//                 var node = pq.Dequeue();
//                 if (node.bound > solution.totalEval) node.EvaluateChildren(skillObjs, ref solution, pq);
//             }

//             // result
//             var result = new List<SkillObj>();
//             var ptr = solution;
//             while (ptr.index >= 0)
//             {
//                 if (ptr.pick) result.Add(skillObjs[ptr.index]);
//                 ptr = ptr.Parent;
//             }
//             return result;
//         }

//         public class Node
//         {
//             public Node Parent;
//             public int index;
//             public bool pick;
//             public int[] remainPoints;
//             public float totalEval;
//             public float bound;

//             public Node(Node parent, int index, bool pick, int[] remainPoints, float totalEval)
//             {
//                 Parent = parent;
//                 this.index = index;
//                 this.pick = pick;
//                 this.remainPoints = remainPoints;
//                 this.totalEval = totalEval;
//             }

//             public void EvaluateChildren(List<SkillObj> skillObjs, ref Node solution, SimplePriorityQueue<Node, float> pq)
//             {
//                 var lChild = new Node(this, index + 1, true, remainPoints.ToArray(), totalEval);
//                 var rChild = new Node(this, index + 1, false, remainPoints.ToArray(), totalEval);

//                 if (lChild.Evaluate(skillObjs, ref solution)) pq.Enqueue(lChild, 10000 - lChild.bound);
//                 if (rChild.Evaluate(skillObjs, ref solution)) pq.Enqueue(rChild, 10000 - rChild.bound);
//             }

//             public bool Evaluate(List<SkillObj> skillObjs, ref Node solution)
//             {
//                 if (index >= skillObjs.Count) return false;

//                 var skillObj = skillObjs[index];
//                 if (pick)
//                 {
//                     remainPoints[0] -= skillObj.StrCost;
//                     remainPoints[1] -= skillObj.AgiCost;
//                     remainPoints[2] -= skillObj.SklCost;
//                     remainPoints[3] -= skillObj.SprCost;
//                     if (remainPoints.Any(r => r < 0)) return false;
//                     totalEval += skillObj.Evaluation;
//                     if (totalEval > solution.totalEval)
//                     {
//                         solution = this;
//                         System.Console.WriteLine("Current Eval: " + totalEval.ToString());
//                     }
//                 }

//                 // calculate bound
//                 bound = totalEval;
//                 var totalPoints = remainPoints.Sum();
//                 skillObjs.Skip(index + 1).ToList().ForEach(s =>
//                 {
//                     if (s.TotalCost > totalPoints)
//                     {
//                         bound += s.Evaluation * totalPoints / s.TotalCost;
//                         return;
//                     }
//                     else
//                     {
//                         totalPoints -= s.TotalCost;
//                         bound += s.Evaluation;
//                     }
//                 });

//                 return true;
//             }
//         }
//     }
// }
