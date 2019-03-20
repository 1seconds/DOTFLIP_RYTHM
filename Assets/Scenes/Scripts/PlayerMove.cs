using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Direct currentDirect;
    public float speed;
    private bool isEnterTrigger = false;
    private float time_;
    private IEnumerator speedUpCor;
    private IEnumerator speedDownCor;
    private GameSystem gameSystem;

    private void Start()
    {
        gameSystem = GameObject.FindWithTag("GameManager").GetComponent<GameSystem>();
    }

    public void SpeedDown()
    {
        if (!isEnterTrigger)
        {
            speedDownCor = SpeedControl(5, 2.5f);
            StartCoroutine(speedDownCor);
        }
            
        else
        {
            if (speedUpCor != null)
                StopCoroutine(speedUpCor);
            speedDownCor = SpeedControl(5, 2.5f);
            StartCoroutine(speedDownCor);
        }
    }
    public void SpeedUp()
    {
        if(!isEnterTrigger)
        {
            speedUpCor = SpeedControl(5, 10);
            StartCoroutine(speedUpCor);
        }
            
        else
        {
            if(speedDownCor != null)
                StopCoroutine(speedDownCor);
            speedUpCor = SpeedControl(5, 10);
            StartCoroutine(speedUpCor);
        }
    }

    IEnumerator SpeedControl(float speedOrigin, float speedAdapt)
    {
        time_ = 0;
        isEnterTrigger = true;
        
        while(true)
        {
            speed = speedAdapt;
            time_ += Time.deltaTime;
            if (time_ > 1f)
                break;
            yield return new WaitForEndOfFrame();
        }

        speed = speedOrigin;
        isEnterTrigger = false;
    }

    private void Update()
    {
        if (gameSystem.currentGameState.Equals(GameState.READY))
            return;
        switch (currentDirect)
        {
            case Direct.HOLD:
                break;
            case Direct.RIGHT:
                gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
                break;
            case Direct.LEFT:
                gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case Direct.UP:
                gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
                break;
            case Direct.DOWN:
                gameObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;
        }
    }
}
