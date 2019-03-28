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

    private void Awake()
    {
        gameObject.GetComponent<SoundManager>().bgmSource.clip = currentStageInfo.currentBGM;           // 현재 테마곡 삽입.

        for(int i = 0; i< currentStageInfo.stageOrder.Length;i++)
        {
            prefabObj = Instantiate(currentStageInfo.stageOrder[i].obj);
            prefabObj.transform.localPosition = new Vector3(initUIPos + (i * 100), 60, 0);
            prefabObj.transform.parent = downSideUIPoolTrans;
        }
        
    }
}
