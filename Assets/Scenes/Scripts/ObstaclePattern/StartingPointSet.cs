using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPointSet : MonoBehaviour
{
    private Vector3 tmpVector;
    private float restX;    //나머지
    private float restY;
    private int modX;       //몫
    private int modY;

    void Start()
    {
        tmpVector = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
        restX = tmpVector.x % 32;
        restY = tmpVector.y % 32;
        modX = (int)(tmpVector.x / 32);
        modY = (int)(tmpVector.y / 32);

        if (restX < 32)
        {
            if (restY < 32)
            {
                gameObject.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(32 * modX, 32 * modY, 0));
                GameSystem.TileObject(modX - 1, modY - 1, false);
            }
            else if (restY >= 32)
            {
                gameObject.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(32 * modX, 32 * (modY + 1), 0));
                GameSystem.TileObject(modX - 1, modY, false);
            }
        }
        else if (restX >= 32)
        {
            if (restY < 32)
            {
                gameObject.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(32 * (modX + 1), 32 * modY, 0));
                GameSystem.TileObject(modX, modY - 1, false);
            }
            else if (restY >= 32)
            {
                gameObject.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(32 * (modX + 1), 32 * (modY + 1), 0));
                GameSystem.TileObject(modX, modY, false);
            }
        }
        gameObject.transform.position += new Vector3(0, 0, 10);
    }
}
