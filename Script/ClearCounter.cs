
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject())  { //no kitchen object
            if (player.HasKitchenObject()) { //player have something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else { //player have nothing
            
            }
        }
        else {  //has kitchen object
            if (player.HasKitchenObject())  { //player have something 
             
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))  {
                        //holding a plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                             GetKitchenObject().DestroySelf();
                        }
                    } else { //not carry a plate but something else
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                            //counter has something 
                            if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                      }
                
            } else { //has nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
}
    
