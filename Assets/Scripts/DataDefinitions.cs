using System;

namespace Data
{
    public class InputPlayer
    {
        public float CostMult { get; set; }
        public string Type { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Skill { get; set; }
        public int Spirit { get; set; }
    }

    public class InputSkills
    {
        public int SkillId { get; set; }
        public string DiscountLv { get; set; }
    }

    public class Output
    {
        public string AbilityName { get; set; }
    }

    public class DataCost
    {
        public int SkillId { get; set; }
        public string DiscountLv { get; set; }
        public int StrCost { get; set; }
        public int AgiCost { get; set; }
        public int SklCost { get; set; }
        public int SprCost { get; set; }
        public void CostMult(float mult)
        {
            StrCost = (int)(StrCost * mult);
            AgiCost = (int)(AgiCost * mult);
            SklCost = (int)(SklCost * mult);
            SprCost = (int)(SprCost * mult);
        }
    }

    public class DataEvaluation
    {
        public int SkillId { get; set; }
        public string NameZh { get; set; }
        public float CF { get; set; }
        public float ST { get; set; }
        public float WG { get; set; }
        public float OMF { get; set; }
        public float CMF { get; set; }
        public float SMF { get; set; }
        public float DMF { get; set; }
        public float CB { get; set; }
        public float SB { get; set; }
        public float GK { get; set; }
    }

    public class SkillObj
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public int StrCost { get; set; }
        public int AgiCost { get; set; }
        public int SklCost { get; set; }
        public int SprCost { get; set; }
        public float Evaluation { get; set; }
        public int TotalCost => StrCost + AgiCost + SklCost + SprCost;
        public float ECvalue => Evaluation / TotalCost;
        public SkillObj(DataCost data)
        {
            SkillId = data.SkillId;
            StrCost = data.StrCost;
            AgiCost = data.AgiCost;
            SklCost = data.SklCost;
            SprCost = data.SprCost;
        }
    }
}
