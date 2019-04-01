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
    public GameObject[] orderedBlocks;

    static public Stack<GameObject> switchContainObjectsStack = new Stack<GameObject>();           //스위치가 있는 오브젝트들
    private Vector3[] switchContainObjectPos;               //스위치가 있는 오브젝트의 위치값
    private Vector3[] switchContainObjectEulerAngle;               //스위치가 있는 오브젝트의 각도값
    private GameObject[] switchContainObject;

    public static int rowCnt = 17;
    public static int colCnt = 35;

    private float time_;

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
    }

    IEnumerator PlayerMoveCor(int index)
    {
        time_ = 0;
        while (true)
        {
            time_ += Time.deltaTime;
            yield return new WaitForEndOfFrame();

            player.transform.position = Vector2.Lerp(player.transform.position, orderedBlocks[index].transform.position, time_ * orderedBlocks[index].GetComponent<BlockData>().speed / (Vector2.Distance(player.transform.position, orderedBlocks[index].transform.position)));
            if (time_ > (Vector2.Distance(player.transform.position, orderedBlocks[index].transform.position)) / orderedBlocks[index].GetComponent<BlockData>().speed)
                break;
        }

        index += 1;
        if (orderedBlocks.Length > index)
        {
            StartCoroutine(PlayerMoveCor(index));
        }
    }

    //게임 시작
    public void GameStart()
    {
        //다 사용했을때에만 시작가능
        if (stageSystem.downSideUIPoolTrans.childCount > 0)
            return;

        blocks = GameObject.FindGameObjectsWithTag("Block");
        orderedBlocks = new GameObject[blocks.Length];

        for(int i =0; i< blocks.Length;i++)
        {
            for(int j = 0; j< orderedBlocks.Length;j++)
            {
                if (blocks[j].GetComponent<BlockData>().order == i)
                {
                    orderedBlocks[i] = blocks[j];
                    break;
                }   
            }
        }
        StartCoroutine(PlayerMoveCor(0));
        currentGameState = GameState.DISPLAYING;
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
        yield return new WaitForSeconds(0.8f);
        SoundManager.instance_.SFXPlay(SoundManager.instance_.sfxClips[0], 0.5f);
        yield return new WaitForSeconds(0.9f);

        currentGameState = GameState.READY;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !currentGameState.Equals(GameState.DISPLAYING))
            GameStart();
    }
}
