using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Reflection;
using static Games.Games;

namespace Games
{
    //현재 진행중의 버그
    //아이템의 장착 가능 갯수 제한이 안됨(수정됨)
    //장착중인 아이템의 이름이 제대로 출력이 안됨(수정됨)

    internal partial class Games
    {
        public static int playerX = 1;
        public static int playerY = 1;
        public static string[,] field;
        public static string[,] shop;

        static List<Tuple<int, int>> PlayerPosition = new List<Tuple<int, int>>();

        static void Main(string[] args)
        {



            Player player = ChoiceJob();//시작은 직업 선택으로
            Item item = new Item("", 0, 0, 0, 0);//기본 인벤토리
            EquipItem equipItem = new EquipItem("", 0, 0, 0, 0);//기본 장착템
            Console.Clear();
            player.MyStatus();

            while (true)
            {
                Console.WriteLine("1을 입력하면 맵으로 이동합니다.");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.Clear();
                    WorldMap(player, item, equipItem);
                }
                else
                {

                }
            }
        }
        static Player ChoiceJob()
        {
            Console.WriteLine("조선에 궁궐에 당도한 것을 환영하오 낯선이여 나는 깨우친 임금 세종이오\n낯선이여 그대는 지금 이세계로 전생을 한것이오\n어떤 사람으로 살고 싶은지 골라 보시오");
            Console.WriteLine("1. 전사, 2. 궁수 3.기사");
            string input = Console.ReadLine();
            if (input == "1")
            {
                return new Player("전사", 50, 15, 12, 250);
            }
            else if (input == "2")
            {
                return new Player("궁수", 40, 20, 8, 250);
            }
            else if (input == "3")
            {
                return new Player("기사", 60, 10, 16, 250);
            }
            else
            {
                Console.WriteLine("올바른 선택을 입력하세요.");
                return ChoiceJob();
            }
        }




