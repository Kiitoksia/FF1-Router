using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF1Router.Models;

namespace FF1Router
{ 
    public enum StepCountPointerDirection
    {
        Up,
        Down
    }

    public enum Status
    {
        None,
        Darkness,
        KO,
        Paralysis,
        Poison,
        Sleep,
        Stone
    }

    public enum MonsterType
    {
        Undead,
        Standard
    }

    public enum Element
    {
        Fire,
        Cold,
        Lightning,
        Poison,
        Earth,
        Status,
        [Description("All Elements")]
        All,
        None,
        Death
    }

    public enum ZoneType
    {
        Overworld,
        Ocean,
        Dungeon,
        NotApplicable
    }

    public static class Const
    {
        public static string SettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "Settings.xml");
        public static readonly string RoutesPath = Path.Combine(Directory.GetCurrentDirectory(), "Routes");

        public static readonly int EncounterStartPoint = 255;

        public static readonly int[] EncounterTable = 
        {
            31, 166, 222, 186, 204, 18, 125, 116, 27, 243, 180, 136, 248, 82, 244, 7, 144, 171, 179, 189, 170, 85, 40, 188, 138, 
            109, 14, 196, 131, 169, 59, 118, 32, 124, 9, 146, 253, 74, 168, 240, 97, 227, 242, 105, 108, 187, 56, 195, 174, 183, 
            67, 132, 120, 35, 123, 155, 45, 219, 62, 145, 207, 2, 42, 182, 134, 238, 156, 142, 184, 111, 26, 87, 5, 233, 115, 49, 
            210, 217, 29, 251, 148, 157, 177, 10, 58, 17, 90, 71, 149, 44, 68, 224, 106, 140, 91, 122, 167, 93, 54, 112, 229, 199, 
            73, 220, 104, 151, 216, 102, 163, 15, 176, 159, 3, 214, 119, 22, 19, 48, 37, 60, 16, 23, 173, 152, 107, 47, 215, 161, 
            255, 164, 235, 81, 254, 39, 141, 147, 213, 61, 246, 8, 117, 225, 165, 70, 99, 245, 77, 218, 50, 175, 64, 55, 211, 192, 
            137, 103, 6, 33, 110, 129, 181, 160, 79, 12, 46, 231, 28, 88, 133, 232, 89, 206, 53, 203, 30, 198, 43, 154, 230, 221, 
            241, 236, 150, 202, 172, 0, 80, 201, 76, 252, 20, 126, 86, 128, 208, 121, 191, 41, 135, 72, 36, 25, 197, 34, 113, 127, 
            114, 13, 205, 143, 190, 63, 158, 52, 237, 83, 84, 4, 98, 162, 194, 65, 94, 130, 75, 38, 92, 66, 101, 153, 78, 96, 139, 
            247, 11, 51, 223, 209, 100, 200, 193, 1, 239, 249, 250, 228, 95, 24, 185, 178, 57, 212, 21, 226, 234, 69
        };

        public static readonly int[] RandomBattleGroup =
        {
            3, 4, 3, 6, 2, 2, 7, 5, 3, 5, 5, 1, 6, 2, 5, 1,
            2, 4, 5, 7, 4, 2, 4, 7, 1, 4, 2, 1, 1, 4, 6, 6,
            3, 7, 1, 2, 7, 1, 4, 5, 3, 3, 5, 4, 4, 6, 6, 1,
            4, 6, 1, 1, 6, 3, 6, 3, 4, 3, 7, 2, 2, 1, 4, 6,
            1, 4, 3, 2, 6, 4, 3, 2, 1, 4, 5, 5, 2, 3, 3, 6,
            2, 3, 5, 1, 6, 2, 3, 1, 2, 4, 1, 3, 4, 2, 3, 6,
            4, 3, 6, 5, 4, 1, 1, 3, 4, 2, 3, 4, 3, 2, 5, 3,
            1, 2, 6, 2, 2, 5, 4, 7, 2, 2, 4, 3, 4, 4, 2, 3,
            8, 4, 4, 2, 7, 4, 2, 2, 2, 7, 6, 1, 5, 3, 4, 1,
            3, 5, 2, 3, 5, 4, 1, 6, 2, 1, 1, 4, 1, 3, 4, 1,
            5, 3, 2, 2, 4, 4, 3, 3, 1, 4, 3, 2, 5, 1, 3, 1,
            4, 3, 4, 3, 5, 4, 2, 1, 4, 1, 2, 1, 2, 7, 2, 7,
            2, 1, 2, 6, 8, 4, 1, 1, 4, 3, 1, 3, 5, 8, 5, 2,
            2, 2, 7, 8, 3, 5, 4, 2, 2, 1, 3, 3, 1, 1, 3, 1,
            1, 4, 3, 1, 4, 3, 2, 3, 1, 6, 1, 5, 3, 2, 4, 1,
            1, 1, 4, 6, 6, 4, 3, 3, 6, 5, 6, 2, 2, 3, 4, 1,
        };

        public static readonly StepCountPointerDirection[] StepCountDirectionPattern =
        {
            StepCountPointerDirection.Down,
            StepCountPointerDirection.Down,
            StepCountPointerDirection.Up,
            StepCountPointerDirection.Down,
            StepCountPointerDirection.Up,
            StepCountPointerDirection.Up,
            StepCountPointerDirection.Down,
            StepCountPointerDirection.Up,
            StepCountPointerDirection.Down
        };

