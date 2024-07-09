using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{

    [CreateAssetMenu(menuName = "Inventory/Item Definition", fileName = "New Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField]
        string _name;
        [SerializeField]
        private bool _isStackable;
        [SerializeField]
        private int _value;
        [SerializeField]
        Sprite _inGameSprite;
        [SerializeField]
        Sprite _uiSprite;

        public string Name => _name;
        public bool IsStackable => _isStackable;
        public Sprite InGameSprite => _inGameSprite;
        public Sprite UiSprite => _uiSprite;
        
        public int Value => _value;
    }
}