using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class AbilityMenuPanel : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";
    const string EntryPoolKey = "AbilityMenuPanel.Entry";
    const int MenuCount = 4;


    [SerializeField]  GameObject entryPrefab; // Préfab servant au panneau
    [SerializeField]  Text titleLabel; // Titre du panneau
    [SerializeField]  Panel panel; // Panel qui sert de collection pour les entrys
    [SerializeField]  GameObject canvas; // 

    List<AbilityMenu> menuEntries = new List<AbilityMenu>(MenuCount);
    public int selection { get; private set; }

    /** Au trigger du script on génère un pool de prefab pour les menus où MenuCount est le nombre de menu max**/
    void Awake()
    {
        // PoolManager.PoolManagerData(MenuCount, entryPrefab);
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }

    /** Récupère un des gameObject généré dans Awake pour créer un menu **/
    AbilityMenu Dequeue()
    {
        /*GameObject p = PoolManager.GetObject();
        AbilityMenu entry = p.GetComponent<AbilityMenu>();
        entry.transform.SetParent(panel.transform, false);
        entry.transform.localScale = Vector3.one;
        entry.gameObject.SetActive(true);
        entry.Reset();
        return entry;*/

        Poolable p = GameObjectPoolController.Dequeue(EntryPoolKey);
        AbilityMenu entry = p.GetComponent<AbilityMenu>();
        entry.transform.SetParent(panel.transform, false);
        entry.transform.localScale = Vector3.one;
        entry.gameObject.SetActive(true);
        entry.Reset();
        return entry;
    }

    /** Libère l'objet pour une autre utilisation ultérieure (aka pas de création suppression excessive)**/
    void Enqueue(AbilityMenu entry)
    {
        /*GameObject g = entry.gameObject;
        PoolManager.DestroyObjectPool(g);*/
        Poolable p = entry.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    /** On n'a plus besoin de données**/
    void Clear()
    {
        for (int i = menuEntries.Count - 1; i >= 0; --i)
            Enqueue(menuEntries[i]);
        menuEntries.Clear();
    }


    void Start()
    {
        panel.SetPosition(HideKey, false);
        canvas.SetActive(false);
    }

    Tweener TogglePos(string pos)
    {
        Debug.Log("pos ! " + pos);
        Tweener t = panel.SetPosition(pos, true);
        t.easingControl.duration = 0.5f;
        t.easingControl.equation = EasingEquations.EaseOutQuad;
        return t;
    }

    /** Un seul élément en état selected à la fois **/
    bool SetSelection(int value)
    {
        if (menuEntries[value].isLocked)
            return false;

        // Deselect the previously selected entry
        if (selection >= 0 && selection < menuEntries.Count)
            menuEntries[selection].isSelected = false;

        selection = value;

        // Select the new entry
        if (selection >= 0 && selection < menuEntries.Count)
            menuEntries[selection].isSelected = true;

        return true;
    }

    /** Vérifie que les entrées suivantes ne sont pas selectionnées  **/
    public void Next()
    {
        for (int i = selection + 1; i < selection + menuEntries.Count; ++i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
                break;
        }
    }

    /** Vérifie que les entrées précédentes ne sont pas selectionnées  **/
    public void Previous()
    {
        for (int i = selection - 1 + menuEntries.Count; i > selection; --i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
                break;
        }
    }

    /** Méthode initiale pour donner le titre de la fenêtre et les options que ce menu contiendra **/
    public void Show(string title, List<string> options)
    {
        
        canvas.SetActive(true);
        Clear();
        titleLabel.text = title;
        for (int i = 0; i < options.Count; ++i)
        {
            AbilityMenu entry = Dequeue();
            entry.Title = options[i];
            menuEntries.Add(entry);
        }
        SetSelection(0);
        TogglePos(ShowKey);
    }


    /** Fonction utile. Oui toi là bas, tu ne bougeras pas deux fois. **/
    public void SetLocked(int index, bool value)
    {
        if (index < 0 || index >= menuEntries.Count)
            return;

        menuEntries[index].isLocked = value;
        if (value && selection == index)
            Next();
    }


    public void Hide()
    {
        Tweener t = TogglePos(HideKey);
        t.easingControl.completedEvent += delegate (object sender, System.EventArgs e)
        {
            if (panel.CurrentPosition == panel[HideKey])
            {
                Clear();
                canvas.SetActive(false);
            }
        };
    }
}
