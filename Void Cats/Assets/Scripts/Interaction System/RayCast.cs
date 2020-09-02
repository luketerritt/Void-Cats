using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    //how far the raycast goes
    [SerializeField] private float range;
    // what ever the player is looking at - item,door,enemy etc
    private IInteractable currentTarget;
    // refence to camera for the raycast
    private Camera mainCamera;

    private void Awake()
    {
        // what ever object has the tag "Main Camera"
        mainCamera = Camera.main; 
    }

    private void Update()
    {
        RaycastForInteractable();
        if(Input.GetKey(KeyCode.E))
        {
            if(currentTarget != null)
            {
                currentTarget.OnInteract();
            }
            
        }
    }

    private void RaycastForInteractable()
    {
        // what i hit
        RaycastHit whatIHit;
        // Shoots out ray from camera
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out whatIHit, range))
        {
            IInteractable interactable = whatIHit.collider.GetComponent<IInteractable>();
            if (interactable != null) // only if not nothing eg floor but not interactable 
            {
                if (whatIHit.distance <= interactable.MaxRange) // only if within range
                {
                    if (interactable == currentTarget)
                    {
                        return; // if within range not nothing has change - do nothing
                    }
                    else if (currentTarget != null) // if not the current target
                    {
                        currentTarget.OnEndHover(); // ending what you were currently looking at 
                        currentTarget = interactable; // make new thing current
                        currentTarget.OnStartHover(); // starts a new one
                        return;
                    }
                    else // if you are looking at something but werent originally
                    {
                        currentTarget = interactable;
                        currentTarget.OnStartHover();
                        return;
                    }
                }
                else
                {
                    // if you are still looking at something but its out of range
                    if (currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                }
            }
            else
            {   // if you arent looking at something but its still out of range
                if(currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }
        else
        {   // if the raycat hits nothing like the skybox
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }
    }
}
