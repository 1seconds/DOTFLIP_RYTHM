using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[Serializable]
//public class ObjectInfo
//{
//    public GameObject obj;
//    public Vector2 pos;                        
//}
[Serializable]
public class MessageInfo
{
    public string ment;                 //나오는 멘트
    public string preMent;              //게임 시작 전 나오는 멘트
    public float messageDisplayTime;    //메세지가 몇초에 걸쳐서 나올것인지 ..
    
}

//[Serializable]
//public class NextStageInfo
//{
//    public Direct stageDirect;
//    public int nextStage;
//}

//[Serializable]
//public class BeforeStageInfo
//{
//    public Direct stageDirect;
//    public int beforeStage;
//}

//[Serializable]
//public class PlayerInfo
//{
//    public Vector2 pos;
//    public Direct shootDirect;
//}

public class StageData : ScriptableObject
{
    public int currentStage;                //몇스테이지인지
    //public ObjectInfo[] ObjectInfo;         //이곳에서나오는 오브젝트의 정보
    public MessageInfo messageInfo;         //메세지 정보
    //public BeforeStageInfo[] beforeStageInfo;       //스테이지 정보
    //public NextStageInfo[] nextStageInfo;       //스테이지 정보
    //public PlayerInfo playerInfo;           //플레이어 정보
    public int currentMaximumTime;          //현재 스테이지에서 최대의 시간
}
