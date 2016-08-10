#define cheat
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//导入
using System.Threading;
using System.IO;


using League_OF_Legends;
using System.Data;
namespace League_OF_Legends //Work
{
    #region 一、枚举和结构体

    #region 枚举
    /// <summary>
    /// 移动方向
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// 上
        /// </summary>
        up,
        /// <summary>
        /// 下
        /// </summary>
        down,
        /// <summary>
        /// 左
        /// </summary>
        left,
        /// <summary>
        /// 右
        /// </summary>
        right
    }

    /// <summary>
    /// 音调
    /// </summary>
    public enum Fy
    {
        d1 = 262,
        d1_ = 277,
        d2 = 294,
        d2_ = 311,
        d3 = 330,
        d4 = 349,
        d5 = 392,
        d5_ = 415,
        d6 = 440,
        d6_ = 466,
        d7 = 494,
        z1 = 523,
        z1_ = 554,
        z2 = 578,
        z2_ = 622,
        z3 = 659,
        z4 = 698,
        z4_ = 740,
        z5 = 784,
        z5_ = 831,
        z6 = 880,
        z6_ = 932,
        z7 = 988,
        g1 = 1046,
        g1_ = 1109,
        g2 = 1175,
        g2_ = 1245,
        g3 = 1318,
        g4 = 1397,
        g4_ = 1480,
        g5 = 1568,
        g5_ = 1661,
        g6 = 1760,
        g6_ = 1865,
        g7 = 1976,
        stop = 25000
    }
    /// <summary>
    /// 节拍
    /// </summary>
    public enum Delay
    {
        /// <summary>
        /// 一拍
        /// </summary>
        one = 200,
        /// <summary>
        /// 半拍
        /// </summary>
        half = 100,
        /// <summary>
        /// 1/4拍
        /// </summary>
        fourone = 50,
        /// <summary>
        /// 附点音符
        /// </summary>
        onedot = 150
    }

    #endregion

    #region 结构体
    /// <summary>
    /// 游戏地图点坐标
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// 行
        /// </summary>
        public int x;
        /// <summary>
        /// 列
        /// </summary>
        public int y;
    }

    /// <summary>
    /// 英雄结构体
    /// </summary>
    public struct Hero
    {
        /// <summary>
        /// 英雄姓名
        /// </summary>
        public string Name;
        /// <summary>
        ///血量
        /// </summary>
        public int Hp;
        /// <summary>
        /// 攻击力
        /// </summary>
        public int Attack;
        /// <summary>
        /// 防御力
        /// </summary>
        public int Defense;
        /// <summary>
        /// 拥有的金钱
        /// </summary>
        public int Money;
        /// <summary>
        /// 武器
        /// </summary>
        public Equip AttackEquip;
        /// <summary>
        /// 防具
        /// </summary>
        public Equip DefenseEquip;
        /// <summary>
        /// 玩家坐标
        /// </summary>
        public Point PlayerPoint;
        /// <summary>
        /// 普通钥匙（黄）数量
        /// </summary>
        public int KeyNum1;
        /// <summary>
        /// 特殊钥匙（红）数量
        /// </summary>
        public int KeyNum2;
    }

    /// <summary>
    /// 装备结构体
    /// </summary>
    public struct Equip
    {
        /// <summary>
        /// 装备名称
        /// </summary>
        public string Ename;
        /// <summary>
        /// 装备攻击力
        /// </summary>
        public int Eattatck;
        /// <summary>
        /// 装备防御力
        /// </summary>
        public int Edefense;
        /// <summary>
        /// 装备附加血量
        /// </summary>
        public int Ehp;
        /// <summary>
        /// 装备购买金钱
        /// </summary>
        public int Emoney;
        /// <summary>
        /// 装备类型
        /// </summary>
        public int Type;
    }

    /// <summary>
    /// 怪物结构体
    /// </summary>
    public struct Monster
    {
        /// <summary>
        /// 怪物编号
        /// </summary>
        public int Mno;
        /// <summary>
        /// 怪物名称
        /// </summary>
        public string Mname;
        /// <summary>
        /// 怪物攻击力
        /// </summary>
        public int Mattack;
        /// <summary>
        /// 怪物防御力
        /// </summary>
        public int Mdefense;
        /// <summary>
        /// 怪物血量
        /// </summary>
        public int Mhp;
        /// <summary>
        /// 杀死怪物获得金钱数
        /// </summary>
        public int Mmoney;
    }

    /// <summary>
    /// 宝石结构体
    /// </summary>
    public struct Stone
    {
        /// <summary>
        /// 宝石编号
        /// </summary>
        public int Sno;
        /// <summary>
        /// 宝石名称
        /// </summary>
        public string Sname;
        /// <summary>
        /// 宝石攻击力
        /// </summary>
        public int Sattack;
        /// <summary>
        /// 宝石防御力
        /// </summary>
        public int Sdefense;
        /// <summary>
        /// 宝石类型
        /// </summary>
        public int Stype;
    }

    //地图结构体
    public struct Map
    {
        public string MapName;//地图名
        public int[,] MapPoints;//地图二维表
    }

    //钥匙结构体
    public struct Key
    {
        /// <summary>
        /// 钥匙编号（24-黄钥匙 25-红钥匙）
        /// </summary>
        public int Keyno;

        /// <summary>
        /// 钥匙名称
        /// </summary>
        public string KeyName;

        /// <summary>
        /// 钥匙数量
        /// </summary>
        public int KeyNum;
    }

    //门结构体
    public struct Door
    {
        /// <summary>
        /// 门编号
        /// </summary>
        public int DoorNo;

        /// <summary>
        /// 门名称
        /// </summary>
        public string DoorName;
    }

    #endregion

    #endregion

    class Program
    {
        #region 二、成员变量
        /// <summary>
        /// 英雄
        /// </summary>
        public static Hero PlayerHero = new Hero();
        /// <summary>
        /// 装备
        /// </summary>
        public static Equip[] equips;
        /// <summary>
        /// 怪物
        /// </summary>
        public static Monster[] monsters;
        /// <summary>
        /// 宝石
        /// </summary>
        public static Stone[] stones;
        /// <summary>
        /// 钥匙
        /// </summary>
        public static Key[] keys;
        /// <summary>
        /// 门
        /// </summary>
        public static Door[] doors;
        /// <summary>
        /// 难度
        /// </summary>
        public static int level = 1;
        /// <summary>
        /// 光标状态
        /// </summary>
        public static int leveStatel = 1;

        public static Map[] InitMaps;
        //当前地图结构体变量
        public static Map[] CurMaps;
        //游戏总关卡数
        public static int Count;
        //当前处于哪一关
        public static int Index = 0;
        //每关地图的列宽
        public static int Width;
        //每关地图的行高
        public static int Height;
        //每关初始游戏地图结构体变量
        public static Map OneMap = new Map();
        public static Map TwoMap = new Map();
        public static Map ThreeMap = new Map();
        public static Map FourMap = new Map();
        public static Map FiveMap = new Map();
        public static Map SixMap = new Map();
        public static Map SevenMap = new Map();
        public static Map EigeMap = new Map();
        public static Map NineMap = new Map();
        public static Map TenMap = new Map();
        //当前地图结构体变量
        //public static Map CurMap = new Map();
        //提示区域的开始列
        public static int HelpColumn = 61;
        //显示状态
        public static int ShowState = 1;
        #endregion

        #region 三、自定义函数
        #region 0、通用函数
        /// <summary>
        /// 在控制台指定位置输出彩色信息
        /// </summary>
        /// <param name="s">要输出的字符串</param>
        /// <param name="bColor">背景色</param>
        /// <param name="fColor">前景色-字体颜色</param>
        /// <param name="sLine">显示行</param>
        /// <param name="sColumn">显示列</param>
        public static void ShowColor(string s, ConsoleColor bColor, ConsoleColor fColor, int sLine, int sColumn)
        {
            //设置新颜色
            Console.BackgroundColor = bColor;
            Console.ForegroundColor = fColor;
            //设置光标位置
            Console.SetCursorPosition(sColumn, sLine);
            //输出指定字符串
            Console.Write(s);
            //恢复原来颜色
            Console.ResetColor();
            //重置光标位置
            Console.SetCursorPosition(sColumn, sLine);
        }
        #endregion

        #region  1、初始化
        //初始化游戏
        public static void InitGame()
        {
            //初始化地图
            InitM();
            CurMaps = InitMaps;
            InitDoor();
            InitPlayer(leveStatel);
            InitEquips();
            InitMonsters();
            InitStones();
            InitKeys();
        }
        //初始化地图数据
        public static void InitOneMaps()
        {
            //初始化第一关地图
            OneMap = new Map();
            OneMap.MapName = "第一关";
            int[,] points ={{0,0,0,0,0,0,24,0,0,1,},
                              {0,1,1,1,26,1,1,1,0,1,},
                              {2,0,1,0,2,0,1,0,2,1,},
                              {12,1,0,2,1,2,0,1,13,1,},
                              {1,1,2,1,1,1,2,1,1,1,},
                              {0,3,0,0,1,0,0,3,0,1,},
                              {12,0,1,0,3,0,1,0,4,1,},
                              {24,1,1,1,13,1,1,1,30,1,}};
            OneMap.MapPoints = points;
        }
        public static void InitTwoMaps()
        {
            //初始化第二关地图
            TwoMap = new Map();
            TwoMap.MapName = "第二关";
            int[,] points ={{31,0,1,0,24,0,1,25,13,1},
                           {0,0,26,2,0,2,26,0,0,1},
                           {1,1,1,1,2,1,1,1,1,1},
                           {0,0,3,0,0,0,3,0,0,1},
                           {14,0,1,0,0,0,1,0,13,1},
                           {1,1,1,1,27,1,1,1,1,1},
                           {0,0,3,3,3,3,3,0,0,1},
                           {14,0,1,13,25,13,1,0,30,1}};
            TwoMap.MapPoints = points;
        }
        public static void InitThreeMaps()
        {
            //初始化第三关地图
            ThreeMap = new Map();
            ThreeMap.MapName = "第三关";
            int[,] points ={{31,0,0,0,0,0,0,0,0,1},
                            {1,1,1,0,2,0,1,1,1,1},
                            {24,2,0,1,0,1,0,2,25,1},
                            {12,2,2,0,2,0,2,2,13,1},
                            {1,1,0,2,0,2,0,1,1,1},
                            {0,1,1,0,2,0,1,1,0,1}, 
                            {0,0,1,1,0,1,1,0,0,1},
                            {0,0,0,0,2,0,0,0,30,1}};
            ThreeMap.MapPoints = points;
        }
        public static void InitFourMaps()
        {
            //初始化第四关地图
            FourMap.MapName = "第四关";
            FourMap = new Map();
            int[,] points ={{31,0,0,0,0,0,0,0,0,1,},
                            {0,1,1,1,1,5,5,1,0,1,},
                            {0,26,0,13,1,0,0,1,0,1,},
                            {0,5,3,0,1,24,24,1,0,1,},
                            {0,1,1,1,1,1,1,1,0,1,},
                            {0,1,0,13,1,13,0,26,0,1,},
                            {0,26,3,0,1,0,3,5,0,1,},
                            {0,1,0,0,1,1,1,1,30,1}};
            FourMap.MapPoints = points;
        }
        public static void InitFiveMaps()
        {
            //初始化第五关地图
            FiveMap.MapName = "第五关";
            FiveMap = new Map();
            int[,] points ={{31,0,0,0,0,0,0,0,0,1},
                           {0,0,1,26,1,26,1,0,0,1,},
                           {1,1,0,3,1,0,3,1,0,1,},
                           {0,5,0,24,1,24,0,1,0,1,},
                           {4,1,26,1,1,1,26,1,0,1,},
                           {13,1,4,0,1,0,4,1,0,1,},
                           {0,1,0,25,1,25,0,1,0,1,},
                           {5,25,1,1,1,1,1,0,30,1,} };
            FiveMap.MapPoints = points;
        }
        public static void InitSixMaps()
        {
            //初始化第六关地图
            SixMap.MapName = "第六关";
            SixMap = new Map();
            int[,] points ={{31,0,3,0,0,0,4,0,0,1,},
                            {3,0,1,0,0,0,1,0,12,1,},
                            {1,1,1,1,27,1,1,1,1,1,},
                            {0,5,26,4,4,4,26,5,0,1,},
                            {13,0,1,24,25,24,1,0,13,1,},
                            {1,1,1,1,27,1,1,1,1,1,},
                            {1,1,1,4,4,4,1,1,1,1},
                            {1,0,0,0,13,0,0,0,30,1}};
            SixMap.MapPoints = points;
        }
        public static void InitSevenMaps()
        {
            //初始化第七关地图
            SevenMap.MapName = "第七关";
            SevenMap = new Map();
            int[,] points ={{31,0,3,0,0,0,4,0,0,1,},
                            {3,0,1,0,0,0,1,0,12,1,},
                            {1,1,1,1,27,1,1,1,1,1,},
                            {0,5,26,4,4,4,26,5,0,1,},
                            {13,0,1,24,25,24,1,0,13,1,},
                            {1,1,1,1,27,1,1,1,1,1,},
                            {1,1,1,4,4,4,1,1,1,1},
                            {1,0,0,0,13,0,0,0,30,1}};
            SevenMap.MapPoints = points;
        }
        public static void InitEigeMaps()
        {
            //初始化第八关地图
            EigeMap.MapName = "第八关";
            EigeMap = new Map();
            int[,] points ={{31,0,0,0,0,0,0,0,0,1,},
                           {0,0,1,1,0,0,0,1,1,1,},
                           {0,1,0,0,1,0,1,0,0,1,},
                           {0,1,13,4,26,0,26,4,13,1,},
                           {0,1,1,1,1,0,1,1,1,1,},
                           {0,1,0,0,27,0,5,0,0,1,},
                           {0,1,24,24,1,0,1,4,4,1,},
                           {0,1,0,24,1,0,1,4,30,1}};
            EigeMap.MapPoints = points;
        }
        public static void InitNineMaps()
        {
            //初始化第九关地图
            NineMap.MapName = "第九关";
            NineMap = new Map();
            int[,] points ={{31,0,0,0,0,0,0,0,0,1,},
                           {1,1,1,3,3,3,1,1,1,1,},
                           {13,3,0,1,0,1,0,3,24,1,},
                           {0,4,0,26,0,26,0,4,0,1,},
                           {1,1,1,1,0,1,1,1,1,1,},
                           {4,3,4,27,0,27,4,0,4,1,},
                           {5,0,5,1,0,1,12,0,12,1,},
                           {27,13,26,1,0,1,0,25,30,1}};
            NineMap.MapPoints = points;
        }
        public static void InitTenMaps()
        {
            //初始化第十关地图
            TenMap.MapName = "第十关";
            TenMap = new Map();
            int[,] points ={{31,0,0,0,0,0,0,0,0,0,},
                            {1,0,0,0,0,0,0,0,0,1,},
                            {1,0,1,27,1,1,1,1,0,1,},
                            {1,0,1,13,0,0,0,1,0,1,},
                            {1,0,1,0,0,0,0,1,0,1,},
                            {1,0,1,0,0,0,6,1,0,1,},
                            {1,0,1,1,1,1,1,1,0,1,},
                            {1,0,0,0,0,0,0,0,0,1}};
            TenMap.MapPoints = points;
        }
        //初始化地图数组
        public static void InitM()
        {
            //InitOneMaps();
            //InitTwoMaps();
            //InitThreeMaps();
            //InitFourMaps();
            //InitFiveMaps();
            //InitSixMaps();
            //InitSevenMaps();
            //InitEigeMaps();
            //InitNineMaps();
            //InitTenMaps();
            //InitMaps[0] = OneMap;
            //InitMaps[1] = TwoMap;
            //InitMaps[2] = ThreeMap;
            //InitMaps[3] = FourMap;
            //InitMaps[4] = FiveMap;
            //InitMaps[5] = SixMap;
            //InitMaps[6] = SevenMap;
            //InitMaps[7] = EigeMap;
            //InitMaps[8] = NineMap;
            //InitMaps[9] = TenMap;
            ReadInitMaps();
        }
        //初始化玩家属性
        public static void InitPlayer(int level)
        {
            //玩家初始位置坐标点
            PlayerHero.PlayerPoint.x = 0;
            PlayerHero.PlayerPoint.y = 0;
            //玩家初始人物数据
            //PlayerHero.Name = "盖伦";
            switch (level)
            {
                case 1:
                    PlayerHero.Attack = 200;
                    PlayerHero.Defense = 50;
                    PlayerHero.Hp = 200;
                    PlayerHero.Money = 100;
                    PlayerHero.KeyNum1 = 3;
                    PlayerHero.KeyNum2 = 1;
                    break;
                case 2:
                    PlayerHero.Attack = 30;
                    PlayerHero.Defense = 15;
                    PlayerHero.Hp = 100;
                    PlayerHero.Money = 0;
                    PlayerHero.KeyNum1 = 2;
                    PlayerHero.KeyNum2 = 0;
                    break;
                case 3:
                    PlayerHero.Attack = 20;
                    PlayerHero.Defense = 10;
                    PlayerHero.Hp = 50;
                    PlayerHero.Money = 0;
                    PlayerHero.KeyNum1 = 0;
                    PlayerHero.KeyNum2 = 0;
                    break;
                default:
                    break;
            }
            /*PlayerHero.Attack = 2000;
            PlayerHero.Defense = 10;
            PlayerHero.Hp = 100;
            PlayerHero.Money = 0;*/
        }
        //初始化装备数组
        public static void InitEquips()
        {
            equips = new Equip[7];
            //多兰之剑
            equips[0].Ename = "多兰之剑";
            equips[0].Eattatck = 10;
            equips[0].Emoney = 40;
            equips[0].Type = 1;
            //暴风之剑
            equips[1].Ename = "暴风之剑";
            equips[1].Eattatck = 25;
            equips[1].Emoney = 100;
            equips[1].Type = 1;
            //多兰之盾
            equips[2].Ename = "多兰之盾";
            equips[2].Eattatck = 15;
            equips[2].Emoney = 40;
            equips[2].Type = 2;
            //荆棘之甲
            equips[3].Ename = "荆棘之甲";
            equips[3].Edefense = 40;
            equips[3].Emoney = 100;
            equips[3].Type = 2;
            //恢复药水
            equips[4].Ename = "恢复药水";
            equips[4].Ehp = 100;
            equips[4].Emoney = 50;
            equips[4].Type = 3;
            //黄钥匙
            equips[5].Ename = "黄钥匙  ";
            equips[5].Emoney = 50;
            equips[5].Type = 4;
            //红钥匙
            equips[6].Ename = "红钥匙  ";
            equips[6].Emoney = 200;
            equips[6].Type = 5;
        }
        //初始化宝石数组
        public static void InitMonsters()
        {
            //宝石数组编号从13-23。

            stones = new Stone[4];
            //黄宝石
            stones[0].Sname = "蓝宝石";
            stones[0].Sdefense = 30;
            stones[0].Sno = 12;
            stones[0].Stype = 1;

            //蓝宝石
            stones[1].Sname = "黄宝石";
            stones[1].Sattack = 20;
            stones[1].Sno = 13;
            stones[1].Stype = 2;
        }
        //初始化怪物数组
        public static void InitStones()
        {
            //怪物数组编号从2-12。

            monsters = new Monster[5];
            //蝙蝠
            monsters[0].Mname = "蝙蝠";
            monsters[0].Mattack = 15;
            monsters[0].Mdefense = 10;
            monsters[0].Mhp = 30;
            monsters[0].Mmoney = 20;
            monsters[0].Mno = 2;
            //史莱姆
            monsters[1].Mname = "史莱姆";
            monsters[1].Mattack = 35;
            monsters[1].Mdefense = 15;
            monsters[1].Mhp = 50;
            monsters[1].Mmoney = 50;
            monsters[1].Mno = 3;
            //兽人
            monsters[2].Mname = "兽人";
            monsters[2].Mattack = 80;
            monsters[2].Mdefense = 30;
            monsters[2].Mhp = 100;
            monsters[2].Mmoney = 80;
            monsters[2].Mno = 4;
            //骷髅骑士
            monsters[3].Mname = "骷髅兵";
            monsters[3].Mattack = 100;
            monsters[3].Mdefense = 80;
            monsters[3].Mhp = 250;
            monsters[3].Mmoney = 150;
            monsters[3].Mno = 5;
            //魔王
            monsters[4].Mname = "魔王";
            monsters[4].Mattack = 280;
            monsters[4].Mdefense = 250;
            monsters[4].Mhp = 500;
            monsters[4].Mmoney = 999;
            monsters[4].Mno = 6;
        }
        //初始化钥匙数组
        public static void InitKeys()
        {
            keys = new Key[2];
            keys[0].Keyno = 24;
            keys[0].KeyName = "黄钥匙";
            keys[0].KeyNum = 0;

            keys[1].Keyno = 25;
            keys[1].KeyName = "红钥匙";
            keys[1].KeyNum = 0;
        }
        //初始化门数组
        public static void InitDoor()
        {
            doors = new Door[2];
            doors[0].DoorNo = 26;
            doors[0].DoorName = "黄门";

            doors[1].DoorNo = 27;
            doors[1].DoorName = "红门";
        }
        #endregion

        #region 2、绘图显示

        //在指定游戏坐标点画玩家
        public static void DrawPlayer(Hero player)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = player.PlayerPoint.x * 3;
            int cursorY = player.PlayerPoint.y * 6;
            //画玩家
            ShowColor(" ◢◣ ", ConsoleColor.Black, ConsoleColor.Green, cursorX, cursorY);
            ShowColor("◢■◣", ConsoleColor.Black, ConsoleColor.Green, cursorX + 1, cursorY);
            ShowColor(" ◢◣ ", ConsoleColor.Black, ConsoleColor.Green, cursorX + 2, cursorY);
        }

        //在指定游戏坐标点画指定怪物
        public static void DrawMonster(Monster monster, Point p)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = p.x * 3;
            int cursorY = p.y * 6;
            switch (monster.Mno)
            {
                case 2:
                    //画怪物
                    ShowColor("◣  ◢", ConsoleColor.Black, ConsoleColor.DarkBlue, cursorX, cursorY);
                    ShowColor(" 蝙蝠 ", ConsoleColor.Black, ConsoleColor.Red, cursorX + 1, cursorY);
                    ShowColor("◥  ◤", ConsoleColor.Black, ConsoleColor.DarkBlue, cursorX + 2, cursorY);
                    break;
                case 3:
                    //画怪物
                    ShowColor(" ◢◣ ", ConsoleColor.Black, ConsoleColor.DarkMagenta, cursorX, cursorY);
                    ShowColor("史莱姆", ConsoleColor.Black, ConsoleColor.DarkMagenta, cursorX + 1, cursorY);
                    ShowColor(" ◥◤ ", ConsoleColor.Black, ConsoleColor.DarkMagenta, cursorX + 2, cursorY);
                    break;
                case 4:
                    //画怪物
                    ShowColor("◣■◢ ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX, cursorY);
                    ShowColor(" 兽人  ", ConsoleColor.Black, ConsoleColor.Red, cursorX + 1, cursorY);
                    ShowColor("◣  ◢", ConsoleColor.Black, ConsoleColor.Red, cursorX + 2, cursorY);
                    break;
                case 5:
                    //画怪物
                    ShowColor(" ◢◣ ", ConsoleColor.Black, ConsoleColor.Red, cursorX, cursorY);
                    ShowColor("骷髅兵", ConsoleColor.Black, ConsoleColor.Red, cursorX + 1, cursorY);
                    ShowColor(" ◢◣ ", ConsoleColor.Black, ConsoleColor.Red, cursorX + 2, cursorY);
                    break;
                case 6:
                    //画怪物
                    ShowColor("◣■◢", ConsoleColor.Black, ConsoleColor.Red, cursorX, cursorY);
                    ShowColor(" 魔王 ", ConsoleColor.Black, ConsoleColor.Magenta, cursorX + 1, cursorY);
                    ShowColor("◢  ◣", ConsoleColor.Black, ConsoleColor.Red, cursorX + 2, cursorY);
                    break;
                default:
                    break;
            }
        }

        //在指定游戏坐标点画指定宝石
        public static void DrawStone(ref Stone stone, Point p)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = p.x * 3;
            int cursorY = p.y * 6;
            switch (stone.Sno)
            {
                case 12:
                    //画宝石
                    if (ShowState == 1)
                    {
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.Blue, cursorX, cursorY);
                        ShowColor("◆  ◆", ConsoleColor.Black, ConsoleColor.Blue, cursorX + 1, cursorY);
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.Blue, cursorX + 2, cursorY);
                    }
                    else
                    {
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.DarkBlue, cursorX, cursorY);
                        ShowColor("◆  ◆", ConsoleColor.Black, ConsoleColor.DarkBlue, cursorX + 1, cursorY);
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.DarkBlue, cursorX + 2, cursorY);
                    }
                    break;
                case 13:
                    //画宝石
                    if (ShowState == 1)
                    {
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX, cursorY);
                        ShowColor("◆◆◆", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 1, cursorY);
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 2, cursorY);
                    }
                    else
                    {
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.DarkYellow, cursorX, cursorY);
                        ShowColor("◆◆◆", ConsoleColor.Black, ConsoleColor.DarkYellow, cursorX + 1, cursorY);
                        ShowColor("  ◆  ", ConsoleColor.Black, ConsoleColor.DarkYellow, cursorX + 2, cursorY);
                    }
                    break;
                default:
                    break;
            }
        }

        //在指定游戏坐标点画墙
        public static void DrawWall(Point p)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = p.x * 3;
            int cursorY = p.y * 6;
            //
            ShowColor("▓▓▓", ConsoleColor.Black, ConsoleColor.Gray, cursorX, cursorY);
            ShowColor("▓▓▓", ConsoleColor.Black, ConsoleColor.Gray, cursorX + 1, cursorY);
            ShowColor("▓▓▓", ConsoleColor.Black, ConsoleColor.Gray, cursorX + 2, cursorY);
        }

        //在指定游戏坐标点画钥匙
        public static void DrawKey(Key keys, Point p)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = p.x * 3;
            int cursorY = p.y * 6;
            //
            switch (keys.Keyno)
            {
                case 24:
                    ShowColor("  ▓  ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX, cursorY);
                    ShowColor("黄钥匙", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 1, cursorY);
                    ShowColor("◢▓◣ ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 2, cursorY);
                    break;
                case 25:
                    ShowColor("红钥匙", ConsoleColor.Black, ConsoleColor.DarkRed, cursorX, cursorY);
                    ShowColor("* ▓  ", ConsoleColor.Black, ConsoleColor.DarkRed, cursorX + 1, cursorY);
                    ShowColor("◢▓◣", ConsoleColor.Black, ConsoleColor.DarkRed, cursorX + 2, cursorY);
                    break;
            }
        }

        //在指定游戏坐标点画门
        public static void DrawDoor(Door doors, Point p)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = p.x * 3;
            int cursorY = p.y * 6;
            //
            switch (doors.DoorNo)
            {
                case 26:
                    ShowColor("▓  ▓", ConsoleColor.Black, ConsoleColor.Yellow, cursorX, cursorY);
                    ShowColor("▓**▓", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 1, cursorY);
                    ShowColor("▓  ▓", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 2, cursorY);
                    break;
                case 27:
                    ShowColor("▓  ▓", ConsoleColor.Black, ConsoleColor.DarkRed, cursorX, cursorY);
                    ShowColor("▓**▓", ConsoleColor.Black, ConsoleColor.DarkRed, cursorX + 1, cursorY);
                    ShowColor("▓  ▓", ConsoleColor.Black, ConsoleColor.DarkRed, cursorX + 2, cursorY);
                    break;
            }
        }

        //在指定游戏坐标点画通关点
        public static void DrawTgd(Point p)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = p.x * 3;
            int cursorY = p.y * 6;
            //画玩家
            ShowColor("     *", ConsoleColor.Black, ConsoleColor.Green, cursorX, cursorY);
            ShowColor("   *▓", ConsoleColor.Black, ConsoleColor.Green, cursorX + 1, cursorY);
            ShowColor(" *▓▓", ConsoleColor.Black, ConsoleColor.Green, cursorX + 2, cursorY);
        }

        //清除指定游戏坐标点
        public static void Erase(Point p)
        {
            //游戏坐标点转化成控制台光标开始位置
            int cursorX = p.x * 3;
            int cursorY = p.y * 6;
            //
            ShowColor("      ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX, cursorY);
            ShowColor("      ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 1, cursorY);
            ShowColor("      ", ConsoleColor.Black, ConsoleColor.Yellow, cursorX + 2, cursorY);
        }

        //在指定位置显示玩家基本信息
        public static void ShowHero()
        {
            ClearHelp(0, 6);
            ShowColor("姓名：" + PlayerHero.Name, ConsoleColor.Black, ConsoleColor.Green, 0, HelpColumn);
            ShowColor("攻击：" + PlayerHero.Attack, ConsoleColor.Black, ConsoleColor.Green, 1, HelpColumn);
            ShowColor("防御：" + PlayerHero.Defense, ConsoleColor.Black, ConsoleColor.Green, 1, HelpColumn + 20);
            ShowColor("血量：" + PlayerHero.Hp, ConsoleColor.Black, ConsoleColor.Green, 2, HelpColumn);
            ShowColor("金钱：" + PlayerHero.Money, ConsoleColor.Black, ConsoleColor.Green, 2, HelpColumn + 20);
            ShowColor("黄钥匙数量：" + PlayerHero.KeyNum1, ConsoleColor.Black, ConsoleColor.Green, 3, HelpColumn);
            ShowColor("红钥匙数量：" + PlayerHero.KeyNum2, ConsoleColor.Black, ConsoleColor.Green, 3, HelpColumn + 20);
            Console.WriteLine();
        }

        //绘制指定地图 
        public static void DrawMap(Map map)
        {
            //获取地图二维数组
            int[,] m = map.MapPoints;

            for (int i = 0; i < 8; i++)//行
            {
                for (int j = 0; j < 10; j++)//列
                {
                    Point p = new Point();
                    p.x = i;
                    p.y = j;
                    if (m[i, j] == 1)
                    {
                        DrawWall(p);
                    }
                    else if (m[i, j] >= 2 && m[i, j] <= 11)
                    {
                        Monster monster = GetMonsterByNo(m[i, j]);
                        DrawMonster(monster, p);
                    }
                    else if (m[i, j] >= 12 && m[i, j] <= 23)
                    {
                        Stone stone = GetStoneByNo(m[i, j]);
                        DrawStone(ref stone, p);
                    }
                    else if (m[i, j] >= 24 && m[i, j] <= 25)
                    {
                        Key key = GetKeyByNo(m[i, j]);
                        DrawKey(key, p);
                    }
                    else if (m[i, j] >= 26 && m[i, j] <= 27)
                    {
                        Door door = GetDoorByNo(m[i, j]);
                        DrawDoor(door, p);
                    }
                    else if (m[i, j] == 30)
                    {
                        DrawTgd(p);
                    }
                }
            }
        }

        //绘制游戏界面（绘制地图，显示玩家信息，显示操作提示.....）
        public static void DrawInterface(Map map)
        {
            Console.WindowWidth = 110;
            Console.Clear();
            DrawMap(map);
            DrawPlayer(PlayerHero);
            ShowHero();
            Help();
        }

        //根据编号获取怪物
        public static Monster GetMonsterByNo(int no)
        {
            Monster result = new Monster();
            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i].Mno == no)
                {
                    return monsters[i];
                }
            }
            return result;
        }

        //根据编号获取宝石
        public static Stone GetStoneByNo(int no)
        {
            Stone result = new Stone();
            for (int i = 0; i < stones.Length; i++)
            {
                if (stones[i].Sno == no)
                {
                    return stones[i];
                }
            }
            return result;
        }

        //根据编号获取钥匙
        public static Key GetKeyByNo(int no)
        {
            Key result = new Key();
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].Keyno == no)
                {
                    return keys[i];
                }
            }
            return result;
        }

        //根据编号获取门编号
        public static Door GetDoorByNo(int no)
        {
            Door result = new Door();
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].DoorNo == no)
                {
                    return doors[i];
                }
            }
            return result;
        }

        //清除函数2
        public static void clears()
        {
            for (int i = 1; i <= 13; i++)
            {
                ShowColor("                                              ", ConsoleColor.Black, ConsoleColor.Cyan, i + 7, HelpColumn);
            }
        }
        #endregion

        #region 3、音乐
        //播放指定歌曲（音乐数组）
        public static void PlaySong(int[,] song)
        {
            for (int i = 0; i < song.Length / 2; i++)
            {
                Console.Beep(song[i, 0], song[i, 1]);
            }
        }
        //开机音乐
        //通关音乐
        //最终胜利音乐

        #endregion

        #region 4、动画
        //开机动画
        public static void IconAnimation()
        {
            Console.Clear();
            int[,] banry = new int[10, 10];
            //随机初始化二进制数组
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    banry[i, j] = new Random().Next(0);
                    Thread.Sleep(10);
                }
            }
            //循环动态显示

        }
        //开始剧情+进度条动画
        public static void StartAnimation()
        {
        }
        //通关动画
        private static void PassAnimation()
        {
            Console.Clear();
            ShowColor("YOU WIN!!", ConsoleColor.Black, ConsoleColor.Red, 25, 50);
            for (int i = 2000; i < 4000; i = i + 200)
            {
                Console.Beep(i, 200);
            }
        }
        //结束谢幕动画
        private static void FinishAnimation()
        {
            Console.Clear();
            ShowColor("Game over!!", ConsoleColor.Black, ConsoleColor.Red, 25, 50);
            for (int i = 4000; i > 2000; i = i - 200)
            {
                Console.Beep(i, 200);
            }
        }

        /// <summary>
        /// 闪烁函数
        /// </summary>
        private static void Twinkle()
        {
            //第一帧
            //生成随机颜色
            ConsoleColor randomColor;
            do
            {
                randomColor = (ConsoleColor)(new Random().Next(16));
            } while (randomColor == ConsoleColor.Black);

            //输出笑脸
            string str1 = " ◢◣ \n";
            string str2 = "◢■◣\n";
            string str3 = " ◢◣ \n";
            string str4 = str1 + str2 + str3;
            ShowColor(str1, ConsoleColor.Black, randomColor, PlayerHero.PlayerPoint.x * 3, PlayerHero.PlayerPoint.y * 6);
            ShowColor(str2, ConsoleColor.Black, randomColor, PlayerHero.PlayerPoint.x * 3 + 1, PlayerHero.PlayerPoint.y * 6);
            ShowColor(str3, ConsoleColor.Black, randomColor, PlayerHero.PlayerPoint.x * 3 + 2, PlayerHero.PlayerPoint.y * 6);
            //延时
            Console.Beep(3500, 250);
            Console.Beep(1500, 250);
        }
        #endregion

        #region 5、游戏功能
        //进行某游戏（参数：某关地图）
        public static bool PlayGame()
        {
            //画界面
            DrawInterface(CurMaps[Index]);
            Help();
            //循环等待按键
            do
            {
                ConsoleKey curKey = ConsoleKey.Enter;
                if (Console.KeyAvailable)
                {
                    //获取按键
                    curKey = Console.ReadKey(false).Key;
                    //根据按键选择功能
                }
                switch (curKey)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        Move(Direction.left);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        Move(Direction.down);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        Move(Direction.right);
                        break;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        Move(Direction.up);
                        break;
                    case ConsoleKey.B:
                        Back();
                        break;
                    case ConsoleKey.P:
                        Shoping();
                        break;
                    case ConsoleKey.F:
                        ShowMonster();
                        break;
                    default:
                        break;
                }
                //判断玩家血量
                if (PlayerHero.Hp <= 0)
                {
                    return false;
                }
                //判断是否通关
                Point newp = PlayerHero.PlayerPoint;
                if (CurMaps[Index].MapPoints[newp.x, newp.y] == 30)
                {
                    //进入下一关
                    if (Index == 9)
                    {
                        return true;
                    }
                    else
                    {
                        Index++;
                        Console.Clear();
                        PlayerHero.PlayerPoint.x = 0;
                        PlayerHero.PlayerPoint.y = 1;
                        //进入下一关动画
                        //显示一次
                        DrawInterface(CurMaps[Index]);

                    }
                }
                else if (CurMaps[Index].MapPoints[newp.x, newp.y] == 31)
                {
                    //进入下一关
                    if (Index >= 1)
                    {
                        Index--;
                        Console.Clear();
                        PlayerHero.PlayerPoint.x = 0;
                        PlayerHero.PlayerPoint.y = 1;
                        //进入下一关动画
                        //显示一次
                        DrawInterface(CurMaps[Index]);

                    }
                }

                ShowState = 0 - ShowState;
                DrawMap(CurMaps[Index]);
                Thread.Sleep(200);
                Console.SetCursorPosition(HelpColumn, 18);

            } while (true);
        }

        //移动一步
        public static void Move(Direction d)
        {
            clears();
            ShowColor("您移动了移动一步      ", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
            Point newp = PlayerHero.PlayerPoint;
            bool flag = true;
            //获取移动后的点
            switch (d)
            {
                case Direction.up:
                    newp.x -= 1;
                    break;
                case Direction.down:
                    newp.x += 1;
                    break;
                case Direction.left:
                    newp.y -= 1;
                    break;
                case Direction.right:
                    newp.y += 1;
                    break;
                default:
                    break;
            }
            if (newp.x < 0 || newp.x >= 8 || newp.y < 0 || newp.y >= 10)
            {
                flag = false;
                Console.Beep();
            }
            else if (CurMaps[Index].MapPoints[newp.x, newp.y] == 1)
            {
                flag = false;
                Console.Beep();
            }
            else if (CurMaps[Index].MapPoints[newp.x, newp.y] >= 2 && CurMaps[Index].MapPoints[newp.x, newp.y] <= 11)//怪物
            {
                Monster fightMonster = GetMonsterByNo(CurMaps[Index].MapPoints[newp.x, newp.y]);
                if (Fight(fightMonster, newp))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    Console.Beep();
                }
            }
            else if (CurMaps[Index].MapPoints[newp.x, newp.y] >= 12 && CurMaps[Index].MapPoints[newp.x, newp.y] <= 23)//宝石
            {
                Stone eatStone = GetStoneByNo(CurMaps[Index].MapPoints[newp.x, newp.y]);
                CurMaps[Index].MapPoints[newp.x, newp.y] = 0;
                EatStone(eatStone);
            }
            else if (CurMaps[Index].MapPoints[newp.x, newp.y] >= 24 && CurMaps[Index].MapPoints[newp.x, newp.y] <= 25)//钥匙
            {
                Key usekey = GetKeyByNo(CurMaps[Index].MapPoints[newp.x, newp.y]);
                CurMaps[Index].MapPoints[newp.x, newp.y] = 0;
                Key(usekey);
            }
            else if (CurMaps[Index].MapPoints[newp.x, newp.y] >= 26 && CurMaps[Index].MapPoints[newp.x, newp.y] <= 27)//门
            {
                Door usedoor = GetDoorByNo(CurMaps[Index].MapPoints[newp.x, newp.y]);
                if (Door(usedoor, newp))
                {
                    flag = true;
                    CurMaps[Index].MapPoints[newp.x, newp.y] = 0;
                }
                else
                {
                    flag = false;
                    Console.Beep();
                }
            }

            //判断是否刷新
            if (flag)
            {
                //刷新
                //擦除
                Erase(PlayerHero.PlayerPoint);
                //画新玩家
                PlayerHero.PlayerPoint = newp;
                DrawPlayer(PlayerHero);
            }
            //重新显示玩家属性
            ShowHero();
            Console.SetCursorPosition(HelpColumn, 16);

        }

        //回城
        public static void Back()
        {
            clears();
            ShowColor("正在回城...     ", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
            //插入回城动画
            Thread.Sleep(500);
            Erase(PlayerHero.PlayerPoint);
            PlayerHero.PlayerPoint.x = 0;
            PlayerHero.PlayerPoint.y = 0;
            DrawPlayer(PlayerHero);
        }

        //商店
        public static void Shoping()
        {
            clears();
            ShowColor("编号  装备名\t金币\t攻击\t防御\t血量", ConsoleColor.Black, ConsoleColor.Yellow, 8, HelpColumn);
            for (int i = 1; i <= equips.Length; i++)
            {
                string str = i + "\t" + equips[i - 1].Ename + "\t" + equips[i - 1].Emoney + "\t" + equips[i - 1].Eattatck + "\t" + equips[i - 1].Edefense + "\t" + equips[i - 1].Ehp;
                ShowColor(str, ConsoleColor.Black, ConsoleColor.Cyan, i + 8, HelpColumn);
            }
            ShowColor("请输入装备对应的编号来购买装备。", ConsoleColor.Black, ConsoleColor.Cyan, 17, HelpColumn);
            Console.SetCursorPosition(HelpColumn, 18);
            int num = int.Parse(Console.ReadLine());
            if (num >= 1 && num <= equips.Length)
            {
#if (cheat)
                //编号输入正确开始购买并判断玩家金钱是否购买装备
                if (PlayerHero.Money != equips[num - 1].Emoney)
#else
                if (PlayerHero.Money >= equips[num - 1].Emoney)
#endif
                {
                    PlayerHero.Money -= equips[num - 1].Emoney;
                    //刷新玩家装备后的属性
                    switch (equips[num - 1].Type)
                    {
                        case 1:
                            PlayerHero.Attack += equips[num - 1].Eattatck;
                            break;
                        case 2:
                            PlayerHero.Defense += equips[num - 1].Edefense;
                            break;
                        case 3:
                            //玩家购买血瓶后自动加血
                            PlayerHero.Hp += equips[num - 1].Ehp;
                            break;
                        case 4:
                            PlayerHero.KeyNum1 += 1;
                            break;
                        case 5:
                            PlayerHero.KeyNum2 += 1;
                            break;
                    }
                    ShowColor("购买成功！", ConsoleColor.Black, ConsoleColor.Yellow, 19, HelpColumn);
                    ShowHero();

                }
                else
                {
                    ShowColor("玩家金钱不足！", ConsoleColor.Black, ConsoleColor.Red, 19, HelpColumn);
                }
            }
            else
            {
                ShowColor("编号输入错误！", ConsoleColor.Black, ConsoleColor.Red, 20, HelpColumn);
            }
        }

        //帮助
        public static void Help()
        {
            ShowColor("**************** 帮助 ***************", ConsoleColor.Black, ConsoleColor.Yellow, 21, HelpColumn - 1);
            ShowColor("按“wasd键”或按方向键操作人物移动", ConsoleColor.Black, ConsoleColor.DarkGreen, 22, HelpColumn);
            ShowColor(" B键 回城   P键 商店 F键 显示怪物信息", ConsoleColor.Black, ConsoleColor.DarkGreen, 23, HelpColumn);
        }

        //清除指定区域函数
        public static void ClearHelp(int startLine, int endLine)
        {
            for (int i = startLine; i <= endLine; i++)
            {
                ShowColor("                     ", ConsoleColor.Black, ConsoleColor.Cyan, i, HelpColumn);
            }
        }

        //显示怪物属性和宝石属性
        public static void ShowMonster()
        {
            clears();
            ShowColor("怪物名\t攻击力\t防御力\t生命值\t击杀获得", ConsoleColor.Black, ConsoleColor.Yellow, 8, HelpColumn);
            for (int i = 1; i <= monsters.Length; i++)
            {
                string str = i + "\t" + monsters[i - 1].Mname + "\t" + monsters[i - 1].Mattack + "\t" + monsters[i - 1].Mdefense + "\t" + monsters[i - 1].Mhp + "\t" + monsters[i - 1].Mmoney;
                ShowColor(str, ConsoleColor.Black, ConsoleColor.Cyan, i + 8, HelpColumn);
            }
        }

        //对战
        public static bool Fight(Monster m, Point p)
        {
            bool result = false;
            int i = 1;
            do
            {
                Twinkle();
                int hurt1 = PlayerHero.Attack - m.Mdefense;
                ShowColor("第" + i + "回合\n", ConsoleColor.Black, ConsoleColor.Cyan, 10, HelpColumn);
                ShowColor(PlayerHero.Name + "向" + m.Mname + "发起攻击！", ConsoleColor.Black, ConsoleColor.Cyan, 11, HelpColumn);
                ShowColor("造成" + hurt1 + "点伤害\n", ConsoleColor.Black, ConsoleColor.Cyan, 12, HelpColumn);
                m.Mhp -= hurt1;
                i++;
                if (m.Mhp <= 0)
                {
                    //怪物死
                    CurMaps[Index].MapPoints[p.x, p.y] = 0;
                    result = true;
                    PlayerHero.Money += m.Mmoney;
                    //Thread.Sleep(500);
                    ShowColor("获得了" + m.Mmoney + "金币", ConsoleColor.Black, ConsoleColor.Cyan, 15, HelpColumn);
                    break;
                }
                int hurt2 = m.Mattack - PlayerHero.Defense;
                if (hurt2 < 0)
                {
                    hurt2 = 0;
                }
                ShowColor(m.Mname + "向" + PlayerHero.Name + "发起攻击！", ConsoleColor.Black, ConsoleColor.Cyan, 13, HelpColumn);
                ShowColor("造成" + hurt2 + "点伤害", ConsoleColor.Black, ConsoleColor.Cyan, 14, HelpColumn);
                PlayerHero.Hp -= hurt2;
                if (PlayerHero.Hp <= 0)
                {
                    Thread.Sleep(500);
                    ShowColor("您已死亡", ConsoleColor.Black, ConsoleColor.Red, 16, HelpColumn);
                    Thread.Sleep(1000);
                    break;
                }

            } while (true);
            return result;
        }

        //吃宝石
        public static void EatStone(Stone s)
        {

            switch (s.Stype)
            {
                case 1:
                    PlayerHero.Attack += s.Sdefense;
                    break;
                case 2:
                    PlayerHero.Attack += s.Sattack;
                    break;
            }
            ClearHelp(9, 15);
            ShowColor("你拾取到了" + s.Sname + "!", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
            ShowColor("攻击力增加" + s.Sattack + "点   防御力增加" + s.Sdefense + "点", ConsoleColor.Black, ConsoleColor.Cyan, 9, HelpColumn);
        }

        //登录
        public static void Cetvy()
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓　　　　　　　　　　   　　　　　　　 　　　　　　　　　　　　　  ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓　　　　　　　　  　　                 　　　　  　　　　　　　　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓　　　          　　　　 　英雄联盟版魔塔  　          　　　　　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                           　　　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                           　　　　     　　      ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                            　　　　      　      ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                             　　　　    　       ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                               　　　　 　      　▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                               　　　　 　     　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                              　　▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                            　 　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                             　　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                                  ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                               　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                                　▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                          　　　　　              ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                               　　　　 　      　▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                               　　　　 　      　▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                                 　　　　     　  ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓                                               　　　　 　     　 ▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"); Thread.Sleep(100);
            Console.WriteLine(" ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"); Thread.Sleep(100);
            Console.ResetColor();
            ShowColor("    请选择游戏难度：", ConsoleColor.Black, ConsoleColor.White, 7, 27);
            CetvyShowDefault();
            //这个我们想弄成用光标选择难度，就是接受用户输入的上下键来让其对应难度文字闪烁，而不是接受用户输入的数字
            do
            {

                ConsoleKey curKey;
                if (Console.KeyAvailable)
                {
                    //获取按键
                    curKey = Console.ReadKey(false).Key;
                    //根据按键选择功能
                    //根据按键选择功能
                    switch (curKey)
                    {
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            if (leveStatel < 3)
                            {
                                leveStatel++;

                            }
                            else
                            {
                                Console.Beep();
                            }
                            break;
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            if (leveStatel > 1)
                            {
                                leveStatel--;
                            }
                            else
                            {
                                Console.Beep();
                            }
                            break;
                        case ConsoleKey.Enter:
                            //清除
                            ShowColor("请输入角色名：", ConsoleColor.Black, ConsoleColor.White, 15, 27);
                            Console.SetCursorPosition(45, 15);
                            PlayerHero.Name = Console.ReadLine();
                            return;
                    }
                }

                ShowState = 0 - ShowState;
                CetvyTwinkle(leveStatel);
                Thread.Sleep(300);
            } while (true);
        }

        public static void CetvyShowDefault()
        {
            ShowColor("    １简单（推荐新手）       ", ConsoleColor.Black, ConsoleColor.White, 9, 27);
            ShowColor("    ２普通（新手进阶）       ", ConsoleColor.Black, ConsoleColor.White, 11, 27);
            ShowColor("    ３困难（高手进阶）       ", ConsoleColor.Black, ConsoleColor.White, 13, 27);
        }

        //登录闪烁
        public static void CetvyTwinkle(int leveStatel)
        {
            CetvyShowDefault();
            switch (leveStatel)
            {
                case 1:
                    if (ShowState == 1)
                    {
                        ShowColor("    １简单（推荐新手）", ConsoleColor.Black, ConsoleColor.White, 9, 27);
                    }
                    else
                    {
                        ShowColor("    １简单（推荐新手）", ConsoleColor.Black, ConsoleColor.Green, 9, 27);
                    }
                    Console.SetCursorPosition(51, 9);
                    break;
                case 2:
                    if (ShowState == 1)
                    {
                        ShowColor("    ２普通（新手进阶）", ConsoleColor.Black, ConsoleColor.White, 11, 27);
                    }
                    else
                    {
                        ShowColor("    ２普通（新手进阶）", ConsoleColor.Black, ConsoleColor.Green, 11, 27);
                    }
                    Console.SetCursorPosition(51, 11);
                    break;
                case 3:
                    if (ShowState == 1)
                    {
                        ShowColor("    ３困难（高手进阶）", ConsoleColor.Black, ConsoleColor.White, 13, 27);
                    }
                    else
                    {
                        ShowColor("    ３困难（高手进阶）", ConsoleColor.Black, ConsoleColor.Green, 13, 27);
                    }
                    Console.SetCursorPosition(51, 13);
                    break;
            }

        }

        //钥匙功能
        public static void Key(Key k)
        {
            switch (k.Keyno)
            {
                case 24:
                    PlayerHero.KeyNum1 += 1;
                    break;
                case 25:
                    PlayerHero.KeyNum2 += 1;
                    break;
            }
            ClearHelp(9, 15);
            ShowColor("你拾取到了" + k.KeyName + "一把!", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
        }

        //门
        public static bool Door(Door d, Point p)
        {
            bool result = false;
            switch (d.DoorNo)
            {
                case 26:
                    if (PlayerHero.KeyNum1 > 0)
                    {
                        ClearHelp(9, 15);
                        PlayerHero.KeyNum1--;
                        ShowColor("你成功开启了一扇" + d.DoorName + "!", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
                        ShowColor("对应的钥匙将减少一把", ConsoleColor.Black, ConsoleColor.Cyan, 9, HelpColumn);
                        result = true;
                    }
                    else
                    {
                        ClearHelp(9, 15);
                        ShowColor("对应钥匙不足，无法开启!", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
                        ShowColor("请到按P键到商店购买，或去拾取!", ConsoleColor.Black, ConsoleColor.Cyan, 9, HelpColumn);
                    }
                    break;
                case 27:
                    if (PlayerHero.KeyNum2 > 0)
                    {
                        ClearHelp(9, 15);
                        PlayerHero.KeyNum2--;
                        ShowColor("你成功开启了一扇" + d.DoorName + "!", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
                        ShowColor("对应的钥匙将减少一把", ConsoleColor.Black, ConsoleColor.Cyan, 9, HelpColumn);
                        result = true;
                    }
                    else
                    {
                        ClearHelp(9, 15);
                        ShowColor("对应钥匙不足，无法开启!", ConsoleColor.Black, ConsoleColor.Cyan, 8, HelpColumn);
                        ShowColor("请到按P键到商店购买，或去拾取!", ConsoleColor.Black, ConsoleColor.Cyan, 9, HelpColumn);
                    }
                    break;
            }
            return result;
        }
        #endregion

        #region 6、保存和读取进度
        //读取初始地图
        public static bool ReadInitMaps()
        {
            try
            {
                //1.打开文件用于读取
                StreamReader r = File.OpenText("InitMaps.txt");
                //2.读取文件内容
                //获取游戏关卡数
                Count = int.Parse(r.ReadLine());
                //获取地图行高
                Height = int.Parse(r.ReadLine());
                //获取地图列宽
                Width = int.Parse(r.ReadLine());
                //根据读取的关卡数重新初始化初始地图数组
                InitMaps = new Map[Count];
                //循环读取文件信息保存到对应学员数组
                for (int i = 0; i < Count; i++)
                {
                    //地图名称
                    InitMaps[i].MapName = r.ReadLine();
                    //初始化每关地图二维数组
                    InitMaps[i].MapPoints = new int[Height, Width];
                    //依次读取每行地图信息给对应二维数组赋值
                    for (int x = 0; x < Height; x++)
                    {
                        string mapLine = r.ReadLine();
                        string[] mapIntValue = mapLine.Split(',');
                        //读取地图信息并放入二维数组
                        for (int y = 0; y < mapIntValue.Length; y++)
                        {
                            InitMaps[i].MapPoints[x, y] = int.Parse(mapIntValue[y]);
                        }
                    }

                }
                //3.关闭文件
                r.Close();
                return true;

            }
            catch (Exception)
            {
                //return false;
                throw;
            }

        }
        //保存进度
        public static bool Save()
        {
            return true;
        }
        //读取进度
        public static bool Read()
        {
            try
            {
                //1.打开文件用于写入
                StreamWriter w = File.CreateText("Record.txt");

                //2.写入文件内容
                //写入总关卡数
                w.WriteLine(Count);
                //写入行高
                w.WriteLine(Height);
                //写入列宽
                w.WriteLine(Width);
                //写入当前关
                w.WriteLine(Index);
                //循环写入地图信息
                for (int i = 0; i < Count; i++)
                {
                    //写地图名
                    //循环写入每行地图

                }
                //写入英雄各项属性信息
                //3.关闭文件
                w.Close();
                return true;
            }
            catch (Exception)
            {
                //return false;
                throw;
            }
        }

        #endregion

        #endregion

        //主函数
        static void Main(string[] args)
        {
            Game();
        }
        static void Game()
        {
            #region  新增信息，功能及BUG备注
            /*新增信息: 1.新增地图七张，共十张  行号：392 （ 见//初始化游戏地图）
             *          2.新增钥匙（Key），门（Door）结构体 及数组  （见定义成员变量）
             *              以及对应的初始化 （见1.初始化）
             *          3.新增人物初始属性三种（InitPlayer）对应三种难度 （行号：512  见1.初始化） 
             *             以及成员变量（public static int leveStatel = 1;）代表当前选择中的难度  （行号：333）
             * BUG信息：登录模块
             *          1.无法实现选项闪烁或闪烁存在问题
             *          2.未实现返回之前已通过的关卡功能（考虑放弃？）
             */
            #endregion

            //不显示光标
            Console.CursorVisible = false;
            //登录
            Cetvy();

            //初始化
            InitGame();

            Console.Clear();

            //进行游戏
            if (PlayGame())
            {
                PassAnimation();
            }
            else
            {
                FinishAnimation();
            }
            //FinishAnimation();
            //PassAnimation();

        }
    }
}