using UnityEngine;

public class ClickToNext : MonoBehaviour
{
    public static bool onClick = false;
    public void ClickNext()
    {
        onClick = true;
        SwipeScreen.selectedID += 1;
    }
}
