// based on https://github.com/CodingTrain/QuadTree

using System.Collections.Generic;
using UnityEngine;

public class QuadTree
{
    Rectangle boundary;
    int capacity;
    List<Point> points;
    bool divided;

    QuadTree northEast;
    QuadTree northWest;
    QuadTree southEast;
    QuadTree southWest;

    public QuadTree(Rectangle _boundary, int _capacity)
    {
        boundary = _boundary;
        capacity = _capacity;
        points = new List<Point>();
        divided = false;
    }

    public bool Insert(Point point)
    {
        if (boundary.Contains(point) == false)
        {
            return false;
        }

        if (points.Count < capacity)
        {
            points.Add(point);
            return true;
        }
        else // need to divide
        {
            // if not already split, then divide
            if (divided == false)
            {
                Subdivide();
            }

            // check which child contains this point
            if (northEast.Insert(point) == true)
            {
                return true;
            }
            else if (northWest.Insert(point) == true)
            {
                return true;
            }
            else if (southEast.Insert(point) == true)
            {
                return true;
            }
            else if (southWest.Insert(point) == true)
            {
                return true;
            }
        }

        // error?
        return false;
    }

    void Subdivide()
    {
        var x = boundary.centerX;
        var y = boundary.centerY;
        var w = boundary.width;
        var h = boundary.height;
        var ne = new Rectangle(x + w / 2, y + h / 2, w / 2, h / 2);
        northEast = new QuadTree(ne, capacity);
        var nw = new Rectangle(x - w / 2, y + h / 2, w / 2, h / 2);
        northWest = new QuadTree(nw, capacity);
        var se = new Rectangle(x + w / 2, y - h / 2, w / 2, h / 2);
        southEast = new QuadTree(se, capacity);
        var sw = new Rectangle(x - w / 2, y - h / 2, w / 2, h / 2);
        southWest = new QuadTree(sw, capacity);
        divided = true;
    }


    public void Show(Texture2D tex)
    {
        // draw boundary edges
        for (float x = 0; x < boundary.width * 2; x++)
        {
            tex.SetPixel((int)(boundary.centerX + x - boundary.width), (int)(boundary.centerY - boundary.height - 1), Color.red);
            tex.SetPixel((int)(boundary.centerX + x - boundary.width), (int)(boundary.centerY + boundary.height), Color.green);
        }
        for (float y = 0; y < boundary.height * 2; y++)
        {
            tex.SetPixel((int)(boundary.centerX - boundary.width - 1), (int)(boundary.centerY + y - boundary.height), Color.blue);
            tex.SetPixel((int)(boundary.centerX + boundary.width), (int)(boundary.centerY + y - boundary.height), Color.gray);
        }

        // recursively show children
        if (divided == true)
        {
            northEast.Show(tex);
            northWest.Show(tex);
            southEast.Show(tex);
            southWest.Show(tex);
        }

        // draw actual points
        for (int i = 0, length = points.Count; i < length; i++)
        {
            tex.SetPixel((int)points[i].x, (int)points[i].y, Color.white);
        }

    }
}