        static void WorldMap(Player player, Item item, EquipItem equipItem)
        {
            string[,] field = {                       //가운데
                {"# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", "* ", "* ", ". ", ". ", "* ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", "* ", "* ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", "# "},
                {"# ", ". ", ". ", "$ ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},//가운
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},//데
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# "}
            };
            field[playerY, playerX] = "나";

            bool Moving = true;

            while (Moving)
            {
                Console.WriteLine("방향키로 이동을 할 수 있습니다.\nI를 누르면 현재 상태와 인벤토리를 확인 할 수 있습니다.\nE를 누르면 현재 장착중인 아이템을 확인합니다.\nEnter를 누르면 상호작용을 합니다.");
                // 맵 출력 2차원 배열
                for (int i = 0; i < 16; i++)//세로
                {
                    for (int j = 0; j < 13; j++)//가로
                    {
                        Console.Write(field[i, j]);
                    }
                    Console.WriteLine();
                }

                // 누르는 키 받기
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.I)
                {
                    player.MyStatus();
                    item.ShowInventory();
                }
                if (keyInfo.Key == ConsoleKey.E)
                {
                    item.ShowInventory();
                    EquipStatus(player, item, equipItem);
                }

                // 플레이어 이동 처리
                switch (keyInfo.Key)                            //위에서 키 입력 받은거로 판단
                {
                    case ConsoleKey.UpArrow:                    //입력받은 키가 위쪽 방향키면
                        if (field[playerY - 1, playerX] == ". ") //현재 좌표에서 Y좌표의 -1한 위치가 . 이면  
                        {
                            field[playerY, playerX] = ". ";       //원래 있던 자리를 . 으로 
                            playerY--;                          //Y좌표에 값에서 1빼기 == 위로 한칸
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (field[playerY + 1, playerX] == ". ") // 아래로 한칸
                        {
                            field[playerY, playerX] = ". ";
                            playerY++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (field[playerY, playerX - 1] == ". ") //X가 작아지니 왼쪽으로(0쪽으로) 한칸 
                        {
                            field[playerY, playerX] = ". ";
                            playerX--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (field[playerY, playerX + 1] == ". ") //오른쪽으로 한칸
                        {
                            field[playerY, playerX] = ". ";
                            playerX++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (field[playerY, playerX] == field[8, 3])   /*Enter 누를 때 좌표가  8,3이면*/
                        {
                            PlayerPosition.Add(new Tuple<int, int>(playerY, playerX));
                            Console.Clear();
                            Shop(player, item, equipItem);
                        }
                        else if (field[playerY, playerX] == field[7, 11] || field[playerY, playerX] == field[8, 11])
                        {
                            PlayerPosition.Add(new Tuple<int, int>(playerY, playerX));
                            Console.Clear();
                            Dungeon(player, item, equipItem);
                        }
                        break;
                }
                // 플레이어 위치 갱신
                field[playerY, playerX] = "나";
            }
        }

        static void Shop(Player player, Item item, EquipItem equipItem)
        {
            string[,] shop = {
                {"# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# "},
                {"# ", ". ", ". ", ". ", ". ", "상", "인", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# "},
                {"# ", ". ", ". ", ". ", "# ", "나", ". ", "# ", ". ", ". ", ". ", ". ", "# "},
                {"# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# "}
             };
            int playerX = 5; // 플레이어의 초기 X 위치 왼쪽 최상단이 0,0
            int playerY = 14;
            bool Moving = true;

            while (Moving)
            {
                Console.WriteLine("방향키로 이동을 할 수 있습니다.\nI를 누르면 현재 상태와 인벤토리를 확인 할 수 있습니다.\nE를 누르면 현재 장착중인 아이템을 확인합니다.\nEnter를 누르면 상호작용을 합니다.");
                // 맵 출력 2차원 배열
                for (int i = 0; i < 16; i++)//세로
                {
                    for (int j = 0; j < 13; j++)//가로
                    {
                        Console.Write(shop[i, j]);
                    }
                    Console.WriteLine();
                }
                // 누르는 키 받기
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.I)
                {
                    player.MyStatus();
                    item.ShowInventory();
                }
                if (keyInfo.Key == ConsoleKey.E)
                {
                    equipItem.ShowEquip();
                    EquipStatus(player, item, equipItem);
                }
                // 플레이어 이동 처리
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (shop[playerY - 1, playerX] == ". ")
                        {
                            shop[playerY, playerX] = ". ";
                            playerY--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (shop[playerY + 1, playerX] == ". ") // 아래로 한칸
                        {
                            shop[playerY, playerX] = ". ";
                            playerY++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (shop[playerY, playerX - 1] == ". ") //X가 작아지니 왼쪽으로(0쪽으로) 한칸 
                        {
                            shop[playerY, playerX] = ". ";
                            playerX--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (shop[playerY, playerX + 1] == ". ") //오른쪽으로 한칸
                        {
                            shop[playerY, playerX] = ". ";
                            playerX++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (shop[playerY, playerX] == shop[14, 5] || shop[playerY, playerX] == shop[14, 6])      /* Enter를 누를 때 좌표가  14,5이나 14, 6이면*/
                        {
                            Console.Clear();                                                                    //상점 나가기 구현
                            WorldMap(player, item, equipItem);
                        }
                        else if (shop[playerY, playerX] == shop[2, 5] || shop[playerY, playerX] == shop[2, 6])  /* 아니면 Enter를 누를 때 좌표가  2,5이나 2, 6이면*/
                        {
                            BuyItem(player, item, equipItem);                                                              //상점을 이용
                        }
                        break;
                }
                // 플레이어 위치 갱신
                shop[playerY, playerX] = "나";
            }
        }



        static void BuyItem(Player player, Item item, EquipItem equipItem)
        {
            while (true)
            {

                Console.WriteLine("아이템을 구매합니다.");
                Console.WriteLine("1. 검 구매 (150 Gold)");
                Console.WriteLine("2. 갑옷 구매 (100 Gold)");
                Console.WriteLine("3. 판매하기");
                Console.WriteLine("4. 나가기");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    if (player.Gold >= 150)
                    {
                        Console.Clear();
                        player.Gold -= 150;
                        item = new Item("검", 0, 4, 0, 150);
                        Item.inventory.Add(item);

                        Console.WriteLine("검을 구매했습니다.");

                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }
                else if (input == "2")
                {
                    if (player.Gold >= 100)
                    {
                        Console.Clear();
                        player.Gold -= 100;
                        item = new Item("갑옷", 10, 0, 2, 100);
                        Item.inventory.Add(item);

                        Console.WriteLine("갑옷을 구매했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }
                else if (input == "3")
                {
                    SellItem(player, item, equipItem);
                }
                else if (input == "4")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("올바른 선택을 입력하세요.");
                }
            }
        }

        static void SellItem(Player player, Item item, EquipItem equipItem)
        {
            item = new Item("1", 0, 0, 0, 0);
            Console.Clear();
            Console.WriteLine();
            item.ShowInventory();
            Console.WriteLine("판매할 아이템의 번호를 입력하세요 (취소: 0):");

            int itemNumber;
            if (int.TryParse(Console.ReadLine(), out itemNumber))
            {
                if (itemNumber >= 1 && itemNumber <= Item.inventory.Count)
                {


                    Item itemToSell = Item.inventory[itemNumber - 1];
                    player.Gold += itemToSell.ItemGold;
                    Item.inventory.Remove(itemToSell);

                    Console.WriteLine($"{itemToSell.ItemName}을 판매하여 {itemToSell.ItemGold} Gold를 획득하였습니다.");
                    player.MyStatus();
                }

                else if (itemNumber == 0)
                {
                }
                else
                {
                    Console.WriteLine("다시 입력해주시기 바랍니다.");
                }
            }
        }

        static void EquipStatus(Player player, Item item, EquipItem equipItem)
        {

            Console.Clear();
            Console.WriteLine();
            item.ShowInventory();
            Console.WriteLine("장착 할 아이템의 번호를 입력 해 주세요\n0을 입력하면 장비 해제를 할 수 있습니다");


            int equipNumber;
            if (int.TryParse(Console.ReadLine(), out equipNumber))
            {
                if (EquipItem.equip.Count == 0)
                {
                    if (equipNumber >= 1 && equipNumber <= Item.inventory.Count)
                    {

                        Item ItemToEquip = Item.inventory[equipNumber - 1];
                        player.Hp += ItemToEquip.ItemHp;
                        player.Atk += ItemToEquip.ItemAtk;
                        player.Def += ItemToEquip.ItemDef;
                        Item.inventory.Remove(ItemToEquip);
                        equipItem = new EquipItem(ItemToEquip.ItemName, ItemToEquip.ItemHp, ItemToEquip.ItemAtk, ItemToEquip.ItemDef, ItemToEquip.ItemGold);
                        EquipItem.equip.Add(equipItem);

                        Console.WriteLine($"{ItemToEquip.ItemName}을 장착했습니다");
                        player.MyStatus();
                    }
                    else
                    {

                    }
                }
                else if (equipNumber == 0)
                {
                    DisarmItem(player, item, equipItem);
                }
                else
                {
                    Console.WriteLine("장비는 하나만 장착 할 수 있습니다");
                }
            }
        }
        static void DisarmItem(Player player, Item item, EquipItem equipItem)
        {

            Console.Clear();
            Console.WriteLine();
            equipItem.ShowEquip();
            Console.WriteLine("장착해제 할 아이템의 번호를 입력 해 주세요");

            int equipNumber;
            if (int.TryParse(Console.ReadLine(), out equipNumber))
            {
                if (equipNumber >= 1 && equipNumber <= EquipItem.equip.Count)
                {

                    EquipItem ItemToDisarm = EquipItem.equip[equipNumber - 1];
                    player.Hp -= ItemToDisarm.BonusHp;
                    player.Atk -= ItemToDisarm.BonusAtk;
                    player.Def -= ItemToDisarm.BonusDef;
                    item = new Item(ItemToDisarm.EquipName, ItemToDisarm.BonusHp, ItemToDisarm.BonusAtk, ItemToDisarm.BonusDef, ItemToDisarm.EquipGold);
                    Item.inventory.Add(item);
                    EquipItem.equip.Remove(ItemToDisarm);



                    Console.WriteLine($"{ItemToDisarm.EquipName}을 장착해제했습니다");
                    player.MyStatus();
                }
                else if (equipNumber == 0)
                {

                }
                else
                {
                    Console.WriteLine("다시 입력해주시기 바랍니다.");
                }
            }
        }
        static void Dungeon(Player player, Item item, EquipItem equipItem)
        {
            string[,] field = {                       //가운데
                    {"# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# "},
                    {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", "상", "자", "# "},
                    {"# ", ". ", "# ", "# ", "# ", "# ", "# ", "# ", ". ", "# ", ". ", ". ", "# "},
                    {"# ", ". ", ", ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", "# ", ". ", "# "},
                    {"# ", ". ", "# ", "# ", "# ", "# ", "# ", "# ", ". ", ". ", "# ", ". ", "# "},
                    {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", ". ", "# ", ". ", "# "},
                    {"# ", "# ", "# ", "# ", "# ", "# ", "# ", ". ", "# ", ". ", "# ", ". ", "# "},
                    {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", ". ", "# ", ". ", "# "},//가운
                    {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", ". ", "# ", ". ", "# "},//데
                    {"# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", ". ", "# ", ". ", "# "},
                    {"# ", ". ", ". ", ". ", "# ", ". ", ". ", ". ", "# ", ". ", "# ", ". ", "# "},
                    {"# ", ". ", "# ", ". ", "# ", ". ", "# ", ". ", ". ", ". ", "# ", ". ", "# "},
                    {"# ", ". ", ", ", ". ", ". ", ". ", ". ", "# ", "# ", "# ", "# ", ". ", "# "},
                    {"# ", ". ", "# ", "# ", "# ", "# ", " .", "# ", "# ", ". ", ". ", ". ", "# "},
                    {"# ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", ". ", "# ", ". ", "# "},
                    {"# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# ", "# "}
                };
            int playerY = 8;
            int playerX = 1;
            field[playerY, playerX] = "나";

            bool Moving = true;
            bool openTreasure = false;

            while (Moving)
            {
                Console.WriteLine("방향키로 이동을 할 수 있습니다.\nI를 누르면 현재 상태와 인벤토리를 확인 할 수 있습니다.\nE를 누르면 현재 장착중인 아이템을 확인합니다.\nEnter를 누르면 상호작용을 합니다.");
                // 맵 출력 2차원 배열
                for (int i = 0; i < 16; i++)//세로
                {
                    for (int j = 0; j < 13; j++)//가로
                    {
                        Console.Write(field[i, j]);
                    }
                    Console.WriteLine();
                }

                // 누르는 키 받기
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.I)
                {
                    player.MyStatus();
                    item.ShowInventory();
                }
                if (keyInfo.Key == ConsoleKey.E)
                {
                    item.ShowInventory();
                    EquipStatus(player, item, equipItem);
                }

                // 플레이어 이동 처리
                switch (keyInfo.Key)                            //위에서 키 입력 받은거로 판단
                {
                    case ConsoleKey.UpArrow:                    //입력받은 키가 위쪽 방향키면
                        if (field[playerY - 1, playerX] == ". ") //현재 좌표에서 Y좌표의 -1한 위치가 . 이면  
                        {
                            field[playerY, playerX] = ". ";       //원래 있던 자리를 . 으로 
                            playerY--;                          //Y좌표의 값에서 1빼기 == 위로 한칸
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (field[playerY + 1, playerX] == ". ") // 아래로 한칸
                        {
                            field[playerY, playerX] = ". ";
                            playerY++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (field[playerY, playerX - 1] == ". ") //X가 작아지니 왼쪽으로(0쪽으로) 한칸 
                        {

                            field[playerY, playerX] = ". ";
                            playerX--;

                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (field[playerY, playerX + 1] == ". ") //오른쪽으로 한칸
                        {
                            field[playerY, playerX] = ". ";
                            playerX++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (field[playerY, playerX] == field[7, 1] || field[playerY, playerX] == field[8, 1])   /*Enter 누를 때 좌표가  [7,1],[8,1]이면*/
                        {
                            Console.Clear();
                            WorldMap(player, item, equipItem);
                        }
                        else if (!openTreasure)
                        {
                            if (field[playerY, playerX] == field[2, 10] || field[playerY, playerX] == field[2, 11])
                            {
                                item = new Item("방패", 0, 2, 5, 80);
                                Console.WriteLine($"{item.ItemName}을(를) 획득했습니다");
                                Item.inventory.Add(item);
                                openTreasure = true;
                                item.OpenTreasure.Push(openTreasure);
                            }
                            break;
                        }
                        break;
                }
                // 플레이어 위치 갱신
                field[playerY, playerX] = "나";
            }
        }
    }
}

