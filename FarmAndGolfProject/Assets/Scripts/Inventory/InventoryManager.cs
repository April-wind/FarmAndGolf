﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //单例模式
    static InventoryManager instance;

    public Inventory myBag;//存储层的背包
    public GameObject slotGrid;//一堆格子,表层意义上的"背包"
    public GameObject emptySlot;
    public Text itemInformation;//物品描述
    public Image itemImagePanel;//物品的放大图片
    public List<GameObject> slots = new List<GameObject>();//格子列表


    void Awake()//单例
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    //每次打开背包,都能刷新一下物品,保证物品数量显示正确
    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";//这个背包的描述框是一直显示的,所以要保证没点击物品的时候描述为"空"
    }

    //更新描述框文本为 传入的这个文本
    public static void UpdateItemInfo(string itemDescription, Sprite itemImage)
    {
        instance.itemInformation.text = itemDescription;
        instance.itemImagePanel.sprite = itemImage;
    }


    //为了解决,背包中已有,如何增加 视觉层 的背包里的"物品数字"
    //数据层的背包当然不用担心,直接增加数量了
    public static void RefreshItem()
    {
        //直接毁掉整个 视觉层 的背包!
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        //再根据数据层的背包重建视觉层的背包
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);//设置父物体
            instance.slots[i].GetComponent<Slot>().slotID = i;//同步ID
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);//同步图片等物品信息
        }
    }
}
