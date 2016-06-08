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
    }
    
	public void ShowStats ()
	{

		var myObject= GameObject.Find("BattleController").GetComponent<BattleController>().currentTile.contentTile;
        if (!myObject) return;
        nameLabel.text = myObject.GetComponent<CreatureDescriptor>().Name.value;

        hpLabel.text = string.Format("HP : {0}/{1}", myObject.GetComponent<CreatureDescriptor>().HP.CurrentValue, myObject.GetComponent<CreatureDescriptor>().HP.value);
        mpLabel.text = string.Format("Mana : {0}/{1}", myObject.GetComponent<CreatureDescriptor>().MP.CurrentValue, myObject.GetComponent<CreatureDescriptor>().MP.value);
        lvLabel.text = string.Format("LV. {0}", myObject.GetComponent<CreatureDescriptor>().Level.value);
	}
    void Update()
    {
        ShowStats();
    }



    public string my { get; set; }
}