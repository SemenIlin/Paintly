using UnityEngine;
using UnityEngine.UI;

public class ScrollPalitra : MonoBehaviour
{
    public PictureItem PictureItem;

    [Range(0f, 20f)]
    public float SnapSpeed;

    public float OffsetX;
    public float Spacing;

    private RectTransform contentRect;
    private Vector2 contentVector;

    public ScrollRect scrollRect;

    private float endPositionX = 0;
    private bool IsScroll;
    
    void Start()
    {
        contentRect = GetComponent<RectTransform>();

        for (int i = 0; i < PictureItem.ColorPrefabs.Count; ++i)
        {
            PictureItem.ColorPrefabs[i].transform.localPosition = Vector2.zero;
            if (i == 0)
            {
                PictureItem.ColorPrefabs[i].transform.localPosition = new Vector2(OffsetX, 0);
                continue;
            }

            PictureItem.ColorPrefabs[i].transform.localPosition = new Vector2(PictureItem.ColorPrefabs[i-1].transform.localPosition.x + PictureItem.ColorPrefabs[i-1].GetComponent<RectTransform>().sizeDelta.x + Spacing,
                0);
        }
    }

    private void FixedUpdate()
    {
        if(PictureItem.ColorPrefabs.Count < 1)
        {
            return;
        }
        endPositionX = PictureItem.ColorPrefabs[PictureItem.ColorPrefabs.Count - 1].GetComponent<RectTransform>().anchoredPosition.x ;
        if (contentRect.anchoredPosition.x > 0 || Mathf.Abs(contentRect.anchoredPosition.x) > endPositionX)
        {
            scrollRect.inertia = false;
        }
        else
        {
            scrollRect.inertia = true;
        }

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (!IsScroll && scrollVelocity < 200)
            scrollRect.inertia = false;

        if (contentRect.anchoredPosition.x > 0)
        {
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, 0, SnapSpeed * Time.fixedDeltaTime);

            contentRect.anchoredPosition = contentVector;
        }
        if (Mathf.Abs(contentRect.anchoredPosition.x) > endPositionX + OffsetX)
        {
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, -endPositionX - OffsetX, SnapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }

       
    }

    public void Scroll(bool isScroll)
    {
        IsScroll = isScroll;
        if(isScroll) 
            scrollRect.inertia = true;
    }
}
