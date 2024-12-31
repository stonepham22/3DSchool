using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteracUI : MonoBehaviour
{
    [SerializeField] private GameObject _containerObj;
    [SerializeField] private PlayerInteract _playerInteract;
    [SerializeField] private TextMeshProUGUI _interactText;

    void Update()
    {
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

    void Hide()
    {
        _containerObj.SetActive(false);
    }   

}
