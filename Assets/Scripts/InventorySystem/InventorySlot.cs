using System;
using UnityEngine;

namespace InventorySystem
{

    [Serializable]
    public class InventorySlot
    {
        public event EventHandler<InventorySlotStateChangedArgs> StateChanged;

        [SerializeField]
        private ItemStack _state;
        private bool _active;


        public ItemStack State
        {
            get => _state;
            set
            {
                _state = value;
                NotifyAboutStateChanged();
            }
        }
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                NotifyAboutStateChanged();
            }
        }

        public bool HasItem => (_state?.Item != null);

        public ItemDefinition Item => _state?.Item;

        public int NumberOfItems
        {
            get => _state.NumberofItems;
            set
            {
                _state.NumberofItems = value;
                NotifyAboutStateChanged();
            }
        }
        
        public void Clear()
        {
            State = null;
        }

        private void NotifyAboutStateChanged()
        {
            Debug.Log("NotifyStateChange");
            StateChanged?.Invoke(this, new InventorySlotStateChangedArgs(_state, _active));
        }
    }
}