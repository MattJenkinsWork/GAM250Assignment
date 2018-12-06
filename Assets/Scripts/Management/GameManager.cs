using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Dirty singleton script
    public GameManager instance;

    private void Awake()
    {
        //Dirty singleton script
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(this);
    }
    



    public List<GameObject> enemyList = new List<GameObject>();

    public bool allEnemiesDead = false;


    public void AddEnemyList(GameObject enemy)
    {
        enemyList.Add(enemy);

    }


    public void RemoveEnemyList(GameObject enemy)
    {
        enemyList.Remove(enemy);

        EnemiesDeadCheck();

    }

    public bool EnemiesDeadCheck()
    {
        if (enemyList.Count == 0)
        {
            allEnemiesDead = true;
        }

        else
            allEnemiesDead = false;

        return allEnemiesDead;

    }

}
