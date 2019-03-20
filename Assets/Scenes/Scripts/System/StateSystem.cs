using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    READY,
    DISPLAYING,
    FAIL
}

public enum Direct
{
    HOLD,
    UP,
    DOWN,
    RIGHT,
    LEFT,
}

public enum ClockDirect
{
    CLOCKWISE,          //시계방향
    ANTICLOCKWISE       //반시계방향
}

public enum UISet
{
    UIBLOCK,
    SETBLOCK
}

public enum Block
{
    NORMAL,
    INFINITY
}

