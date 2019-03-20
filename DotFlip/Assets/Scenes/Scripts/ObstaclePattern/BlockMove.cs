using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public Direct direct;
    public Block currentBlock;

    private Vector3 tmpVector;
    private float restX;    //나머지
    private float restY;
    private int modX;       //몫
    private int modY;
    private CameraSystem cameraSystem;

    private void OnTriggerEnter(Collider obj)
    {
        //if (obj.CompareTag("Player"))
        //{
        //    switch (currentBlock)
        //    {
        //        case Block.SLOW:
        //            obj.GetComponent<PlayerMove>().SpeedDown();
        //            break;
        //        case Block.BOOSTER:
        //            obj.GetComponent<PlayerMove>().SpeedUp();
        //            break;
        //    }
        //}
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            switch(obj.GetComponent<PlayerMove>().currentDirect)
            {
                case Direct.DOWN:
                    if(obj.transform.position.y < gameObject.transform.position.y)
                    {
                        obj.transform.position = gameObject.transform.position;
                        obj.GetComponent<PlayerMove>().currentDirect = direct;
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                    }
                    break;
                case Direct.UP:
                    if (obj.transform.position.y > gameObject.transform.position.y)
                    {
                        obj.transform.position = gameObject.transform.position;
                        obj.GetComponent<PlayerMove>().currentDirect = direct;
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                    }
                    break;
                case Direct.RIGHT:
                    if (obj.transform.position.x > gameObject.transform.position.x)
                    {
                        obj.transform.position = gameObject.transform.position;
                        obj.GetComponent<PlayerMove>().currentDirect = direct;
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                    }
                    break;
                case Direct.LEFT:
                    if (obj.transform.position.x < gameObject.transform.position.x)
                    {
                        obj.transform.position = gameObject.transform.position;
                        obj.GetComponent<PlayerMove>().currentDirect = direct;
                        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
                        gameObject.GetComponent<BoxCollider>().enabled = false;
                    }
                    break;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!UISystem.isDelBtnOn)
            return;
        else
        {
            gameObject.GetComponent<BlockDestroy>().isDestroyClick = true;
            gameObject.GetComponent<BlockDestroy>().enabled = true;
        }
    }
}
