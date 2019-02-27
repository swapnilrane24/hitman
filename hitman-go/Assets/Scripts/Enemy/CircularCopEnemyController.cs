﻿using UnityEngine;
using Common;
using PathSystem;
using System.Collections;

namespace Enemy
{
    public class CircularCopEnemyController : EnemyController
    {


        public CircularCopEnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {


        }

    }
}