using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHUD : MonoBehaviour
{
    public static EnemyHUD instance;
    [SerializeField] GameObject[] enemyhearts;
    [SerializeField] public int enemylife;
    [SerializeField] public int damage;
    // Start is called before the first frame update

    void Awake()
    {

        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);



        enemylife = enemyhearts.Length;
    }

    public void EnemyTakeDamage(int damage)
    {
        if (enemylife >= 1)
        {
            enemylife -= damage;
            Destroy(enemyhearts[enemylife].gameObject);
        }
    }
}
