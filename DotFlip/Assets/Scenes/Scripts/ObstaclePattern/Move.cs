using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject switchObj;            //스위치 - 없을경우 null
    private Switch switchScript;            //스위치 스크립트
    private bool isSwitchNone;              //스위치가 있는지 여부 판단

    public float speed;                     //오브젝트 속도

    public Direct direct;                   //분류 - 방향
    public float maxValue;                  //최댓값     
    public float minValue;                  //최솟값

    private void Start()
    {
        if (switchObj == null)
            isSwitchNone = true;
        
        else
        {
            switchScript = switchObj.GetComponent<Switch>();
            isSwitchNone = false;
        }


        StartCoroutine(Working(direct));
    }

    private void Awake()
    {
        if (switchObj != null)
        {
            GameSystem.switchContainObjectsStack.Push(gameObject);
        }
    }

    IEnumerator Working(Direct currentDirect)
    {

        while(true)
        {
            //스위치가있으면
            if(!isSwitchNone)
            {
                if (!switchScript.switchOn)
                    break;
            }

            switch (currentDirect)
            {
                case Direct.HOLD:
                    break;
                case Direct.DOWN:
                    if (minValue > gameObject.transform.localPosition.y)
                    {
                        direct = Direct.UP;
                        currentDirect = Direct.UP;
                    }
                    else
                        gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
                    break;
                case Direct.UP:
                    if (maxValue < gameObject.transform.localPosition.y)
                    {
                        direct = Direct.DOWN;
                        currentDirect = Direct.DOWN;
                    }
                    else
                        gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
                    break;
                case Direct.LEFT:
                    if (minValue > gameObject.transform.localPosition.x)
                    {
                        direct = Direct.RIGHT;
                        currentDirect = Direct.RIGHT;
                    }
                    else
                        gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
                    break;
                case Direct.RIGHT:
                    if (maxValue < gameObject.transform.localPosition.x)
                    {
                        direct = Direct.LEFT;
                        currentDirect = Direct.LEFT;
                    }
                    else
                        gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
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
