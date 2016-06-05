using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Classe pour les panels d'UI (Stats/Action etc)
[RequireComponent(typeof(LayoutAnchor))]
public class Panel : MonoBehaviour
{

    /** Classe qui permet de gérer la localisation des éléments **/
    [Serializable]
    public class Position
    {
        public string name;
        public TextAnchor myAnchor;
        public TextAnchor parentAnchor;
        public Vector2 offset;

        public Position(string name)
        {
            this.name = name;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor) : this(name)
        {
            this.myAnchor = myAnchor;
            this.parentAnchor = parentAnchor;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset) : this(name, myAnchor, parentAnchor)
        {
            this.offset = offset;
        }
    }


    [SerializeField]
    List<Position> positionList;
    Dictionary<string, Position> positionMap;
    LayoutAnchor anchor;


    /** Position par défaut **/
    void Awake()
    {
        anchor = GetComponent<LayoutAnchor>();
        positionMap = new Dictionary<string, Position>(positionList.Count);
        for (int i = positionList.Count - 1; i >= 0; --i)
            AddPosition(positionList[i]);
    }


    // Position actuelle du Panel
    public Position CurrentPosition { get; private set; }
    // Transition vers une nouvelle position
    public Tweener Transition { get; private set; }
    // Savoir si on est à l'instant en transition
    public bool InTransition { get { return Transition != null; } }

    public Position this[string name]
    {
        get
        {
            if (positionMap.ContainsKey(name))
                return positionMap[name];
            return null;
        }
    }

    /** Manipulation d'éléments : Ajout **/
    public void AddPosition(Position p)
    {
        positionMap[p.name] = p;
    }

    /** Manipulation d'éléments : Suppression **/
    public void RemovePosition(Position p)
    {
        if (positionMap.ContainsKey(p.name))
            positionMap.Remove(p.name);
    }

    /** Gestion du placement des éléments via l'emplacement auquel on souhaite ajouter l'objet **/
    public Tweener SetPosition(string positionName, bool animated)
    {
        return SetPosition(this[positionName], animated);
    }

    public Tweener SetPosition(Position p, bool animated)
    {
        CurrentPosition = p;
        if (CurrentPosition == null)
            return null;

        if (InTransition)
            Transition.easingControl.Stop();

        if (animated)
        {
            Transition = anchor.MoveToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return Transition;
        }
        else
        {
            anchor.SnapToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
            return null;
        }
    }

    void Start()
    {
        if (CurrentPosition == null && positionList.Count > 0)
            SetPosition(positionList[0], false);
    }
}