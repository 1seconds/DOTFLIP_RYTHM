using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSystem : MonoBehaviour
{
    public StageData currentStageInfo;
    public Transform downSideUIPoolTrans;

    private GameObject prefabObj;
    private float initUIPos = 180;
    private float time_;

    private void Awake()
    {
        gameObject.GetComponent<SoundManager>().bgmSource.clip = currentStageInfo.currentBGM;           // 현재 테마곡 삽입.

        for(int i = 0; i< currentStageInfo.stageOrder.Length;i++)
        {
            prefabObj = Instantiate(currentStageInfo.stageOrder[i].obj);
            prefabObj.transform.localPosition = new Vector3(initUIPos + (i * 100) + 10, 60, 0);
            prefabObj.transform.parent = downSideUIPoolTrans;
            prefabObj.GetComponent<UIDrag>().block.GetComponent<BlockData>().order = i;                 //블럭에 대한 순서
        }
    }

    public void DownSideUIPoolLeftMove()
    {
        StartCoroutine(DownSideUIPoolLeftMoveCor());
    }

    IEnumerator DownSideUIPoolLeftMoveCor()
    {
        time_ = 0;
        while(true)
        {
            yield return new WaitForEndOfFrame();
            time_ += Time.deltaTime;

            for (int i = 0; i < downSideUIPoolTrans.childCount; i++)
                downSideUIPoolTrans.GetChild(i).localPosition = Vector3.Lerp(new Vector3(initUIPos + (i * 100) - 40, -40, 0), new Vector3(initUIPos + (i * 100) - 40, -40, 0) - new Vector3(100, 0, 0), time_);

            if (time_ > 1.0f)
                break;
        }
    }
}
