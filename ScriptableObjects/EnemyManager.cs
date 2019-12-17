using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyData> enemyList = new List<EnemyData>();

    private void DoEnemyTurns()
    {
        foreach (EnemyData enemy in enemyList)
        {
            if(enemy.enemyName == "Bob")
            {
                //do bob's turn
            }
            else if (enemy.enemyName == "Suzy")
            {
                //do suzy's turn
            }
            else if (enemy.enemyName == "Hank")
            {
                //do hank's turn
            }
            else if (enemy.enemyName == "WhyDidIDoItThisWay")
            {
                //do this guys's turn
            }
        }
    }
}
