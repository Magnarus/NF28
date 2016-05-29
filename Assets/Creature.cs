using UnityEngine;
using System.Collections;
using Descriptors;

[RequireComponent(typeof(CreatureDescriptor))]
public class Creature : MonoBehaviour {

    Vector2 Destination;
    Vector2 Position
    {
        get
        {
            return this.GetComponent<IsoObject>().Position;
        }
        set
        {
            //TODO: terrain.height à la place de 0
            this.GetComponent<IsoObject>().Position = new Vector3(value.x, value.y, 0);
        }
    }

	// Use this for initialization
	void Start () {
        Destination = Position;
        //MoveTo(5, 5);
	}
	
	// Update is called once per frame
	void Update () {
        //Animate movement
        if (Destination != Position)
        {
            var dist = (Destination - Position);
           
            if (dist.magnitude < Mathf.Sqrt(2) / 4)
            {
                Position = Destination;
            }
            else
            {
                Position += dist.normalized / 12;
            }
            
        }
	}

    void MoveTo(int x, int y)
    {
        Destination = new Vector2(x, y);
    }
}
