using UnityEngine;

public class Sigment : MonoBehaviour
{
    private PictureItem pictureItem;
    private MeshFilter meshFilter;
    private LineRenderer lineRenderer;
    private RectTransform rectTransform;

    public bool IsFilled { get; private set; } = false;

    private const float HEIGHT_SCREEN = 300;
    private const float WIDTH_SCREEN = 600;

    private const float NEW_HEIGHT_SCREEN = 300;
    private const float NEW_WIDTH_SCREEN = 600;

    void Start()
    {
        pictureItem = transform.parent.parent.gameObject.
            GetComponent<PictureItem>();
        if(pictureItem == null)
        {
            pictureItem = transform.parent.parent.parent.gameObject.
            GetComponent<PictureItem>();
        }

        Mesh mesh = new Mesh();
        lineRenderer = GetComponent<LineRenderer>();

        rectTransform = GetComponent<RectTransform>();
        var scale = NEW_HEIGHT_SCREEN / HEIGHT_SCREEN;
        GameObject parentComponent = transform.parent.gameObject;
        if (parentComponent.GetComponent<MeshRenderer>() != null)
        {
            rectTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rectTransform.localScale = new Vector3(scale, scale, scale);
        }

        int countVertex = lineRenderer.positionCount;
        Vector3[] vertices = new Vector3[countVertex];

        int countVertexTriangl = (countVertex - 2) * 3;
        int[] triangles;
        if (lineRenderer.positionCount > 2)
        {
            triangles = new int[countVertexTriangl];
        }
        else
        {
            triangles = new int[0];
        }

        int createdTriangle = 0;

        for (int i = 0; i < countVertex; ++i)
        {
            Vector3 lineRenderPosition = lineRenderer.GetPosition(i);
            lineRenderPosition.z = 0;

            lineRenderer.SetPosition(i, lineRenderPosition);
            vertices[i] = lineRenderer.GetPosition(i);
        }

        for (int i = 0; i < countVertexTriangl; ++i)
        {
            if (i % 3 == 0 && i + 2 < countVertexTriangl)
            {
                triangles[i] = 0;
                triangles[i + 1] = createdTriangle + 1;
                triangles[i + 2] = createdTriangle + 2;

                ++createdTriangle;
            }
        }
        lineRenderer.enabled = false;

        Vector2[] uv = new Vector2[countVertex];

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
   }

    private void OnMouseDown()
    {
        if (pictureItem.ColorPrefabs[ButtonsController.SelectedButton].GetComponent<ColorBtn>().IsSelect)
        {            
            if (pictureItem.ColorPrefabs[ButtonsController.SelectedButton].tag == transform.tag)
            {
                if(!this.IsFilled)
                    pictureItem.ColorPrefabs[ButtonsController.SelectedButton].GetComponent<ColorBtn>().CurrentCountFilled++;

                MeshRenderer[] childComponents;
                GameObject parentComponent = transform.parent.gameObject;// if click on clid component
                if (parentComponent.GetComponent<MeshRenderer>() != null)//if GameObject have anything child components
                {
                    childComponents = parentComponent.GetComponentsInChildren<MeshRenderer>();
                    if (PlayerController.PaintForColoring > 0 &&
                        !childComponents[0].enabled)
                        PlayerController.PaintForColoring -= PriceList.PriceColorFilled;

                    for (int i = 0; i < childComponents.Length; ++i)
                    {
                        childComponents[i].enabled = true; //render from chield to parent
                        parentComponent.GetComponentsInChildren<Sigment>()[i].IsFilled = true;
                    }
                }
                else
                {
                    childComponents = gameObject.GetComponentsInChildren<MeshRenderer>();
                    if (childComponents.Length > 0)
                    {
                        if (PlayerController.PaintForColoring > 0 && 
                            !childComponents[0].enabled)
                            PlayerController.PaintForColoring -= PriceList.PriceColorFilled;

                        for (int i = 0; i < childComponents.Length;++i)
                        {
                            childComponents[i].enabled = true;/// rendere from parent to chield
                            gameObject.GetComponentsInChildren<Sigment>()[i].IsFilled = true;
                        }
                    }
                    else
                    {
                        if (PlayerController.PaintForColoring > 0 &&
                              !gameObject.GetComponent<MeshRenderer>().enabled)
                            PlayerController.PaintForColoring -= PriceList.PriceColorFilled;

                        gameObject.GetComponent<MeshRenderer>().enabled = true;
                        gameObject.GetComponent<Sigment>().IsFilled = true;
                    }
                }
            }
        }
    }
}
