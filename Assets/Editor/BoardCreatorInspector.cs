#if UNITY_EDITOR
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

        if (GUILayout.Button("Réinitialiser"))
            current.Clear();
        if (GUILayout.Button("Créer herbe"))
            current.Grow();
        if (GUILayout.Button("Créer eau"))
            current.GrowWater();
        if (GUILayout.Button("Créer boue"))
            current.GrowBoue();
        if (GUILayout.Button("Diminuer"))
            current.Shrink();
        if (GUILayout.Button("Générer zone"))
            current.GrowArea();
        if (GUILayout.Button("Diminuer zone"))
            current.ShrinkArea();
        if (GUILayout.Button("Save"))
            current.Save();
        if (GUILayout.Button("Load"))
            current.Load();

        if (GUI.changed)
            current.UpdateMarker();
    }

}
#endif