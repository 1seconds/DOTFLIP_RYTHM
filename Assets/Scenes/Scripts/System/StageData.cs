using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ORDER
{
    public GameObject obj;
}

public class StageData : ScriptableObject
{
    public int index;                   //몇스테이지인지
    public AudioClip currentBGM;        //사용될 배경음악
    public float speed;                 //사용될 박자 & 속도감
    public ORDER[] stageOrder;          //블럭순서
}
