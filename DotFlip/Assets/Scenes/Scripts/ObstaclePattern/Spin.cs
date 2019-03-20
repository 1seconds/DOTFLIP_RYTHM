using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public GameObject switchObj;            //스위치 - 없을경우 null
    private Switch switchScript;            //스위치 스크립트
    private bool isSwitchNone;              //스위치가 있는지 여부 판단

    public float speed;                     //오브젝트 속도
    public ClockDirect clockDirect;

    private void Start()
    {
        if (switchObj == null)
            isSwitchNone = true;

        else
        {
            switchScript = switchObj.GetComponent<Switch>();
            isSwitchNone = false;
        }
        StartCoroutine(Working(clockDirect));
    }

    private void Awake()
    {
        if (switchObj != null)
        {
            GameSystem.switchContainObjectsStack.Push(gameObject);
        }
    }

    IEnumerator Working(ClockDirect currentDirect)
    {
        while (true)
        {
            //스위치가있으면
            if (!isSwitchNone)
            {
                if (!switchScript.switchOn)
                    break;
            }

            switch (currentDirect)
            {
                case ClockDirect.ANTICLOCKWISE:
                    gameObject.transform.eulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
                    
                    break;
                case ClockDirect.CLOCKWISE:
                    gameObject.transform.eulerAngles += new Vector3(0, 0, -speed * Time.deltaTime);
                    break;
            }

            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (switchScript.switchOn)
                break;
        }
        StartCoroutine(Working(currentDirect));
    }
}
