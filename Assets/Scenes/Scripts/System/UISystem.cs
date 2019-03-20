using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    public GameObject downSideCanvas;

    public GameObject settingBtn;
    public GameObject soundBtn;
    public GameObject hintBtn;
    public GameObject delBtn;
    public GameObject saveBlockBtn;

    public GameObject[] blockCanvasSet = new GameObject[10];

    //public GameObject shootDirectSet;
    public Sprite[] btnSprSet;
    public Text messageText;
    private char[] messageCharArray;

    static public bool isTriggerEnter = false;
    private bool isSettingBtnOn = false;
    static public bool isDelBtnOn = false;
    static public bool isSaveBlockOn = true;

    public float waitingTime;
    private float time_;

    private Transform blockCanvasTrans;

    private IEnumerator coroutine;
    private IEnumerator coroutine_;

    private void Start()
    {
        //init
        messageText.text = "";
        blockCanvasTrans = downSideCanvas.transform.GetChild(5).transform.GetChild(0).transform;
        if (saveBlockBtn != null)
            SaveBlockOn();
        if(delBtn != null)
            DelBtnOff();
    }

    private IEnumerator CanvasMoveCor(GameObject canvas, Vector3 pos)
    {
        isTriggerEnter = true;
        time_ = 0;

        while (true)
        {
            time_ += Time.deltaTime;
            canvas.transform.localPosition = Vector3.Lerp(canvas.transform.localPosition, pos, time_ / waitingTime);
            yield return new WaitForEndOfFrame();
            if (waitingTime < time_ + 0.2f)
                break;
        }
        canvas.transform.localPosition = pos;
        isTriggerEnter = false;
    }

    private IEnumerator CanvasMoveCor(GameObject canvas1, GameObject canvas2, Vector3 pos1, Vector3 pos2)
    {
        isTriggerEnter = true;
        time_ = 0;

        while (true)
        {
            time_ += Time.deltaTime;
            canvas1.transform.localPosition = Vector3.Lerp(canvas1.transform.localPosition, pos1, time_ / waitingTime);
            canvas2.transform.localPosition = Vector3.Lerp(canvas2.transform.localPosition, pos2, time_ / waitingTime);
            yield return new WaitForEndOfFrame();
            if (waitingTime < time_ + 0.2f)
                break;
        }
        canvas1.transform.localPosition = pos1;
        canvas2.transform.localPosition = pos2;
        isTriggerEnter = false;
    }

    //비활성화 - 게임시작
    public void DownSideCanvasOff(int direct)
    {        
        gameObject.GetComponent<GameSystem>().obstacleBlocks = GameObject.FindGameObjectsWithTag("ObstacleBlock");

        switch (direct)
        {
            case 1:
                gameObject.GetComponent<GameSystem>().GameStart(Direct.UP); 
                break;
            case 2:
                gameObject.GetComponent<GameSystem>().GameStart(Direct.DOWN);
                break;
            case 3:
                gameObject.GetComponent<GameSystem>().GameStart(Direct.RIGHT);
                break;
            case 4:
                gameObject.GetComponent<GameSystem>().GameStart(Direct.LEFT);
                break;
        }

        //shootDirectSet.SetActive(false);
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = CanvasMoveCor(downSideCanvas, new Vector3(0, -130, 0));
        StartCoroutine(coroutine);

        if (isSettingBtnOn)
        {
            if(coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CanvasMoveCor(soundBtn, hintBtn, new Vector3(576, -295, 0), new Vector3(576, -295, 0));
            StartCoroutine(coroutine);
        }
            
    }

    //활성화
    public void DownSideCanvasOn()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = CanvasMoveCor(downSideCanvas, Vector3.zero);
        StartCoroutine(coroutine);
    }

    public void SettingCanvasActive()
    {
        if (isTriggerEnter)
            return;

        //비활성화
        if (isSettingBtnOn)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = CanvasMoveCor(soundBtn, hintBtn, new Vector3(576, -295, 0), new Vector3(576, -295, 0));
            StartCoroutine(coroutine);

            isSettingBtnOn = false;
        }

        //활성화
        else
        {
            StartCoroutine(CanvasMoveCor(soundBtn, hintBtn, new Vector3(576, -190, 0), new Vector3(576, -85, 0)));
            isSettingBtnOn = true;
        }
    }

    public void MessageManager(string message, float time)
    {
        if (coroutine_ != null)
            StopCoroutine(coroutine_);
        coroutine_ = MessageManagerCor(message, time);
        StartCoroutine(coroutine_);
    }

    private IEnumerator MessageManagerCor(string message, float time)
    {
        messageText.text = "";
        messageCharArray = message.ToCharArray();

        for (int i = 0; i < messageCharArray.Length; i++)
        {
            messageText.text += messageCharArray[i];
            yield return new WaitForSeconds(time / messageCharArray.Length);
        }
    }

    private void DelBtnOn()
    {
        isDelBtnOn = true;
        delBtn.GetComponent<Image>().sprite = btnSprSet[1];
    }
    private void DelBtnOff()
    {
        isDelBtnOn = false;
        delBtn.GetComponent<Image>().sprite = btnSprSet[0];
    }
    public void DelBtnActive()
    {
        if (isDelBtnOn)
            DelBtnOff();
        else
            DelBtnOn();
    }

    private void SaveBlockOn()
    {
        isSaveBlockOn = true;
        saveBlockBtn.GetComponent<Image>().sprite = btnSprSet[2];
    }
    private void SaveBlockOff()
    {
        isSaveBlockOn = false;
        saveBlockBtn.GetComponent<Image>().sprite = btnSprSet[3];
    }

    public void SaveBlockActive()
    {
        if (isSaveBlockOn)
            SaveBlockOff();
        else
            SaveBlockOn();
    }

    public void BackActive()
    {
        SceneManager.LoadScene("00");
    }
}