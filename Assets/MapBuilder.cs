using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(IsoObject))]
public class MapBuilder : MonoBehaviour {

    int[] size = new int[2];
    System.Random rng = new System.Random();

	void Start ()
    {
        size[0] = (int)System.Math.Round(this.GetComponent<IsoObject>().Size[0]);
        size[1] = (int)System.Math.Round(this.GetComponent<IsoObject>().Size[1]);
        for (int i = 0; i < size[0]; ++i)
        {
            for (int j = 0; j < size[1]; ++j)
            {
                var primitive = GameObject.Instantiate(GameObject.Find("SpriteObject"));
                primitive.transform.parent = this.transform;
                var iso = primitive.GetComponent<IsoObject>();
                var sprite = primitive.GetComponent<SpriteRenderer>();
                iso.Size = new Vector3(1, 1, 0.5f);
                iso.Position = new Vector3(i - size[0] / 2f, j - size[1] / 2f, 0);
                //sprite.sprite = Resources.Load("altlas4_" + rng.Next(0, 23), typeof(Sprite)) as Sprite;

                primitive.GetComponent<inactive>().active = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
