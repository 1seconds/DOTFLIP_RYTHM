using UnityEngine;
using UnityEditor;

public class StageDataAsset : ScriptableObject
{
    [MenuItem("Assets/Create/Stage")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<StageData>();
    }
}