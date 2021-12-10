using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PawasakaSkillCalc
{
    public class DFS
    {
        public List<SkillObj> skillObjs;
        public int[] remainPoints;
        public DFS(List<SkillObj> skillObjs, InputPlayer inputPlayer)
        {
            this.skillObjs = skillObjs;
            remainPoints = new int[] { inputPlayer.Strength, inputPlayer.Agility, inputPlayer.Skill, inputPlayer.Spirit };
        }

        public (List<int> list, float evel) Run()
        {
            var leftChild = Traverse(0, true, remainPoints.ToArray());
            var rightChild = Traverse(0, false, remainPoints.ToArray());
            var biggerChild = leftChild.evel > rightChild.evel ? leftChild : rightChild;
            return (biggerChild.list, biggerChild.evel);
        }

        public (List<int> list, float evel) Traverse(int index, bool pick, int[] remainPoints)
        {
            if (index >= skillObjs.Count) return (new List<int>(), 0f);

            var skillObj = skillObjs[index];
            if (pick)
            {
                remainPoints[0] -= skillObj.StrCost;
                remainPoints[1] -= skillObj.AgiCost;
                remainPoints[2] -= skillObj.SklCost;
                remainPoints[3] -= skillObj.SprCost;
                if (remainPoints.Any(r => r < 0)) return (new List<int>(), 0f);
            }

            var leftChild = Traverse(index + 1, true, remainPoints.ToArray());
            var rightChild = Traverse(index + 1, false, remainPoints.ToArray());
            var biggerChild = leftChild.evel > rightChild.evel ? leftChild : rightChild;
            if (pick)
            {
                biggerChild.list.Add(skillObj.SkillId);
                return (biggerChild.list, biggerChild.evel + skillObj.Evaluation);
            }
            else return (biggerChild.list, biggerChild.evel);
        }
    }
}
