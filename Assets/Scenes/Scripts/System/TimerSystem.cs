using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    public GameObject copImg;
    public GameObject thiefImg;

    private int currentStageTime;
    private int startPoint = -470;
    private int endPoint = 0;
    private int currentPoint;
    private int intervalPoint;

    private void Start()
    {
        currentStageTime = gameObject.GetComponent<StageSystem>().stage[gameObject.GetComponent<StageSystem>().currentStage - 1].currentMaximumTime;
        intervalPoint = endPoint - startPoint;
        currentPoint = startPoint;
        StartCoroutine(TimerWorking(intervalPoint / currentStageTime));
    }

    public IEnumerator TimerWorking(int flowPoint)
    {
        yield return new WaitForSeconds(1f);
        currentPoint += flowPoint;
        copImg.transform.localPosition = new Vector3(currentPoint, 318, 0);

        if (currentPoint < endPoint)
            StartCoroutine(TimerWorking(flowPoint));

        //else
        //    //게임 종료
    }
}
