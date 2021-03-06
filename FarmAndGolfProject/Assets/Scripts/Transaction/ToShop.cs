﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToShop : MonoBehaviour
{
    private bool shopIsOn = false;//判断是否已打开商店
    //public TipsUI tips;//提示框(已弃置,商店通过对话框过渡打开)
    public storeInventoryManager siy;//不要再滥用static方法了!!
    public Inventory store;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            siy.GotPlayer(other.GetComponent<Player>());
            siy.storeInventory = store;
        }
    }

    //测试用, 实装会接在对话后而非碰撞触发
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {

            /*弃用键盘响应, 改用对话的按钮打开
            if (Input.GetKeyDown(KeyCode.K) && shopIsOn == false)
            {
                shopIsOn = true;
                siy.OpenSellStore();

            }
            if (Input.GetKeyDown(KeyCode.L) && shopIsOn == false)
            {
                shopIsOn = true;
                siy.OpenBuyStore();
            }
            */

            if (!shopIsOn)
            {
                //tips.Show();//显示提示栏
                other.GetComponent<Player>().KeepMove();
                //tips.UpdateTooltip("按下\"K\"键出售\n\"L\"键购买");
            }

            if (shopIsOn)
            {
                other.GetComponent<Player>().StopMove();//强制播放自动行走,剥夺玩家控制权
                // tips.Hide();//隐藏提示栏
            }
            if (siy.storePanel.activeSelf == false)
            {
                shopIsOn = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")//检测碰撞物体是否为主角
        {
            shopIsOn = false;
            //tips.Hide();//隐藏提示栏
        }
    }


    //用于按钮打开商店的两个方法
    public void OpenBuyMode()
    {
        if (shopIsOn == false)
        {
            shopIsOn = true;
            siy.OpenBuyStore();
        }
    }

    public void OpenSellMode()
    {
        if (shopIsOn == false)
        {
            shopIsOn = true;
            siy.OpenSellStore();
        }
    }
}
