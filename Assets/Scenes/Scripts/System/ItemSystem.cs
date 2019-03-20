using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    static public GameObject paintPrefab;
    public GameObject paint;
    private Camera mainCamera;
    static private bool isPaintingClick;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void UseItem()
    {
        paintPrefab = Instantiate(paint);
        paintPrefab.transform.parent = GameObject.FindWithTag("GameManager").transform.GetChild(1).transform;
    }
    static public void CancelItem()
    {
        Destroy(paintPrefab);
        isPaintingClick = false;
    }

    public void ItemClick()
    {
        if(isPaintingClick)
        {
            isPaintingClick = false;
            CancelItem();
        }
        else
        {
            isPaintingClick = true;
            UseItem();
        }
    }

    private void Update()
    {
        if(isPaintingClick)
            paintPrefab.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
    }
}
