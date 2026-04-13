using UnityEngine;

using Rect = System.Drawing.Rectangle; 

public class lab05Grid : DrawableGrid
{
    
    Rect box;
    Rect boxOnGrid;

    Line circleRadiusLine;
    float circleRadius = 10;
    Line ellipseRadiusLine;
    Vector2 ellipseAxis = new Vector2(10, 20);

    float offset;

    public override void SetupScenes()
    {
        AddScene("DrawingTools Example");

        box = new Rect(100, 100, 100, 100);
        boxOnGrid = new Rect(2, 3, 2, 2);

        offset = 0;

        circleRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.blue);
        ellipseRadiusLine = new Line(Vector3.zero, Vector3.zero, Color.yellow);
    }

    public override void Tick()
    {
        offset += Time.deltaTime;

        DrawingTools.DrawRectangle(box, Color.green, this);
        DrawingTools.DrawRectangle(boxOnGrid, Color.red, this);

        circleRadiusLine.end = DrawingTools.CircleRadiusPoint(Vector3.zero, offset * 10, circleRadius);
        DrawLine(circleRadiusLine);

        ellipseRadiusLine.end = DrawingTools.EllipseRadiusPoint(Vector3.zero, offset * 2, ellipseAxis);
        DrawLine(ellipseRadiusLine);
    }
}
