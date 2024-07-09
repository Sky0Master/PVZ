using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        [SerializeField]
        private ItemDefinition _item;

        [SerializeField]
        private int _numberofItems;

        public bool IsStackable => _item.IsStackable;

        public ItemDefinition Item => _item;

        public int NumberofItems
        {
            get => _numberofItems;
            set
            {
                value = value < 0 ? 0 : value;
                _numberofItems = IsStackable ? value : 1;
            }
        }
        public int Value
        {
            get => _item.Value * NumberofItems;
        }

        public ItemStack(ItemDefinition item, int numberofItems)
        {
            _item = item;
            NumberofItems = numberofItems;
        }
        public ItemStack()
        {

        }

    }
}