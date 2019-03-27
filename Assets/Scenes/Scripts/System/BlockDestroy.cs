using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroy : MonoBehaviour
{
    private float power = 10;
    private float time_;

    private float restX;    //나머지
    private float restY;
    private int modX;       //몫
    private int modY;
    private Vector3 tmpVector;
    private CameraSystem cameraSystem;

    public bool isDestroyClick = false;

    private int randomValue(int min, int max)
    {
        return Random.Range(min, max);
    }

    private void Start()
    {
        cameraSystem = GameObject.FindWithTag("GameManager").GetComponent<CameraSystem>();
        //gameObject.GetComponent<Rigidbody>()
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3((randomValue(0, 200) - 100), (randomValue(0, 25)), 0) * power);
        

        Destroy(gameObject.GetComponent<UIDrag>());

        if (isDestroyClick)
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
                    GameSystem.TileObject(modX - 1, modY - 1, true);
                }
                else if (restY >= 32)
                {
                    GameSystem.TileObject(modX - 1, modY, true);
                }
            }
            else if (restX >= 32)
            {
                if (restY < 32)
                {
                    GameSystem.TileObject(modX, modY - 1, true);
                }
                else if (restY >= 32)
                {

                    GameSystem.TileObject(modX, modY, true);
                }
            }
        }
        else
            return;

        
    }



    private void Update()
    {
        time_ += Time.deltaTime;
        if (time_ > 1.0f)
            Destroy(gameObject);
    }
}