        #region Zone/Monster Population
        public static readonly MonsterModel[] Monsters =
       {
            new MonsterModel("IMP", 8, 6, 6, 4, 1, 85, Status.None, 1, 4, 3, 8, 13, 0, 0, MonsterType.Standard,
                Element.None, null),
            new MonsterModel("GrIMP", 16, 18, 18, 8, 1, 86, Status.None, 1, 6, 5, 12, 20, 0, 0, MonsterType.Standard,
                Element.None, null),
            new MonsterModel("WOLF", 20, 6, 24, 8, 1, 86, Status.None, 1, 0, 18, 14, 13, 0, 0, MonsterType.Standard,
                Element.None, null),
            new MonsterModel("GrWOLF", 72, 22, 93, 14, 1, 93, Status.None, 1, 0, 27, 23, 14, 0, 0, MonsterType.Standard,
                Element.None, null),
            new MonsterModel("WrWOLF", 68, 67, 135, 14, 1, 92, Status.Poison, 1, 6, 21, 23, 20, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("FrWOLF", 92, 200, 402, 25, 1, 95, Status.None, 1, 0, 27, 28, 0, 0, 25,
                MonsterType.Standard, Element.Fire, new[] {Element.Cold}),
            new MonsterModel("IGUANA", 92, 50, 153, 18, 1, 95, Status.None, 5, 12, 12, 28, 27, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("AGAMA", 296, 1200, 2472, 31, 2, 120, Status.None, 1, 18, 18, 72, 0, 0, 25,
                MonsterType.Standard, Element.Cold, new[] {Element.Fire}),
            new MonsterModel("SAURIA", 196, 658, 1977, 30, 1, 110, Status.None, 1, 20, 12, 46, 0, 0, 50,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("GIANT", 240, 879, 879, 38, 1, 113, Status.None, 1, 12, 24, 60, 28, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("FrGIANT", 336, 1752, 1752, 60, 1, 122, Status.None, 1, 16, 24, 75, 0, 0, 0,
                MonsterType.Standard, Element.Fire, new[] {Element.Cold}),
            new MonsterModel("R.GIANT", 300, 1506, 1506, 73, 1, 125, Status.None, 1, 20, 24, 68, 0, 0, 0,
                MonsterType.Standard, Element.Cold, new[] {Element.Fire}),
            new MonsterModel("SAHAG", 28, 30, 30, 10, 1, 87, Status.None, 1, 4, 36, 14, 15, 0, 0, MonsterType.Standard,
                Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("R.SAHAG", 64, 105, 105, 15, 1, 92, Status.None, 1, 8, 39, 23, 31, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("WzSAHAG", 204, 882, 882, 47, 1, 109, Status.None, 1, 20, 48, 51, 0, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("PIRATE", 6, 40, 40, 8, 1, 85, Status.None, 1, 0, 6, 8, 0, 0, 0, MonsterType.Standard,
                Element.None, null),
            new MonsterModel("KYZOKU", 50, 120, 60, 14, 1, 90, Status.None, 1, 6, 12, 19, 13, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("SHARK", 120, 66, 267, 22, 1, 99, Status.None, 1, 0, 36, 35, 21, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("GrSHARK", 344, 600, 2361, 50, 1, 126, Status.None, 1, 8, 36, 85, 0, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("OddEYE", 10, 10, 42, 4, 1, 85, Status.None, 1, 0, 42, 7, 15, 0, 1, MonsterType.Standard,
                Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("BigEYE", 304, 3591, 3591, 30, 2, 121, Status.None, 1, 16, 12, 78, 0, 0, 50,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("BONE", 10, 3, 9, 10, 1, 85, Status.None, 1, 0, 6, 9, 22, 0, 0, MonsterType.Undead,
                Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("R.BONE", 144, 378, 378, 26, 1, 101, Status.None, 1, 12, 21, 38, 38, 0, 0,
                MonsterType.Undead, Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("CREEP", 56, 15, 63, 17, 1, 91, Status.None, 1, 8, 12, 20, 12, 0, 0, MonsterType.Standard,
                Element.Fire, null),
            new MonsterModel("CRAWL", 84, 200, 186, 1, 8, 94, Status.Paralysis, 1, 8, 21, 26, 13, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("HYENA", 120, 72, 288, 22, 1, 99, Status.None, 1, 4, 24, 38, 21, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("CEREBUS", 192, 600, 1182, 30, 1, 107, Status.None, 1, 8, 24, 52, 33, 0, 50,
                MonsterType.Standard, Element.Cold, null),
            new MonsterModel("OGRE", 100, 195, 195, 18, 1, 96, Status.None, 1, 10, 9, 33, 18, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("GrOGRE", 132, 300, 282, 23, 1, 100, Status.None, 1, 14, 15, 36, 23, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("WzOGRE", 144, 723, 723, 23, 1, 101, Status.None, 1, 10, 27, 40, 27, 50, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("ASP", 56, 50, 123, 6, 1, 91, Status.Poison, 1, 6, 15, 23, 14, 0, 0, MonsterType.Standard,
                Element.None, null),
            new MonsterModel("COBRA", 80, 50, 165, 22, 1, 94, Status.None, 16, 10, 18, 28, 15, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("SeaSNAKE", 224, 600, 957, 35, 1, 111, Status.None, 0, 12, 24, 58, 0, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("SCORPION", 84, 70, 225, 22, 2, 94, Status.Poison, 1, 10, 27, 28, 16, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("LOBSTER", 148, 300, 639, 35, 3, 102, Status.Poison, 1, 18, 30, 43, 0, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("BULL", 164, 489, 489, 22, 2, 104, Status.None, 1, 4, 24, 48, 22, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("ZomBULL", 224, 1050, 1050, 40, 1, 111, Status.None, 1, 14, 18, 58, 28, 0, 0,
                MonsterType.Undead, Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("TROLL", 184, 621, 621, 24, 3, 106, Status.None, 1, 12, 24, 50, 28, 0, 0,
                MonsterType.Standard, Element.Fire, null),
            new MonsterModel("SeaTROLL", 216, 852, 852, 40, 1, 110, Status.None, 1, 20, 24, 55, 0, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth}),
            new MonsterModel("SHADOW", 50, 45, 90, 10, 1, 90, Status.Darkness, 1, 0, 18, 19, 22, 0, 0,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("IMAGE", 86, 231, 231, 22, 1, 95, Status.Paralysis, 1, 4, 45, 26, 40, 0, 0,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("WRAITH", 114, 432, 432, 40, 1, 98, Status.Paralysis, 1, 12, 54, 34, 40, 0, 0,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("GHOST", 180, 990, 990, 93, 1, 106, Status.Paralysis, 1, 30, 18, 43, 0, 0, 0,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("ZOMBIE", 20, 12, 24, 10, 1, 86, Status.None, 1, 0, 3, 13, 20, 0, 0, MonsterType.Undead,
                Element.Fire, new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("GHOUL", 48, 50, 93, 8, 3, 90, Status.Paralysis, 1, 6, 6, 18, 22, 0, 0, MonsterType.Undead,
                Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("GEIST", 56, 117, 117, 8, 3, 91, Status.Paralysis, 1, 10, 23, 20, 40, 0, 0,
                MonsterType.Undead, Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("SPECTER", 52, 150, 150, 20, 1, 90, Status.Paralysis, 1, 12, 21, 23, 40, 0, 0,
                MonsterType.Undead, Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("WORM", 448, 1000, 4344, 65, 1, 139, Status.None, 5, 10, 18, 100, 0, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("Sand W", 200, 900, 2683, 46, 1, 108, Status.None, 1, 14, 31, 52, 22, 0, 50,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("Grey W", 280, 400, 1671, 50, 1, 118, Status.None, 1, 31, 2, 72, 0, 0, 0,
                MonsterType.Standard, Element.Cold, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("EYE", 162, 3225, 3225, 30, 1, 104, Status.None, 1, 30, 6, 46, 0, 63, 23,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("PHANTOM", 360, 1, 1, 120, 1, 158, Status.Paralysis, 2, 60, 12, 80, 0, 50, 25,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("MEDUSA", 68, 699, 699, 20, 1, 92, Status.Poison, 1, 10, 18, 28, 35, 0, 50,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("GrMEDUSA", 96, 1218, 1218, 11, 10, 96, Status.Paralysis, 1, 12, 36, 35, 0, 0, 50,
                MonsterType.Standard, Element.Fire, new[] {Element.Cold, Element.Earth}),
            new MonsterModel("CATMAN", 160, 780, 780, 30, 2, 103, Status.Poison, 1, 16, 24, 47, 34, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("MANCAT", 110, 800, 603, 20, 3, 98, Status.None, 1, 30, 30, 31, 35, 75, 0,
                MonsterType.Standard, Element.None,
                new[]
                {
                    Element.Cold, Element.Death, Element.Earth, Element.Fire, Element.Lightning, Element.Poison,
                    Element.Status
                }),
            new MonsterModel("PEDE", 222, 300, 1194, 39, 1, 111, Status.Poison, 1, 20, 24, 58, 16, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("GrPEDE", 320, 1000, 2244, 73, 1, 123, Status.None, 1, 24, 24, 93, 48, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Cold, Element.Fire}),
            new MonsterModel("TIGER", 132, 108, 438, 22, 2, 100, Status.None, 13, 8, 24, 43, 18, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("Saber T", 200, 500, 843, 24, 2, 108, Status.None, 35, 8, 21, 53, 50, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("VAMPIRE", 156, 2000, 1200, 76, 1, 103, Status.Paralysis, 1, 24, 36, 38, 0, 0, 25,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("WzVAMP", 300, 3000, 2385, 90, 1, 104, Status.Paralysis, 1, 28, 36, 42, 0, 25, 19,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("GARGOYLE", 80, 80, 132, 12, 4, 94, Status.None, 1, 8, 23, 27, 26, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("R.GOYLE", 94, 387, 387, 10, 4, 96, Status.None, 1, 32, 36, 64, 27, 50, 0,
                MonsterType.Standard, Element.None, new[] {Element.Cold, Element.Earth, Element.Fire}),
            new MonsterModel("EARTH", 288, 768, 1536, 66, 1, 119, Status.None, 1, 20, 9, 65, 0, 0, 0,
                MonsterType.Standard, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Lightning, Element.Poison, Element.Status}),
            new MonsterModel("FIRE", 276, 800, 1620, 50, 1, 118, Status.None, 1, 20, 21, 65, 0, 0, 0,
                MonsterType.Standard, Element.Cold,
                new[] {Element.Death, Element.Earth, Element.Fire, Element.Poison, Element.Status}),
            new MonsterModel("Frost D", 200, 2000, 1701, 53, 1, 108, Status.None, 1, 8, 60, 98, 0, 0, 50,
                MonsterType.Standard, new[] {Element.Fire, Element.Lightning},
                new[] {Element.Cold, Element.Earth, Element.Poison}),
            new MonsterModel("Red D", 248, 4000, 2904, 75, 1, 114, Status.None, 1, 30, 48, 100, 0, 0, 50,
                MonsterType.Standard, new[] {Element.Cold, Element.Poison}, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("ZombieD", 268, 999, 2331, 56, 1, 117, Status.Paralysis, 1, 30, 12, 68, 0, 0, 0,
                MonsterType.Undead, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("SCUM", 24, 20, 84, 1, 1, 84, Status.Poison, 1, 255, 0, 18, 22, 0, 0, MonsterType.Standard,
                new[] {Element.Cold, Element.Fire},
                new[] {Element.Death, Element.Earth, Element.Lightning, Element.Poison, Element.Status}),
            new MonsterModel("MUCK", 76, 70, 255, 30, 1, 93, Status.None, 1, 7, 2, 28, 36, 0, 0, MonsterType.Standard,
                Element.Lightning,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Fire, Element.Poison, Element.Status}),
            new MonsterModel("OOZE", 76, 70, 252, 32, 1, 93, Status.None, 1, 6, 3, 28, 32, 0, 0, MonsterType.Standard,
                new[] {Element.Cold, Element.Fire},
                new[] {Element.Death, Element.Earth, Element.Lightning, Element.Poison, Element.Status}),
            new MonsterModel("SLIME", 156, 900, 1101, 49, 1, 103, Status.Poison, 1, 255, 12, 43, 0, 0, 0,
                MonsterType.Standard, Element.Fire,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Lightning, Element.Poison, Element.Status}),
            new MonsterModel("SPIDER", 28, 8, 30, 10, 1, 87, Status.None, 1, 0, 15, 14, 15, 0, 0, MonsterType.Standard,
                Element.None, null),
            new MonsterModel("ARACHNID", 64, 50, 141, 5, 1, 92, Status.Poison, 1, 12, 12, 23, 16, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("MANTICOR", 164, 650, 1317, 22, 2, 104, Status.None, 1, 8, 36, 48, 35, 0, 50,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("SPHINX", 228, 1160, 1160, 23, 3, 112, Status.None, 1, 12, 60, 58, 26, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("R.ANKYLO", 256, 300, 1428, 60, 3, 115, Status.None, 1, 38, 28, 65, 33, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("ANKYLO", 352, 1, 2610, 98, 1, 127, Status.None, 1, 48, 24, 78, 32, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("MUMMY", 80, 300, 300, 30, 1, 94, Status.Sleep, 1, 20, 12, 30, 46, 0, 0,
                MonsterType.Undead, Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("WzMUMMY", 188, 1000, 984, 43, 1, 107, Status.Sleep, 1, 24, 12, 48, 34, 0, 0,
                MonsterType.Undead, Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("COCTRICE", 50, 200, 186, 1, 1, 89, Status.Stone, 1, 4, 36, 24, 22, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("PERILISK", 44, 500, 423, 20, 1, 89, Status.None, 1, 4, 36, 23, 22, 0, 25,
                MonsterType.Standard, Element.Cold, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("WYVERN", 212, 50, 1173, 30, 1, 110, Status.Poison, 1, 12, 48, 58, 35, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("WYRM", 260, 502, 1218, 40, 1, 116, Status.None, 1, 22, 30, 66, 35, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Earth}),
            new MonsterModel("TYRO", 480, 502, 3387, 65, 1, 150, Status.None, 1, 10, 30, 100, 32, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("T REX", 600, 600, 7200, 115, 1, 155, Status.None, 15, 10, 30, 100, 35, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("CARIBE", 92, 20, 240, 22, 1, 95, Status.None, 1, 0, 36, 34, 29, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("R.CARIBE", 172, 46, 546, 37, 1, 105, Status.None, 1, 20, 36, 42, 31, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("GATOR", 184, 900, 816, 42, 2, 106, Status.None, 1, 16, 24, 52, 29, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("FrGATOR", 288, 2000, 1890, 56, 2, 119, Status.None, 1, 20, 24, 72, 31, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("OCHO", 208, 102, 1224, 20, 3, 109, Status.Poison, 1, 24, 12, 58, 48, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("NAOCHO", 344, 500, 3189, 35, 3, 126, Status.Poison, 1, 32, 12, 85, 0, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("HYDRA", 212, 150, 915, 30, 3, 110, Status.None, 1, 14, 18, 58, 29, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("R.HYDRA", 182, 400, 1215, 20, 3, 106, Status.None, 1, 14, 18, 52, 36, 0, 50,
                MonsterType.Standard, Element.Cold, new[] {Element.Fire}),
            new MonsterModel("GUARD", 200, 400, 1224, 25, 2, 108, Status.Paralysis, 1, 40, 36, 55, 0, 0, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("SENTRY", 400, 2000, 4000, 102, 1, 128, Status.None, 1, 48, 48, 80, 35, 0, 0,
                MonsterType.Standard, Element.Lightning,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Fire, Element.Poison, Element.Status}),
            new MonsterModel("WATER", 300, 800, 1962, 69, 1, 117, Status.None, 1, 20, 36, 65, 0, 0, 0,
                MonsterType.Standard, Element.Cold,
                new[] {Element.Death, Element.Earth, Element.Fire, Element.Poison, Element.Status}),
            new MonsterModel("AIR", 358, 807, 1614, 53, 1, 114, Status.None, 1, 4, 72, 65, 0, 0, 0,
                MonsterType.Standard, Element.None,
                new[] {Element.Death, Element.Earth, Element.Poison, Element.Status}),
            new MonsterModel("NAGA", 356, 2355, 2355, 9, 1, 119, Status.Poison, 1, 8, 36, 58, 0, 75, 0,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("GrNAGA", 420, 4000, 3489, 7, 1, 127, Status.Poison, 1, 16, 24, 72, 37, 75, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("CHIMERA", 300, 2500, 2064, 30, 4, 113, Status.None, 1, 20, 36, 65, 0, 0, 50,
                MonsterType.Standard, Element.Cold, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("JIMERA", 350, 5000, 4584, 40, 4, 118, Status.None, 1, 18, 30, 72, 0, 0, 50,
                MonsterType.Standard, Element.Cold, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("WIZARD", 84, 300, 276, 30, 2, 94, Status.None, 1, 16, 33, 49, 23, 0, 0,
                MonsterType.Standard, Element.None, new[] {Element.Cold, Element.Fire, Element.Poison, Element.Status}),
            new MonsterModel("SORCERER", 112, 999, 822, 1, 3, 98, Status.KO, 1, 12, 24, 94, 25, 0, 50,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("GARLAND", 106, 250, 130, 15, 1, 97, Status.None, 1, 10, 6, 32, 0, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("Gas D", 353, 5000, 4068, 72, 1, 117, Status.None, 1, 16, 48, 100, 0, 0, 50,
                MonsterType.Standard, Element.Cold, new[] {Element.Earth}),
            new MonsterModel("Blue D", 454, 2000, 3274, 92, 1, 126, Status.None, 1, 20, 48, 100, 0, 0, 50,
                MonsterType.Standard, Element.None, new[] {Element.Earth, Element.Lightning}),
            new MonsterModel("MudGOL", 176, 800, 1257, 64, 1, 105, Status.Poison, 1, 7, 14, 47, 0, 25, 0,
                MonsterType.Standard, Element.None,
                new[] {Element.Cold, Element.Death, Element.Fire, Element.Lightning, Element.Poison, Element.Status}),
            new MonsterModel("RockGOL", 200, 1000, 2385, 70, 1, 108, Status.None, 1, 16, 12, 55, 0, 38, 0,
                MonsterType.Standard, Element.None,
                new[]
                {
                    Element.Cold, Element.Death, Element.Earth, Element.Fire, Element.Lightning, Element.Poison,
                    Element.Status
                }),
            new MonsterModel("IronGOL", 304, 3000, 6717, 93, 1, 121, Status.None, 1, 100, 12, 72, 0, 0, 13,
                MonsterType.Standard, Element.None,
                new[] {Element.Cold, Element.Death, Element.Earth, Element.Fire, Element.Poison, Element.Status}),
            new MonsterModel("BADMAN", 260, 1800, 1263, 44, 2, 116, Status.None, 1, 38, 18, 68, 0, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("EVILMAN", 190, 3000, 2700, 55, 1, 107, Status.None, 1, 32, 21, 87, 0, 25, 0,
                MonsterType.Standard, Element.None, new[] {Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("ASTOS", 168, 2000, 2250, 26, 1, 104, Status.None, 1, 40, 39, 85, 0, 75, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("MAGE", 105, 1095, 1095, 26, 1, 97, Status.None, 1, 40, 39, 85, 0, 50, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("FIGHTER", 200, 3420, 3420, 40, 1, 106, Status.None, 1, 38, 45, 93, 39, 38, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("MADPONY", 64, 15, 63, 10, 2, 92, Status.None, 1, 2, 11, 20, 13, 0, 0,
                MonsterType.Standard, Element.None, null),
            new MonsterModel("NITEMARE", 200, 700, 1272, 30, 3, 108, Status.None, 1, 24, 66, 50, 0, 0, 25,
                MonsterType.Standard, Element.Cold,
                new[] {Element.Death, Element.Earth, Element.Fire, Element.Poison, Element.Status}),
            new MonsterModel("WarMECH", 1000, 32000, 32000, 128, 2, 183, Status.None, 1, 80, 48, 100, 0, 0, 25,
                MonsterType.Standard, Element.None,
                new[]
                {
                    Element.Cold, Element.Death, Element.Earth, Element.Fire, Element.Lightning, Element.Poison,
                    Element.Status
                }),
            new MonsterModel("LICH", 400, 3000, 2200, 40, 1, 108, Status.Paralysis, 1, 40, 12, 60, 0, 75, 0,
                MonsterType.Undead, Element.Fire, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("LICH", 500, 1, 2000, 50, 1, 115, Status.Paralysis, 1, 50, 24, 70, 0, 75, 0,
                MonsterType.Undead, Element.None, new[] {Element.Cold, Element.Death, Element.Poison, Element.Status}),
            new MonsterModel("KARY", 600, 3000, 2475, 40, 6, 115, Status.None, 1, 50, 24, 92, 0, 38, 0,
                MonsterType.Standard, Element.Status,
                new[] {Element.Cold, Element.Fire, Element.Lightning, Element.Poison}),
            new MonsterModel("KARY", 700, 1, 2000, 60, 6, 115, Status.None, 1, 60, 30, 92, 0, 38, 0,
                MonsterType.Standard, Element.None,
                new[] {Element.Cold, Element.Fire, Element.Lightning, Element.Poison}),
            new MonsterModel("KRAKEN", 800, 5000, 4245, 50, 8, 128, Status.None, 1, 60, 42, 80, 0, 0, 25,
                MonsterType.Standard, Element.Lightning, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("KRAKEN", 900, 1, 2000, 70, 8, 140, Status.None, 1, 70, 49, 100, 0, 38, 16,
                MonsterType.Standard, Element.None, new[] {Element.Earth, Element.Fire}),
            new MonsterModel("TIAMAT", 1000, 6000, 5496, 49, 4, 123, Status.None, 1, 80, 36, 100, 0, 0, 50,
                MonsterType.Standard, Element.Poison,
                new[] {Element.Cold, Element.Earth, Element.Fire, Element.Lightning}),
            new MonsterModel("TIAMAT", 1100, 1, 2000, 75, 4, 126, Status.None, 1, 90, 45, 100, 0, 50, 25,
                MonsterType.Standard, Element.None,
                new[] {Element.Cold, Element.Earth, Element.Fire, Element.Lightning}),
        };

        public static readonly ZoneModel[] Zones =
        {
            new ZoneModel(0, ZoneType.Overworld, "Onrac, Gaia", 10, new[]
            {
                "Troll 2-4", "GrPede 1", "Wyvern 1-3", "FrGiant 1, FrWolf 0-2", "Wyvern 1-3", "Hydra 1-2, Ocho 0-2", "Manticor 1-3", "Giant 1-4, Iguana 1"
            }, "1,0/1,1/6,0/7,0", 3),
            new ZoneModel(1, ZoneType.Overworld, "NE Ordeals Desert", 10, new[]
            {
                "Giant 1-3, Iguana 0-2", "Wyvern 1-3", "Sand W 1", "Hydra 1-2, Ocho 0-2", "Sand W 1", "Catman 2-4", "Catman 2-4", "Sauria 1-2"
            }, "2,0/5,0"),
            new ZoneModel(2, ZoneType.Overworld, "Ordeals, NE Cardia", 10, new[]
            {
                "Sphinx 1-4", "Hydra 1-2, Ocho 0-2", "Catman 1-3, Saber T 0-2", "Catman 2-4", "Catman 1-3, Saber T 0-2", "Sauria 1-2", "Wyrm 1-3", "GrPede 1-2"
            }, "3,0/4,0/3,1/4,1"),
            new ZoneModel(3, ZoneType.Overworld, "South Onrac", 10, new[]
            {
                "Ankylo 1", "Saber T 1-3, Tiger 0-2", "Ankylo 1", "Saber T 1-3, Tiger 0-2", "Cerebus 1-3, WzOgre 0-2", "Cerebus 1-3, WzOgre 0-2", "Wyrm 1-3", "GrPede 1-2"
            }, "0,1/0,2/1,2",4,5),
            new ZoneModel(4, ZoneType.Overworld, "West Cardia, South Cardia", 10, new[]
            {
                "Wyrm 1-3", "Wyrm 1-3", "Ocho 1-3", "Ocho 1-3", "Manticor 1-3", "Manticor 1-3", "R.Ankylo 1-3", "Wyrm 1-3"
            }, "2,1/3,2"),
            new ZoneModel(5, ZoneType.Overworld, "Mirage Desert, Lefein Lake", 10, new[]
            {
                "Ankylo 1-2", "Tyro 1, Wyvern 0-1", "Ankylo 1-2", "Tyro 1, Wyvern 0-1", "R.Ankylo 1-4", "R.Ankylo 1-4", "Sand W 1-2", "T Rex 1"
            }, "5,1/6,1/5,2/6,2"),
            new ZoneModel(6, ZoneType.Overworld, "Peninsula of Power, Lefein", 10, new[]
            {
                "ZomBull 1-4, Troll 0-2", "Giant 2-4", "ZomBull 1-4, Troll 0-2", "Giant 2-4", "FrWolf 4-7", "FrWolf 4-7", "Tyro 1", "Wyvern 1-4"
            }, "7,1/7,2/7,3", 0,2),
            new ZoneModel(7, ZoneType.Overworld, "Temple of Fiends", 10, new[]
            {
                "Bone 2-4", "Spider 1-2", "GrImp 1-3", "Imp 3-5", "Madpony 1 ", "Creep 1-2", "Imp 3-6, GrImp 0-4", "Madpony 2-4"
            }, "3,3/4,3"),
            new ZoneModel(8, ZoneType.Overworld, "Matoya, Elfland, NW Castle, Dwarf C, N Melmond", 10, new[]
            {
                "Wolf 4-6, GrWolf 0-1", "Asp 1-2", "Creep 1-3, Ogre 1", "Ogre 1-2", "Creep 1-3, Ogre 1", "GrWolf 2-5, Wolf 0-3", "Arachnid 1-4", "GrOgre 1, Ogre 1-2"
            }, "2,4/3,4/3,5/3,6/4,6/4,7/5,3"),
            new ZoneModel(9, ZoneType.Overworld, "SW Melmond, Earth Cave, Sarda's Cave", 10, new[]
            {
                "Ghoul 2-5, Geist 0-4", "Shadow 3-7", "GrOgre 1, Ogre 1-2", "Shadow 3-7", "Ogre 1-3, Hyena 0-2", "GrWolf 4-8", "Tiger 1-3", "Arachnid 3-6, Spider 0-2"
            }, "1,4/0,5/1,5/2,5"),
            new ZoneModel(10, ZoneType.Overworld, "North Corneria", 10, new[]
            {
                "Imp 3-5", "Imp 3-5", "Imp 3-5", "Imp 3-5", "GrImp 1-3", "Wolf 1-2", "Madpony 1 ", "Imp 3-6, GrImp 0-4"
            }, "4,4"),
            new ZoneModel(11, ZoneType.Overworld, "Path to Pravoka", 10, new[]
            {
                "Iguana 1", "Creep 1-2", "GrImp 1-3, Wolf 0-2, GrWolf 0-2, Imp 0-2", "Madpony 2-4", "Wolf 4-6, GrWolf 0-1", "Wolf 4-6, GrWolf 0-1", "Ogre 1-2", "Creep 1-3, Ogre 1"
            }, "5,4/6,4/0,0"),
            new ZoneModel(12, ZoneType.Overworld, "S Pravoka, SW Elfland, Marsh C, Ice C, Gurgu", 10, new[]
            {
                "GrImp 2-5, WrWolf 0-2", "Ogre 1-2", "GrImp 2-5, WrWolf 0-2", "Geist 1-4", "GrWolf 2-5, Wolf 0-3", "Arachnid 1-4", "GrOgre 1, Ogre 1-2", "Scorpion 2-4"
            }, "2,6/2,7/3,7/5,6/5,7/6,5/7,4/7,5"),
            new ZoneModel(13, ZoneType.Overworld, "Near Crescent Lake", 10, new[]
            {
                "Pede 1-4", "Troll 1-2", "Cobra 2-6, Scorpion 0-4", "Giant 1-2", "Cobra 2-6, Scorpion 0-4", "Troll 1-2, Bull 0-2", "Troll 1-2, Bull 0-2", "Scorpion 2-6, Bull 1-2"
            }, "6,6/7,6/6,7/7,7"),
            new ZoneModel(14, ZoneType.Overworld, "SW Pravoka, West Ice Cave", 10, new[]
            {
                "Asp 1-2", "Arachnid 1-2", "Wolf 4-6, GrWolf 0-1", "GrImp 2-5, WrWolf 0-2", "Ogre 1-2", "Ogre 1-2", "GrWolf 2-5, Wolf 0-3", "WrWolf 3-6"
            }, "5,5"),
            new ZoneModel(15, ZoneType.Overworld, "South Coneria/Default Zone", 10, new[]
            {
                "Imp 3-5", "Imp 3-5", "Imp 3-5", "Imp 3-5", "Imp 3-5", "Imp 3-5", "Imp 3-5", "Imp 3-5"
            }, "4,5"),
            new ZoneModel(16, ZoneType.Ocean, "Ocean", 3, new[]
            {
                "Sahag 0-6, OddEye 1-2", "Shark 1", "Kyzoku 1-5", "Sahag 4-6", "Shark 1, R.Sahag 0-1", "Sahag 3-7*, R.Sahag 0-2", "Shark 1-2, Sahag 0-2", "Shark 1-2, R.Sahag 0-3"
            }),
            new ZoneModel(17, ZoneType.Ocean,"Northern River", 10, new[]
            {
                "Hydra 1-4*, Gator 0-3", "Hydra 1-4*, Gator 0-3", "Caribe 0-2, Gator 0-2, Ocho 1", "Naocho 1", "Caribe 0-2, Gator 0-2, Ocho 1", "FrGator 1-2, R.Caribe 0-3", "FrGator 1-2, R.Caribe 0-3", "FrGator 1, R.Caribe 1-4"
            }),
            new ZoneModel(18, ZoneType.Ocean,"Southern River", 10, new[]
            {
                "Hydra 1-2", "Caribe 2-6", "Hydra 1-2", "Caribe 2-6", "Ocho 1, Caribe 0-2", "Hydra 1, Ocho 0-1", "Caribe 3-8", "Caribe 2-4, Gator 0-2"
            }),
            new ZoneModel(19, ZoneType.Dungeon, "Temple of Fiends", 8, new[]
            {
                "Bone 2-4", "Spider 1-2", "Zombie 2-4", "Ghoul 1", "Creep 1-2", "Wolf 4-6, GrWolf 0-1", "GrImp 1-3, Wolf 0-2, GrWolf 0-2, Imp 0-2", "GrImp 2-5, WrWolf 0-2"
            }),
            new ZoneModel(20, ZoneType.Dungeon, "Marsh Cave B1", 8, new[]
            {
                "Shadow 2-4", "Shadow 2-4", "Scum 2-4", "Zombie 2-3, Ghoul 2-4", "Bone 3-5, Crawl 0-2", "Arachnid 1-2, Spider 0-2, Scum 0-1, Muck 0-1", "Muck 1-3", "WrWolf 3-6"
            }),
            new ZoneModel(21, ZoneType.Dungeon, "Marsh Cave B2", 8, new[]
            {
                "Zombie 2-3, Ghoul 2-4", "Scum 2-4", "Gargoyle 2-3", "Bone 3-5, Crawl 0-2", "R.Bone 1, Bone 2-4, Crawl 1", "Arachnid 1-2, Spider 0-2, Scum 0-1, Muck 0-1", "Scorpion 2-4", "WrWolf 2-5, GrWolf 0-5"
            }),
            new ZoneModel(22, ZoneType.Dungeon, "Marsh Cave B3", 8, new[]
            {
                "Scum 2-4", "Gargoyle 2-3", "R.Bone 1, Bone 2-4, Crawl 1", "Muck 1-3", "Arachnid 1-2, Spider 0-2, Scum 0-1, Muck 0-1", "WrWolf 3-6", "Scorpion 2-4", "WrWolf 2-5, GrWolf 0-5"
            }),
            new ZoneModel(23, ZoneType.Dungeon, "Titan's Tunnel", 8, new[]
            {
                "GrOgre 1, Ogre 1-2", "GrOgre 1, Ogre 1-2", "GrWolf 4-8", "GrWolf 4-8", "GrWolf 4-8", "Tiger 1-3", "Tiger 1-3", "Arachnid 4-8"
            }),
            new ZoneModel(24, ZoneType.Dungeon, "Earth Cave B1", 8, new[]
            {
                "Cobra 2-6", "Cobra 2-6", "Bull 1-2", "Asp 3-7", "GrOgre 1-4, Ogre 0-2", "Gargoyle 3-8", "Arachnid 3-6, Spider 0-2", "Mummy 2-5"
            }),
            new ZoneModel(25, ZoneType.Dungeon, "Earth Cave B2", 8, new[]
            {
                "Asp 3-7", "Bull 1-2", "Gargoyle 3-8", "GrOgre 1-4, Ogre 0-2", "WrWolf 2-5, GrWolf 0-5", "Arachnid 4-8", "Troll 1-2, Bull 0-1", "Giant 1-2"
            }),
            new ZoneModel(26, ZoneType.Dungeon, "Earth Cave B3", 8, new[]
            {
                "WrWolf 2-5, GrWolf 0-5", "GrOgre 1-4, Ogre 0-2", "Wizard 2-4", "Specter 2-5, Geist 2-5", "Image 2-6", "Coctrice 2-6", "Mummy 2-5", "Ooze 2-5, Arachnid 0-5"
            }, null, 2),
            new ZoneModel(27, ZoneType.Dungeon, "Earth Cave B4", 8, new[]
            {
                "Wizard 2-4", "Specter 2-5, Geist 2-5", "Image 2-6", "Troll 1-2", "Coctrice 2-6", "GrImp 0-5, WrWolf 1-3, Giant 0-2", "GrImp 0-5, WrWolf 1-3, Giant 0-2", "Ooze 2-5, Arachnid 0-5"
            }, null, 0),
            new ZoneModel(28, ZoneType.Dungeon, "Earth Cave B5", 8, new[]
            {
                "Troll 1-2", "Image 2-6", "Arachnid 4-8", "Troll 1-2, Bull 0-1", "Giant 1-2", "Mummy 2-5", "Ooze 2-5, Arachnid 0-5", "Earth 1"
            }, null, 7),
            new ZoneModel(29, ZoneType.Dungeon, "Gurgu Volcano B1", 8, new[]
            {
                "Sphinx 1-2", "Sphinx 1-2", "R.Goyle 2-5", "WzOgre 1, GrOgre 1, Hyena 0-7*", "Pede 1-6*", "Scorpion 2-6, Bull 1-2", "Scorpion 2-6, Bull 1-2", "Muck 4-7"
            }),
            new ZoneModel(30, ZoneType.Dungeon, "Gurgu Volcano B2", 8, new[]
            {
                "R.Goyle 2-5", "WzOgre 1, GrOgre 1, Hyena 0-7*", "Giant 1-2, Iguana 0-3", "Pede 1-6*", "Perilisk 2-5", "R.Hydra 1", "Muck 4-7", "Fire 1-2"
            }, null, 7),
            new ZoneModel(31, ZoneType.Dungeon, "Gurgu Volcano B3", 8, new[]
            {
                "Giant 1-2, Iguana 0-3", "WzOgre 1, GrOgre 1, Hyena 0-7*", "Cerebus 1-2", "R.Hydra 1", "Perilisk 2-5", "Cerebus 0-1, WzOgre 1-2", "WzOgre 1-3, GrOgre 0-2", "Fire 1-2"
            }, null, 5,7),
            new ZoneModel(32, ZoneType.Dungeon, "Gurgu Volcano B4", 8, new[]
            {
                "Cerebus 1-2", "R.Hydra 1", "Grey W 1", "Bull 2-4", "R.Giant 1-2", "WzOgre 1-3, GrOgre 0-2", "Agama 1", "Red D 1"
            }),
            new ZoneModel(33, ZoneType.Dungeon, "Gurgu Volcano B5", 8, new[]
            {
                "Grey W 1", "Bull 2-4", "WzOgre 1-3, GrOgre 0-2", "R.Giant 1-2", "Cerebus 0-1, WzOgre 1-2", "Fire 1-2", "Agama 1", "Red D 1"
            }, null, 4,5),
            new ZoneModel(34, ZoneType.Dungeon, "Ice Cave B1", 8, new[]
            {
                "Wizard 3-7", "Wizard 3-7", "Coctrice 2-6, Mummy 1-5", "Coctrice 2-6, Mummy 1-5", "R.Bone 3-6", "Wraith 2-6", "Wraith 1-5, Image 0-3, Specter 0-3, Geist 0-3", "Frost D 1-2"
            }, null, 0,1),
            new ZoneModel(35, ZoneType.Dungeon, "Ice Cave B2", 8, new[]
            {
                "Image 2-6, Wraith 0-4", "Image 2-6, Wraith 0-4", "Sorcerer 1-3", "Wraith 2-6", "Mage 1-4", "Wraith 1-5, Image 0-3, Specter 0-3, Geist 0-3", "FrGiant 1, FrWolf 0-2", "Frost D 1-2"
            }, null, 6),
            new ZoneModel(36, ZoneType.Dungeon, "Ice Cave B3", 8, new[]
            {
                "R.Bone 3-6", "Mage 1-4", "Wraith 2-6", "Sorcerer 1-3", "FrGiant 1, FrWolf 0-2", "GrPede 1", "Frost D 1-2", "FrWolf 3-7"
            }, null, 4),
            new ZoneModel(37, ZoneType.Dungeon, "Castle of Ordeals 2F", 8, new[]
            {
                "ZomBull 1-3", "ZomBull 1-3", "R.Goyle 3-7", "Medusa 2-5", "ManCat 3-5", "Mummy 3-7, WzMummy 1", "Sorcerer 2-5", "ZombieD 1-2"
            }, null, 0,1,7),
            new ZoneModel(38, ZoneType.Dungeon, "Castle of Ordeals 3F", 8, new[]
            {
                "R.Goyle 3-7", "Medusa 2-5", "ManCat 3-5", "Mummy 3-7, WzMummy 1", "Mummy 3-7, WzMummy 1", "Sorcerer 2-5", "Sorcerer 2-5", "ZombieD 1-2"
            }, null, 7),
            new ZoneModel(39, ZoneType.Dungeon, "Sea Shrine B2", 8, new[]
            {
                "GrShark 1-2, Shark 0-1", "SeaSnake 2-4", "WzSahag 1-2, R.Sahag 8", "Ghost 2-5", "GrShark 1-2, Shark 0-1", "GrShark 1, BigEye 0-1", "Water 1-3", "SeaTroll 1-2, SeaSnake 0-2, Lobster 0-2"
            }, null, 2,3,6),
            new ZoneModel(40, ZoneType.Dungeon, "Sea Shrine B3 (enter)", 8, new[]
            {
                "SeaTroll 1-2, Lobster 1-3", "SeaSnake 2-4", "Naga 1, Water 0-1", "GrShark 1-2, Shark 0-1", "GrShark 1-2, Shark 0-1", "WzSahag 1-2, R.Sahag 8", "WzSahag 0-1, GrShark 1-2", "SeaTroll 1-2, SeaSnake 0-2, Lobster 0-2"
            }, null, 5),
            new ZoneModel(41, ZoneType.Dungeon, "Sea Shrine B4", 8, new[]
            {
                "SeaTroll 1, SeaSnake 0-3", "Lobster 1-5, SeaSnake 0-3", "WzSahag 1-2, R.Sahag 8", "Ghost 2-5", "SeaTroll 1-2, Lobster 1-4", "GrShark 1, BigEye 0-1", "Lobster 3-7", "SeaSnake 3-6"
            }, null, 2,3),
            new ZoneModel(42, ZoneType.Dungeon, "Sea Shrine B5", 8, new[]
            {
                "Lobster 1-5, SeaSnake 0-3", "SeaTroll 1, SeaSnake 0-3", "GrShark 1, BigEye 0-1", "SeaTroll 1-2, Lobster 1-4", "Water 1-3", "Lobster 3-7", "Lobster 3-7", "SeaSnake 3-6"
            }, null, 4),
            new ZoneModel(43, ZoneType.Dungeon, "Waterfall", 8, new[]
            {
                "Nitemare 1-3*", "MudGol 1-3", "Nitemare 1-3*", "MudGol 1-3", "WzMummy 1-2, Mummy 1-6", "WzMummy 1-2, Mummy 1-6", "Perilisk 4-8", "Gas D 1"
            }, null, 7),
            new ZoneModel(44, ZoneType.Dungeon, "Mirage Tower 1F", 8, new[]
            {
                "Nitemare 1-2, Badman 1-2", "Nitemare 1-2, Badman 1-2", "Vampire 2-5", "Chimera 1-3", "Guard 2-5", "Medusa 3-6, Saber T 1-2", "Catman 4-7", "Catman 3-6, Saber T 1-2"
            }),
            new ZoneModel(45, ZoneType.Dungeon, "Mirage Tower 2F", 8, new[]
            {
                "Chimera 1-3", "Cerebus 3-4", "Guard 2-5", "Vampire 2-5", "WzMummy 1-5, Mummy 0-8, Coctrice 0-8, Perilisk 0-8", "Catman 4-7", "Catman 3-6, Saber T 1-2", "Blue D 1"
            }, null, 7),
            new ZoneModel(46, ZoneType.Dungeon, "Mirage Tower 3F", 8, new[]
            {
                "Cerebus 3-4", "Vampire 2-5", "Catman 4-7", "WzMummy 1-5, Mummy 0-8, Coctrice 0-8, Perilisk 0-8", "Medusa 3-6, Saber T 1-2", "Wyvern 1-3, Wyrm 0-5*", "Wyvern 1-3, Wyrm 0-5*", "Blue D 1"
            }, null, 7),
            new ZoneModel(47, ZoneType.Dungeon, "Sky Castle 1F", 8, new[]
            {
                "Badman 2-5", "GrNaga 1, Air 0-1", "Eye 1", "Badman 2-5", "Evilman 1, Nitemare 1-2", "Manticor 3-4", "GrMedusa 1-4", "Slime 3-6"
            }, null, 2),
            new ZoneModel(48, ZoneType.Dungeon, "Sky Castle 2F", 8, new[]
            {
                "GrNaga 1, Air 0-1", "Eye 1", "Manticor 3-4", "Chimera 1", "Evilman 1, Nitemare 1-2", "GrMedusa 1-4", "RockGol 2-4", "Slime 3-6"
            }, null, 1),
            new ZoneModel(49, ZoneType.Dungeon, "Sky Castle 3F", 8, new[]
            {
                "Guard 0-1, Sentry 1", "Mancat 3-7, Medusa 0-5", "Chimera 1", "Manticor 3-4", "R.Hydra 4", "RockGol 2-4", "WzVamp 1-3", "Fighter 1-2"
            }),
            new ZoneModel(50, ZoneType.Dungeon, "Sky Castle 4F", 8, new[]
            {
                "Mancat 3-7, Medusa 0-5", "Guard 0-1, Sentry 1", "Naocho 1-2", "Air 2-4", "Sorcerer 1-6, MudGol 1-2", "WzVamp 1-3", "GrNaga 0-1, Air 1-3", "Fighter 1-2"
            }),
            new ZoneModel(51, ZoneType.Dungeon, "Sky Castle 5F", 24, new[]
            {
                "Naocho 1-2", "Air 2-4", "Sorcerer 1-6, MudGol 1-2", "R.Hydra 4", "GrNaga 0-1, Air 1-3", "Slime 3-6", "Warmech 1", "Fighter 1-2"
            }),
            new ZoneModel(52, ZoneType.Dungeon, "Temple of Fiends 1F", 8, new[]
            {
                "Worm 1-2", "Worm 1-2", "Frost D 3-4", "Frost D 3-4", "Frost D 3-4", "Chimera 3-4", "Chimera 3-4", "Slime 4-8"
            }, null, 0,1),
            new ZoneModel(53, ZoneType.Dungeon, "Temple of Fiends 2F", 8, new[]
            {
                "ZombieD 2-4", "ZombieD 2-4", "FrGiant 2, FrWolf 2-6", "FrGiant 2, FrWolf 2-6", "Chimera 1-2, Jimera 1-2", "Chimera 1-2, Jimera 1-2", "Slime 4-8", "WzVamp 1-3, ZombieD 1-2"
            }, null, 0,1,2,3),
            new ZoneModel(54, ZoneType.Dungeon, "Temple of Fiends 3F", 8, new[]
            {
                "Badman 5-9", "Badman 5-9", "Gas D 2-4", "Gas D 2-4", "Gas D 2-4", "Mage 2-3, Fighter 1", "Mage 2-3, Fighter 1", "WzVamp 1-3, ZombieD 1-2"
            }, null, 2,3,4),
            new ZoneModel(55, ZoneType.Dungeon, "Temple of Fiends - Earth", 9, new[]
            {
                "MudGol 1-4, RockGol 1-3*", "GrMedusa 4-7", "MudGol 1-4, RockGol 1-3*", "GrMedusa 4-7", "Earth 2-4", "Earth 2-4", "Earth 2-4", "Sauria 2-4"
            }, null, 4,5,6),
            new ZoneModel(56, ZoneType.Dungeon, "Temple of Fiends - Fire", 10, new[]
            {
                "Grey W 2-4", "Grey W 2-4", "R.Giant 1, Agama 1-3", "R.Giant 1, Agama 1-3", "Agama 2-4", "Agama 2-4", "Fire 3-4", "Red D 2-4"
            }, null, 6),
            new ZoneModel(57, ZoneType.Dungeon, "Temple of Fiends - Water", 11, new[]
            {
                "Water 3-6", "Lobster 1-6, SeaSnake 2-5, SeaTroll 2", "Water 3-6", "Lobster 1-6, SeaSnake 2-5, SeaTroll 2", "WzSahag 3-6, GrShark 2", "WzSahag 3-6, GrShark 2", "GrShark 1-2, BigEye 1-2", "Naga 1-2, Water 3-6"
            }, null, 0,2),
            new ZoneModel(58, ZoneType.Dungeon, "Temple of Fiends - Air", 12, new[]
            {
                "Air 3-6", "Worm 3-4", "WzVamp 1, Vampire 3-6", "RockGol 2-4", "Evilman 1-2, Nitemare 1-2", "WzVamp 1-3, ZombieD 1-2", "Sorcerer 3-7", "IronGol 1-2"
            }, null, 1,7),
        };        

        #endregion
    }


}
