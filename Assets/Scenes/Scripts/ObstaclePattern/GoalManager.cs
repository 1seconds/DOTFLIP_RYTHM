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
}
