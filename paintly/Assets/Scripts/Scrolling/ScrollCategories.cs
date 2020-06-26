using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollCategories : MonoBehaviour
{
    [Range(0f, 20f)]
    public float SnapSpeed;

    public float OffsetX;
    public float Spacing;
    public List<GameObject> Pictures = new List<GameObject>();

    private RectTransform contentRect;
    private Vector2 contentVector;

    public ScrollRect scrollRect;
 
    private float endPositionX = 0;

    void Start()
    {
        contentRect = GetComponent<RectTransform>();

        for (int i = 0; i < Pictures.Count; ++i)
        {
            //Instantiate(Pictures[i], transform, false);
            Pictures[i].transform.localPosition = Vector2.zero;
            if (i == 0)
            {
                Pictures[i].transform.localPosition = new Vector2(OffsetX, Pictures[i].transform.localPosition.y);
                continue;
            }

            Pictures[i].transform.localPosition = new Vector2(Pictures[i - 1].transform.localPosition.x + Pictures[i - 1].GetComponent<RectTransform>().sizeDelta.x + Spacing,
                Pictures[i - 1].transform.localPosition.y);
        }
    }

    private void FixedUpdate()
    {
        endPositionX = Pictures[Pictures.Count - 1].GetComponent<RectTransform>().anchoredPosition.x + OffsetX;
        if(contentRect.anchoredPosition.x > 0 || Mathf.Abs(contentRect.anchoredPosition.x) > endPositionX)
        {
            scrollRect.inertia = false;
        }
        else
        {
            scrollRect.inertia = true;
        }

        if (contentRect.anchoredPosition.x > 0)
        {
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, 0, SnapSpeed * Time.fixedDeltaTime);

            contentRect.anchoredPosition = contentVector;
        }
        if (Mathf.Abs(contentRect.anchoredPosition.x) > endPositionX )
        {
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, -endPositionX, SnapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
    }
}