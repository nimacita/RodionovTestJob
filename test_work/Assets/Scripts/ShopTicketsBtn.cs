using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopTicketsBtn : MonoBehaviour
{

    [Header("Product Name")]
    [SerializeField]
    private string productName;
    [Header("Price")]
    [SerializeField]
    private int ticketsPrice;
    [Header("needed Lvl")]
    [SerializeField]
    private int neededLvl;
    [Header("Product Image")]
    [SerializeField]
    private Sprite productImage;

    [Space]
    [SerializeField]
    private bool sold;


    [Space]
    [Header("BtnObjects")]
    [SerializeField]
    private TextMeshProUGUI ticketsPriceTxt;
    [SerializeField]
    private TextMeshProUGUI productNameTxt;
    [SerializeField]
    private GameObject Product;
    [SerializeField]
    private GameObject Locked;
    [SerializeField]
    private TextMeshProUGUI requiredLvl;
    [SerializeField]
    private GameObject buyBtnGameObject;
    private Button buyBtn;
    private Image buyBtnImage;
    [SerializeField]
    private Color IteractableColor, SoldedColor, NonIteractableColor;
    [SerializeField]
    private GameObject Price, Sold;


    private ShopScreenController shopScreenController;

    void Start()
    {
        //Solded = false;

        shopScreenController = GameObject.Find("ShopScrollView").GetComponent<ShopScreenController>();
        sold = Solded;
        InitializedPriceAndValue();

        buyBtn = buyBtnGameObject.GetComponent<Button>();
        buyBtnImage = buyBtnGameObject.GetComponent<Image>();
        InitializedBuyBtnColor();
    }

    private void FixedUpdate()
    {
        IsIteractable();
    }

    private void InitializedBuyBtnColor()
    {
        if (Solded)
        {
            buyBtnImage.color = SoldedColor;
        }
        else if (PlayerPrefs.GetInt("currLevel") < neededLvl || shopScreenController.Tickets < ticketsPrice)
        {
            buyBtnImage.color = NonIteractableColor;
        }
        else
        {
            buyBtnImage.color = IteractableColor;
        }
    }

    private void InitializedPriceAndValue()
    {
        if (Product != null && Locked != null) {
            if (PlayerPrefs.GetInt("currLevel") >= neededLvl)
            {
                Locked.SetActive(false);
                Product.SetActive(true);
                if (productImage != null) {
                    Product.GetComponent<Image>().sprite = productImage;
                }
            }
            else
            {
                Product.SetActive(false);
                Locked.SetActive(true);
                requiredLvl.text = $"LVL{neededLvl}";
            }
        }

        UpdateSoldBtn();

        if (productNameTxt != null)
        {
            productNameTxt.text = $"{productName}";
        }
    }

    private void UpdateSoldBtn()
    {
        if (Price != null && Sold != null)
        {
            if (Solded)
            {
                Price.SetActive(false);
                Sold.SetActive(true);
            }
            else
            {
                Sold.SetActive(false);
                Price.SetActive(true);

                if (ticketsPrice > 0 && ticketsPriceTxt != null)
                {
                    ticketsPriceTxt.text = $"{ticketsPrice}";
                }
            }
        }
    }

    private bool Solded
    {
        get
        {
            if (!PlayerPrefs.HasKey($"{productName}"))
            {
                PlayerPrefs.SetString($"{productName}", "false");
            }

            if (PlayerPrefs.GetString($"{productName}") == "false")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        set
        {
            if (value)
            {
                PlayerPrefs.SetString($"{productName}", "true");
            }
            else
            {
                PlayerPrefs.SetString($"{productName}", "false");
            }
            UpdateSoldBtn();
        }
    }


    private void IsIteractable()
    {
        if (Solded || PlayerPrefs.GetInt("currLevel") < neededLvl || shopScreenController.Tickets < ticketsPrice)
        {
            buyBtn.interactable = false;
        }
        else
        {
            buyBtn.interactable = true;
        }

        InitializedBuyBtnColor();
    }

    public void BtnClicl()
    {
        shopScreenController.Tickets -= ticketsPrice;
        Solded = true;
    }

}
