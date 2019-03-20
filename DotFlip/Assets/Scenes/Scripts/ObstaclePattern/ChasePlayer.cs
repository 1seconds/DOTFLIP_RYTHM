using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    private GameObject player;
    private float time_;
    private RaycastHit2D hit;
    private bool isChaseOn = false;
    public float speed;
    private Vector3 playerTmpPos;
    private Direct savedTmpDirect;
    private Direct currentDirect = Direct.DOWN;
    private int savedAngleZ;
    private Vector3 savedTmpVec;
    
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    IEnumerator Detect()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Run());
    }

    IEnumerator SpinCorner()
    {
        time_ = 0;
        savedAngleZ = (int)gameObject.transform.eulerAngles.z;
        
        while (true)
        {
            switch(savedTmpDirect)
            {
                case Direct.RIGHT:
                    gameObject.transform.eulerAngles = Vector3.Lerp(new Vector3(0,0, savedAngleZ), new Vector3(0,0, savedAngleZ) + new Vector3(0, 0, 90), time_ / 1);
                    break;
                case Direct.LEFT:
                    gameObject.transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, savedAngleZ), new Vector3(0, 0, savedAngleZ) + new Vector3(0, 0, -90), time_ / 1);
                    break;
            }
            time_ += Time.deltaTime;
            if (time_ > 1.1f)
                break;
            yield return new WaitForEndOfFrame();
        }

        savedAngleZ = (int)gameObject.transform.eulerAngles.z;
        Debug.Log(savedAngleZ);
        switch (savedAngleZ)
        {
            case 0:
                currentDirect = Direct.DOWN;
                break;
            case 90:
                currentDirect = Direct.RIGHT;
                break;
            case 180:
                currentDirect = Direct.UP;
                break;
            case 270:
                currentDirect = Direct.LEFT;
                break;
        }

        isChaseOn = false;
    }

    private IEnumerator Run()
    {
        savedTmpVec = (playerTmpPos - gameObject.transform.position).normalized;
        Debug.Log(savedTmpVec);
        while (true)
        {
            gameObject.transform.position += (playerTmpPos - gameObject.transform.position).normalized * speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if(savedTmpVec != (playerTmpPos - gameObject.transform.position).normalized)
                break;
        }
        if (currentDirect.Equals(savedTmpDirect))
        {
            switch (player.GetComponent<PlayerMove>().currentDirect)
            {
                case Direct.DOWN:
                    savedTmpDirect = Direct.DOWN;
                    playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    break;
                case Direct.UP:
                    savedTmpDirect = Direct.UP;
                    playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    break;
                case Direct.RIGHT:
                    savedTmpDirect = Direct.RIGHT;
                    if (currentDirect.Equals(Direct.RIGHT))
                    {
                        playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    }
                    else if (currentDirect.Equals(Direct.DOWN))
                    {
                        playerTmpPos = new Vector3(gameObject.transform.position.x, player.transform.position.y, 0);
                    }
                    break;
                case Direct.LEFT:
                    savedTmpDirect = Direct.LEFT;
                    playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    break;
            }
            StartCoroutine(Run());
        }
        else
            StartCoroutine(SpinCorner());
    }

    private void Update()
    {
        hit = Physics2D.Raycast(gameObject.transform.position + (-gameObject.transform.up)* 0.65f, -gameObject.transform.up);
        Debug.DrawRay(gameObject.transform.position + (-gameObject.transform.up) * 0.65f, -gameObject.transform.up, Color.black);

        if (hit.collider == null)
            return;

        if(hit.collider.gameObject.CompareTag("Player") && !isChaseOn)
        {
            isChaseOn = true;
            switch (player.GetComponent<PlayerMove>().currentDirect)
            {
                case Direct.DOWN:
                    savedTmpDirect = Direct.DOWN;
                    playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    break;
                case Direct.UP:
                    savedTmpDirect = Direct.UP;
                    playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    break;
                case Direct.RIGHT:
                    savedTmpDirect = Direct.RIGHT;
                    if (currentDirect.Equals(Direct.RIGHT))
                    {
                        playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                        Debug.Log(playerTmpPos);
                    }
                    else if(currentDirect.Equals(Direct.DOWN))
                    {
                        playerTmpPos = new Vector3(gameObject.transform.position.x, player.transform.position.y, 0);
                    }
                    break;
                case Direct.LEFT:
                    savedTmpDirect = Direct.LEFT;
                    playerTmpPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                    break;
            }
            StartCoroutine(Detect());
        }
    }
}
