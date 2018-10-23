using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF1Router.Models
{
    public class EncounterModel : BaseModel
    {
        public EncounterModel(ZoneType type, string zone, ZoneEncounterModel encounter, int? battleGroup, int? stepsSinceLastEnc, string comments)
        {
            Type = type;
            Zone = zone;
            Encounter = encounter;
            BattleGroup = battleGroup;
            StepsSinceLastEnc = stepsSinceLastEnc;
            Comments = comments;
        }

        public ZoneType Type { get; }
        public string Zone { get; }
        public ZoneEncounterModel Encounter { get; }
        public int? BattleGroup { get; }
        public int? StepsSinceLastEnc { get; }
        public string Comments { get; }
    }
}
