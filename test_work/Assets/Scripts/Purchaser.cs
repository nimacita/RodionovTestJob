using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{

    private ShopScreenController shopScreenController;

    private void Start()
    {
        shopScreenController = gameObject.GetComponent<ShopScreenController>();
    }

    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "tickets500":
                Add500Tickets();
            break;
            case "tickets1200":
                Add1200Tickets();
            break;
        }
    }

    
    private void Add500Tickets()
    {
        shopScreenController.Tickets += 500;
        //добавление монет
    }

    private void Add1200Tickets()
    {
        shopScreenController.Tickets += 1200;
    }

    public void OnpurchaseFailure(Product product, PurchaseFailureReason  reason)
    {
        Debug.Log($"Purchase failde because {reason}");
    }
}
