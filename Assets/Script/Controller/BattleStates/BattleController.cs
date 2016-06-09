using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BattleController : StateMachine
{
    public CameraRig cameraRig; // Caméra suit l'action
    public Board board; // Référence sur le board
    public LevelData levelData; // Board généré avec le BoardGenerator et sauvegardé
    public Transform tileSelectionIndicator; // Marqueur de la case selectionnée
    public Point pos;

    public int teamSize; // Taille d'origine de l'armée
    public List<Creature> creaturesJ1; // Créatures du joueur 1
    public List<Creature> creaturesJ2; // Créatures du joueur 2

    public Turn turn = new Turn();

    public IEnumerator round;

    public GameObject heroPrefab; // Héros (= Roi)
    public GameObject warriorPrefab; // Guerrier
    public GameObject archerPrefab; // Archer


    public PhysicTile currentTile // Case actuelle
    {
        get { return board.tiles[pos]; }
    }

    void Start()
    {
        turn.owner = this;
        ChangeState<InitBattleState>();
    }
}