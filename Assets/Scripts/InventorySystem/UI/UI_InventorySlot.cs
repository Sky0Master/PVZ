using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{

    public class UI_InventorySlot : MonoBehaviour
    {
        [SerializeField]
        private Inventory _inventory;

        [SerializeField]
        private int _slotIndex;

        [SerializeField]
        private Image _itemIcon;

        [SerializeField]
        private Image _active;

        [SerializeField]
        private TMP_Text _itemNum;

        private InventorySlot _slot;


        private void Start()
        {
            AssignSlot(_slotIndex);
        }

        public void AssignSlot(int slotIndex)
        {
            if (_slot != null) _slot.StateChanged -= OnStateChanged;
            _slotIndex = slotIndex;
            if(_inventory == null )
            {
                _inventory = GetComponentInParent<UI_Inventory>().Inventory;
            }
            _slot = _inventory.Slots[_slotIndex];
            _slot.StateChanged += OnStateChanged;
            UpdateViewState(_slot.State, _slot.Active);
        }

        private void UpdateViewState(ItemStack state,bool active)
        {
            _active.enabled = active;
            var item = state?.Item;
            var hasItem = item!= null;
            var isStackable = hasItem && item.IsStackable;
            _itemIcon.enabled = hasItem;
            _itemNum.enabled = isStackable;
            if (!hasItem) return;
            
            _itemIcon.sprite = item.UiSprite;
            if(isStackable)
            {
                _itemNum.SetText(state.NumberofItems.ToString());
            }

        }
        private void OnStateChanged(object sender, InventorySlotStateChangedArgs args)
        {
            UpdateViewState(args.NewState,args.Active);
            //Debug.Log("UI_OnStateChange!");
        }

    }
}