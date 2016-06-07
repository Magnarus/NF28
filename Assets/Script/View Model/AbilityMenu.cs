using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbilityMenu : MonoBehaviour
{
    /*** Différents états possibles pour une entrée du menu ***/
    [System.Flags]
    enum States
    {
        None = 0,
        Selected = 1 << 0,
        Locked = 1 << 1
    }

    [SerializeField]   Image bullet; // Image de la case 
    [SerializeField]   Sprite normalSprite; // Dans son état normal
    [SerializeField]   Sprite selectedSprite; // Sélectionnée
    [SerializeField]   Sprite disabledSprite; // Indisponible
    [SerializeField]   Text label; // Texte
    Outline outline;


    public bool isLocked { get; set; }
    public bool isSelected { get; set; }
    public string Title
    {
        get { return label.text; }
        set { label.text = value; }
    }

    /** Modifie l'affichage d'une entrée du menu des capacités en fonction de l'état (Sélectionnée/Non Sélectionnée/Désactivée/) **/
    States State
    {
        get { return state; }
        set
        {
            if (state == value)
                return;
            state = value;

            if (isLocked)
            {
                bullet.sprite = disabledSprite;
                label.color = Color.gray;
                outline.effectColor = new Color32(20, 36, 44, 255);
            }
            else if (isSelected)
            {
                bullet.sprite = selectedSprite;
                label.color = new Color32(249, 210, 118, 255);
                outline.effectColor = new Color32(255, 160, 72, 255);
            }
            else
            {
                bullet.sprite = normalSprite;
                label.color = Color.white;
                outline.effectColor = new Color32(20, 36, 44, 255);
            }
        }
    }
    States state;

    /** Déclenché par Unity **/
    void Awake()
    {
        outline = label.GetComponent<Outline>();
    }

    /** Réinitialise l'état **/
    public void Reset()
    {
        State = States.None;
    }

    void OnTouch()
    {
        switch(Title)
        {
            case "Déplacement":
                Debug.Log("dpt start ! ");
                break;
            case "Attaque":
                Debug.Log("att start ! ");
                break;
            case "Attendre":
                Debug.Log("wait start ! ");
                break;
        }
    }
    /** **/

}