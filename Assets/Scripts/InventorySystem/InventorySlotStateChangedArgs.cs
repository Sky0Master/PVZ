using InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotStateChangedArgs
{
    public ItemStack NewState { get; }
    public bool Active {  get; }
    public InventorySlotStateChangedArgs(ItemStack newState,bool active)
    {
        NewState = newState;
        Active = active;
    }
}
