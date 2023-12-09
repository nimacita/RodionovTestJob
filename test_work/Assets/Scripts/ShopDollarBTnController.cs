using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;


public class ShopDollarBTnController : MonoBehaviour
{

    [Header("Product Name")]
    [SerializeField]
    private string productName;
    [Header("Value&Price")]
    [SerializeField]
    private float dollarValue;
    [SerializeField]
    private int ticketsPrice;


    [Space]
    [Header("BtnObjects")]
    [SerializeField]
    private TextMeshProUGUI dollarValueTxt;
    [SerializeField]
    private TextMeshProUGUI ticketsPriceTxt;
    [SerializeField]
    private TextMeshProUGUI productNameTxt;


    void Start()
    {
        InitializedPriceAndValue();
    }

    private void InitializedPriceAndValue()
    {
        if (dollarValue > 0 && dollarValueTxt!= null)
        {
            dollarValueTxt.text = $"{dollarValue.ToString("0.00")}$";
        }

        if (ticketsPrice > 0 && ticketsPriceTxt != null)
        {
            ticketsPriceTxt.text = $"X{ticketsPrice}";
        }

        if (productNameTxt != null) 
        {
            productNameTxt.text = $"[{productName}]";
        }
    }

    public void BtnClick()
    {
       
    }

    


    
}
