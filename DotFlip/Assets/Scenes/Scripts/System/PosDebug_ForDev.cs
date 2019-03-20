using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosDebug_ForDev : MonoBehaviour
{
    private GameObject infoPrefab;

	void Start ()
    {
        //Debug.Log(gameObject.transform.position);
        infoPrefab = Instantiate(Resources.Load("ForDev/INFO") as GameObject);
        infoPrefab.transform.parent = GameObject.FindWithTag("Canvas").transform;
        infoPrefab.transform.position = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
        infoPrefab.GetComponentInChildren<Text>().text = "Vector (" + Mathf.Round(gameObject.transform.position.x * 100)*0.01f + ", " + Mathf.Round(gameObject.transform.position.y* 100)*0.01f + ", " + Mathf.Round(gameObject.transform.position.z * 100) * 0.01f + ")";
    }


}
