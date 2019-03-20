using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool switchOn = false;
    public GameObject[] lines;
    private Coroutine myCor;
    private float time_;
    public int constValue;

    private void Start()
    {
        if(!switchOn)
        {
            for (int i = 0; i < lines.Length; i++)
                lines[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        else
            myCor = StartCoroutine(ElectronicStart());
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag.Equals("Player") || obj.CompareTag("Auto"))
        {
            if (!switchOn)
            {
                if (myCor != null)
                    StopCoroutine(myCor);

                for (int i = 0; i < lines.Length; i++)
                    lines[i].transform.GetChild(0).gameObject.SetActive(true);

                myCor = StartCoroutine(ElectronicStart());
                switchOn = true;
            }
            else
            {
                StopCor();

                for (int i = 0; i < lines.Length; i++)
                    lines[i].transform.GetChild(0).gameObject.SetActive(false);
                switchOn = false;
            }
        }
    }

    IEnumerator ElectronicStart()
    {

        time_ = 0;
        while (true)
        {
            for(int i =0;i<lines.Length ; i++)
                lines[i].transform.GetChild(0).transform.localPosition = Vector3.Lerp(new Vector3(-0.0111529f, -0.03800342f, 0), new Vector3(0.00947f / constValue, -0.03800342f, 0), time_);
            
            time_ += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (time_ > 1.0f)
                break;
        }
        myCor = StartCoroutine(ElectronicStart());
    }

    public void StopCor()
    {
        if (myCor == null)
            return;

        for (int i = 0; i < lines.Length; i++)
            lines[i].transform.GetChild(0).gameObject.SetActive(false);

        StopCoroutine(myCor);
    }
}
