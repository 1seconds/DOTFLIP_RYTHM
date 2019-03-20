using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbPlayer : MonoBehaviour
{
    private bool isGrabOn = false;

    private void OnTriggerExit(Collider obj)
    {
        if(obj.CompareTag("Player"))
        {
            isGrabOn = false;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider obj)
    {
        if (obj.CompareTag("Player") && !isGrabOn)
        {
            if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < 0.5f)
            {
                obj.transform.position = gameObject.transform.position + new Vector3(-0.15f, 0, 0);
                obj.GetComponent<PlayerMove>().currentDirect = Direct.HOLD;
                GameObject.FindWithTag("GameManager").GetComponent<UISystem>().DownSideCanvasOn();
                GameObject.FindWithTag("GameManager").GetComponent<GameSystem>().currentGameState = GameState.READY;
                isGrabOn = true;
            }
        }
    }
}
