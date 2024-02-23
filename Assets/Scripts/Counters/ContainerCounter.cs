using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {

        if (!player.HasKitchenObject()) {
            // Player is not carrying anything

            // Spawn the sliced version
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            // Fire the event for the player grabbing the object, so that the animation can trigger
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

        }

    }

}
