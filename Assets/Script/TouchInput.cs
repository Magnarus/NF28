using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

    public LayerMask touchInputMask;

	// Update is called once per frame
	void Update () {

        foreach(Touch touch in Input.touches)
        {
            Debug.Log("one touch !");
            Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
            RaycastHit hit;

            if(Physics.Raycast(ray,out hit, touchInputMask))
            {
                GameObject recipient = hit.transform.gameObject;
                if(touch.phase == TouchPhase.Ended)
                {
                    recipient.SendMessage("OnTouch", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
