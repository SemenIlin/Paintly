using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMainScreen : MonoBehaviour
{
    [Range(0f, 20f)]
    public float SnapSpeed;

    public float OffsetY;
    public float Spacing;
    public List<GameObject> Categories = new List<GameObject>();

    private RectTransform contentRect;
    private Vector2 contentVector;

    public ScrollRect scrollRect;

    private float endPositionY = 0;
    private bool IsScroll;

    void Start()
    {
        contentRect = GetComponent<RectTransform>();

        for (int i = 0; i < Categories.Count; ++i)
        {
            Categories[i].transform.localPosition = Vector2.zero;
            if (i == 0)
            {
                Categories[i].transform.localPosition = new Vector2(Categories[i].transform.localPosition.x, 0);
                continue;
            }

            Categories[i].transform.localPosition = new Vector2(Categories[i - 1].transform.localPosition.x,
                Categories[i - 1].transform.localPosition.y - Categories[i].GetComponent<RectTransform>().sizeDelta.y - Spacing);
        }

        contentRect.anchoredPosition = new Vector2(0, 200);
    }

    private void FixedUpdate()
    {
        endPositionY = Categories[Categories.Count - 1].GetComponent<RectTransform>().anchoredPosition.y;
        if (contentRect.anchoredPosition.y < 200 || Mathf.Abs(contentRect.anchoredPosition.y) > Mathf.Abs(endPositionY+OffsetY))
        {
            scrollRect.inertia = false;
        }
        else
        {
            scrollRect.inertia = true;
        }

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (!IsScroll && scrollVelocity < 200)
            scrollRect.inertia = false;

        if (contentRect.anchoredPosition.y < OffsetY)
        {
            contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, OffsetY, SnapSpeed * Time.fixedDeltaTime);

            contentRect.anchoredPosition = contentVector;
        }
        if (contentRect.anchoredPosition.y > Mathf.Abs(endPositionY + OffsetY))
        {
            contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, Mathf.Abs(endPositionY + OffsetY), SnapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
    }

    public void Scrolling(bool isScroll)
    {
        IsScroll = isScroll;
        if(isScroll)
            scrollRect.inertia = true;
    }
}
