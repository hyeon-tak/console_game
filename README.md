# 스파르타 코딩클럽 본과정 1주차 개인과제
C#언어만으로 만든 console게임
## 구현 기능

1.시작시 직업 선택 기능
2.2차원배열을 이용한 맵 구현
3.맵 간의 이동
4.상점 기능(구매, 판매)
5.인벤토리 내용 확인
6.장비 장착, 해제 그리고 장비한 아이템 확인

----
### 1. 시작시 직업 선택 기능
시작시 직업 선택 기능은 제가 가장 처음 구현한 기능입니다 

    string input = Console.ReadLine();
    if (input == "1")
    {
        return new Player("전사", 50, 15, 12, 250 );
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

if문을 활용해 입력한 값에 따라 기본적인 직업이름, 생명력, 공격력, 방어력, 소지금입니다 

소지금은 관리하기 쉽게 플레이어의 스텟에 부여를 해놨습니다
### 2. 2차원배열을 이용한 맵 구현
----
단순히 2차원 배열을 이용해 맵을 구현 했습니다

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
    field[playerY, playerX] = "나";//플레이어가 있는 위치는 나 라고 나옵니다

맨 첫 부분에 아래의 코드로 플레이어의 기본 위치를 잡아줍니댜

    public static int playerX = 1;
    public static int playerY = 1;
    
아래의 코드를 이용해 맵 이동시의 플레이어의 위치를 기억 해 줍니다 그리고 다른 맵에서 돌아올 시 그 기억한 좌표를 불러와 위치를 정해줍니다다

    PlayerPosition.Push(new Tuple<int, int>(playerY, playerX)); //stack에 내 위치를 Push하여 내위치 기억
    PlayerPosition.Pop();                                       //내 위치 불러오기


맵에서의 이동은 switch,case문을 이용해 구현을 했습니다

     switch (keyInfo.Key)                            //위에서 키 입력 받은거로 판단
        {
            case ConsoleKey.UpArrow:                    //입력받은 키가 위쪽 방향키면
                if (field[playerY - 1, playerX] == ". ") //현재 좌표에서 Y좌표의 -1한 위치가 . 이면  . 외엔 이동 불가
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
          }

### 3. 맵간 이동
----
맵간의 이동은 위의 플레이어의 이동과 같은 방식으로 구현 했으며 엔터를 누르면 이동합니다

    case ConsoleKey.Enter:
        if (field[playerY, playerX] == field[8, 3])   /*Enter 누를 때 좌표가  8,3이면*/
        {
            PlayerPosition.Push(new Tuple<int, int>(playerY, playerX));
            Console.Clear();
            Shop(player, item, equipItem);
        }


Tuple을 이용해 맵간의 이동 시 전 맵의 플레이어의 위치를 기억하고 불러 올 수 있도록 하였습니다

### 4.상점 기능(구매, 판매)
----
다음은 상점의 구매 판매 기능입니다 

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
    

하나만 따로 보여드리면 상점 내 상인 앞에서 엔터를 누를시 상점을 이용할 수 있습니다

상점 이용 시 1을 입력하면 검을 살 수 있으며 플레이어의 골드가 150(검의 가격)의 이상이라면 구매가 되며 구매한 아이템은 

Item의 inventory라는 리스트로 값을 보내게 됩니다 

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

위의 코드는 판매의 경우입니다 제가 제일 고생했던 부분이기도 합니다 

일단 아이템에 숫자를 부여합니다 그 이후 그 숫자가 인벤토리 리스트의 갯수 내의 숫자라면 아이템을 팔고 그 아이템의 골드 

스텟 만큼 플레이어의 골드를 올려줍니다

그리고 그 아이템의 이름과 같은 인벤토리 리스트 내의 아이템을 지워줍니다 

0을 누르면 판매화면에서 나올 수 있습니다

----
### 5. 인벤토리의 내용확인
단순히 인벤토리 리스트의 내용을 확인하는 것으로 구현했습니다

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

### 6. 장비 장착, 해제 그리고 장비한 아이템 확인
마지막으로 장비의 장착 및 해제입니다 

장비 작착 코드입니다 

    int equipNumber;
    if (int.TryParse(Console.ReadLine(), out equipNumber))
    {
        if(Item.inventory.Count <= 2)
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
    
            else if (equipNumber == 0)
            {
                DisarmItem(player, item, equipItem);
            }
            else
            {
               
            }
        }                
    }

