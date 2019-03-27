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
    private int dataCnt = 1;

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
    }
}
