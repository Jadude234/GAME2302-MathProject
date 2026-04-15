using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Rect = System.Drawing.Rectangle;
public static class CollisionTools 
{
    public static void DrawTriangle(TriangleData data, Color color, DrawableGrid grid = null)
    {
        Line lineA = new Line(data.PointA, data.PointB, color);
        Line lineB = new Line(data.PointB, data.PointC, color);
        Line lineC = new Line(data.PointC, data.PointA, color);

        if (grid == null)
        {
            // Info is in Screen Space 
            Glint.AddCommand(lineA);
            Glint.AddCommand(lineB);
            Glint.AddCommand(lineC); 
        }
        else
        {
            grid.DrawLine(lineA);
            grid.DrawLine(lineB);
            grid.DrawLine(lineC);
        }
    }

    public static void SetColor(DrawableObject thing,  Color color)
    {
        for (int i = 0; i < thing.LineList.Count; i++)
        {
            Line item = thing.LineList[i];
            item.color = color;
            thing.LineList[i] = item;

            // C# is acting weird... 
            // won't let me use foreach
            // wont' let me do LineList[i].color = color; 
        }
    }

    public static bool IsPointInCircle(Vector3 Point, Vector3 Center, float Radius)
    {
        return (Center - Point).magnitude < Radius; 
    }

    public static bool IsPointInRectangle(Vector3 Point, Rect Rectangle)
    {
        return (Point.x >= Mathf.Min(Rectangle.X, (Rectangle.X + Rectangle.Width))) && (Point.x <= Mathf.Max(Rectangle.X, (Rectangle.X + Rectangle.Width))) && 
            (Point.y >= Mathf.Min(Rectangle.Y, (Rectangle.Y + Rectangle.Height))) && (Point.y <= Mathf.Max(Rectangle.Y, (Rectangle.Y + Rectangle.Height)));
    }
    public static bool IsPointInTriangle(Vector3 Point, TriangleData Triangle)
    {
        // stub code 
        return TriangleCalculation(Point, Triangle.PointA, Triangle.PointB, Triangle.PointC) && TriangleCalculation(Point, Triangle.PointB, Triangle.PointC, Triangle.PointA) && 
            TriangleCalculation(Point, Triangle.PointC, Triangle.PointA, Triangle.PointB);
    }

    public static bool TriangleCalculation(Vector3 point, Vector3 compare, Vector3 start, Vector3 end)
    {
        Vector3 cp1 = Vector3.Cross(end - start, point - start);
        Vector3 cp2 = Vector3.Cross(end - start, compare - start);
        return Vector3.Dot(cp1, cp2) >= 0;
    }
}
