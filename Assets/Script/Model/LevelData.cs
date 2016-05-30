using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class InputOutputData
{
    public Vector3 pos;
    public string type;

    public InputOutputData(Vector3 v, string t)
    {
        type = t;
        pos = v;

    }
}


public class LevelData : ScriptableObject
{
    public List<InputOutputData> tiles = new List<InputOutputData>();
}