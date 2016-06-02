using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
    private float ratio = 0.2f;

	public void Start()
    {
        Debug.Log("AddListeners CameraZoom");
        InputController.fireEvent += OnFire;
    }


    protected void Exit()
    {
        Debug.Log("RemoveListeners CameraZoom");
        InputController.fireEvent -= OnFire;
    }

    protected void OnFire(object sender, InfoEventArgs<int> e)
    {
        if(e.info == 1 || e.info == 2)
        {
            float finalRatio;
            if (e.info == 2) finalRatio = 1 - ratio;
            else finalRatio = 1 + ratio;
            GetComponent<Camera>().orthographicSize *= finalRatio;
            Debug.Log(GetComponent<Camera>().orthographicSize);
        }
    }

}
