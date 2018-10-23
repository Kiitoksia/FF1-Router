using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF1Router.Models
{
    public class ZoneEncounterModel : BaseModel
    {
        public ZoneEncounterModel(FormationModel[] enemies, int battleGroup, bool canFlee = true)
        {
            Enemies = new BindingList<FormationModel>(enemies);
            CanFlee = canFlee;
            BattleGroup = battleGroup;
        }

        public BindingList<FormationModel> Enemies { get; }
        public bool CanFlee { get; set; }
        public int BattleGroup { get; }
    }
}
