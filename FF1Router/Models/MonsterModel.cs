using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiiLibrary;

namespace FF1Router.Models
{
    public class MonsterModel : BaseModel
    {
        public MonsterModel(string name, int hp, int gold, int experience, int damage, int hits, int hitPercent,
            Status status, int critPercent, int abs, int evasion, int magDef, int specialAttack, int runLevel, int magicPercent,
            MonsterType type, Element[] weaknesses, Element[] resists)
        {
            Name = name;
            HP = hp;
            Gold = gold;
            Experience = experience;
            Damage = damage;
            Hits = hits;
            HitPercent = hitPercent;
            Status = status;
            CritPercent = critPercent;
            Abs = abs;
            Evasion = evasion;
            MagDef = magDef;
            RunLevel = runLevel;
            MagicPercent = magicPercent;
            Type = type;
            Weaknesses = weaknesses;
            Resists = resists;
            SpecialAttack = specialAttack;

            if (Resists == null) Resists = new [] { Element.None };
            if (Weaknesses == null) Weaknesses = new [] { Element.None };
        }

        public MonsterModel(string name, int hp, int gold, int experience, int damage, int hits, int hitPercent,
            Status status, int critPercent, int abs, int evasion, int magDef, int specialAttack, int runLevel,
            int magicPercent,
            MonsterType type, Element weakness, Element[] resists)
        :this(name, hp, gold, experience, damage, hits, hitPercent, status, critPercent, abs, evasion, magDef, specialAttack, runLevel, magicPercent, type,
            new [] { weakness }, resists)
        {
        }

        public string Name { get; }
        public int HP { get; }
        public int Gold { get;}
        public int Experience { get; }
        public int Damage { get; }
        public int Hits { get; }
        public int HitPercent { get; }
        public Status Status { get; }
        public int CritPercent { get; }
        public int Abs { get; }
        public int Evasion { get; }
        public int MagDef { get; }
        public int RunLevel { get; }
        public int SpecialAttack { get; }
        public int MagicPercent { get; }
        public MonsterType Type { get; }
        public Element[] Resists { get; }
        public Element[] Weaknesses { get; }

        public string WeaknessDisplay => string.Join(", ", Weaknesses.Select(s => s.GetDescription()));
        public string ResistsDisplay => string.Join(", ", Resists.Select(s => s.GetDescription()));

        public int Index { get; }
    }
}
