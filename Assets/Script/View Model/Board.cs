using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Classe qui est capable de générer un board ( = De cube dans la scène unity) à partir d'un objet LevelData
 * */
public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;
    [SerializeField]
    GameObject tilePrefabWater;
    [SerializeField]
    GameObject tilePrefabBoue;
    public Dictionary<Point, PhysicTile> tiles = new Dictionary<Point, PhysicTile>();

    public void Load(LevelData data)
    {
        for (int i = 0; i < data.tiles.Count; ++i)
        {
            GameObject instance;

            if (data.tiles[i].type == "water")
            {
                // Terrain composé d'eau
                instance = Instantiate(tilePrefabWater) as GameObject;

            } else if(data.tiles[i].type == "boue")
            {
                // Terrain composé de boue
                instance = Instantiate(tilePrefabBoue) as GameObject;
            } else
            {
                // Terrain par défaut
                instance = Instantiate(tilePrefab) as GameObject;
            }

            PhysicTile t = instance.GetComponent<PhysicTile>();
            t.Load(data.tiles[i].pos);
            tiles.Add(t.pos, t);
        }
    }
}