using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;
    public GameObject camera_;   

    private Vector3 rightMovedPos = new Vector3(20.6f, 0, -10);
    private Vector3 downMovedPos = new Vector3(0, -11.7f, -10);
    private Vector3 leftMovedPos = new Vector3(-20.6f, 0, -10);
    private Vector3 upMovedPos = new Vector3(0, 11.7f, -10);

    private float time_ = 0;

    private StageSystem stageSystem;

    public void Start()
    {
        stageSystem = gameObject.GetComponent<StageSystem>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if ((player.transform.localPosition.y < -5f && player.transform.localPosition.y > -6.5f))
        {
            player.GetComponent<PlayerMove>().speed = 1.0f;
            time_ += Time.deltaTime;
            camera_.transform.position = Vector3.Lerp(camera_.transform.position, downMovedPos, time_);
        }
        else
        {
            time_ = 0;
            player.GetComponent<PlayerMove>().speed = 5.0f;
        }
    }
}
