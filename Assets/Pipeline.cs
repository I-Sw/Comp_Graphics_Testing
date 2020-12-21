using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    While running the following hotkeys toggle the corresponding functions:
    B - Back Face Culling
    F - Fill
    R - Rotation
    T - Full Letter or Single Triangle View
    (Up and Down Arrow Keys can cycle through selected triangles)

    The selected options will be displayed in green on the UI as well as the number of the current selected triangle
*/


public class Pipeline : MonoBehaviour
{
    #region Declaring Required Fields

    public GameObject plane;
    public Text fillText, bfcText, triangleText, rotationText;
    Texture2D our_texture;
    Renderer renderer;
    Model letter;

    Matrix4x4 translation, rotation, scale, viewing, projection;
    private float angle;
    private bool DoFill, BackFaceCulling, LoadTrianglesIndividually, DoRotation;
    private int TriangleToLoad;
    #endregion

    //Contains Start and Update methods, as well as the method to create our gameobject
    #region Start, Update and GO Creation    

    // Start is called before the first frame update
    void Start()
    {
        letter = new Model();
        BackFaceCulling = false;
        DoFill = false;
        LoadTrianglesIndividually = true;
        TriangleToLoad = 0;

        our_texture = new Texture2D(128, 128);
        renderer = plane.GetComponentInChildren<Renderer>();
        renderer.material.mainTexture = our_texture;

        Vector2Int testStart = new Vector2Int(100, 35);
        Vector2Int testEnd = new Vector2Int(110, 27);
      
        List<Vector2Int> testList = rasterise(testStart, testEnd);
        draw_pixels_to_texture(testList);


        //CreateUnityGameObject(letter);

        if (LoadTrianglesIndividually)
            triangleText.color = Color.green;
        else
            triangleText.color = Color.red;


        if (DoFill)
            fillText.color = Color.green;
        else
            fillText.color = Color.red;


        if (BackFaceCulling)
            bfcText.color = Color.green;
        else
            bfcText.color = Color.red;

        if (DoRotation)
            rotationText.color = Color.green;
        else
            rotationText.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(our_texture);
        our_texture = new Texture2D(128, 128);
        renderer.material.mainTexture = our_texture;

        if(DoRotation)
        {
            angle++;
        }

        Matrix4x4 super_Matrix = Matrix4x4.Perspective(45.0f, 1, -1, 1000.0f) * Matrix4x4.TRS(new Vector3(0,0,-10), Quaternion.LookRotation(Vector3.forward, Vector3.up), Vector3.one) * Matrix4x4.Rotate(Quaternion.AngleAxis(angle, (new Vector3(3, 2, 4)).normalized));
        List<Vector3> transformed_vertices = multiplyVertices(super_Matrix, letter._vertices);
        List<Vector2> viewport_verts_2D = get_2D_viewport(transformed_vertices);
       
        if(LoadTrianglesIndividually)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (TriangleToLoad < (letter._index_list.Count - 3))
                    TriangleToLoad += 3;

                if (TriangleToLoad == (letter._index_list.Count - 3))
                    TriangleToLoad = 0;

                triangleText.text = (TriangleToLoad / 3).ToString();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (TriangleToLoad > 0)
                    TriangleToLoad -= 3;

                if (TriangleToLoad == 0)
                    TriangleToLoad = 105;

                triangleText.text = (TriangleToLoad / 3).ToString();
            }

