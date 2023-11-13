using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Games.Games;

namespace Games
{    

    
    internal class EquipItem
    {
        public string EquipName { get; set; }
        public int BonusAtk { get; set; }
        public int BonusDef {  get; set; }
        public int BonusHp {  get; set; }
        public int EquipGold { get; set; }

        public EquipItem(string equipName, int equipHp, int equipAtk, int equipDef, int equipGold)
        {
            EquipName = equipName;
            BonusHp = equipHp;
            BonusAtk = equipAtk;
            BonusDef = equipDef;
            EquipGold = equipGold;

        }

        public static List<EquipItem> equip = new List<EquipItem>();
        public void ShowEquip()
        {
            string[] showEquip = { $"장비명 :{EquipName},생명력 :{BonusHp}, 공격력 :{BonusAtk}, 방어력 :{BonusDef}" };

            if (EquipItem.equip.Count == 0)
            {
                Console.WriteLine("장착하고 있는 아이템이 없습니다.");
            }
            else
            {
                Console.WriteLine("장착품 목록");
                for (int i = 0; i < EquipItem.equip.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {EquipItem.equip[i].EquipName},{EquipItem.equip[i].BonusHp},{EquipItem.equip[i].BonusAtk},{EquipItem.equip[i].BonusDef}");
                }
            }
        }
    }    
}
