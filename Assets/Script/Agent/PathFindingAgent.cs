﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathFindingAgent : MonoBehaviour {
    
    static Board b;

    static PathFindingAgent mInstance;

    public static PathFindingAgent Instance
    {
        get
        {
            if (mInstance == null)
            {
                Debug.Log(b);
                GameObject go = new GameObject();
                mInstance = go.AddComponent<PathFindingAgent>();
            }
            return mInstance;
        }
    }

    // Tableau qui correspond aux directions pour la méthode toEuler de ExtensionsDirection = permet la rotation des personnages
    // également utile pour le pathfinding puisque pour une case X données on a 4 cases adjacentes
    Point[] dirs = new Point[4] { new Point(0, 1), new Point(0, -1), new Point(1, 0), new Point(-1, 0) };

    // Use this for initialization
    void Start () {
        BattleController bat = GetComponent<BattleController>();
        b = bat.board;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /**
  * Fonction de recherche des chemins disponibles.
  * start : fonction qui donne le point de départ de la recherche
  * addTile : fonctionne qui détermine la possibilité d'emprunter un chemin
  * */
    public List<PhysicTile> Search(PhysicTile start, string type, Func<PhysicTile, PhysicTile, bool> addTile)
    {
        // Liste des cases sur laquelle on va pouvoir se déplacer
        List<PhysicTile> retValue = new List<PhysicTile>();
        retValue.Add(start);

        // Nettoyage du board
        ClearBoardPathData();
        //
        Queue<PhysicTile> toCheck = new Queue<PhysicTile>();
        Queue<PhysicTile> check = new Queue<PhysicTile>();

        // Initialisation de la recherche
        start.distance = 0;
        check.Enqueue(start);

        // Parcours et vérification de toutes les cases tant qu'on a plus de cases à évaluer
        while (check.Count > 0)
        {
            PhysicTile t = check.Dequeue();

            for (int i = 0; i < 4; ++i)
            {
                PhysicTile next = null;
                if (b.tiles.ContainsKey(t.pos + dirs[i])) // Il a une case qui existe dans cette zone
                {
                    next = b.tiles[t.pos + dirs[i]];
                }

                // On vérifie que cette case existe/qu'on ajoute le chemin le plus opti
                if (next == null || next.distance <= t.distance + 1)
                    continue;

                // On ajoute la case au pathfinding
                if (addTile(t, next))
                {
                    if (type == "foot")
                    { // On prend en considération les problèmes de terrains
                        next.distance = t.distance + t.descriptor.WalkPenality.value;
                    }
                    else //TODO: appliquer les modifier pour cheval/volant (volant: difficile de voler par temps froid, portance toussa et cheval + marai = fossile)
                    {
                        next.distance = t.distance + 1; break;
                    }
                    next.prev = t;
                    toCheck.Enqueue(next);
                    // Elle a passée toutes les conditions = on peut ajouter à la liste des cases OK
                    retValue.Add(next);
                }

                if (check.Count == 0)
                    SwapReference(ref check, ref toCheck);
            }
        }

        return retValue;
    }

    /*
     *  Fonction pour swap les références deux listes #NotSexy
     * */
    void SwapReference(ref Queue<PhysicTile> a, ref Queue<PhysicTile> b)
    {
        Queue<PhysicTile> temp = a;
        a = b;
        b = temp;
    }


    /**
     *  Fonction pour réinitialiser les données
     * */
    void ClearBoardPathData()
    {
        Dictionary<Point,PhysicTile> tiles = b.tiles;
        foreach (PhysicTile t in tiles.Values)
        {
            // Réinitialisation des champs de la case
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }


}
