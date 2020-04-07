﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public string scene;//传送目标场景名，在检查器输入
    public float x, y;//传送后坐标，在检查器输入

    public GameObject gsm;

    void Start()
    {
        gsm = GameObject.Find("GameSaveManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //起“传送门”的作用
        if (collision.tag == "Player")//检测碰撞物体是否为主角
        {
            gsm.GetComponent<GameSaveManager>().SaveGame();//每次转场都自动存储游戏
            Player.initialPosition = new Vector3(x, y, 0);
            SceneManager.LoadScene(scene);//切换场景
        }
    }
}
