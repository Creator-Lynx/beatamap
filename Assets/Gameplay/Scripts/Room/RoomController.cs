using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private List<EnemyController> _enemies = new List<EnemyController>();
    [SerializeField] private List<LatticeController> _lattices = new List<LatticeController>();

    public void OnPlayerEnter()
    {
        CloseLattices();
        AwakeEnemies();
    }

    private void CloseLattices()
    {
        foreach(var lattice in _lattices)
        {
            lattice.Close();
        }
    }

    private void OpenLattices()
    {
        foreach (var lattice in _lattices)
        {
            lattice.Open();
        }
    }

    private void AwakeEnemies()
    {
        foreach(var  enemy in _enemies)
        {
            enemy.OnDeath += OnEnemyDead;
            enemy.AwakeEnemy();
        }
    }

    private void OnEnemyDead(EnemyController enemy)
    {
        _enemies.Remove(enemy);
        if(_enemies.Count == 0)
        {
            OnRoomFinished();
        }
    }

    private void OnRoomFinished()
    {
        OpenLattices();
    }
}
