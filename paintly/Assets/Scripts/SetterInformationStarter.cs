using UnityEngine.UI;
using UnityEngine;

public class SetterInformationStarter : MonoBehaviour
{
    public GameObject MagicSearch;    
    public GameObject MagicWand;

    
    void Start()
    {
        MagicSearch.GetComponentInChildren<Text>().text = PriceList.PriceMagicSearch.ToString();
        MagicWand.GetComponentInChildren<Text>().text = PriceList.PriceMagicWand.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
