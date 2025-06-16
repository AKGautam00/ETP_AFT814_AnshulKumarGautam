using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Yo!!");
        // Your custom logic here

        if (GetComponent<SpriteRenderer>().material.color == Color.green)
        {
            GetComponent<SpriteRenderer>().material.color = Color.red; // Use any Color
        }
        else
        {
            GetComponent<SpriteRenderer>().material.color = Color.green; // Use any Color
        }
    }
}