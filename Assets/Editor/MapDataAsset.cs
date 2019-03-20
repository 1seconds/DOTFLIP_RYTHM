using UnityEngine;
using UnityEditor;

public class MapDataAsset : ScriptableObject
{
    [MenuItem("Example/Create ExampleAsset")]
    static void CreateExampleAsset()
    {
        var exampleAsset = CreateInstance<MapDataAsset>();

        AssetDatabase.CreateAsset(exampleAsset, "Assets/Editor/ExampleAsset.asset");
        AssetDatabase.Refresh();
    }
}