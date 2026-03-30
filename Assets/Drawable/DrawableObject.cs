using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public class DrawableObject
{

    public bool PerformDraw = true;

    // Transform information 
    public Vector3 Position = Vector3.zero;
    // In Radians
    public float Rotation = 0;
    public Vector3 Scale = Vector3.one;

    public List<Line> LineList;

    public DrawableObject()
    {
        LineList = new List<Line>();
        Initalize();
    }

    public virtual void Initalize()
    {

    }

    public virtual void Tick()
    {
        //Test
    }

    public void AddLineToObject(Line line)
    {
        LineList.Add(line);
    }

    public void AddLineToObject(Vector3 start, Vector3 end, Color color)
    {
        LineList.Add(new Line(start, end, color));
    }


    /// <summary>
    /// Draws the Drawing Object Instance
    /// </summary>
    /// <param name="grid">Optional, When a Grid2d is applied, object is drawn relative to the grid and location is in Grid space</param>
    public virtual void Draw(DrawableGrid grid)
    {
        if (!PerformDraw) { return; }
        if (LineList == null) { return; }
        if (LineList.Count == 0) { return; }

        for (int i = 0; i < LineList.Count; i++)
        {
            grid.DrawLine(TranslateLinePoints(LineList[i]));
        }
    }

    public float GetRotationinDegrees()
    {
        return 0;
    }

    public void SetRotationinDegrees(float degrees)
    {
        //
    }

    public static float V3ToAngle(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 lineVector = endPoint - startPoint;
        float radians = Mathf.Atan2(lineVector.y, lineVector.x);
        return radians;
    }

    public static float V3ToAngleinDegrees(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 lineVector = endPoint - startPoint;
        float radians = Mathf.Atan2(lineVector.y, lineVector.x);
        return (radians * Mathf.Rad2Deg);
    }

    public static float LineToAngle(Line line)
    {
        return V3ToAngle(line.start, line.end);
    }

    public static Vector3 RotatePoint(Vector3 Center, Vector3 pointIN, float angleInRadians)
    {
        // if not AT 000- translate back to origin
        Vector3 pointAtZero = pointIN - Center;
        Vector3 result = Vector3.zero;

        result.x = pointAtZero.x * Mathf.Cos(angleInRadians) - pointAtZero.y * Mathf.Sin(angleInRadians);
        result.y = pointAtZero.x * Mathf.Sin(angleInRadians) + pointAtZero.y * Mathf.Cos(angleInRadians);

        // translate back to center point
        return (result + Center);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="line"></param>
    /// <returns>given line translated by scale, rotation, postion</returns>
    public Line TranslateLinePoints(Line line)
    {
        Line translatedLine = line;

        // Scale
        translatedLine.start.x *= Scale.x;
        translatedLine.start.y *= Scale.y;
        translatedLine.start.z *= Scale.z;

        translatedLine.end.x *= Scale.x;
        translatedLine.end.y *= Scale.y;
        translatedLine.end.z *= Scale.z;

        // Rotate
        translatedLine.start = RotatePoint(Vector3.zero, translatedLine.start, Rotation);
        translatedLine.end = RotatePoint(Vector3.zero, translatedLine.start, Rotation);

        // Position
        translatedLine.start += Position;
        translatedLine.end += Position;


        return translatedLine;
    }
}