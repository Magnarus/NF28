﻿using UnityEngine;
using System.Collections;
using Game;

public class TerrainDescriptor : Descriptor {
    [SimpleGameValue]
    public GameValue<bool> Accessible;

    public TerrainDescriptor() : base()
    {
        Accessible = new GameValue<bool>();
    }
}
