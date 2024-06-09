using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrahed;
    new public static void ResetStaticData()
    {
        OnAnyObjectTrahed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
        }

        OnAnyObjectTrahed?.Invoke(this, EventArgs.Empty);
    }
}
