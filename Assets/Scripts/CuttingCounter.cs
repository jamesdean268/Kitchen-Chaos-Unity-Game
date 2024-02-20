using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player) {

        if (!HasKitchenObject()) {
            // There is no KitchenObject here
            if (player.HasKitchenObject()) {
                // Player is carrying a kitchen object, place it on the clear counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // Player has nothing
            }
        } else {
            // There is a KitchenObject here
            if (player.HasKitchenObject()) {
                // Player is carrying something
            } else {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // There is a KitchenObject here. Destroy and then spawn a cut object
            GetKitchenObject().DestroySelf();
            // Spawn the sliced version
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            

        }
    }


}
