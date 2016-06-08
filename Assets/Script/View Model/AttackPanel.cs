using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Descriptors;

public class AttackPanel : SidePanel 
{
    //différenciation groupe du hero ?
	/*[SerializeField] GameObject blueBacker;
	[SerializeField] GameObject redBacker;*/
	[SerializeField] Text nameLabel;
	[SerializeField] Text hpLabel;
	[SerializeField] Text mpLabel;
	[SerializeField] Text lvLabel;

    void Start()
    {
        ShowStats();


    }
    
	public void ShowStats ()
	{
		
            nameLabel.text =gameObject.GetComponent<CreatureDescriptor>().Name.value;
			hpLabel.text= string.Format("HP : {0}/{1}", gameObject.GetComponent<CreatureDescriptor>().HP.CurrentValue,gameObject.GetComponent<CreatureDescriptor>().HP);
			mpLabel.text = string.Format("Mana : {0}/{1}", gameObject.GetComponent<CreatureDescriptor>().Mana.CurrentValue,gameObject.GetComponent<CreatureDescriptor>().Mana);
			lvLabel.text = string.Format("LV. {0}", gameObject.GetComponent<CreatureDescriptor>().Level);

      
	}
    void Update()
    {
        ShowStats();
    }


}