using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    string stage = "0";
    private float time_ = 0;
    private bool isTrigger = false;

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<PlayerMove>().currentDirect = Direct.HOLD;
            isTrigger = true;
        }
    }

    private void Update()
    {
        if(isTrigger)
        {
            time_ += Time.deltaTime;
            if (time_ > 0.5f)
            {
                if (System.Convert.ToInt32((GameObject.FindWithTag("GameManager").GetComponent<StageSystem>().currentStage + 1).ToString()) < 10)
                    stage += (GameObject.FindWithTag("GameManager").GetComponent<StageSystem>().currentStage + 1).ToString();
                else
                    stage = (GameObject.FindWithTag("GameManager").GetComponent<StageSystem>().currentStage + 1).ToString();
                SceneManager.LoadScene(stage);
            }
        }
    }
}
