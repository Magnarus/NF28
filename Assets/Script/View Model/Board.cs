using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

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


    // Tableau qui correspond aux directions pour la méthode toEuler de ExtensionsDirection = permet la rotation des personnages
    // également utile pour le pathfinding puisque pour une case X données on a 4 cases adjacentes
    Point[] dirs = new Point[4] {  new Point(0, 1),  new Point(1, 0), new Point(0, -1), new Point(-1, 0) };

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

            foreach (Transform child in t.gameObject.transform)
            { 
                if (child.name == "Highlight")
                {
                    t.instanceHightlighTile = child.gameObject;
                    break;
                }
            }
            t.instanceHightlighTile.SetActive(false);
            tiles.Add(t.pos, t);
        }
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
        //retValue.Add(start);

        // Nettoyage du board
        ClearBoardPathData();

        // Deux piles l'une pour les cases en train d'être analysés et une autre pour ceux qui vont être analysés
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
                if (tiles.ContainsKey(t.pos + dirs[i]) ) // Il a une case qui existe dans cette zone
                {
                      next = tiles[t.pos + dirs[i]];
                }
                // On vérifie que cette case existe/qu'on ajoute le chemin le plus opti
                if (next == null || next.distance <= t.distance + 1)
                    continue;


                // On ajoute la case au pathfinding
                if (addTile(t, next))
                {
                    if (type == "foot") { // On prend en considération les problèmes de terrains
                       // Debug.Log(t.descriptor.WalkPenality.value);
                        next.distance = t.distance + t.descriptor.WalkPenality.value;
                    }
                    else 
                    {
                        next.distance = t.distance + 1;
                    }  
                    next.prev = t;
                    toCheck.Enqueue(next);
                    // Elle a passée toutes les conditions = on peut ajouter à la liste des cases OK
                    retValue.Add(next);
                }

                if (check.Count == 0)
                {
                    SwapReference(ref check, ref toCheck);
                }
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
        foreach (PhysicTile t in tiles.Values)
        {
            // Réinitialisation des champs de la case
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    /**
     *  Un peu de couleur pour mettre en avant les cases sélectionnables
     * */
    public void SelectedColor(List<PhysicTile> tiles)
    { 
        for (int i = tiles.Count - 1; i >= 0; --i) {
            tiles[i].instanceHightlighTile.SetActive(true);
        }
    }

    /**
     *  Un peu de couleur pour ne pas mettre en avant celles auxquelles ont a pas accès
     * */
    public void NotSelectedColor(List<PhysicTile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            tiles[i].instanceHightlighTile.SetActive(false);
        }
    }

	public List<PhysicTile> GetMaxRange(AgentCreature agent, List<PhysicTile> tiles) {
		// Calcul de la range des cases extrêmes
		List<PhysicTile> returnedList = new List<PhysicTile>(tiles);
		Hashtable table = new Hashtable();

		int xMax = -1; 
		int xMin = 9999;
		// Récupération des tiles extrêmes
		foreach(PhysicTile t in tiles) {
			if (!table.ContainsKey (t.pos.x)) {
				table.Add (t.pos.x, new List<PhysicTile> ());
			} else {
				if (((List<PhysicTile>)table[t.pos.x])[0].pos.y > t.pos.y) { 
					// C'est le plus éloigné sur la ligne à gauche
					((List<PhysicTile>)table[t.pos.x])[0] = t;

				} else if (((List<PhysicTile>)table[t.pos.x])[1].pos.y <= t.pos.y) {
					((List<PhysicTile>)table[t.pos.x])[1] = t;
				}
			}
			if (xMin > t.pos.x)
				xMin = t.pos.x;
			if (xMax < t.pos.x)
				xMax = t.pos.x;
		}
		// On récupère la portée de l'unité
		GameObject ability = agent.CurrentCreature.GetComponentInChildren<AbilityRangeCalculator>().gameObject;
		AbilityRangeCalculator abilityRange = ability.GetComponent<AbilityRangeCalculator>();
		// Calcul de leurs portées
		foreach (int i in table.Keys) {
			List<PhysicTile> tmp = abilityRange.GetTilesInRange (this, ((List<PhysicTile>)table[i])[0]);
			List<PhysicTile> tmpBis = (abilityRange.GetTilesInRange (this, ((List<PhysicTile>)table [i]) [0]));
			tmp.AddRange (tmpBis);
			foreach (PhysicTile t in tmp) {
				if (!returnedList.Contains(t)) {
					returnedList.Add (t);
				}			
			}	
		}

		// Et on ajoute les tiles max et min
		int yMin = ((List<PhysicTile>)table[xMin])[0].pos.y;
		int yMax = ((List<PhysicTile>)table [xMin]) [1].pos.y;
		for (int i = yMin; i < yMax; i++) {
			List<PhysicTile> tmp = abilityRange.GetTilesInRange (this, ((List<PhysicTile>)table[i])[0]);
			foreach (PhysicTile t in tmp) {
				if (!returnedList.Contains(t)) {
					returnedList.Add (t);
				}			
			}	
		}
		// Et on ajoute les tiles max et min
		yMin = ((List<PhysicTile>)table[xMax])[0].pos.y;
		yMax = ((List<PhysicTile>)table [xMax]) [1].pos.y;
		for (int i = yMin; i < yMax; i++) {
			List<PhysicTile> tmp = abilityRange.GetTilesInRange (this, ((List<PhysicTile>)table[i])[0]);
			foreach (PhysicTile t in tmp) {
				if (!returnedList.Contains(t)) {
					returnedList.Add (t);
				}			
			}	
		}
		return returnedList;
	}
}