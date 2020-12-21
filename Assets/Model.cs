using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model 
{
    public List<Vector3> _vertices;
    public List<int> _index_list;
    public List<Vector2> _texture_coordinates;
    public List<int> _texture_index_list;
    public List<Vector3> _face_normals;

    public Model()
    {
        initialize_lists();
        add_vertices();
        add_texture_coordinates();
        add_indices_and_normals();       
    }

    private void initialize_lists()
    {
        _vertices = new List<Vector3>();
        _index_list = new List<int>();
        _texture_coordinates = new List<Vector2>();
        _texture_index_list = new List<int>();
        _face_normals = new List<Vector3>();
    }

    private void add_vertices()
    {
        _vertices.Add(new Vector3(-3, -4, 1));      //0
        _vertices.Add(new Vector3(3, -4, 1));       //1
        _vertices.Add(new Vector3(2, -2, 1));       //2
        _vertices.Add(new Vector3(1, -2, 1));       //3
        _vertices.Add(new Vector3(1, 2, 1));        //4
        _vertices.Add(new Vector3(2, 2, 1));        //5
        _vertices.Add(new Vector3(3, 4, 1));        //6
        _vertices.Add(new Vector3(-3, 4, 1));       //7
        _vertices.Add(new Vector3(-2, 2, 1));       //8
        _vertices.Add(new Vector3(-1, 2, 1));       //9
        _vertices.Add(new Vector3(-1, -2, 1));      //10
        _vertices.Add(new Vector3(-2, -2, 1));      //11
        _vertices.Add(new Vector3(-3, -4, -1));     //12
        _vertices.Add(new Vector3(3, -4, -1));      //13
        _vertices.Add(new Vector3(2, -2, -1));      //14
        _vertices.Add(new Vector3(1, -2, -1));      //15
        _vertices.Add(new Vector3(1, 2, -1));       //16
        _vertices.Add(new Vector3(2, 2, -1));       //17
        _vertices.Add(new Vector3(3, 4, -1));       //18
        _vertices.Add(new Vector3(-3, 4, -1));      //19
        _vertices.Add(new Vector3(-2, 2, -1));      //20
        _vertices.Add(new Vector3(-1, 2, -1));      //21
        _vertices.Add(new Vector3(-1, -2, -1));     //22
        _vertices.Add(new Vector3(-2, -2, -1));     //23
    }

    private void add_texture_coordinates()
    {
        _texture_coordinates.Add(new Vector2(2, 0));        //0
        _texture_coordinates.Add(new Vector2(13, 0));       //1
        _texture_coordinates.Add(new Vector2(4, 3));        //2
        _texture_coordinates.Add(new Vector2(6, 3));        //3
        _texture_coordinates.Add(new Vector2(9, 3));        //4
        _texture_coordinates.Add(new Vector2(11, 3));       //5
        _texture_coordinates.Add(new Vector2(4, 8));        //6
        _texture_coordinates.Add(new Vector2(6, 8));        //7
        _texture_coordinates.Add(new Vector2(9, 8));        //8
        _texture_coordinates.Add(new Vector2(11, 8));       //9
        _texture_coordinates.Add(new Vector2(2, 11));       //10
        _texture_coordinates.Add(new Vector2(13, 11));      //11
        _texture_coordinates.Add(new Vector2(2, 13));       //12
        _texture_coordinates.Add(new Vector2(13, 13));      //13
        _texture_coordinates.Add(new Vector2(0, 14));       //14
        _texture_coordinates.Add(new Vector2(15, 14));      //15
        _texture_coordinates.Add(new Vector2(4, 16));       //16
        _texture_coordinates.Add(new Vector2(6, 16));       //17
        _texture_coordinates.Add(new Vector2(9, 16));       //18
        _texture_coordinates.Add(new Vector2(11, 16));      //19
        _texture_coordinates.Add(new Vector2(2, 17));       //20
        _texture_coordinates.Add(new Vector2(13, 17));      //21
        _texture_coordinates.Add(new Vector2(4, 18));       //22
        _texture_coordinates.Add(new Vector2(6, 18));       //23
        _texture_coordinates.Add(new Vector2(9, 18));       //24
        _texture_coordinates.Add(new Vector2(11, 18));      //25
        _texture_coordinates.Add(new Vector2(4, 19));       //26
        _texture_coordinates.Add(new Vector2(6, 19));       //27
        _texture_coordinates.Add(new Vector2(9, 19));       //28
        _texture_coordinates.Add(new Vector2(11, 19));      //29        
        _texture_coordinates.Add(new Vector2(2, 20));       //30
        _texture_coordinates.Add(new Vector2(13, 20));      //31
        _texture_coordinates.Add(new Vector2(4, 21));       //32
        _texture_coordinates.Add(new Vector2(6, 21));       //33
        _texture_coordinates.Add(new Vector2(9, 21));       //34
        _texture_coordinates.Add(new Vector2(11, 21));      //35
        _texture_coordinates.Add(new Vector2(0, 23));       //36
        _texture_coordinates.Add(new Vector2(15, 23));      //37
        _texture_coordinates.Add(new Vector2(2, 24));       //38
        _texture_coordinates.Add(new Vector2(13, 24));      //39
        _texture_coordinates.Add(new Vector2(2, 26));       //40
        _texture_coordinates.Add(new Vector2(13, 26));      //41
    }

    private void add_indices_and_normals()
    {
        _index_list.Add(6);     _texture_index_list.Add(39);
        _index_list.Add(7);     _texture_index_list.Add(38);    _face_normals.Add(new Vector3(0, 1 ,0));
        _index_list.Add(19);    _texture_index_list.Add(40);    // top 1
        _index_list.Add(6);     _texture_index_list.Add(39);
        _index_list.Add(19);    _texture_index_list.Add(40);    _face_normals.Add(new Vector3(0, 1, 0));
        _index_list.Add(18);    _texture_index_list.Add(41);    // top 2

        _index_list.Add(0);     _texture_index_list.Add(12);
        _index_list.Add(11);    _texture_index_list.Add(16);    _face_normals.Add(new Vector3(0, 0, 1));
        _index_list.Add(1);     _texture_index_list.Add(13);    // front bottom 1
        _index_list.Add(1);     _texture_index_list.Add(13);
        _index_list.Add(11);    _texture_index_list.Add(16);    _face_normals.Add(new Vector3(0, 0, 1));
        _index_list.Add(2);     _texture_index_list.Add(19);    // front bottom 2

        _index_list.Add(3);     _texture_index_list.Add(18);
        _index_list.Add(10);    _texture_index_list.Add(17);    _face_normals.Add(new Vector3(0, 0, 1));
        _index_list.Add(9);     _texture_index_list.Add(33);    // front mid 1
        _index_list.Add(3);     _texture_index_list.Add(18);
        _index_list.Add(9);     _texture_index_list.Add(33);    _face_normals.Add(new Vector3(0, 0, 1));
        _index_list.Add(4);     _texture_index_list.Add(34);    // front mid 2

        _index_list.Add(7);     _texture_index_list.Add(38);
        _index_list.Add(6);     _texture_index_list.Add(39);    _face_normals.Add(new Vector3(0, 0, 1));
        _index_list.Add(8);     _texture_index_list.Add(32);    // front top 1
        _index_list.Add(6);     _texture_index_list.Add(39);
        _index_list.Add(5);     _texture_index_list.Add(35);    _face_normals.Add(new Vector3(0, 0, 1));
        _index_list.Add(8);     _texture_index_list.Add(32);    // front top 2

        _index_list.Add(7);     _texture_index_list.Add(38);
        _index_list.Add(8);     _texture_index_list.Add(32);    _face_normals.Add(new Vector3(0, -1, 1));
        _index_list.Add(20);    _texture_index_list.Add(30);    // slanted 1 1
        _index_list.Add(7);     _texture_index_list.Add(38);
        _index_list.Add(20);    _texture_index_list.Add(30);    _face_normals.Add(new Vector3(0, -1, 1));
        _index_list.Add(19);    _texture_index_list.Add(36);    // slanted 1 2

        _index_list.Add(8);     _texture_index_list.Add(32);
        _index_list.Add(9);     _texture_index_list.Add(33);    _face_normals.Add(new Vector3(0, -1, 0));
        _index_list.Add(20);    _texture_index_list.Add(26);    // under 1 1
        _index_list.Add(9);     _texture_index_list.Add(33);
        _index_list.Add(21);    _texture_index_list.Add(27);    _face_normals.Add(new Vector3(0, -1, 0));
        _index_list.Add(20);    _texture_index_list.Add(26);    // under 1 2

        _index_list.Add(5);     _texture_index_list.Add(35);
        _index_list.Add(6);     _texture_index_list.Add(39);    _face_normals.Add(new Vector3(0, 1, 1));
        _index_list.Add(17);    _texture_index_list.Add(31);    // slanted 2 1 
        _index_list.Add(6);     _texture_index_list.Add(39);
        _index_list.Add(18);    _texture_index_list.Add(37);    _face_normals.Add(new Vector3(0, 1, 1));
        _index_list.Add(17);    _texture_index_list.Add(31);    // slanted 2 2

        _index_list.Add(4);     _texture_index_list.Add(34);
        _index_list.Add(5);     _texture_index_list.Add(35);    _face_normals.Add(new Vector3(0, -1, 0));
        _index_list.Add(17);    _texture_index_list.Add(29);    // under 2 1
        _index_list.Add(4);     _texture_index_list.Add(34);
        _index_list.Add(17);    _texture_index_list.Add(29);    _face_normals.Add(new Vector3(0, -1, 0));
        _index_list.Add(16);    _texture_index_list.Add(28);    // under 2 2

        _index_list.Add(0);     _texture_index_list.Add(12);
        _index_list.Add(12);    _texture_index_list.Add(14);    _face_normals.Add(new Vector3(0, 1, 1));
        _index_list.Add(23);    _texture_index_list.Add(20);    // slanted 3 1
        _index_list.Add(0);     _texture_index_list.Add(12);
        _index_list.Add(23);    _texture_index_list.Add(20);    _face_normals.Add(new Vector3(0, 1, 1));
        _index_list.Add(11);    _texture_index_list.Add(16);    // slanted 3 2

        _index_list.Add(10);    _texture_index_list.Add(17);
        _index_list.Add(23);    _texture_index_list.Add(22);    _face_normals.Add(new Vector3(0, 1, 0));
        _index_list.Add(22);    _texture_index_list.Add(23);    // under 3 1
        _index_list.Add(10);    _texture_index_list.Add(17);
        _index_list.Add(11);    _texture_index_list.Add(16);    _face_normals.Add(new Vector3(0, 1, 0));
        _index_list.Add(23);    _texture_index_list.Add(22);    // under 3 2

        _index_list.Add(1);     _texture_index_list.Add(13);
        _index_list.Add(2);     _texture_index_list.Add(19);    _face_normals.Add(new Vector3(0, -1, 1));
        _index_list.Add(14);    _texture_index_list.Add(21);    // slanted 4 1
        _index_list.Add(1);     _texture_index_list.Add(13);
        _index_list.Add(14);    _texture_index_list.Add(21);    _face_normals.Add(new Vector3(0, -1, 1));
        _index_list.Add(13);    _texture_index_list.Add(15);    // slanted 4 2

        _index_list.Add(2);     _texture_index_list.Add(19);
        _index_list.Add(3);     _texture_index_list.Add(18);    _face_normals.Add(new Vector3(0, 1, 0));
        _index_list.Add(14);    _texture_index_list.Add(25);    // under 4 1
        _index_list.Add(3);     _texture_index_list.Add(18);
        _index_list.Add(15);    _texture_index_list.Add(24);    _face_normals.Add(new Vector3(0, 1, 0));
        _index_list.Add(14);    _texture_index_list.Add(25);    // under 4 2

        _index_list.Add(0);     _texture_index_list.Add(12);
        _index_list.Add(1);     _texture_index_list.Add(13);    _face_normals.Add(new Vector3(0, -1, 0));
        _index_list.Add(13);    _texture_index_list.Add(11);    // bottom 1
        _index_list.Add(0);     _texture_index_list.Add(12);
        _index_list.Add(13);    _texture_index_list.Add(11);    _face_normals.Add(new Vector3(0, -1, 0));
        _index_list.Add(12);    _texture_index_list.Add(10);    // bottom 2

        _index_list.Add(12);    _texture_index_list.Add(10);
        _index_list.Add(13);    _texture_index_list.Add(11);    _face_normals.Add(new Vector3(0, 0, -1));
        _index_list.Add(23);    _texture_index_list.Add(6);     // back top 1
        _index_list.Add(13);    _texture_index_list.Add(11);
        _index_list.Add(14);    _texture_index_list.Add(9);     _face_normals.Add(new Vector3(0, 0, -1));
        _index_list.Add(23);    _texture_index_list.Add(6);     // back top 2

        _index_list.Add(15);    _texture_index_list.Add(8);
        _index_list.Add(16);    _texture_index_list.Add(4);     _face_normals.Add(new Vector3(0, 0, -1));
        _index_list.Add(22);    _texture_index_list.Add(7);     // back mid 1
        _index_list.Add(15);    _texture_index_list.Add(8);
        _index_list.Add(21);    _texture_index_list.Add(3);     _face_normals.Add(new Vector3(0, 0, -1));
        _index_list.Add(22);    _texture_index_list.Add(7);     // back mid 2

        _index_list.Add(17);    _texture_index_list.Add(5);
        _index_list.Add(18);    _texture_index_list.Add(1);     _face_normals.Add(new Vector3(0, 0, -1));
        _index_list.Add(20);    _texture_index_list.Add(2);     // back bottom 1
        _index_list.Add(18);    _texture_index_list.Add(1);
        _index_list.Add(19);    _texture_index_list.Add(0);     _face_normals.Add(new Vector3(0, 0, -1));
        _index_list.Add(20);    _texture_index_list.Add(2);     // back bottom 2

        _index_list.Add(3);    _texture_index_list.Add(8);
        _index_list.Add(15);    _texture_index_list.Add(9);     _face_normals.Add(new Vector3(1, 0, 0));
        _index_list.Add(16);    _texture_index_list.Add(5);     // right side 1
        _index_list.Add(3);    _texture_index_list.Add(8);
        _index_list.Add(16);    _texture_index_list.Add(5);     _face_normals.Add(new Vector3(1, 0, 0));
        _index_list.Add(4);    _texture_index_list.Add(4);     // right side 2

        _index_list.Add(9);    _texture_index_list.Add(3);
        _index_list.Add(22);    _texture_index_list.Add(6);     _face_normals.Add(new Vector3(-1, 0, 0));
        _index_list.Add(10);    _texture_index_list.Add(7);     // left side 1
        _index_list.Add(9);    _texture_index_list.Add(3);
        _index_list.Add(21);    _texture_index_list.Add(2);     _face_normals.Add(new Vector3(-1, 0, 0));
        _index_list.Add(22);    _texture_index_list.Add(6);     // left side 2
    }
}
