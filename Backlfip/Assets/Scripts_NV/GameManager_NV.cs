using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_NV : MonoBehaviour
{
    public static GameManager_NV gameManagerNV { get; private set; }

    public PlayerTemp playerTemp = new PlayerTemp(100, 100);

    void Awake()
    {
        if (gameManagerNV != null && gameManagerNV != this)
        {
            Destroy(this);
        }
        else
        {
            gameManagerNV = this;
        }
    }

}
