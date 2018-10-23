using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF1Router.Models
{
    public class FormationModel
    {
        public FormationModel(MonsterModel formation, int minAmount, int maxAmount)
        {
            Formation = formation;
            MinAmount = minAmount;
            MaxAmount = maxAmount;
        }

        public MonsterModel Formation { get; }
        public int MinAmount { get; }
        public int MaxAmount { get; }

        public string AmountDisplay => MinAmount == MaxAmount ? MinAmount.ToString() : $"{MinAmount}-{MaxAmount}";
    }
}
