using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomPan : MonoBehaviour
{
    public float zoomMin = 1;
    public float zoomMax = 8;

    private Vector3 touch;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchOne.deltaPosition;
            Vector2 touchOneLastpos = touchOne.position - touchOne.deltaPosition;

            float disTouh = (touchZeroLastPos - touchOneLastpos).magnitude;
            float currentDisTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDisTouch - disTouh;

            Zoom(difference * 0.01f);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = touch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }

        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomMin, zoomMax); 
    }
}
