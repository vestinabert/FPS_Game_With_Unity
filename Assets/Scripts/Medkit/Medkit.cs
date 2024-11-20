using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, IItem
{
    [SerializeField] private string itemName; 
    [SerializeField] private int healthValue; 

    public string GetName() 
    { 
        return itemName; 
    } 

    private void OnTriggerEnter(Collider other) 
    { 
        GameObjectDescriptor _otherType = other.GetComponent<GameObjectDescriptor>(); 

        if (_otherType != null) 
        { 
            if (_otherType.type == GameObjectType.Player) 
            { 
                _otherType.GetComponent<PlayerHealth>().AddHealth(healthValue); 
                Destroy(gameObject); 
            } 
        } 
    } 
}
