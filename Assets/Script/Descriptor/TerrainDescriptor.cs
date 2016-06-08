using UnityEngine;
using System.Collections;
using Game;

public class TerrainDescriptor : Descriptor {
    [SimpleGameValue]
    [SerializeField]
    public IntGameValue WalkPenality;
    [SimpleGameValue]
    [SerializeField]
    public IntGameValue FlyPenality;
    [SimpleGameValue]
    [SerializeField]
    public IntGameValue HorsePenality;
    [SimpleGameValue]
    [SerializeField]
    public StringGameValue Type;

    

    public TerrainDescriptor() : base()
    {
        WalkPenality = new IntGameValue();
        FlyPenality = new IntGameValue();
        HorsePenality = new IntGameValue();
        Type = Name;
    }
}
