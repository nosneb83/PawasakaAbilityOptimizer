// using CsvHelper;
// using CsvHelper.Configuration;
// using Data;
// using System.Collections.Generic;
// using System.Globalization;
// using System.IO;
// using System.Linq;

// namespace PawasakaSkillCalc
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             InputPlayer inputPlayer;
//             List<InputSkills> inputSkills;
//             using (var reader = new StreamReader("data\\InputPlayer.csv"))
//             using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//             {
//                 inputPlayer = csv.GetRecords<InputPlayer>().First();
//             }
//             using (var reader = new StreamReader("data\\InputSkills.csv"))
//             using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//             {
//                 inputSkills = csv.GetRecords<InputSkills>().ToList();
//             }

//             List<SkillObj> skillObjs;
//             using (var reader = new StreamReader("data\\DataCost.csv"))
//             using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//             {
//                 var dataCost = csv.GetRecords<DataCost>();
//                 skillObjs = inputSkills.Select(inputSkill =>
//                 {
//                     var data = dataCost.First(d => d.SkillId == inputSkill.SkillId && d.DiscountLv.Equals("Lv" + inputSkill.DiscountLv));
//                     if (inputPlayer.CostMult != 1f) data.CostMult(inputPlayer.CostMult);
//                     return new SkillObj(data);
//                 }).ToList();
//             }
//             using (var reader = new StreamReader("data\\DataEvaluation.csv"))
//             using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//             {
//                 var dataEval = csv.GetRecords<DataEvaluation>();
//                 skillObjs.ForEach(skillObj =>
//                 {
//                     var data = dataEval.First(d => d.SkillId == skillObj.SkillId);
//                     skillObj.Name = data.NameZh;
//                     skillObj.Evaluation = (float)typeof(DataEvaluation).GetProperty(inputPlayer.Type).GetValue(data);
//                 });
//             }
//             skillObjs = skillObjs.OrderBy(skillObj => skillObj.SkillId > 85 ? 0 : 1).ThenByDescending(skillObj => skillObj.ECvalue).ToList();


//             /********* Algorithm *********/

//             // // Back-tracking
//             // var dfs = new DFS(skillObjs, inputPlayer);
//             // var result = dfs.Run();

//             // Branch and Bound
//             var bnb = new BranchNBound(skillObjs, inputPlayer);
//             var result = bnb.Run();

//             /*****************************/


//             // output
//             // var output = result.list.OrderBy(id => id).Select(id => new Output { AbilityName = skillObjs.First(skill => skill.SkillId == id).Name });
//             var output = result.OrderBy(skillObj => skillObj.SkillId).Select(skillObj => new Output { AbilityName = skillObj.Name });
//             var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false };
//             using (var writer = new StreamWriter("data\\Output.csv"))
//             using (var csv = new CsvWriter(writer, config))
//             {
//                 csv.WriteRecords(output);
//             }
//             System.Console.WriteLine("Last ECValue: " + result.OrderBy(skillObj => skillObj.ECvalue).First().ECvalue.ToString());
//         }
//     }
// }
