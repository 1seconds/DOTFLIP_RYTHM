using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightManager : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Vector3[] desPos;
    private Vector3 desNormalVec;

    Coroutine lightCor;
    private float time_;
    private Vector3 initPos;
    private Vector3 startPos;
    private GameObject player;

    public void CoroutineStop()
    {
        if(lightCor != null)
            StopCoroutine(lightCor);

        StartCoroutine(DetectPlayer());
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        startPos = gameObject.transform.position;

        lightCor = StartCoroutine(Moving(0));
    }

    IEnumerator Moving(int index)
    {
        while (true)
        {
            desNormalVec = desPos[index] - gameObject.transform.localPosition;
            desNormalVec.Normalize();
            gameObject.transform.Translate(desNormalVec * speed);
            yield return new WaitForEndOfFrame();
            
            if (Vector3.Distance(desPos[index], gameObject.transform.localPosition) < 0.1f)
                break; 
        }

        yield return new WaitForSeconds(waitTime);

        if (index < desPos.Length - 1)
        {
            lightCor = StartCoroutine(Moving(index += 1));
        }
            
        else
        {
            while (true)
            {
                desNormalVec = desPos[0] - gameObject.transform.localPosition;
                desNormalVec.Normalize();
                gameObject.transform.Translate(desNormalVec * speed);
                yield return new WaitForEndOfFrame();

                if (Vector3.Distance(desPos[0], gameObject.transform.localPosition) < 0.1f)
                    break;
            }
            index = 0;
            lightCor = StartCoroutine(Moving(0));
        }
    }

    IEnumerator DetectPlayer()
    {
        initPos = gameObject.transform.localPosition;
        time_ = 0;
        while (true)
        {
            time_ += Time.deltaTime;
            gameObject.transform.localPosition = Vector3.Lerp(initPos, player.transform.localPosition + new Vector3(7,2.8f,0), time_);
            yield return new WaitForEndOfFrame();

            if (time_ > 1f)
                break;
        }
        
        yield return new WaitForSeconds(0.2f);
        time_ = 0;
        initPos = gameObject.transform.localPosition;
        while (true)
        {
            time_ += Time.deltaTime;
            gameObject.transform.localPosition = Vector3.Lerp(initPos,new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 3.0f), time_ * 2);
            yield return new WaitForEndOfFrame();

            if (time_ > 0.5f)
                break;
        }

        gameObject.transform.position = startPos;
        lightCor = StartCoroutine(Moving(0));
    }
}
