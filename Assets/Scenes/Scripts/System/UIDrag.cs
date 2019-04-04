using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UISet uiSet;
    public GameObject block;
    private Camera mainCamera;
    private GameObject blockPrefab;
    static public GameObject targetBlock;
    private GameSystem gameSystem;

    private float restX;    //나머지
    private float restY;
    private int modX;       //몫
    private int modY;

    private float intervalValue = 35;
    [HideInInspector] public int order;

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        gameSystem = GameObject.FindWithTag("GameManager").GetComponent<GameSystem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!gameSystem.currentGameState.Equals(GameState.READY))
            return;

        targetBlock = null;
        blockPrefab = Instantiate(block);
        blockPrefab.transform.parent = GameObject.FindWithTag("GameManager").transform.GetChild(1);
        blockPrefab.GetComponent<BlockData>().order = order;
    }

    private void SetTilePos(GameObject obj)
    {
        restX = Input.mousePosition.x % 32;
        restY = Input.mousePosition.y % 32;
        modX = (int)(Input.mousePosition.x / 32);
        modY = (int)(Input.mousePosition.y / 32);

        if (modX < 5)
        {
            restX = 50;
            modX = 4;
        }
        else if (modX > 35)
        {
            restX = 50;
            modX = 35;
        }
        if (modY < 5)
        {
            restY = 50;
            modY = 4;
        }
        else if (modY > 17)
        {
            restY = 50;
            modY = 17;
        }

        if (restX < 32)
        {
            if (restY < 32)
            {
                obj.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(32 * modX, 32 * modY, 0));
            }
            else if (restY >= 32)
            {
                obj.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(32 * modX, 32 * (modY + 1), 0));
            }
        }
        else if (restX >= 32)
        {
            if (restY < 32)
            {
                obj.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(32 * (modX + 1), 32 * modY, 0));
            }
            else if (restY >= 32)
            {
                obj.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(32 * (modX + 1), 32 * (modY + 1), 0));
            }
        }
        obj.transform.position += new Vector3(0, 0, 6);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetTilePos(blockPrefab);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (restX < 32)
        {
            if (restY < 32)
            {
                if (!GameSystem.tileObjectState[modX -2, modY-2])
                    blockPrefab.GetComponent<BlockDestroy>().enabled = true;  
                else
                    GameSystem.TileObject(modX - 1, modY - 1, false);   
            }
            else if (restY >= 32)
            {
                if (!GameSystem.tileObjectState[modX - 2, modY - 1])
                    blockPrefab.GetComponent<BlockDestroy>().enabled = true;
                else
                    GameSystem.TileObject(modX - 1, modY, false);
            }
        }
        else if (restX >= 32)
        {
            if (restY < 32)
            {
                if (!GameSystem.tileObjectState[modX - 1, modY-2])
                    blockPrefab.GetComponent<BlockDestroy>().enabled = true;
                else
                    GameSystem.TileObject(modX, modY - 1, false);
            }
            else if (restY >= 32)
            {
                if (!GameSystem.tileObjectState[modX - 1, modY-1])
                    blockPrefab.GetComponent<BlockDestroy>().enabled = true;
                else
                    GameSystem.TileObject(modX, modY, false);
            }
        }
        blockPrefab.transform.position += new Vector3(0, 0, 3.7f);
        Destroy(gameObject);
        GameObject.FindWithTag("GameManager").GetComponent<StageSystem>().DownSideUIPoolLeftMove();
    }

    public void OnMouseDown()
    {
        if (!gameSystem.currentGameState.Equals(GameState.READY))
            return;

        if (ItemSystem.paintPrefab == null)
        {
            restX = Input.mousePosition.x % 32;
            restY = Input.mousePosition.y % 32;
            modX = (int)(Input.mousePosition.x / 32);
            modY = (int)(Input.mousePosition.y / 32);

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
            return;
        }
    }

    public void OnMouseUp()
    {
        if (restX < 32)
        {
            if (restY < 32)
            {
                if (!GameSystem.tileObjectState[modX - 2, modY-2])
                    gameObject.GetComponent<BlockDestroy>().enabled = true;
                else
                    GameSystem.TileObject(modX - 1, modY - 1, false);
            }
            else if (restY >= 32)
            {
                if (!GameSystem.tileObjectState[modX - 2, modY -1])
                    gameObject.GetComponent<BlockDestroy>().enabled = true;
                else
                    GameSystem.TileObject(modX - 1, modY, false);
            }
        }
        else if (restX >= 32)
        {
            if (restY < 32)
            {
                if (!GameSystem.tileObjectState[modX - 1, modY-2])
                    gameObject.GetComponent<BlockDestroy>().enabled = true;
                else
                    GameSystem.TileObject(modX, modY - 1, false);
            }
            else if (restY >= 32)
            {
                if (!GameSystem.tileObjectState[modX - 1, modY-1])
                    gameObject.GetComponent<BlockDestroy>().enabled = true;
                else
                    GameSystem.TileObject(modX, modY, false);
            }
        }
        gameObject.transform.position += new Vector3(0, 0, 3.7f);
    }

    public void OnMouseDrag()
    {
        SetTilePos(gameObject);
    }
}