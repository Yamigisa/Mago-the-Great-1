using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD instance;
    [SerializeField] GameObject[] playerhearts;
    [SerializeField] public int playerlife;
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



        playerlife = playerhearts.Length;
    }

    public void PlayerTakeDamage(int damage)
    {
        if (playerlife >= 1)
        {
            playerlife -= damage;
            Destroy(playerhearts[playerlife].gameObject);
        }
    }
}
