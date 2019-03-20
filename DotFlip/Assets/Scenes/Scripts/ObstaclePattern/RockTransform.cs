using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTransform : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.eulerAngles = new Vector3(Random.Range(0, 4) * 90, Random.Range(0, 4) * 90, Random.Range(0, 4) * 90);
    }
}
