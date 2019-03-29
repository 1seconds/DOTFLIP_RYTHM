using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    READY,
    DISPLAYING,
    FAIL
}

public enum ClockDirect
{
    CLOCKWISE,          //시계방향
    ANTICLOCKWISE       //반시계방향
}

public enum UISet
{
    SETTINGBLOCK,       //설치해야할 블럭
    SETEDBLOCK          //이미설치된 블럭
}

public enum Block
{
    NORMAL,
    INFINITY
}

