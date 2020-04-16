using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IInteract
{

    private IInteract interactable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))//F button for interaction
        {
            Interact();
        }
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Statue")
        {
            interactable = other.GetComponent<IInteract>();
        }
        
    }

    public void OnTriggerExit(Collider other)
    {

            if (other.tag == "Statue")
            {
            interactable = null;
            }
        
    }


    public void Interact()
    {
        if(interactable != null)
        {
            interactable.Interact();
        }
    }

}
