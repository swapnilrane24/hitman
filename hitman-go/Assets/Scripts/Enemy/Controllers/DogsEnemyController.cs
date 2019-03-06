﻿using Common;
using GameState;
using PathSystem;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class DogsEnemyController : EnemyController
    {
        int dogVision = 2;
        Directions newDirection;
        public DogsEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {
            enemyType = EnemyType.DOGS;
            newDirection = spawnDirection;


        }

        async protected override Task MoveToNextNode(int nodeID)
        {

            if (nodeID == -1)
            {
                return;
            }
            int nodeToCheck = nodeID;
            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                Debug.Log("Next node ID [move to next node]"+ nodeID);
                
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                Vector3 rot = GetRotation(spawnDirection);
                await currentEnemyView.RotateEnemy(rot);

                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;                              
                    AlertEnemy(currentEnemyService.GetPlayerNodeID());
                
            }

            if (CheckForPlayerPresence(nodeID))
            {
                if (!currentEnemyService.CheckForKillablePlayer())
                {
                    return;
                }

                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
                currentEnemyService.TriggerPlayerDeath();
            }
            else
            {
                // int newNodeID = pathService.GetNextNodeID(nodeID, spawnDirection);
                if (stateMachine.GetEnemyState() == EnemyStates.IDLE)
                { CheckForNextNodeInStraightDirection(nodeToCheck); }           
            }

        }

        private void CheckForNextNodeInStraightDirection(int nodeToCheck)
        {
            int nextNodeCheck = pathService.GetNextNodeID(nodeToCheck, spawnDirection);

            if (nextNodeCheck == -1)
            {
                return;
            }
            if (CheckForPlayerPresence(nextNodeCheck))
            {
                Debug.Log("[dog enemy]Enemy alerted at : node" + nextNodeCheck);
                AlertEnemy(nextNodeCheck);
                return;
            }
        }

        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);
        }

      async  private Task CheckForNextNodeInAllDirections(int nextNode)
        {
            for (int i = 0; i < directionList.Count; i++)
            {
                int nextNodeCheck = pathService.GetNextNodeID(nextNode, directionList[i]);
                Debug.Log("[check for all direc]" + nextNodeCheck);
                if (nextNodeCheck == -1)
                {
                    continue;
                }
                if (CheckForPlayerPresence(nextNodeCheck))
                {
                    Debug.Log("[dog enemy]Enemy alerted at : node" + nextNodeCheck);
                    AlertEnemy(nextNodeCheck);
                    break;
                }
            }
            await new WaitForEndOfFrame();
        }

        public override void AlertEnemy(int _destinationID)
        {
            int middleID = pathService.GetNextNodeID(currentNodeID, spawnDirection);
            Debug.Log("Alerted");
            if(alertedPathNodes.Count!=0)
            {
                middleID = alertedPathNodes[2];
            }
            alertedPathNodes.Clear();

            stateMachine.ChangeEnemyState(EnemyStates.CHASE);
            alertedPathNodes.Add(currentNodeID);            
            alertedPathNodes.Add(middleID);
            alertedPathNodes.Add(_destinationID);

            alertMoveCalled = 0;
            currentEnemyView.AlertEnemyView();

        }
    }
}