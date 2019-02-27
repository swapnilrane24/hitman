﻿using UnityEngine;
using Common;
using System.Collections;
using PathSystem;
using GameState.Interface;
using System;

namespace Enemy
{
    public class EnemyController:IEnemyController
    {
        protected IEnemyService currentEnemyService;
        protected EnemyScriptableObject enemyScriptableObject;
        protected IPathService pathService;
        protected IEnemyView currentEnemyView;
        protected IGameService gameService;
        protected Vector3 spawnLocation;
        protected GameObject enemyInstance;
        protected Directions spawnDirection;
        protected int spawnID;
        protected int enemyID;

        public EnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation,EnemyScriptableObject _enemyScriptableObject, int _currentNodeID,Directions _spawnDirection)
        {
            currentEnemyService = _enemyService;
            spawnLocation = _spawnLocation;
            enemyScriptableObject = _enemyScriptableObject;
            pathService = _pathService;
            spawnDirection = _spawnDirection;
            spawnID = _currentNodeID;
            SpawnEnemyView();
        }
    
        protected virtual void SpawnEnemyView()
        {
            //SPAWN ENEMY VIEW
            enemyInstance=GameObject.Instantiate(enemyScriptableObject.enemyPrefab.gameObject);
            currentEnemyView = enemyInstance.GetComponent<IEnemyView>();
            enemyInstance.transform.localPosition = spawnLocation;
            enemyInstance.transform.localRotation = enemyScriptableObject.enemyRotation;
        }

        public void SetID(int _ID)
        {
            enemyID = _ID;
        }

        protected virtual void MoveToNextNode(int nodeID)
        {
            
        }

        public void Move()
        {
            if(gameService.GetCurrentState()== GameStatesType.ENEMYSTATE)
            {
                int nextNodeID = pathService.GetNextNodeID(spawnID,spawnDirection);
                MoveToNextNode(nextNodeID);
            }
        }
    }
}