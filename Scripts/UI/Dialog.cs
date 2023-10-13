using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ResourceInfo("Dialog", 0.3f, 19)]
public class Dialog : MonoBehaviour
{
    [SerializeField] private Text _npcNameText;
    public Text npcNameText { get { return _npcNameText; } }
    [SerializeField] private Text _dialogContentText;
    public Text dialogContentText { get { return _dialogContentText; } }
    [SerializeField] private GameObject _npcButtonUI;
    public GameObject npcButtonUI { get { return _npcButtonUI; } }
    [SerializeField] private GameObject _runeStoneButtonUI;
    public GameObject runeStoneButtonUI { get { return _runeStoneButtonUI; } }

    private void OnDisable()
    {
        npcButtonUI.gameObject.SetActive(false);
        runeStoneButtonUI.gameObject.SetActive(false);
    }
}
