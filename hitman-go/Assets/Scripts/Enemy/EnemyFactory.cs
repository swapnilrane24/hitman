﻿using GameState;
using Common;
using PathSystem;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        IEnemyService enemyService;
        IPathService pathService;
        IGameService gameService;

        public EnemyFactory(IEnemyService _enemyService, IPathService _pathService,IGameService _gameService)
        {
            gameService = _gameService;
            pathService = _pathService;
            enemyService = _enemyService;
            
        }

       

        public List<IEnemyController> SpawnEnemies(EnemyScriptableObjectList _list)
        {
            List<IEnemyController> enemyList = new List<IEnemyController>();
        
            for (int i = 0; i < _list.enemyList.Count; i++)
            {
              enemyList= enemyList.Concat(SpawnSingleEnemyLocations(_list.enemyList[i])).ToList();
            }

            return enemyList;
        }


        private List<IEnemyController> SpawnSingleEnemyLocations(EnemyScriptableObject _enemyScriptableObject)
        {
            List<EnemySpawnData> spawnNodeID = new List<EnemySpawnData>();
            List<IEnemyController> newEnemyControllers = new List<IEnemyController>();
            spawnNodeID.Clear();
            spawnNodeID = pathService.GetEnemySpawnLocation(_enemyScriptableObject.enemyType);

            switch (_enemyScriptableObject.enemyType)
            {
                case EnemyType.STATIC:
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new StaticEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);
                        
                    }
                    break;

                case EnemyType.PATROLLING:
                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        

                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);                       
                        IEnemyController newEnemy = new PatrollingEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);
                    }
                    break;

                case EnemyType.ROTATING_KNIFE:
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new RotatingKnifeEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);
                    }
                    break;

                case EnemyType.CIRCULAR_COP:
                    
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new CircularCopEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                case EnemyType.SHIELDED:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new ShieldedEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);

                        newEnemyControllers.Add(newEnemy);
                    }
                    break;

                case EnemyType.DOGS:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new DogsEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                case EnemyType.SNIPER:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new SniperEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                case EnemyType.TARGET:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new TargetEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;
                case EnemyType.BIDIRECTIONAL:                   
                    for (int i = 0; i < spawnNodeID.Count; i++)
                    {
                        Vector3 spawnLocation = pathService.GetNodeLocation(spawnNodeID[i].node);
                        IEnemyController newEnemy = new BiDirectionalEnemyController(enemyService, pathService, gameService, spawnLocation, _enemyScriptableObject, spawnNodeID[i].node, spawnNodeID[i].dir);
                        newEnemyControllers.Add(newEnemy);

                    }
                    break;

                default:
                    Debug.Log("No Enemy Controller of this type");
                    break;                    
            }

            return newEnemyControllers;
        }
    }

}