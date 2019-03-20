using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (GameObject.FindWithTag("GameManager").GetComponent<GameSystem>().currentGameState.Equals(GameState.FAIL))
                return;

            GameObject.FindWithTag("GameManager").GetComponent<GameSystem>().GameMiss();
            obj.GetComponent<PlayerMove>().currentDirect = Direct.HOLD;
        }
            
    }
}
