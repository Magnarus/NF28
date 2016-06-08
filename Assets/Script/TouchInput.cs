using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

    public LayerMask touchInputMask;

	// Update is called once per frame
	void Update () {

    #if UNITY_EDITOR
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                Debug.Log(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject recipient = hit.transform.gameObject;
                    if (Input.GetMouseButtonUp(0))
                    {
                        Debug.Log("MouseButtonUp");
                        recipient.SendMessage("OnTouch", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if(Input.GetMouseButtonDown(0)) {
                        Debug.Log("MouseButtonDown");
                        recipient.SendMessage("OnTouch", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                }
            }

    /*   foreach(Touch touch in Input.touches)
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
       }*/
       #endif
    }
}
