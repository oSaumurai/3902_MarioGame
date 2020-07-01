﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMario.Interfaces;
using SuperMario.GameObjects;


namespace SuperMario
{
    public static class HandleAllCollison
    {
        public static void HandleAllCollisions(IMario mario,
            List<IGameObject> blocklist,List<IGameObject> enemylist, List<IGameObject> itemlist , List<IGameObject>pipelist)
        {
            HandleMarioCollison(mario,blocklist,enemylist,itemlist,pipelist);
            HandleItemCollision(itemlist, blocklist,pipelist);
            HandleEnemyCollision(enemylist,blocklist, pipelist);
        }


        public static void HandleMarioCollison(IMario mario, List<IGameObject> blocklist,
            List<IGameObject> enemylist, List<IGameObject> itemlist, List<IGameObject> pipelist)
        {
            CollisionHandlerMario myhandler = new CollisionHandlerMario(mario);
            foreach (IBlock block in blocklist)
            {
                myhandler.HandleBlockCollision(block);
            }
            foreach (IPipe pipe in pipelist)
            {
                myhandler.HandlePipeCollision(pipe);
            }
            foreach (IEnemy enemy in enemylist)
            {
                myhandler.HandleEnemyCollision(enemy);
            }
            foreach (IItem item in itemlist)
            {
                myhandler.HandleItemCollision(item);
            }

        }

        public static void HandleItemCollision(List<IGameObject> itemList, List<IGameObject> blocklist, List<IGameObject> pipelist)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                CollisionHandlerItem handler = new CollisionHandlerItem((IItem)itemList[i]);
                foreach (IBlock obj in blocklist)
                {
                    handler.HandleBlockCollision(obj);
                }
                foreach (IPipe obj in pipelist)
                {
                    handler.HandlePipeCollision(obj);
                }
            }

        }
        public static void HandleEnemyCollision(List<IGameObject> enemylist, List<IGameObject> blocklist, List<IGameObject> pipelist)
        {
            
            for(int i=0; i < enemylist.Count; i++)
            {
                CollisionHandlerEnemy handler = new CollisionHandlerEnemy((IEnemy)enemylist[i]);
                foreach (IBlock obj in blocklist)
                {
                    handler.HandleBlockCollision(obj);
                }
                foreach (IPipe obj in pipelist)
                {
                    handler.HandlePipeCollision(obj);
                }
                for (int j = i + 1; j < enemylist.Count; j++)
                {
                    handler.HandleEnemyCollision((IEnemy)enemylist[j]);
                }
            }
        }
        
    }
}
