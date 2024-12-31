using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool IsInteracting;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interacRange = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interacRange);
            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
        }
    }

    public NPCInteractable GetInteractableObject()
    {
        if(IsInteracting) return null;
        List<NPCInteractable> npcInteractableList = new List<NPCInteractable>();
        float interacRange = 4f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interacRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                npcInteractableList.Add(npcInteractable);
            }
        }

        NPCInteractable closestInteractable = null;
        foreach (NPCInteractable npcInteractable in npcInteractableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = npcInteractable;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable.transform.position) < 
                    Vector3.Distance(transform.position, closestInteractable.transform.position))
                {
                    closestInteractable = npcInteractable;
                }
            }
        }

        return closestInteractable;
    }
}
