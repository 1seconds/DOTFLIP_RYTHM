using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmosMisaile : MonoBehaviour
{
    private float speed = 2.5f;
    private float time_ = 1.0f;
    private float currentTime = 0;

    private void Update()
    {
        gameObject.transform.Translate(Vector2.up * speed * Time.deltaTime);
        currentTime += Time.deltaTime;

        if (time_ < currentTime)
            Destroy(gameObject);
    }
}
