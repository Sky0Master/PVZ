using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventorySystem
{

    public class GameItem : MonoBehaviour
    {
        [SerializeField]
        private ItemStack _stack;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public ItemStack Stack => _stack;

        public ItemStack Pick()
        {
            Destroy(gameObject);
            return _stack;
        }

        private void OnValidate()
        {
            SetupGameObject();
        }

        private void SetupGameObject()
        {
            if (_stack.Item == null)
                return;
            SetGameSprite();
            AdjustNumberOfItems();
            UpdateGameObjectName();
        }
        private void SetGameSprite()
        {
            _spriteRenderer.sprite = _stack.Item.InGameSprite;
        }
        private void UpdateGameObjectName()
        {
            var name = _stack.Item.Name;
            var number = _stack.IsStackable ? _stack.NumberofItems.ToString() : "ns";
            gameObject.name = $"{name}({number})";

        }
        private void AdjustNumberOfItems()
        {
            _stack.NumberofItems = _stack.NumberofItems;  //´¥·¢set
        }

    }
}