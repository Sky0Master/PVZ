using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelCard : Card
{
    public override void OnClickWhileSnapping()
    {
        var block = _pointerManager.CurrentHoverGameObject.GetComponent<Block>();
        if (block != null)
        {
            block.RemovePlant();
        }
        _pointerManager.CancelSnapping();
    }
}
