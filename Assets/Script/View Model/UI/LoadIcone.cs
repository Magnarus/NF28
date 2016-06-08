using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Descriptors;

public class LoadIcone : MonoBehaviour {

	// Use this for initialization
    [SerializeField] Image icone;

    public void refreshIcone()
    {
        
            //blueBacker.SetActive(isAlly);
            //redBacker.SetActive(!isAlly);
            icone = gameObject.GetComponent<CreatureDescriptor>().icone;
          
        
    }




}
