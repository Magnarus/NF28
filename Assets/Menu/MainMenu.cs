using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class MainMenu : MonoBehaviour {

    public Canvas Menu;
    public Button Play;
    public Button Exit;

    
	[SerializeField] private Button btn = null;


    // Use this for initialization
	void Start () {
        Menu = Menu.GetComponent<Canvas>();
        Play = Play.GetComponent<Button>();
		Exit = Exit.GetComponent<Button>();
//		Debug.Log ("Start");
		btn.onClick.AddListener (() => { StartSinglePlayer(); });	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ExitGame()
    {

        Application.Quit();
    }


	public void StartSinglePlayer() {
		SceneManager.LoadScene ("IA");
	}
}
