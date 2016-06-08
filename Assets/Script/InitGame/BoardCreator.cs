#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic; // = Dictionnary
using UnityEditor; // = Asset database
using System.IO; // = Directory

public class BoardCreator : MonoBehaviour {
    [SerializeField]
    GameObject tileViewPrefab;
    [SerializeField]
    GameObject tileViewPrefabWater;
    [SerializeField]
    GameObject tileViewPrefabBoue; // Todo : Chercher boue en anglais x)
    [SerializeField]
    GameObject tileSelectionIndicatorPrefab;

    // Permet d'avoir une référence sur la position d'un PhysicTile
    Dictionary<Point, PhysicTile> tiles = new Dictionary<Point, PhysicTile>();

    [SerializeField]  int width = 10; // X
    [SerializeField]  int depth = 10; // Z
    [SerializeField]  int height = 8; // Hauteur
    [SerializeField] Point pos;
    [SerializeField] LevelData levelData; //  Recharger des niveaux déjà crées

    Transform marker
    {
        get
        {
            if (_marker == null)
            {
                GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
                _marker = instance.transform;
            }
            return _marker;
        }
    }
    Transform _marker;

    public void GrowArea()
    {
        Rect r = RandomRect();
        GrowRect(r);
    }

    public void ShrinkArea()
    {
        Rect r = RandomRect();
        ShrinkRect(r);
    }


    Rect RandomRect()
    {
        int x = UnityEngine.Random.Range(0, width);
        int y = UnityEngine.Random.Range(0, depth);
        int w = UnityEngine.Random.Range(1, width - x + 1);
        int h = UnityEngine.Random.Range(1, depth - y + 1);
        return new Rect(x, y, w, h);
    }

    void GrowRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                GrowSingle(p, "default");
            }
        }
    }

    void ShrinkRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                ShrinkSingle(p);
            }
        }
    }


    PhysicTile Create(string type)
    {
        GameObject instance;
        if (type == "default")
        {
            instance = Instantiate(tileViewPrefab) as GameObject;
        } else if(type == "water")
        {
            instance = Instantiate(tileViewPrefabWater) as GameObject;
        } else
        {
            instance = Instantiate(tileViewPrefabBoue) as GameObject;
        }
       
        instance.transform.parent = transform;
        return instance.GetComponent<PhysicTile>();
    }

    PhysicTile GetOrCreate(Point p, string type)
    {
        if (tiles.ContainsKey(p))
            return tiles[p];

        PhysicTile t = Create(type);
        t.Load(p, 0);
        tiles.Add(p, t);

        return t;
    }

    void GrowSingle(Point p, string type)
    {
        PhysicTile t = GetOrCreate(p, type);
        if (t.height < height)
            t.Grow();
    }

    void ShrinkSingle(Point p)
    {
        if (!tiles.ContainsKey(p))
            return;

        PhysicTile t = tiles[p];
        t.Shrink();

        if (t.height <= 0)
        {
            tiles.Remove(p);
            DestroyImmediate(t.gameObject);
        }
    }

    public void Grow()
    {
        GrowSingle(pos, "default");
    }

    public void GrowWater()
    {
        GrowSingle(pos, "water");
    }
    public void GrowBoue()
    {
        GrowSingle(pos, "boue");
    }
    public void Shrink()
    {
        ShrinkSingle(pos);
    }

    public void UpdateMarker()
    {
        PhysicTile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? t.center : new Vector3(pos.x, 0, pos.y);
    }

    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<InputOutputData>(tiles.Count);
        Debug.Log(tiles.Count);
        foreach (PhysicTile t in tiles.Values) {
            board.tiles.Add(new InputOutputData(new Vector3(t.pos.x, t.height, t.pos.y), t.type));
        }

        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
    }

    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        tiles.Clear();
    }

    public void Load()
    {
        Clear();
        if (levelData == null)
            return;

        foreach (InputOutputData v in levelData.tiles)
        {
            PhysicTile t = Create(v.type);
            t.Load(v.pos);
            tiles.Add(t.pos, t);
        }
    }

}
#endif