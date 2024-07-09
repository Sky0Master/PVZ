
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.UI {

    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _inventorySlotPrefab;

        [SerializeField]
        private Inventory _inventory;

        [SerializeField]
        private List<UI_InventorySlot> _slots;

        public Inventory Inventory => _inventory;


        [ContextMenu("Initialize Inventory")]
        private void InitializeInventoryUI()
        {
            if (_inventory == null || _inventorySlotPrefab == null) return;
            _slots = new List<UI_InventorySlot>(_inventory.Size);
            for(int i =0;i< _inventory.Size;i++)
            {
                var uiSlot = PrefabUtility.InstantiatePrefab(_inventorySlotPrefab) as GameObject;
                var uiSlotScript = uiSlot.GetComponent<UI_InventorySlot>();
                uiSlot.transform.SetParent(transform,false);
                _slots.Add(uiSlotScript);
                uiSlotScript.AssignSlot(i);
            }
        }

        

    }
}