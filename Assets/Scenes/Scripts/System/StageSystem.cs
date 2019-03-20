using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSystem : MonoBehaviour
{
    public StageData[] stage;
    private GameObject objPrefab;
    public int currentStage;
    public Text displayStage;
    private GameObject player;
    [HideInInspector] public Vector2 playerInitPos;
    //private GameObject bgPrefab;
    private int dataCnt = 1;

    //private void MakeNextStage()
    //{
    //    for (int i = 0; i < stage[currentStage - 1].nextStageInfo.Length; i++)
    //    {
    //        if (stage[currentStage - 1].nextStageInfo[i].stageDirect.Equals(Direct.DOWN))
    //        {
    //            for (int j = 0; j < stage[stage[currentStage - 1].nextStageInfo[i].nextStage - 1].ObjectInfo.Length; j++)
    //            {
    //                objPrefab = Instantiate(stage[stage[currentStage - 1].nextStageInfo[i].nextStage - 1].ObjectInfo[j].obj);
    //                objPrefab.transform.parent = gameObject.transform.GetChild(0).transform;
    //                objPrefab.transform.position = stage[stage[currentStage - 1].nextStageInfo[i].nextStage - 1].ObjectInfo[j].pos;

    //                tmpVector = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(objPrefab.transform.position);

    //                restX = tmpVector.x % 80;
    //                restY = tmpVector.y % 80;
    //                modX = (int)(tmpVector.x / 80);
    //                modY = (int)(tmpVector.y / 80);

    //                if (restX < 40)
    //                {
    //                    if (restY < 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * modX, 80 * modY, 0));
    //                    }
    //                    else if (restY >= 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * modX, 80 * (modY + 1), 0));
    //                    }
    //                }
    //                else if (restX >= 40)
    //                {
    //                    if (restY < 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * (modX + 1), 80 * modY, 0));
    //                    }
    //                    else if (restY >= 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * (modX + 1), 80 * (modY + 1), 0));
    //                    }
    //                }

    //                objPrefab.transform.position += new Vector3(0, -11.65f, 10);

    //            }
    //            bgPrefab = Instantiate(Resources.Load("Prefab/bg")) as GameObject;
    //            bgPrefab.transform.position = new Vector3(0, -11.65f, 0);
    //        }
    //        else if (stage[currentStage - 1].nextStageInfo[i].stageDirect.Equals(Direct.RIGHT))
    //        {
    //            for (int j = 0; j < stage[stage[currentStage - 1].nextStageInfo[i].nextStage - 1].ObjectInfo.Length; j++)
    //            {
    //                objPrefab = Instantiate(stage[stage[currentStage - 1].nextStageInfo[i].nextStage - 1].ObjectInfo[j].obj);
    //                objPrefab.transform.parent = gameObject.transform.GetChild(0).transform;
    //                objPrefab.transform.position = stage[stage[currentStage - 1].nextStageInfo[i].nextStage - 1].ObjectInfo[j].pos;

    //                tmpVector = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(objPrefab.transform.position);

    //                restX = tmpVector.x % 80;
    //                restY = tmpVector.y % 80;
    //                modX = (int)(tmpVector.x / 80);
    //                modY = (int)(tmpVector.y / 80);

    //                if (restX < 40)
    //                {
    //                    if (restY < 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * modX, 80 * modY, 0));
    //                    }
    //                    else if (restY >= 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * modX, 80 * (modY + 1), 0));
    //                    }
    //                }
    //                else if (restX >= 40)
    //                {
    //                    if (restY < 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * (modX + 1), 80 * modY, 0));
    //                    }
    //                    else if (restY >= 40)
    //                    {
    //                        objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * (modX + 1), 80 * (modY + 1), 0));
    //                    }
    //                }

    //                objPrefab.transform.position += new Vector3(20.68f, 0, 10);

    //            }
    //            bgPrefab = Instantiate(Resources.Load("Prefab/bg")) as GameObject;
    //            bgPrefab.transform.position = new Vector3(20.68f, 0, 0);
    //        }
    //    }
    //}

    //private void CurrentStageMake()
    //{
    //    for (int i = 0; i < stage[currentStage - 1].ObjectInfo.Length; i++)
    //    {
    //        objPrefab = Instantiate(stage[currentStage - 1].ObjectInfo[i].obj);
    //        objPrefab.transform.parent = GameObject.FindWithTag("GameManager").transform.GetChild(0).transform;
    //        objPrefab.transform.position = stage[currentStage - 1].ObjectInfo[i].pos;
    //        tmpVector = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(objPrefab.transform.position);

    //        restX = tmpVector.x % 80;
    //        restY = tmpVector.y % 80;
    //        modX = (int)(tmpVector.x / 80);
    //        modY = (int)(tmpVector.y / 80);

    //        if (restX < 40)
    //        {
    //            if (restY < 40)
    //            {
    //                objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * modX, 80 * modY, 0));
    //                GameSystem.TileObject(modX - 1, modY - 1, false);
    //            }
    //            else if (restY >= 40)
    //            {
    //                objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * modX, 80 * (modY + 1), 0));
    //                GameSystem.TileObject(modX-1, modY, false);
    //            }
    //        }
    //        else if (restX >= 40)
    //        {
    //            if (restY < 40)
    //            {
    //                objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * (modX + 1), 80 * modY, 0));
    //                GameSystem.TileObject(modX, modY-1, false);
    //            }
    //            else if (restY >= 40)
    //            {
    //                objPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(80 * (modX + 1), 80 * (modY + 1), 0));
    //                GameSystem.TileObject(modX, modY, false);
    //            }
    //        }
    //        objPrefab.transform.position += new Vector3(0, 0, 10);
    //    }

    //    player.transform.position = stage[currentStage - 1].playerInfo.pos;
    //    player.AddComponent<PosDebug_ForDev>();
    //}

    private void Awake()
    {
        for (int i = 0; i < 50; i++)
        {
            if (Resources.Load("Data/MAP/" + dataCnt) as StageData == null)
            {
                stage = new StageData[dataCnt - 1];
                break;
            }
            dataCnt += 1;
        }
        dataCnt = 1;
        for (int i = 0; i < stage.Length; i++)
        {
            stage[i] = Resources.Load("Data/MAP/" + dataCnt) as StageData;
            dataCnt += 1;
        }
    }

    private void Start()
    {
        displayStage.text = "Stage " + currentStage.ToString();
        player = GameObject.FindWithTag("Player");
        playerInitPos = player.transform.position;
        //CurrentStageMake();
        //MakeNextStage();
    }
}
