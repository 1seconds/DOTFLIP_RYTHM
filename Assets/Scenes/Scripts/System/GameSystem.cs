using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSystem : MonoBehaviour
{
    public GameState currentGameState;

    private UISystem uiSystem;
    private StageSystem stageSystem;

    private GameObject player;
    /*[HideInInspector]*/public GameObject[] blocks;
    /*[HideInInspector]*/public GameObject[] orderedBlocks;

    public static int rowCnt = 17;
    public static int colCnt = 35;

    private bool isSpaceKeyDown = false;

    static public bool[,] tileObjectState = new bool[colCnt, rowCnt];


    //방향벡터 변수
    private Vector3 initHeading;
    private float distance;

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

    private void Start()
    {
        stageSystem = gameObject.GetComponent<StageSystem>();
        uiSystem = gameObject.GetComponent<UISystem>();
        player = GameObject.FindWithTag("Player");
    }

    IEnumerator BeforeEnterBlockCor()
    {
        initHeading = (orderedBlocks[0].transform.position - player.transform.position) / (orderedBlocks[0].transform.position - player.transform.position).magnitude;
        
        while (true)
        {
            yield return new WaitForEndOfFrame();
            distance = (orderedBlocks[0].transform.position - player.transform.position).magnitude;
            player.transform.Translate(initHeading * GameObject.FindWithTag("GameManager").GetComponent<StageSystem>().currentStageInfo.speed);

            if (isSpaceKeyDown)
            {
                SoundManager.instance_.bgmSource.Play();
                isSpaceKeyDown = false;
                break;
            }
        }

        StartCoroutine(PlayerMoveCor(1));
    }

    IEnumerator PlayerMoveCor(int index)
    {
        initHeading = (orderedBlocks[index].transform.position - player.transform.position) / (orderedBlocks[index].transform.position - player.transform.position).magnitude;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            distance = (orderedBlocks[index].transform.position - player.transform.position).magnitude;


            player.transform.Translate(initHeading * orderedBlocks[index].GetComponent<BlockData>().speed);

            //if (distance* speed > 일정수치)
            //{
            //    Debug.Log("종료");
            //    GameMiss();
            //    break;
            //}

            if (isSpaceKeyDown)
            {
                isSpaceKeyDown = false;
                break;
            }
        }
        index += 1;
        if (orderedBlocks.Length > index)
            StartCoroutine(PlayerMoveCor(index));
    }

    //게임 시작
    public void GameStart()
    {
        //다 사용했을때에만 시작가능
        if (stageSystem.downSideUIPoolTrans.childCount > 0)
            return;

        currentGameState = GameState.DISPLAYING;
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
        StartCoroutine(BeforeEnterBlockCor());
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!currentGameState.Equals(GameState.DISPLAYING))
                GameStart();
            else
                isSpaceKeyDown = true;
        }
    }
}
