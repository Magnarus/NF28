using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour
{

    /**
     *  Entrée dans l'état de la machine
     *  Ajout des listeners de l'état
     * */
    public virtual void Enter()
    {
        AddListeners();
    }

    /**
     *  Sortie de l'état de la machine
     *  Plus besoin d'écouter
     * */
    public virtual void Exit()
    {
        RemoveListeners();
    }

    /**
     * 
     **/
    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }

    /**
     * Ajout des listners
     * */
    protected virtual void AddListeners()
    {

    }

    /**
     * Ajout d'un seul listener
     *
     * */
    protected virtual void AddListener(Event e)
    {

    }

    protected virtual void RemoveListeners()
    {

    }
}