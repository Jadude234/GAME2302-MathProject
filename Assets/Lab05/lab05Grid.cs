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

    Vector3 otherOrigin;

    float offset;

    public override void SetupScenes()
    {
        int sceneIndex = 0;
        DrawableObject newObject;
        otherOrigin = origin;
        otherOrigin.y *= 1.5f;
        otherOrigin.x *= 0.5f;
        AddScene("DrawingTools Example");

        newObject = DrawingTools.CreateCircleObject(ScreenToGrid(otherOrigin), 10, 24, Color.cyan);
        AddObjectToScene(sceneIndex, newObject);
        newObject = DrawingTools.CreateEllipseObject(ScreenToGrid(otherOrigin), new Vector2(10,20), 24, Color.white);
        AddObjectToScene(sceneIndex, newObject);


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

        circleRadiusLine.end = DrawingTools.CircleRadiusPoint(Vector3.zero, offset * 20, circleRadius);
        DrawLine(circleRadiusLine);
        DrawingTools.DrawCircle(origin, circleRadius * gridSize, 36, Color.green);
        DrawingTools.DrawEllipse(origin, ellipseAxis * gridSize, 36, Color.cyan);

        ellipseRadiusLine.end = DrawingTools.EllipseRadiusPoint(Vector3.zero, offset * 20, ellipseAxis);
        DrawLine(ellipseRadiusLine);
    }
}
