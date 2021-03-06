﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleScoreSelector : MonoBehaviour
{
    /*高尔夫球杆ui图标的字典*/
    private Dictionary<GameObject, string> golfClubsDictionary;
    /*场景中高尔夫球杆ui的集合*/
    private GameObject[] golfClubsList;
    /*场景中高尔夫球杆ui的初始位置*/
    private Vector3 FirstPos;
    
    /*场景中高尔夫球杆ui排列的圆的半径*/
    public float radius;
    
    /*计时器*/
    private float localTime;
    
    /*旋转间隔时间*/
    private float rotateIntervalTime;
    /*距离上一次旋转的时间*/
    private float rotateFromLastTime;
    /*开始计时*/
    private bool startTiming;
    
    /*是否旋转*/
    private bool canRotateOne;
    private bool canRotateTwo;
    /*旋转次数*/
    private float rotateTimes;
    private float floatRoTimes;

    private BallDir _ballDir;
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        localTime = 1.00f;
        canRotateOne = false;
        canRotateTwo = false;
        rotateTimes = 0;
        floatRoTimes = 0.0f;
        rotateIntervalTime = 1.5f;
        rotateFromLastTime = 1.6f;
        startTiming = false;
        
        golfClubsList = GameObject.FindGameObjectsWithTag("GolfClubs");
        //记录旋转的中心点
        FirstPos = golfClubsList[0].gameObject.transform.position;
        //ui初始化时自动排列成圆环状
        UIArrangeInCircle(golfClubsList.Length, golfClubsList);

        _ballDir = GameObject.FindWithTag("GameController").GetComponent<BallDir>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startTiming)
        {
            rotateFromLastTime += Time.fixedUnscaledDeltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && KeyStatus._Instance._KeyStatu == KeyStatu.ChooseClub && rotateFromLastTime > rotateIntervalTime)
        {
            rotateFromLastTime = 0;
            //当前按键状态
            //KeyStatus._Instance._KeyStatu = KeyStatu.ChooseClub;
            if (rotateTimes == 2)
                rotateTimes = -1;
            canRotateOne = true;
            localTime = 1.00f;
            //旋转次数加一
            rotateTimes++;

            floatRoTimes = rotateTimes * 5;
            //选杆后的击打距离和击打条件
            _ballDir.length = (floatRoTimes + 1) * 20;
            _ballDir.isCheck = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && KeyStatus._Instance._KeyStatu == KeyStatu.ChooseClub && rotateFromLastTime > rotateIntervalTime)
        {
            rotateFromLastTime = 0;
            //当前按键状态
            //KeyStatus._Instance._KeyStatu = KeyStatu.ChooseClub;
            if (rotateTimes == 0)
                rotateTimes = 3;
            canRotateTwo = true;
            localTime = 1.00f;
            //旋转次数加一
            rotateTimes--;

            floatRoTimes = rotateTimes * 5;
            //选杆后的击打距离和击打条件
            _ballDir.length = (floatRoTimes + 1) * 20;
            _ballDir.isCheck = true;
        }
        //Debug.Log(Time.time - localTime);
