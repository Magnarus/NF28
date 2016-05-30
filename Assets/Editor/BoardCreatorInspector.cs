using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor {


    // Retourne l'élément sélectionné en ce moment
    public BoardCreator current
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    // Interaction avec la GUI
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Clear"))
            current.Clear();
        if (GUILayout.Button("Grow"))
            current.Grow();
        if (GUILayout.Button("GrowWater"))
            current.GrowWater();
        if (GUILayout.Button("GrowBoue"))
            current.GrowBoue();
        if (GUILayout.Button("Shrink"))
            current.Shrink();
        if (GUILayout.Button("Grow Area"))
            current.GrowArea();
        if (GUILayout.Button("Shrink Area"))
            current.ShrinkArea();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();

        if (GUI.changed)
            current.UpdateMarker();
    }

}
