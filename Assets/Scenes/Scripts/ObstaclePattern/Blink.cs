using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public GameObject switchObj;            //스위치 - 없을경우 null
    public Sprite[] digdaImg = new Sprite[3];
    public bool isUpStart;                 //위에서 시작 / 아래서 시작
    private Switch switchScript;            //스위치 스크립트
    private bool isSwitchNone;              //스위치가 있는지 여부 판단

    public float waitingTime;               //이미지가 바뀔때까지의 시간

    private void Start()
    {
        if (switchObj == null)
            isSwitchNone = true;

        else
        {
            switchScript = switchObj.GetComponent<Switch>();
            isSwitchNone = false;
        }
        StartCoroutine(Working());
    }

    private void Awake()
    {
        if (switchObj != null)
        {
            GameSystem.switchContainObjectsStack.Push(gameObject);
        }
    }

    private IEnumerator Working()
    {
        //스위치가있으면
        if (!isSwitchNone)
        {

            if (isUpStart)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = digdaImg[2];
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = digdaImg[0];
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            while (true)
            {
                yield return new WaitForEndOfFrame();
                if (switchScript.switchOn)
                    break;
            }
        }

        if (isUpStart)
        {
            for (int i = 2; i > -1; i--)
            {
                if (i != 2)
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;

                gameObject.GetComponent<SpriteRenderer>().sprite = digdaImg[i];
                yield return new WaitForSeconds(waitingTime);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = digdaImg[1];
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(waitingTime);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (i != 2)
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                else
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;

                gameObject.GetComponent<SpriteRenderer>().sprite = digdaImg[i];
                yield return new WaitForSeconds(waitingTime);
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = digdaImg[1];
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(waitingTime);
        }
        StartCoroutine(Working());
    }
}
