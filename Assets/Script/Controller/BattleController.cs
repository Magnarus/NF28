using UnityEngine;
using System.Collections.Generic;

public class BattleController : StateMachine
{
    public CameraRig cameraRig; // Caméra suit l'action
    public Board board; // Référence sur le board
    public LevelData levelData; // Board généré avec le BoardGenerator et sauvegardé
    public Transform tileSelectionIndicator; // Marqueur de la case selectionnée
    public Point pos;

    public List<Creature> creatures;

    public GameObject heroPrefab; // Quand il y aura des rois
    public Creature currentUnit; // Unité selectionnée en ce moment
    public PhysicTile currentTile // Case actuelle
    {
        get { return board.tiles[pos]; }
    }

    void Start()
    {
        ChangeState<InitBattleState>();
    }
}