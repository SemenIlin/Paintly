using System.Collections.Generic;
using UnityEngine;

public class SwipeScreen : MonoBehaviour
{
    [Range(0f, 20f)]
    public float SnapSpeed;
    public List<GameObject> ModalScreens = new List<GameObject>();
    public GameObject ModalScreen;

    private RectTransform contentRect;
    private Vector2 contentVector;
    [SerializeField]
    public static int selectedID { get; set; }

    private float interval = 0;

    void Start()
    {
        selectedID = 0;
        contentRect = GetComponent<RectTransform>();

        for (int i = 0; i < ModalScreens.Count; ++i)
        {
            Instantiate(ModalScreens[i], transform, false);
            ModalScreens[i].transform.localPosition = Vector2.zero;
            if (i == 0)
            {
                continue;
            }

            ModalScreens[i].transform.localPosition = new Vector2(ModalScreens[i - 1].transform.localPosition.x + ModalScreens[i - 1].GetComponent<RectTransform>().sizeDelta.x,
                ModalScreens[i - 1].transform.localPosition.y);
        }
    }

    private void FixedUpdate()
    {

        for(int i = 0; i < ModalScreens.Count; ++i)
        {
            if(ModalScreens.Count == 1)
            {
                if(Mathf.Abs(contentRect.anchoredPosition.x) > ModalScreens[i].transform.localPosition.x)
                {
                    ModalScreen.SetActive(false);
                    contentRect.anchoredPosition = Vector2.zero;
                }
            }
            else
            {
                interval = (ModalScreens[1].transform.localPosition.x + ModalScreens[0].transform.localPosition.x) / 2;
                if (contentRect.anchoredPosition.x > ModalScreens[0].transform.localPosition.x + interval ||
                    Mathf.Abs(contentRect.anchoredPosition.x) > ModalScreens[ModalScreens.Count - 1].transform.localPosition.x + interval)
                {
                    ModalScreen.SetActive(false);
                    contentRect.anchoredPosition = Vector2.zero;
                }

                if (!ClickToNext.onClick)
                {
                    if (Mathf.Abs(contentRect.anchoredPosition.x) > ModalScreens[i].transform.localPosition.x - interval &&
                        Mathf.Abs(contentRect.anchoredPosition.x) < ModalScreens[i].transform.localPosition.x + interval)                       
                    {
                        selectedID = i;
                    }
                }                
            }
        }

        if (selectedID == ModalScreens.Count || selectedID < 0)
        {
            ModalScreen.SetActive(false);
            selectedID = 0;
            contentRect.anchoredPosition = Vector2.zero;
        }
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x,
                                   -ModalScreens[selectedID].transform.localPosition.x,
                                   SnapSpeed * Time.fixedDeltaTime);

        contentRect.anchoredPosition = contentVector;
        if (-ModalScreens[selectedID].transform.localPosition.x > contentRect.anchoredPosition.x - 30)
        {
            ClickToNext.onClick = false;
        }   
    }   
}
