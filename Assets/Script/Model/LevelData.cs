using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelData : ScriptableObject
{
    public Dictionary<Vector3, string> tiles = new Dictionary<Vector3, string>();
}