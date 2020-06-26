using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonsController : MonoBehaviour
{
    public GameObject ButtonScreen;
    public GameObject MainScreen;
    public GameObject InformationScreen;
    public GameObject ScreenForDrawing;
    public GameObject BuyScreen;
    public GameObject LearningScreen;
    public GameObject GiftScreen;
    public GameObject DrawScreen;
    public GameObject SettingsScreen;


    public GameObject CacaduBtn;
    public GameObject GiftBtn;
    public GameObject DrawBtn;
    public GameObject InfoBtn;


    public List<GameObject> colors = new List<GameObject>();

    public PictureItem PictureItem;
    public static int SelectedButton { get; private set; } = 0;

    private bool IsFirstClick = true;
    private bool IsSettingClick = false;

    private void Start()
    {
        ButtonScreen.SetActive(true);
        MainScreen.SetActive(true);
        CacaduBtn.GetComponent<Outline>().enabled = true;

        PictureItem.ColorPrefabs[SelectedButton].GetComponent<ColorBtn>().IsSelect = true;
    }

    public void SelectColor(int id)
    {
        foreach(var button in PictureItem.ColorPrefabs)
        {
            if(button.GetComponent<ColorBtn>().GetProgress().fillAmount == 1)
            {
                button.GetComponent<Button>().interactable = false;
            }
            button.GetComponent<ColorBtn>().IsSelect = false;
        }

        PictureItem.ColorPrefabs[id].GetComponent<ColorBtn>().IsSelect = true;
        SelectedButton = id;
    }

    public void SearchSigment()
    {
        if (IsFirstClick)
        {
            GoToLearningScreen();
            IsFirstClick = false;
            return;
        }

        if (PictureItem.ColorPrefabs[SelectedButton].GetComponent<ColorBtn>().GetProgress().fillAmount == 1)
            return;

        if (PlayerController.PaintForColoring <PriceList.PriceMagicSearch)
            return;

        if (PictureItem.ColorPrefabs[SelectedButton].GetComponent<ColorBtn>().IsUsedMagicSearch)
            return;

        PlayerController.PaintForColoring -= PriceList.PriceMagicSearch;

        int i = 0;
        for (; i < colors.Count;++i)
        {
            if (colors[i].transform.GetChild(i).tag == PictureItem.ColorPrefabs[SelectedButton].tag)
            {
                var colorComponent = colors[i].transform.GetComponentsInChildren<Sigment>();
                for (int j = 0; j < colorComponent.Length ; ++j)
                {
                    if (!colorComponent[j].IsFilled) 
                    {
                        
                        colorComponent[j].gameObject.GetComponent<LineRenderer>().enabled = true;
                        colorComponent[j].gameObject.GetComponent<LineRenderer>().startWidth = 0.02f;
                        colorComponent[j].gameObject.GetComponent<LineRenderer>().endWidth = 0.02f;
                    }
                }

                PictureItem.ColorPrefabs[SelectedButton].GetComponent<ColorBtn>().IsUsedMagicSearch = true;
                break;
            }
        }        
    }

    public void FillingAllSigments()
    {
        if (IsFirstClick)
        {
            GoToLearningScreen();
            IsFirstClick = false;
            return;
        }

        if (PlayerController.PaintForColoring < PriceList.PriceMagicWand)
            return;

        if (PictureItem.ColorPrefabs[SelectedButton].GetComponent<ColorBtn>().IsUsedMagicWand)
            return;

        PlayerController.PaintForColoring -= PriceList.PriceMagicWand;

        int i = 0;
        for (; i < colors.Count; ++i)
        {
            if (colors[i].transform.GetChild(i).tag == PictureItem.ColorPrefabs[SelectedButton].tag)
            {
                var colorComponent = colors[i].transform.GetComponentsInChildren<Sigment>();
                for (int j = 0; j < colorComponent.Length; ++j)
                {
                    if (!colorComponent[j].IsFilled)
                    {
                        ++PictureItem.ColorPrefabs[SelectedButton].GetComponent<ColorBtn>().CurrentCountFilled;
                        colorComponent[j].gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                PictureItem.ColorPrefabs[SelectedButton].GetComponent<ColorBtn>().IsUsedMagicWand = true;
                break;
            }
        }
    }

    public void GoToMainScree()
    {
        CacaduBtn.GetComponent<Outline>().enabled = true;
        GiftBtn.GetComponent<Outline>().enabled = false;
        DrawBtn.GetComponent<Outline>().enabled = false;
        InfoBtn.GetComponent<Outline>().enabled = false;

        MainScreen.SetActive(true);
        ButtonScreen.SetActive(true);
        InformationScreen.SetActive(false);
        ScreenForDrawing.SetActive(false);
        BuyScreen.SetActive(false);
        LearningScreen.SetActive(false);
        GiftScreen.SetActive(false);
        DrawScreen.SetActive(false);
}

    public void GoToInfoScreen()
    {
        CacaduBtn.GetComponent<Outline>().enabled = false;
        GiftBtn.GetComponent<Outline>().enabled = false;
        DrawBtn.GetComponent<Outline>().enabled = false;
        InfoBtn.GetComponent<Outline>().enabled = true;

        MainScreen.SetActive(false);
        ButtonScreen.SetActive(true);
        InformationScreen.SetActive(true);
        ScreenForDrawing.SetActive(false);
        BuyScreen.SetActive(false);
        LearningScreen.SetActive(false);
        GiftScreen.SetActive(false);
        DrawScreen.SetActive(false);
    }

    public void GoToBuyScreen()
    {
        CacaduBtn.GetComponent<Outline>().enabled = false;
        GiftBtn.GetComponent<Outline>().enabled = false;
        DrawBtn.GetComponent<Outline>().enabled = false;
        InfoBtn.GetComponent<Outline>().enabled = false;

        MainScreen.SetActive(false);
        ButtonScreen.SetActive(false);
        InformationScreen.SetActive(false);
        ScreenForDrawing.SetActive(false);
        BuyScreen.SetActive(true);
        LearningScreen.SetActive(false);
        GiftScreen.SetActive(false);
        DrawScreen.SetActive(false);
    }

    public void CloseBuyScreenOrGoToDrawingScreen()
    {
        CacaduBtn.GetComponent<Outline>().enabled = false;
        GiftBtn.GetComponent<Outline>().enabled = false;
        DrawBtn.GetComponent<Outline>().enabled = false;
        InfoBtn.GetComponent<Outline>().enabled = false;

        MainScreen.SetActive(false);
        ButtonScreen.SetActive(false);
        InformationScreen.SetActive(false);
        ScreenForDrawing.SetActive(true);
        BuyScreen.SetActive(false);
        LearningScreen.SetActive(false);
        GiftScreen.SetActive(false);
        DrawScreen.SetActive(false);
    }

    public void GoToLearningScreen()
    {
        CacaduBtn.GetComponent<Outline>().enabled = false;
        GiftBtn.GetComponent<Outline>().enabled = false;
        DrawBtn.GetComponent<Outline>().enabled = false;
        InfoBtn.GetComponent<Outline>().enabled = false;

        MainScreen.SetActive(false);
        ButtonScreen.SetActive(false);
        InformationScreen.SetActive(false);
        ScreenForDrawing.SetActive(true);
        BuyScreen.SetActive(false);
        LearningScreen.SetActive(true);
        GiftScreen.SetActive(false);
        DrawScreen.SetActive(false);
    }

    public void GoToDrawScreen()
    {
        CacaduBtn.GetComponent<Outline>().enabled = false;
        GiftBtn.GetComponent<Outline>().enabled = false;
        DrawBtn.GetComponent<Outline>().enabled = true;
        InfoBtn.GetComponent<Outline>().enabled = false;

        MainScreen.SetActive(false);
        ButtonScreen.SetActive(true);
        InformationScreen.SetActive(false);
        ScreenForDrawing.SetActive(false);
        BuyScreen.SetActive(false);
        LearningScreen.SetActive(false);
        GiftScreen.SetActive(false);
        DrawScreen.SetActive(true);
    }

    public void GoToGiftScreen()
    {
        CacaduBtn.GetComponent<Outline>().enabled = false;
        GiftBtn.GetComponent<Outline>().enabled = true;
        DrawBtn.GetComponent<Outline>().enabled = false;
        InfoBtn.GetComponent<Outline>().enabled = false;

        MainScreen.SetActive(false);
        ButtonScreen.SetActive(true);
        InformationScreen.SetActive(false);
        ScreenForDrawing.SetActive(false);
        BuyScreen.SetActive(false);
        LearningScreen.SetActive(false);
        GiftScreen.SetActive(true);
        DrawScreen.SetActive(false);
    }

    public void ClickSettings()
    {
        IsSettingClick = !IsSettingClick;
        SettingsScreen.SetActive(IsSettingClick);
    }
}
