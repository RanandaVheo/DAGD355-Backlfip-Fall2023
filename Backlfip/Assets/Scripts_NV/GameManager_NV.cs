using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager_NV : MonoBehaviour
{

    public static GameManager_NV gameManagerNV { get; private set; }

    public PlayerTemp playerTemp = new PlayerTemp(100, 100);

    public int playerScore = 0;
    public TextMeshProUGUI scoreUI;

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

    private void Update()
    {
        scoreUI.text = playerScore.ToString();
    }
}
