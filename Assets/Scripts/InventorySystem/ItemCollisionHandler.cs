using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemCollisionHandler : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponentInParent<Inventory>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<GameItem>(out var gameItem)||
            !_inventory.CanAcceptItem(gameItem.Stack)) return;
        _inventory.AddItem(gameItem.Pick());
    }
}
