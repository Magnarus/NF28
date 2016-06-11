using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Descriptors;

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
    public GameObject canvasRight; 

	public Text localPlayerLabel;
	public Text remotePlayerLabel;
	public Text gameStateLabel;
	public MatchController matchController;


    public PhysicTile currentTile // Case actuelle
    {
        get { return board.tiles[pos]; }
    }

    void Start()
    {
		this.AddObserver (OnPlayerStartedLocal, PlayerController.StartedLocal);
		this.AddObserver (OnSyncDamage, PlayerController.LifeChanged);
		this.AddObserver (OnPlayerStarted, PlayerController.Started);
		this.AddObserver (OnSyncPosition, PlayerController.PositionChanged);
	
        turn.owner = this;
        ChangeState<InitBattleState>();
    }

	void OnEnable ()
	{
		this.AddObserver(OnCoinToss, PlayerController.CoinToss);
	}

	void OnDisable ()
	{
		
		this.RemoveObserver(OnCoinToss, PlayerController.CoinToss);
	}

	void OnPlayerStartedLocal(object sender, object args) {
		tileSelectionIndicator = ((PlayerController)sender).gameObject.transform;
	}
		
	void OnPlayerStarted(object sender, object args) {
		tileSelectionIndicator = ((PlayerController)sender).gameObject.transform;
	}

	void OnCoinToss (object sender, object args)
	{
		bool coinToss = (bool)args;
	}

	void OnSyncDamage(object sender, object args) {
		string[] data = (string[]) args;
		Point p = new Point (int.Parse(data [0]), int.Parse(data [1]));
		Creature current = board.tiles [p].contentTile.GetComponent<Creature>();
		if (current != null) {
			current.GetComponent<CreatureDescriptor> ().HP.CurrentValue = float.Parse(data [2]);
		}
	}


	void OnSyncPosition(object sender, object args) {
		PlayerController s = (PlayerController)sender;
		Debug.Log (s.playerID + " " + matchController.localPlayer.playerID);
		if (s.playerID != matchController.localPlayer.playerID) {
			Point[] positions = (Point[])args;
			int size = positions.Length;
			PhysicTile current = board.tiles [positions [0]];

			for (int i = 1; i < size; i++) {
				board.tiles [positions [i]].prev = current;
				current = board.tiles [positions [i]];
			}
			Debug.Log ("dernière position" + board.tiles [positions [size - 1]].pos);
			Creature creature = board.tiles[positions[size-1]].contentTile.GetComponent<Creature> ();
			launchCoroutine (creature, board.tiles [positions [0]]);
		}
	}

	void launchCoroutine(Creature c, PhysicTile tile) {
		Debug.Log ("Je suis lààààààààà");
		Movement m = c.GetComponent<Movement>();
		StartCoroutine(m.Traverse(tile));
		c.Match ();
	}

}