using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

	public string gameType = "JcJ";

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
		this.AddObserver (OnCharacterDeath, PlayerController.CharacterDied);	
		this.AddObserver (OnVictory, PlayerController.Victory);	
		this.AddObserver (OnMatchReady, MatchController.MatchReady);
		DontDestroyOnLoad (this.gameObject);

    }

	void OnEnable ()
	{
	}

	void OnDisable ()
	{
		
	}

	void OnMatchReady(object sender, object args) {
		GameObject parent = GameObject.Find("Canvas");
		GameObject top=null;
		foreach(Transform child in parent.transform) {
			if (child.name == "Top") {
				top = child.gameObject;
				break;
			} 
		}
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			turn.owner = this;
			ChangeState<InitBattleState> ();
		} else if(level == 4) {
			Debug.Log ("MyLittleSelector" + GameObject.Find ("TileSelector").transform);
			tileSelectionIndicator = GameObject.Find ("TileSelector").transform;
			turn.owner = this;
			ChangeState<InitBattleState> ();
		}
		Debug.Log (level);
	}


	void OnPlayerStartedLocal(object sender, object args) {
		cameraRig.follow = ((PlayerController)sender).gameObject.transform;
		tileSelectionIndicator = ((PlayerController)sender).gameObject.transform;
	}
		
	void OnPlayerStarted(object sender, object args) {
		cameraRig.follow = ((PlayerController)sender).gameObject.transform;
		tileSelectionIndicator = ((PlayerController)sender).gameObject.transform;
	}
		

	void OnSyncDamage(object sender, object args) {
		string[] data = (string[]) args;

		// La créature qui subit
		Point p = new Point (int.Parse(data [0]), int.Parse(data [1]));
		Creature current = board.tiles [p].contentTile.GetComponent<Creature>();

		// La créature qui a fait les dégats
		Point p2 = new Point(int.Parse(data [3]), int.Parse(data [4]));
		Creature currentAttacker = board.tiles [p2].contentTile.GetComponent<Creature>();
		Animator anim = currentAttacker.GetComponent<Animator> ();
		if (currentAttacker.classCreature == "warrior" || currentAttacker.classCreature == "hero")
		{
			anim.SetTrigger("AttackM");
		} else if(currentAttacker.classCreature == "archer")
		{
			anim.SetTrigger("AttackR");
		}

		if (current != null) {
			current.GetComponent<CreatureDescriptor> ().HP.CurrentValue = float.Parse(data [2]);
		}
	}


	void OnSyncPosition(object sender, object args) {
		PlayerController s = (PlayerController)sender;
		Point[] positions = (Point[])args;
		if (s.playerID != matchController.localPlayer.playerID) {
			
			int size = positions.Length;
			PhysicTile current = board.tiles [positions [0]];

			for (int i = 1; i < size; i++) {
				current.prev = board.tiles [positions [i]];
				current = board.tiles [positions [i]];
			}
			Creature creature = board.tiles[positions[size-1]].contentTile.GetComponent<Creature> ();
			launchCoroutine (creature, positions);
		}
	}

	void launchCoroutine(Creature c, Point[] points) {
		Movement m = c.GetComponent<Movement>();
		int size = points.Length;
		for (int i = size-2; i >= 0; i--) {
			StartCoroutine(m.Traverse(board.tiles[points[i]]));

		}
		c.Match ();
	}

	void OnCharacterDeath(object sender, object args) {
		// La créature qui subit
		Point p = (Point) args;
		PlayerController s = (PlayerController)sender;
		Creature current = board.tiles [p].contentTile.GetComponent<Creature>();
		if (s.playerID != matchController.localPlayer.playerID) {
			Animator anim = current.GetComponent<Animator> ();
			anim.SetTrigger ("Die");

			if (creaturesJ1.Contains (current)) {
				creaturesJ1.Remove (current);
			} else {
				creaturesJ2.Remove (current);
			}

			GameObject.Destroy(current.gameObject, 5.0f);

		}
	}

	void OnVictory(object sender, object args) {
		string winner = (string) args;
		if (winner == matchController.localPlayer.playerID) {
			SceneManager.LoadScene ("Victory");
		} else {
			SceneManager.LoadScene ("Defeat");
		}

	}

}