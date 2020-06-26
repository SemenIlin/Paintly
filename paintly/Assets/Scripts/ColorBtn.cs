using UnityEngine;
using UnityEngine.UI;

public class ColorBtn : MonoBehaviour
{
    public int ToltalCountColors { get; set; }
    public int CurrentCountFilled { get; set; } = 0;

    public bool IsSelect { get; set; }
    public Image Progress;
    public Text NumberColor;

    public bool IsUsedMagicWand { get; set; } = false;
    public bool IsUsedMagicSearch { get; set; } = false;

    public Image GetProgress()
    {
        return Progress;
    }

    public Text GetText()
    {
        return NumberColor;
    }
}
