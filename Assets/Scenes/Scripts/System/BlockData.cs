using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData : MonoBehaviour
{
    public int order;       //순서
    public float speed;     //속도

    private void Start()
    {
        if(gameObject.name.Contains("1A_Block"))
        {
            speed = 1f;
        }
        else if(gameObject.name.Contains("1B_Block"))
        {
            speed = 1f;
        }
        else if (gameObject.name.Contains("2A_Block"))
        {
            speed = 2f;
        }
        else if (gameObject.name.Contains("2B_Block"))
        {
            speed = 2f;
        }
        else if (gameObject.name.Contains("4A_Block"))
        {
            speed = 4f;
        }
        else if (gameObject.name.Contains("4B_Block"))
        {
            speed = 4f;
        }
        else if (gameObject.name.Contains("8A_Block"))
        {
            speed = 8f;
        }
        else if (gameObject.name.Contains("8B_Block"))
        {
            speed = 8f;
        }
        else if (gameObject.name.Contains("16A_Block"))
        {
            speed = 16f;
        }
        else if (gameObject.name.Contains("16B_Block"))
        {
            speed = 16f;
        }

        speed *= GameObject.FindWithTag("GameManager").GetComponent<StageSystem>().currentStageInfo.speed;
    }
}
