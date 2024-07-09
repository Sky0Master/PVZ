using InventorySystem.UI;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private int _size = 8;
        [SerializeField]
        private List<InventorySlot> _slots;

        public List<InventorySlot> Slots => _slots;
        public int Size => _size;

        private int _activeSlotIndex;
        public int ActiveSlotIndex
        {
            get => _activeSlotIndex;
            private set
            {
                _slots[_activeSlotIndex].Active = false;
                _activeSlotIndex = value < 0 ? _size - 1 : value % Size; //clamp the index
                _slots[_activeSlotIndex].Active = true;
            }
        }

        private void Awake()
        {
            
        }

        public bool IsFull()
        {
            return _slots.Count(slot => slot.HasItem) >= _size;
        }

        public bool CanAcceptItem(ItemStack itemStack)
        {
            if (!IsFull())
                return true;
            var stackableItemSlot = FindSlot(itemStack.Item, true);
            return stackableItemSlot != null; 
        }

        
        private InventorySlot FindSlot(ItemDefinition item, bool onlyStackable = false)
        {
            return _slots.FirstOrDefault(slot => slot.Item == item && (item.IsStackable || !onlyStackable));

        }
        private void OnValidate()
        {
            AdjustSize();
            foreach(var i in _slots)
            {
                if(i.HasItem)
                    i.NumberOfItems = i.NumberOfItems;
            }
        }
        private void AdjustSize()
        {
            _slots ??= new List<InventorySlot>();
            if (_slots.Count > _size) _slots.RemoveRange(_size,_slots.Count - _size);
            if(_slots.Count<_size) _slots.AddRange(new InventorySlot[_size - _slots.Count]);
        }
        public ItemStack AddItem(ItemStack itemStack)
        {
            
            var relevantSlot = FindSlot(itemStack.Item, true);
            if (IsFull() && relevantSlot == null)
            {
                throw new InventoryException(InventoryOperation.Add, "Inventory is full");
            }
            if(relevantSlot != null)
            {
                relevantSlot.NumberOfItems += itemStack.NumberofItems;

            }
            else
            {
                relevantSlot = _slots.First(slot=>!slot.HasItem);
                relevantSlot.State = itemStack;
            }
            return relevantSlot.State;
        }

        public ItemStack RemoveItem(int index,bool spawn = false)
        {
            if (!_slots[index].HasItem)
                throw new InventoryException(InventoryOperation.Remove,$"Slot{index} is Empty");
            if(spawn)
            {
                
            }
            ClearSlot(index);
            var stack = new ItemStack();
            return stack;
        }
        public ItemStack RemoveItem(ItemStack itemStack)
        {
            var itemSlot = FindSlot(itemStack.Item);
            if (itemSlot == null)
            {
                throw new InventoryException(InventoryOperation.Remove, "No item in the Inventory");
            }
            if (itemSlot.Item.IsStackable && itemSlot.NumberOfItems < itemStack.NumberofItems)
            {
                throw new InventoryException(InventoryOperation.Remove, "No enough Item");
            }
            itemSlot.NumberOfItems -= itemStack.NumberofItems;
            if (itemSlot.Item.IsStackable && itemSlot.NumberOfItems > 0)
            {
                return itemSlot.State;
            }
            itemSlot.Clear();
            return new ItemStack();
        }

        public void ClearSlot(int index)
        {
            _slots[index].Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemStack"></param>
        /// <param name="checkNumber">check the number of item is correct or not</param>
        /// <returns></returns>
        public bool HasItem(ItemStack itemStack, bool checkNumber)
        {
            var slot = FindSlot(itemStack.Item);
            if (slot == null) return false;
            if (!checkNumber) return true;

            if (itemStack.Item.IsStackable)
                return slot.NumberOfItems >= itemStack.NumberofItems;

            return _slots.Count(slot => slot.Item == itemStack.Item) >= itemStack.NumberofItems;
           
        }

        public void ActivateSlot(int index)
        {
            _activeSlotIndex = index;
        }
    }
}
