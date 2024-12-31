using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteracUI : MonoBehaviour
{
    [SerializeField] private GameObject _containerObj;
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private Transform _panelDialog;

    void Update()
    {
        if(_panelDialog.gameObject.activeSelf) return;
        if(_playerInteract.GetInteractableObject() != null)
        {
            Show(_playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    void Show(NPCInteractable npcInteractable)
    {
        _containerObj.SetActive(true);
        _interactText.text = npcInteractable.GetInteractText();
    }

    public void Hide()
    {
        _containerObj.SetActive(false);
    }   

}
