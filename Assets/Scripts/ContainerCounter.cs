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

            // Instantate the type of kitchen object, and immediately give it to the player
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            // Fire the event for the player grabbing the object, so that the animation can trigger
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

        }

    }

}
