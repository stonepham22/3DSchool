using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string _interactText;
    public void Interact()
    {
        ChatBubble.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), ChatBubble.IconType.Happy, "Hello there!");    
    }

    public string GetInteractText()
    {
        return _interactText;
    }
}
