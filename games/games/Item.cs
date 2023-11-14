using static Games.Games;
using System.Numerics;

namespace Games
{


    internal partial class Games
    {
        
        internal class Item 
        {
            public Stack<bool> OpenTreasure = new Stack<bool>();
            public string ItemName { get; set; }
            public int ItemHp { get; set; }
            public int ItemAtk { get; set; }
            public int ItemDef { get; set; }
            public int ItemGold { get; set; }

            public Item(string itemName, int itemHp, int itemAtk, int itemDef, int itemGold)
            {
                ItemName = itemName;
                ItemHp = itemHp;
                ItemAtk = itemAtk;
                ItemDef = itemDef;
                ItemGold = itemGold;
            }

            public static List<Item> inventory = new List<Item>();
            public void ShowInventory()
            {
               
                string[] itemStatus = {$"{ItemName}",$"{ItemHp}",$"{ItemAtk}",$"{ItemDef}",$"{ItemGold}" };

                if (Item.inventory.Count == 0)
                {
                    Console.WriteLine("소지품이 없습니다.");
                }
                else
                {
                    Console.WriteLine("소지품 목록");
                    for (int i = 0; i < Item.inventory.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Item.inventory[i].ItemName}");
                    }
                }
            }
            
        }
        

    }
}
