using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapScrolling : MonoBehaviour
{
    [Range(1f,500F)]
    public float OffsetY;
    [Range(0f, 20f)]
    public float SnapSpeed;
    public List<GameObject> products = new List<GameObject>();
    
    private RectTransform contentRect;
    private Vector2 contentVector;

    [SerializeField]
    private bool isScrolling; 
    private float OffsetPostion = 177f;
    
    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        contentRect.anchoredPosition = new Vector2(-283, 0);
        contentVector.x = -283f;
        for (int i = 1; i < products.Count; ++i)
        {
				
		    Instantiate(products[i], transform, false);
			
            products[i].transform.localPosition = Vector2.zero;
            if (i == 0)
            {
                products[i].transform.localPosition = new Vector2(products[i].transform.localPosition.x,
                    products[i].transform.localPosition.y + OffsetPostion); 
                continue;
            }

            products[i].transform.localPosition = new Vector2(products[i-1].transform.localPosition.x,
                products[i-1].transform.localPosition.y - products[i-1].GetComponent<RectTransform>().sizeDelta.y - OffsetY);
            
        }
    }

    private void FixedUpdate()
    {
        float maxDistance = Mathf.Abs(products[products.Count - 1].transform.localPosition.y) - 200;
       
        if (contentRect.anchoredPosition.y < 0)
        {
            contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, 0, SnapSpeed * Time.fixedDeltaTime);

            contentRect.anchoredPosition = contentVector;
        }
        if(contentRect.anchoredPosition.y > maxDistance)
        {
            contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, maxDistance, SnapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
    }
}
