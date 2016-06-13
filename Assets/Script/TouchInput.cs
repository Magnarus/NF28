using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

    public LayerMask touchInputMask;
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR

		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
			Debug.Log("layer" + touchInputMask.value);
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			Debug.Log(Input.mousePosition);
			RaycastHit hit;
			int value = ~(1 << 8);
			if (Physics.Raycast(ray, out hit, touchInputMask.value))
			{
				GameObject recipient = hit.transform.gameObject;
				if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
				{
					Debug.Log ("Touch mouse!");
					Debug.Log(recipient.ToString());
					recipient.SendMessage("OnTouch", hit.point, SendMessageOptions.RequireReceiver);
				}
			}
		}
		#endif

		if (Input.touchCount > 0) {

			// If there are two touches on the device...
			if (Input.touchCount == 2)
			{
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				// If the camera is orthographic...
				Camera camera = GetComponent<Camera>();
				if (camera.orthographic)
				{
					// ... change the orthographic size based on the change in distance between the touches.
					camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

					// Make sure the orthographic size never drops below zero.
					camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
				}
				else
				{
					// Otherwise change the field of view based on the change in distance between the touches.
					camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

					// Clamp the field of view to make sure it's between 0 and 180.
					camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
				}
			}
			
			foreach (Touch touch in Input.touches) {
				
				Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
				Debug.Log(touch.position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchInputMask.value))
				{
					GameObject recipient = hit.transform.gameObject;
					if (touch.phase == TouchPhase.Began)
					{
						Debug.Log ("Touch!");
						recipient.SendMessage("OnTouch", hit.point, SendMessageOptions.DontRequireReceiver);
					}
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
		*/
    }
}
