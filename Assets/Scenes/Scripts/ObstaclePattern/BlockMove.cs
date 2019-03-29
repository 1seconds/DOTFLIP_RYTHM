using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public Block currentBlock;

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
