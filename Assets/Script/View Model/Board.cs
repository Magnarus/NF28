using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Classe qui est capable de générer un board ( = Des cubes dans la scène unity) à partir d'un objet LevelData
 * */
public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab; // Prefab d'une case normale
    [SerializeField]
    GameObject tilePrefabWater; // Prefab d'une case 'eau'
    [SerializeField]
    GameObject tilePrefabBoue; // Prefab d'une case 'boue'
    public Dictionary<Point, PhysicTile> tiles = new Dictionary<Point, PhysicTile>(); // Liste de toutes les cases du board


    // Parcours l'asset LevelData et crée un board
    public void LoadBoardFromData(LevelData data)
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