            clip_and_rasterise(viewport_verts_2D[letter._index_list[TriangleToLoad]], viewport_verts_2D[letter._index_list[TriangleToLoad + 1]], viewport_verts_2D[letter._index_list[TriangleToLoad + 2]]);
        }

        else
        {
            for (int i = 0; i < letter._index_list.Count-2; i+=3)
            {
                clip_and_rasterise(viewport_verts_2D[letter._index_list[i]], viewport_verts_2D[letter._index_list[i + 1]], viewport_verts_2D[letter._index_list[i + 2]]);
            }
        }

        if (Input.GetKey(KeyCode.P))
        {
            foreach(Vector2 point in viewport_verts_2D)
            {
                Vector2Int newPoint = convert_to_pixel_space(point);
                drawPixel(newPoint.x, newPoint.y);
            }
            our_texture.Apply();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            LoadTrianglesIndividually = !LoadTrianglesIndividually;

            if (LoadTrianglesIndividually)
                triangleText.color = Color.green;
            else
                triangleText.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            DoFill = !DoFill;

            if (DoFill)
                fillText.color = Color.green;
            else
                fillText.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BackFaceCulling = !BackFaceCulling;

            if(BackFaceCulling)           
                bfcText.color = Color.green;
            else
                bfcText.color = Color.red;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DoRotation = !DoRotation;

            if (DoRotation)
                rotationText.color = Color.green;
            else
                rotationText.color = Color.red;
        }
    }

    public GameObject CreateUnityGameObject(Model model)
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();
        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normals = new List<Vector3>();

        for (int i = 0; i <= model._index_list.Count - 3; i = i + 3)
        {
            Vector3 normal_for_face = model._face_normals[i / 3];
            normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);
            coords.Add(model._vertices[model._index_list[i]]); dummy_indices.Add(i); text_coords.Add(model._texture_coordinates[model._texture_index_list[i]]); normals.Add(normal_for_face);
            coords.Add(model._vertices[model._index_list[i + 1]]); dummy_indices.Add(i + 1); text_coords.Add(model._texture_coordinates[model._texture_index_list[i + 1]]); normals.Add(normal_for_face);
            coords.Add(model._vertices[model._index_list[i + 2]]); dummy_indices.Add(i + 2); text_coords.Add(model._texture_coordinates[model._texture_index_list[i + 2]]); normals.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        mesh.uv = text_coords.ToArray();
        mesh.normals = normals.ToArray(); ;

        mesh_renderer.material.mainTexture = our_texture;

        mesh_filter.mesh = mesh;
        return newGO;
    }
    #endregion

    //Contains methods required by regions below
    #region Required Operations & Conversions

    public static List<Vector3> multiplyVertices(Matrix4x4 matrix, List<Vector3> verts)
    {
        List<Vector3> productVertices = new List<Vector3>();

        foreach (Vector3 vertex in verts)
        {
            productVertices.Add(matrix * new Vector4(vertex.x, vertex.y, vertex.z, 1));
        }

        return productVertices;
    }

    private void draw_from_viewport_space(Vector2 startv, Vector2 endv)
    {
        Vector2Int start = convert_to_pixel_space(startv);
        Vector2Int end = convert_to_pixel_space(endv);

        draw_pixels_to_texture(rasterise(start, end));
    }

    private Vector2Int convert_to_pixel_space(Vector2 startv)
    {
        return new Vector2Int((int)((startv.x + 1) * 127 / 2), (int)((startv.y + 1) * 127 / 2)/**/);
    }

    private List<Vector2> get_2D_viewport(List<Vector3> vertices)
    {
        List<Vector2> verts_viewport_space =new  List<Vector2>();
        foreach (Vector3 v in vertices)
            verts_viewport_space.Add(new Vector2(v.x / v.z, v.y / v.z));

        return verts_viewport_space;
    }

    private void draw_pixels_to_texture(List<Vector2Int> list_of_pixels)
    {       
        foreach(Vector2Int pixel in list_of_pixels)
        {
            our_texture.SetPixel(pixel.x, pixel.y, Color.red);
        }
        
        our_texture.Apply();
    }
    #endregion
    
    //Contains code required for and related to line clipping
    #region Line Clipping
    
    private bool line_clip(ref Vector2 start, ref Vector2 end)
    {
        Outcode startOutcode = new Outcode(start), endOutcode = new Outcode(end), inViewportOutcode = new Outcode();

        if((startOutcode == inViewportOutcode) && (endOutcode == inViewportOutcode))
        {
            //print("Trivial Acceptance");
            return true;
        }

        if ((startOutcode * endOutcode) != inViewportOutcode)
        {
            //print("Trivial Rejection");
            return false;
        }

        //If not trivial accept,
        if (startOutcode == inViewportOutcode)
        {
            return (line_clip(ref end, ref start));
        }

        if (startOutcode.up)
        {
            start = intercept(start, end, 0);
            return line_clip(ref start, ref end);
        }

        if (startOutcode.down)
        {
            start = intercept(start, end, 1);
            return line_clip(ref start, ref end);
        }

        if (startOutcode.left)
        {
            start = intercept(start, end, 2);
            return line_clip(ref start, ref end);
        }

        if (startOutcode.right)
        {
            start = intercept(start, end, 3);
            return line_clip(ref start, ref end);
        }

        //Reversed to clip end using the same code
        line_clip(ref end, ref start);

        return false;
    }

    Vector2 intercept(Vector2 start, Vector2 end, int edge)
    {       
        float slope = (end.y - start.y) / (end.x - start.x);

        switch(edge)
        {
            case 0: //U
                return new Vector2(start.x + (1 / slope) * (1 - start.y), 1);

            case 1: //Down
                return new Vector2(start.x + (1 / slope) * (-1 - start.y), -1);

            case 2: //Left
                return new Vector2(-1, start.y + slope* (-1 - start.x));

            default: //Right
                return new Vector2(1, start.y + slope * (1 - start.x));

        }
    }
    #endregion

    //Contains code required for and related to rasterisation
    #region Rasterisation

    List<Vector2Int> rasterise(Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> output = new List<Vector2Int>();

        int dx = end.x - start.x;
        if (dx < 0)
        {
            return rasterise(end, start);
        }

        int dy = end.y - start.y;
        if (dy < 0)
        {
            return NegY(rasterise(NegY(start), NegY(end)));
        }

        if (dy > dx)
        {
            return SwapXY(rasterise(SwapXY(start), SwapXY(end)));
        }

        int dy2 = 2 * dy;  // positive
        int dydx2 = 2 * (dy - dx);  // negative
        int p = 2 * dy - dx;
        int y = start.y;
        for (int x = start.x; x <= end.x; x++)
        {
            output.Add(new Vector2Int(x, y));
            if (p > 0)
            {
                y++;
                p += dydx2;
            }
            else
                p += dy2;
        }
        return output;
    }

    private Vector2Int SwapXY(Vector2Int v)
    {
        return new Vector2Int(v.y, v.x);
    }

    private List<Vector2Int> SwapXY(List<Vector2Int> list)
    {
        List<Vector2Int> output = new List<Vector2Int>();
        foreach (Vector2Int v in list)
            output.Add(SwapXY(v));
        return output;
    }

    private Vector2Int NegY(Vector2Int v)
    {
        return new Vector2Int(v.x, -v.y);
    }

    private List<Vector2Int> NegY(List<Vector2Int> list)
    {
        List<Vector2Int> output = new List<Vector2Int>();
        foreach (Vector2Int v in list)
            output.Add(NegY(v));
        return output;
    }
    #endregion

    //Contains code required for and related to our fill algorithm
    #region Fill Algorithm

    private void fill(Vector2Int pixel, Color newColor)
    {
        if (shouldDrawPixel(pixel, newColor))
        {
            our_texture.SetPixel(pixel.x, pixel.y, newColor);
            our_texture.Apply();
            //Debug.Log(our_texture.GetPixel(pixel.x, pixel.y));
        }

        Vector2Int newPixel = new Vector2Int((pixel.x + 1), pixel.y);
        if(shouldDrawPixel(newPixel, newColor))
            fill(newPixel, newColor);

        
        newPixel = new Vector2Int(pixel.x, (pixel.y + 1));
        if(shouldDrawPixel(newPixel, newColor))
            fill(newPixel, newColor);
            

        newPixel = new Vector2Int((pixel.x - 1), pixel.y);
        if(shouldDrawPixel(newPixel, newColor))
            fill(newPixel, newColor);
        
        newPixel = new Vector2Int(pixel.x, (pixel.y - 1));
        if(shouldDrawPixel(newPixel, newColor))
            fill(newPixel, newColor);            
    }

    private bool shouldDrawPixel(Vector2Int pixel, Color newColor)
    {
        if((pixel.x < 0) || (pixel.y < 0) || (pixel.x >= our_texture.width) || (pixel.y >= our_texture.height))
        {
            return false;
        }

        return our_texture.GetPixel(pixel.x, pixel.y) != newColor;
    }
    #endregion

    //Contains code required for and required to draw our processed graphics to the texture
    #region Drawing to Texture

    private void clip_and_rasterise(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        Vector3 p1 = v1, p2 = v2, p3 = v3;
        if(BackFaceCulling)
        {
            if(Vector3.Cross(p2 - p1, p3 - p2).z < 0)
            {
                drawTriangle(v1, v2, v3);
            }
        }

        else
        {
            drawTriangle(v1, v2, v3);
        }
    }

    private void drawTriangle(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        Vector2 start = v1, end = v2;
        if (line_clip(ref start, ref end)) draw_from_viewport_space(start, end);

        Vector2 start2 = v2, end2 = v3;
        if (line_clip(ref start2, ref end2)) draw_from_viewport_space(start2, end2);

        Vector2 start3 = v3, end3 = v1;
        if (line_clip(ref start3, ref end3)) draw_from_viewport_space(start3, end3);

        if(DoFill)
        {
            //Filling
            float centerX = (v1.x + v2.x + v3.x) / 3;
            float centerY = (v1.y + v2.y + v3.y) / 3;
            Vector2 centerPoint = new Vector2(centerX, centerY);
            Vector2Int centerPoint2 = convert_to_pixel_space(centerPoint);
            //Debug.Log("Center: " + centerPoint);
            if(our_texture.GetPixel(centerPoint2.x, centerPoint2.y) != Color.red)
            {
                fill(centerPoint2, Color.red);
            }           
        }


    }

    private void drawPixel(int x, int y)
    {
        our_texture.SetPixel(x, y, Color.blue);
    }
    #endregion
}