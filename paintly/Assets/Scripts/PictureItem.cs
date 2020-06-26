using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureItem : MonoBehaviour
{
    public string uniqueNubmer;
    public List<int> QuantityElementsOsOneColor = new List<int>();
    public List<Color32> Color32s = new List<Color32>();
    public List<GameObject> ColorPrefabs  = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < ColorPrefabs.Count; ++i)
        {
            ColorPrefabs[i].GetComponent<ColorBtn>().ToltalCountColors = QuantityElementsOsOneColor[i];
            ColorPrefabs[i].GetComponent<Image>().color = Color32s[i];
            ColorPrefabs[i].GetComponent<ColorBtn>().NumberColor.GetComponent<Text>().text = (i + 1).ToString();
        }
    }
}