using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSystem : MonoBehaviour
{
    public InputField input;

    public void LoadScene()
    {
        string stage = "0";
        if (System.Convert.ToInt32(input.text) < 10)
            stage += input.text;
        else
            stage = input.text;

        SceneManager.LoadScene(stage);
    }
}
