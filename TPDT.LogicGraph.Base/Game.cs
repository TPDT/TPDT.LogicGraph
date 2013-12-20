/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 17:04
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using TPDT.LogicGraph.Army;

namespace TPDT.LogicGraph.Base
{
    public delegate NodeBase GetArmyNode(PlayerBase player, Type armyType);
    /// <summary>
    /// Description of Game.
    /// </summary>
    public class Game
    {
        public PlayerBase[] PlayerList { get; private set; }
        public Map Map { get; private set; }
        public Dictionary<int, ArmyBase> ArmyList { get; private set; }
        public Type[] ArmyTpyes { get; private set; }
        public PlayerBase CurrentPlayer { get; private set; }

        public GetArmyNode GetArmyNode;

        private int armyNext;
        public Game()
        {
        }
        public Game(Map map, PlayerBase[] players)
        {
            if (map == null)
                throw new ArgumentNullException("map");
            if (players == null)
                throw new ArgumentNullException("players");
            Map = map;
            PlayerList = players;
            ArmyList = new Dictionary<int, ArmyBase>();
        }

        public virtual bool Start(PlayerBase firstPlayer, Type[] defaultArmies)
        {
            if (Map == null || PlayerList == null || ArmyList == null)
            {
                return false;
            }
            SetCurrentPlayer(firstPlayer);
            armyNext = 1;
            foreach (var pr in PlayerList)
            {
                foreach (var da in defaultArmies)
                {
                    AddArmy(da, this.GetArmyNode(pr, da), pr);
                }
            }
            return true;
        }

        protected virtual void AddArmy(Type type, NodeBase node, PlayerBase player)
        {
            if (type == null && !type.IsSubclassOf(typeof(ArmyBase)))
                throw new ArgumentNullException("type");

            ArmyBase newarmy = Activator.CreateInstance(type, armyNext++, node, player) as ArmyBase;
            player.Armys.Add(newarmy.Id, newarmy);
            this.ArmyList.Add(newarmy.Id, newarmy);
        }

        protected virtual void SetCurrentPlayer(PlayerBase player)
        {
            if (player == null)
                throw new ArgumentNullException("player");
            CurrentPlayer = player;
        }
    }
}
