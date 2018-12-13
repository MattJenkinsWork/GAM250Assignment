using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Dirty singleton script
    public static GameManager instance;

    private void Awake()
    {
        //Singleton pattern
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(this);
    }
    

}
