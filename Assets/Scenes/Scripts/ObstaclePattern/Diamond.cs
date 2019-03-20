using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public float speed;
    private float rot;

    private void Start()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameSystem>().diamond = GameObject.FindGameObjectsWithTag("Diamond");
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        rot += speed * Time.deltaTime;
        gameObject.transform.eulerAngles = new Vector3(0, rot, 0);
    }
}
