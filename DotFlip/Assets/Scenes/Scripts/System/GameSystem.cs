using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSystem : MonoBehaviour
{
    public GameState currentGameState;
    public Transform lightSet;

    private UISystem uiSystem;
    private StageSystem stageSystem;

    private GameObject player;
    public GameObject[] blocks;
    public GameObject[] obstacleBlocks;
    public GameObject[] diamond;

    static public Stack<GameObject> switchContainObjectsStack = new Stack<GameObject>();           //스위치가 있는 오브젝트들
    private Vector3[] switchContainObjectPos;               //스위치가 있는 오브젝트의 위치값
    private Vector3[] switchContainObjectEulerAngle;               //스위치가 있는 오브젝트의 각도값
    private GameObject[] switchContainObject;

    public static int rowCnt = 6;
    public static int colCnt = 13;

    static public bool[,] tileObjectState = new bool[colCnt, rowCnt];

    static public void TileObject(int row, int col, bool isAble)
    {
        if (row - 1 < 0 || col - 1 < 0)
            return;
        else
            tileObjectState[row - 1, col - 1] = isAble;
    }



    private void Awake()
    {
        for (int i = 0; i < colCnt; i++)
        {
            for (int j = 0; j < rowCnt; j++)
            {
                tileObjectState[i, j] = true;
            }
        }
    }
    private void SaveSwitchContainObjectPos()
    {
        switchContainObject = new GameObject[switchContainObjectsStack.Count];
        switchContainObjectPos = new Vector3[switchContainObjectsStack.Count];
        switchContainObjectEulerAngle = new Vector3[switchContainObjectsStack.Count];

        //초기 위치 저장
        for (int i =0; i< switchContainObject.Length; i++)
        {
            switchContainObject[i] = switchContainObjectsStack.Pop();
            switchContainObjectPos[i] = switchContainObject[i].transform.localPosition;
            switchContainObjectEulerAngle[i] = switchContainObject[i].transform.localEulerAngles;
        }
    }

    private void Start()
    {
        stageSystem = gameObject.GetComponent<StageSystem>();
        uiSystem = gameObject.GetComponent<UISystem>();
        player = GameObject.FindWithTag("Player");

        SaveSwitchContainObjectPos();
        uiSystem.MessageManager(stageSystem.stage[stageSystem.currentStage - 1].messageInfo.preMent, 0);
    }

    //게임 시작
    public void GameStart(Direct direct)
    {
        currentGameState = GameState.DISPLAYING;
        uiSystem.MessageManager(stageSystem.stage[stageSystem.currentStage - 1].messageInfo.ment, stageSystem.stage[stageSystem.currentStage - 1].messageInfo.messageDisplayTime);
        player.GetComponent<PlayerMove>().currentDirect = direct;
        blocks = GameObject.FindGameObjectsWithTag("Block");
        SoundManager.instance_.bgmSource.clip = SoundManager.instance_.bgmClips[0];
        SoundManager.instance_.bgmSource.Play();
    }

    //라이프를 1개 소진
    public void GameMiss()
    {
        StartCoroutine(GameMissCor());
    }

    //라이프를 모두 다 사용했을 때
    public void GameEnd()
    {
    }

    IEnumerator GameMissCor()
    {
        currentGameState = GameState.FAIL;

        for (int i = 0; i < lightSet.childCount; i++)
            lightSet.GetChild(i).GetComponent<LightManager>().CoroutineStop();
        yield return new WaitForSeconds(0.8f);
        SoundManager.instance_.SFXPlay(SoundManager.instance_.sfxClips[0], 0.5f);
        yield return new WaitForSeconds(0.9f);

        player.transform.position = stageSystem.playerInitPos;
        player.GetComponent<PlayerMove>().currentDirect = Direct.HOLD;
        currentGameState = GameState.READY;
        uiSystem.DownSideCanvasOn();
        gameObject.GetComponent<CameraSystem>().camera_.transform.position = new Vector3(0, 0, -10);

        uiSystem.MessageManager(stageSystem.stage[stageSystem.currentStage - 1].messageInfo.preMent, 0);
        for (int i = 0; i < obstacleBlocks.Length; i++)
        {
            obstacleBlocks[i].GetComponent<SpriteRenderer>().color = new Color(obstacleBlocks[i].GetComponent<SpriteRenderer>().color.r, obstacleBlocks[i].GetComponent<SpriteRenderer>().color.g, obstacleBlocks[i].GetComponent<SpriteRenderer>().color.b, 1);
            obstacleBlocks[i].GetComponent<BoxCollider>().enabled = true;
        }

        for (int i = 0; i < diamond.Length; i++)
            diamond[i].SetActive(true);

        //Init
        for (int i = 0; i < switchContainObjectPos.Length; i++)
        {
            switchContainObject[i].transform.localEulerAngles = switchContainObjectEulerAngle[i];
            switchContainObject[i].transform.localPosition = switchContainObjectPos[i];

            if (switchContainObject[i].GetComponent<Move>() != null)
            {
                switchContainObject[i].GetComponent<Move>().switchObj.GetComponent<Switch>().switchOn = false;
                switchContainObject[i].GetComponent<Move>().switchObj.GetComponent<Switch>().StopCor();
            }

            else if (switchContainObject[i].GetComponent<Spin>() != null)
            {
                switchContainObject[i].GetComponent<Spin>().switchObj.GetComponent<Switch>().switchOn = false;
                switchContainObject[i].GetComponent<Spin>().switchObj.GetComponent<Switch>().StopCor();
            }

            else if (switchContainObject[i].GetComponent<Blink>() != null)
            {
                switchContainObject[i].GetComponent<Blink>().switchObj.GetComponent<Switch>().switchOn = false;
                switchContainObject[i].GetComponent<Blink>().switchObj.GetComponent<Switch>().StopCor();
            }

        }

        if (UISystem.isSaveBlockOn)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].GetComponent<SpriteRenderer>().color = new Color(blocks[i].GetComponent<SpriteRenderer>().color.r, blocks[i].GetComponent<SpriteRenderer>().color.g, blocks[i].GetComponent<SpriteRenderer>().color.b, 1);
                blocks[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

}
