namespace Games
{
    public class Player
    {
        public string JobName { get; set; }//직업 이름
        public int Hp { get; set; }//hp
        public int Atk { get; set; }//공격력
        public int Def { get; set; }//방어력
        public int Gold { get; set; }//소지금

        
        public Player(string jobName, int hp, int atk, int def, int gold)//클래스 내에서 변수 선언
        {
            JobName = jobName;
            Hp = hp;
            Atk = atk;
            Def = def;
            Gold = gold;
        }

        public void MyStatus()//내 스탯 보여주기
        {
            string content = $"직업: {JobName}\n체력: {Hp}\n공격력: {Atk}\n방어력: {Def}\n소지금: {Gold}";
            Console.WriteLine($"{content}");
        }


    }


}

