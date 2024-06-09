using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO KitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenSOLists;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenSOLists.Contains(kitchenObjectSO)) { //not valid ingredient
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
                return false;
        } else{
                kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { 
                KitchenObjectSO = kitchenObjectSO
            });
                return true;
        }
        
    }

    public List<KitchenObjectSO> GetKitchenObjectSOLists() {
        return kitchenObjectSOList;
    }
   
}
