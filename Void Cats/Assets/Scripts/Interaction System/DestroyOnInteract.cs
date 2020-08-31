using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInteract : MonoBehaviour, IInteractable
{
    public float MaxRange { get { return maxRange; } }
    private const float maxRange = 5f;

    public void OnStartHover()
    {
        Debug.Log("Hover");
    }

    public void OnInteract()
    {
        Debug.Log("Destroy");
        Destroy(gameObject);
    }
    public void OnEndHover()
    {
        if(gameObject !=null)
        {
            Debug.Log("Destroyed");
        }
    }

    }
