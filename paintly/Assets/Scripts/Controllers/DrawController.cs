using UnityEngine;
using UnityEngine.UI;

public class DrawController : MonoBehaviour
{
    public GameObject PictureItem;

    public System.Collections.Generic.List<Text> CurrentQuantityColorPlayerTexts = 
        new System.Collections.Generic.List<Text>();
    private void Start()
    {
    }

    void Update()
    {
        if(PictureItem == null)
        {
            return;
        }

        PictureItem.GetComponent<PictureItem>().ColorPrefabs[ButtonsController.SelectedButton].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        PictureItem.GetComponent<PictureItem>().ColorPrefabs[ButtonsController.SelectedButton].
            GetComponent<ColorBtn>().GetProgress().fillAmount = (float)PictureItem.GetComponent<PictureItem>().ColorPrefabs[ButtonsController.SelectedButton].
                                                                GetComponent<ColorBtn>().CurrentCountFilled /
                                                                (float)PictureItem.GetComponent<PictureItem>().ColorPrefabs[ButtonsController.SelectedButton].
                                                                GetComponent<ColorBtn>().ToltalCountColors;

        for (int i = 0; i < PictureItem.GetComponent<PictureItem>().ColorPrefabs.Count; ++i)
        {
            if (i == ButtonsController.SelectedButton) continue;
            PictureItem.GetComponent<PictureItem>().ColorPrefabs[i].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        TypeText();
    }

    private void TypeText()
    {
        if(PlayerController.PaintForColoring > 0 && PlayerController.PaintForColoring < 1000)
        {
            foreach (var text in CurrentQuantityColorPlayerTexts)
            {
                text.text = PlayerController.PaintForColoring.ToString();
            }
        }
        else if(PlayerController.PaintForColoring >= 1000 && PlayerController.PaintForColoring < 1000000)
        {
            float textValue = (float)PlayerController.PaintForColoring / 1000f;
            foreach (var text in CurrentQuantityColorPlayerTexts)
            {
                text.text = System.String.Format("{0:0.0}K", textValue);
            }
        }
    }
}
