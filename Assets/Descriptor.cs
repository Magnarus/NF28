using UnityEngine;
using System.Collections;
using Game;

public abstract class Descriptor : MonoBehaviour {
    [SimpleGameValue]
    public GameValue<string> Name;
}
