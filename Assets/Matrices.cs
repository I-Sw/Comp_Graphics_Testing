using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour
{
    Matrix4x4 translation, rotation, scale, viewing, projection, allTransform, finalMatrix;
    public List<Vector3> vertices;

    // Start is called before the first frame update
    void Start()
    {      
        vertices = new List<Vector3>();
        add_vertices();

        rotation = Matrix4x4.Rotate(Quaternion.AngleAxis(13f, new Vector3(19,0,0).normalized));
        scale = Matrix4x4.Scale(new Vector3(15, 3, 4));
        translation = Matrix4x4.Translate(new Vector3(-2, 1, -3));
        viewing = Matrix4x4.LookAt(new Vector3(17, -1, 46), new Vector3(0, 19, 0), new Vector3(1, 0, 19));
        projection = Matrix4x4.Perspective(60, 1, 1, 100);

        Debug.Log("\n\nRotation Matrix\n" + rotation);
        Debug.Log("\n\nScale Matrix\n" + scale);
        Debug.Log("\n\nTranslation Matrix\n" + translation);
        Debug.Log("\n\nViewing Matrix\n" + viewing);
        Debug.Log("\n\nProjection Matrix\n" + projection);

        allTransform = (translation * scale * rotation);
        Debug.Log("Single Matrix for transformations\n" + allTransform);

        finalMatrix = (projection * viewing * translation * scale * rotation);
        Debug.Log("Single Matrix for everything\n" + finalMatrix);

        /*
        vertices = multiplyVertices(rotation);
        Debug.Log("Rotated\n\n" + verticesAsString());

        vertices = multiplyVertices(scale);
        Debug.Log("Rotated & Scaled\n\n" + verticesAsString());
        */

        vertices = multiplyVertices(translation);
        Debug.Log("Rotated, Scaled & Translated\n\n" + verticesAsString());

        vertices = multiplyVertices(viewing);
        Debug.Log("Rotated, Scaled, Translated & Viewed\n\n" + verticesAsString());

        vertices = multiplyVertices(projection);
        Debug.Log("Rotated, Scaled, Translated, Viewed & Projected\n\n" + verticesAsString());

        vertices.Clear();
        add_vertices();
        vertices = multiplyVertices(allTransform);
        Debug.Log("Image After Transformation\n\n" + verticesAsString());

        vertices.Clear();
        add_vertices();
        vertices = multiplyVertices(finalMatrix);
        Debug.Log("Image After Full Matrix\n\n" + verticesAsString());

        finalImageCoords();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void add_vertices()
    {
        vertices.Add(new Vector3(-3, -4, 1));      //0
        vertices.Add(new Vector3(3, -4, 1));       //1
        vertices.Add(new Vector3(2, -2, 1));       //2
        vertices.Add(new Vector3(1, -2, 1));       //3
        vertices.Add(new Vector3(1, 2, 1));        //4
        vertices.Add(new Vector3(2, 2, 1));        //5
        vertices.Add(new Vector3(3, 4, 1));        //6
        vertices.Add(new Vector3(-3, 4, 1));       //7
        vertices.Add(new Vector3(-2, 2, 1));       //8
        vertices.Add(new Vector3(-1, 2, 1));       //9
        vertices.Add(new Vector3(-1, -2, 1));      //10
        vertices.Add(new Vector3(-2, -2, 1));      //11
        vertices.Add(new Vector3(-3, -4, -1));     //12
        vertices.Add(new Vector3(3, -4, -1));      //13
        vertices.Add(new Vector3(2, -2, -1));      //14
        vertices.Add(new Vector3(1, -2, -1));      //15
        vertices.Add(new Vector3(-1, 2, -1));       //16
        vertices.Add(new Vector3(2, 2, -1));       //17
        vertices.Add(new Vector3(3, 4, -1));       //18
        vertices.Add(new Vector3(-3, 4, -1));      //19
        vertices.Add(new Vector3(-2, 2, -1));      //20
        vertices.Add(new Vector3(1, 2, -1));      //21
        vertices.Add(new Vector3(-1, -2, -1));     //22
        vertices.Add(new Vector3(-2, -2, -1));     //23
    }

    private List<Vector3> multiplyVertices(Matrix4x4 matrix)
    {
        List<Vector3> productVertices = new List<Vector3>();

        foreach(Vector3 vertex in vertices)
        {
            productVertices.Add(matrix.MultiplyVector(vertex));           
        }

        return productVertices;
    }

    private string verticesAsString()
    {
        string output = "";

        foreach (Vector3 vertex in vertices)
        {
            output += (vertex + ", ");
        }

        return output;
    }

    private void finalImageCoords()
    {
        string xCoords = "";
        string yCoords = "";
        string zCoords = "";

        foreach (Vector3 vertex in vertices)
        {
            xCoords += (vertex.x + "\n ");
            yCoords += (vertex.y + "\n ");
            zCoords += (vertex.z + "\n ");
        }

        Debug.Log("Coords for Final Image:\n X\n" + xCoords + "\n\nY\n" + yCoords + "\n\nZ\n" + zCoords);
    }
}