//        if (canRotateOne)
//        {
//            startTiming = true;
//            
//            localTime -= Time.fixedUnscaledDeltaTime;
//            //更新轮盘上按钮位置
//            UpdateNewPos(golfClubsList.Length, 1);
//            //更新状态
//            UpdateNewState((int)rotateTimes);
//            //过了1s 停止旋转
//            if (localTime <= 0.001f)
//            {
//                canRotateOne= false;
//            }
//        }
//        if (canRotateTwo)
//        {
//            startTiming = true;
//            
//            localTime -= Time.fixedUnscaledDeltaTime;
//            //更新轮盘上按钮位置
//            UpdateNewPos(golfClubsList.Length, -1);
//            //更新状态
//            UpdateNewState((int)rotateTimes);
//            //过了1s 停止旋转
//            if (localTime <= 0.001f)
//            {
//                canRotateTwo= false;
//            }
//        }
    }

    void LateUpdate()
    {
        if (canRotateOne)
        {
            startTiming = true;
            
            localTime -= Time.fixedUnscaledDeltaTime;
            //更新轮盘上按钮位置
            UpdateNewPos(golfClubsList.Length, 1);
            //更新状态
            UpdateNewState((int)rotateTimes);
            //过了1s 停止旋转
            if (localTime <= 0.001f)
            {
                canRotateOne= false;
            }
        }
        if (canRotateTwo)
        {
            startTiming = true;
            
            localTime -= Time.fixedUnscaledDeltaTime;
            //更新轮盘上按钮位置
            UpdateNewPos(golfClubsList.Length, -1);
            //更新状态
            UpdateNewState((int)rotateTimes);
            //过了1s 停止旋转
            if (localTime <= 0.001f)
            {
                canRotateTwo= false;
            }
        }
    }

    /// <summary>
    /// ui初始化时自动排列成圆环状
    /// </summary>
    /// <param name="uiNum">ui个数</param>
    /// <param name="uiList">ui集合</param>
    void UIArrangeInCircle(int uiNum, GameObject[] uiList)
    {
        float angle = 120;
        for (int i = 0; i < uiNum; i++)
        {
            //角度转弧度
            float radian = (angle / 180) * Mathf.PI;
            float xPos = radius * Mathf.Cos(radian);
            float yPos = radius * Mathf.Sin(radian);

            golfClubsList[i].gameObject.transform.localPosition = new Vector3(xPos, yPos, 0);
            
            //ui间间隔的角度
            angle += 360 / uiNum;
        }
    }

    /// <summary>
    /// 更新轮盘上各按钮位置
    /// </summary>
    /// <param name="direction">正数为顺时针 负数为逆时针</param>
    void UpdateNewPos(int uiNum,int direction)
    {
//        Debug.Log(FirstPos);
        for (int i = 0; i < uiNum; i++)
        {
            golfClubsList[i].gameObject.transform.RotateAround(FirstPos, Vector3.back,Time.fixedUnscaledDeltaTime  * direction * 360/uiNum);
            golfClubsList[i].transform.localRotation = Quaternion.identity;
        }
    }

    void UpdateNewState(int index)
    {
        //Debug.Log(index);
        Color color = golfClubsList[index].gameObject.GetComponent<Image>().color;
        BallStatus._Instance.ballName = golfClubsList[index].gameObject.GetComponentInChildren<Text>().text;
        if (canRotateOne)
        {
            if (index >= 1)
            {
                Color color0 = golfClubsList[index - 1].gameObject.GetComponent<Image>().color;
                golfClubsList[index].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color, new Color(color.r, color.g, color.b, 255), Time.fixedUnscaledDeltaTime * 10);
                golfClubsList[index - 1].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color0, new Color(color0.r, color0.g, color0.b, 0), Time.fixedUnscaledDeltaTime * 10);
                //Debug.Log(1);
            }
            else
            {
                Color color0 = golfClubsList[2].gameObject.GetComponent<Image>().color;
                golfClubsList[index].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color, new Color(color.r, color.g, color.b, 255), Time.fixedUnscaledDeltaTime * 10);
                golfClubsList[2].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color0, new Color(color0.r, color0.g, color0.b, 0), Time.fixedUnscaledDeltaTime * 10);
            }
        }

        if (canRotateTwo)
        {
            if (index <= 1)
            {
                Color color0 = golfClubsList[index + 1].gameObject.GetComponent<Image>().color;
                golfClubsList[index].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color, new Color(color.r, color.g, color.b, 255), Time.fixedUnscaledDeltaTime * 10);
                golfClubsList[index + 1].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color0, new Color(color0.r, color0.g, color0.b, 0), Time.fixedUnscaledDeltaTime * 10);
                //Debug.Log(1);
            }
            else
            {
                Color color0 = golfClubsList[0].gameObject.GetComponent<Image>().color;
                golfClubsList[index].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color, new Color(color.r, color.g, color.b, 255), Time.fixedUnscaledDeltaTime * 10);
                golfClubsList[0].gameObject.GetComponent<Image>().color =
                    Color.Lerp(color0, new Color(color0.r, color0.g, color0.b, 0), Time.fixedUnscaledDeltaTime * 10);
            }
        }
    }
}
