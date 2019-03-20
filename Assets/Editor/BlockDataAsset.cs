using UnityEngine;
using UnityEditor;

public class BlockDataAsset : ScriptableObject
{
    [MenuItem("Assets/Create/Block")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<BlockData>();
    }
}