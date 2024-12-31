using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string _interactText;
    [SerializeField] private NPCConversation _nPCConversation;
    [SerializeField] private PlayerInteracUI _playerInteracUI;
    public void Interact()
    {
        // ChatBubble.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), ChatBubble.IconType.Happy, "Hello there!"); 
        ConversationManager.Instance.StartConversation(_nPCConversation);
        _playerInteracUI.Hide();
        
    }

    public string GetInteractText()
    {
        return _interactText;
    }
}
