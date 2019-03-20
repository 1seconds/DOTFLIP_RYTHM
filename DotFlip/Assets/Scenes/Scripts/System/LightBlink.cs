using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    private float time_;
    private void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        time_ = 0;
        while(true)
        {
            time_ += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(0.98f, 0.98f, 0.98f), new Vector3(1.02f, 1.02f, 1.02f), time_);
            
            yield return new WaitForEndOfFrame();

            if (time_ > 1.0f)
                break;
        }

        time_ = 0;
        while (true)
        {
            time_ += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(1.02f, 1.02f, 1.02f), new Vector3(0.98f, 0.98f, 0.98f), time_);

            yield return new WaitForEndOfFrame();

            if (time_ > 1.0f)
                break;
        }
        StartCoroutine(Blink());
    }
}