장비 해제 코드입니다

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

대동소이합니다 아이템 구매 코드와 비슷한 방식으로 구현했습니다 장착시에는 그 아이템이 갖고있던 스텟을 플레이어의 스텟에 더해줍니다

해제시에는 마찬가지로 장비했던 아이템의 스텟만큼 플레이어의 스텟을 감소시켜줍니다 

지금 이 코드에 문제점이 하나 있습니다 장비의 장착갯수의 제한이 안걸려 있어 장비를 무한정 장착하여 무한한 스텟업이 가능합니

### 마지막으로 Main 함수에 대한 설명입니다
----
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

일단 처음 말씀드린 직업 선택 창으로 시작합니다 

    player.MyStatus();

함수를 이용해 자신이 선택한 직업의 스텟을 보여줍니다

1을 누를시 위의 시작맵으로 이동하며 게임이 시작됩니다

# 피드백 이후 달라진 점
----
1. (이동과 관련하여) 지금처럼 특정 칸에 있는지를 직접 조건문으로 따지는 방법은 성장이 어렵습니다. 보여지는 데이터와 다른 실제 칸의 특성에 대한 2차원 배열을 생성하시고, 그 칸에서 인터렉션을 할 수 있는 내용을 정의해보시는 방법 혹은 칸에 있는 오브젝트들을 리스트나 딕셔너리로 관리하시어 특정 좌표에서의 인터렉션을 함수로 처리할 수 있을 것 같습니다. 지금처럼 내가 특정한 위치에 있는 지 판단하는 방법은 확장성이 떨어집니다.

첫 번째 피드백입니다 이 피드백 내용 중에 제가 아는건 List 밖에 없어 List를 통해 수정했습니다
    
    List<Tuple<int, int>> MapMove = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(8, 3),  //0
            new Tuple<int, int>(7, 11), //1
            new Tuple<int, int>(8, 11), //2
            new Tuple<int, int>(14, 5), //3
            new Tuple<int, int>(14, 6), //4
            new Tuple<int, int>(2, 6),  //5
            new Tuple<int, int>(2, 5),  //6
            new Tuple<int, int>(7, 1),  //7
            new Tuple<int, int>(8, 1),  //8
            new Tuple<int, int>(2, 11), //9
            new Tuple<int, int>(2, 10)  //10
        };

보는 바와같이 맵 이동에 필요한 좌표를 리스트에 넣었습니다 

      case ConsoleKey.Enter:
          if (MapMove.Contains(0))   /*Enter 누를 때 좌표가  8,3이면*/
          {
              PlayerPosition.Add(new Tuple<int, int>(playerY, playerX));
              Console.Clear();
              Shop(player, item, equipItem);
          }

그리고 이걸 말씀하신게 아닌것 같은 느낌이 많이 들지만 위와같이 if문의 조건이 짧아졌습니다 

2. Games를 부분 클래스로 정의하셨는데, 굳이 플레이어가 Games의 부분 클래스의 자식으로 들어가야 할 사유를 알기 어렵습니다. 아마 실수를 하신 것으로 보입니다. partial 키워드는 클래스의 정의를 여러 스크립트에 나눠서 정의해야하는 경우 활용합니다.(Player.cs)

두 번째 피드백입니다 

partial키워드를 왜 썼는지 모르겠다고 하셨습니다 네 저도 모릅니다 혼자 멋대로 들어있었습니다 수정 했습니다

3. 스택에 대한 활용 케이스가 부적절합니다. 지금처럼 바로 push하고 pop을 해버리시면 변수 한 개를 쓰는 것과 차이가 없습니다. 만약 모든 클리어를 진행하고 지금까지 왔던 길들을 보여준다든지 하는 경우에는 stack을 사용할 수 있겠습니다.

세 번째 피드백입니다 스텍을 굳이 쓸 필요가 없다 만약 스텍을 쓴다면 다른 기능에 써라 였습니다 이 부분같은 경우 GPT다 stack을 쓰라고 알려줬기에 stack을 쓴 것입니다 다른 방법(List를 이용한)이 있는 지 몰랐습니다 수정 했습니다






























