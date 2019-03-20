using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmos : MonoBehaviour
{
    public GameObject cosmosMisaile;
    private GameObject misailePrefab;
    public float waitingShootTime;
    private float time_;
    private int cnt = 0;

    private void Start()
    {
        StartCoroutine(ShootMisaile());
    }

    IEnumerator ShootMisaile()
    {
        for(int i =0; i < 4;i++)
        {
            misailePrefab = Instantiate(cosmosMisaile);
            misailePrefab.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0); 
            misailePrefab.transform.eulerAngles = new Vector3(0, 0, gameObject.transform.eulerAngles.z + (90 * i));
            misailePrefab.transform.parent = GameObject.FindWithTag("GameManager").transform.GetChild(0).transform;
        }

        time_ = 0;
        while (true)
        {
            time_ += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            gameObject.transform.localEulerAngles = Vector3.Lerp(new Vector3(0,0,cnt * 90), new Vector3(0,0, (cnt+1) * 90), time_);
            if (time_ > 1)
                break;
        }
        cnt += 1;
        yield return new WaitForSeconds(1);
        StartCoroutine(ShootMisaile());
    }
}
