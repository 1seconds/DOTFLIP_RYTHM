using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoManager : MonoBehaviour
{
    public Direct currentDirect;
    public float speed;

    private void Update()
    {
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
