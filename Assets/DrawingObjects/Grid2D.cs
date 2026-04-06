using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Grid2D : MonoBehaviour
{

    public Vector3 screenSize;
    public Vector3 origin;

    public float gridSize = 10f;
    public float minGridSize = 2f;
    public float originSize = .6f;

    public int divisionCount = 5;
    public int minDivisionCount = 2;

    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;
    public Color axisColor = Color.white;

    public bool isDrawingObjects = true; 
    public bool isDrawingGrid = true; 
    public bool isDrawingOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;

    List<DrawingObject> drawObjects;

    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);

        drawObjects = new List<DrawingObject>();
        drawObjects.Add(new Arrow());
    }

    void Update()
    {
        GetInput();

        DrawGrid();

        DrawObjects(); 
    }

    /// <summary>
    /// Grabs Input 
    /// </summary>
    void GetInput()
    {
        Mouse mouse = Mouse.current;
        Keyboard keyboard = Keyboard.current;

        if ((keyboard == null) || (mouse == null))
        {
            Debug.LogError("Missing Keyboard or Mouse Input");
            return;
        }


        if (mouse.middleButton.isPressed)
        {
            origin = mouse.position.ReadValue();
        }

        Vector2 Scroll = mouse.scroll.ReadValue();

        if (Scroll.y > 0 && !(keyboard.ctrlKey.IsPressed()))
        {
            gridSize++;
        }

        if (Scroll.y < 0 && !(keyboard.ctrlKey.IsPressed()))
        {
            gridSize--;

            if (gridSize <= minGridSize)
            {
                gridSize = minGridSize;
            }
        }

        if (Scroll.y > 0 && keyboard.ctrlKey.IsPressed())
        {
            divisionCount++;
        }

        if (Scroll.y < 0 && keyboard.ctrlKey.IsPressed())
        {
            divisionCount--;

            if (divisionCount <= minDivisionCount)
            {
                divisionCount = minDivisionCount;
            }
        }

        if (keyboard.digit1Key.wasPressedThisFrame)
        { isDrawingOrigin = !isDrawingOrigin; }

        if (keyboard.digit2Key.wasPressedThisFrame)
        { isDrawingAxis = !isDrawingAxis; }

        if (keyboard.digit3Key.wasPressedThisFrame)
        { isDrawingDivisions = !isDrawingDivisions; }

        if (keyboard.digit4Key.wasPressedThisFrame)
        { isDrawingGrid = !isDrawingGrid; }

        if (keyboard.digit5Key.wasPressedThisFrame)
        { isDrawingObjects = !isDrawingObjects; }
    }

    /// <summary>
    /// Draws the grid
    /// </summary>
    void DrawGrid()
    {
        if (!isDrawingGrid) { return; }


        bool isDrawing = true;
        Color drawColor = lineColor;

        Vector3 PosPoint = Vector3.zero;
        Vector3 NegPoint = Vector3.zero;
        Vector3 PointOffset = Vector3.zero;

        int lineIndex = 0;

        bool posOut = false;
        bool negOut = false;

        while (isDrawing)
        {
            // 1. Figire out What color to Draw
            // Normal Line 
            drawColor = lineColor;

            if (isDrawingOrigin && (lineIndex == 0)) 
            {
                drawColor = axisColor;
                DrawOrigin(drawColor);
                drawColor = lineColor;
            }

            if (isDrawingDivisions && ((lineIndex % divisionCount) == 0))
            {
                drawColor = divisionColor;
            }

            // Then check if Axis Lines
            if (isDrawingAxis && (lineIndex == 0))
            {
                drawColor = axisColor;
            }



            // 2. Figure out the two Points to draw 
            PointOffset = new Vector3(gridSize, gridSize, 0) * lineIndex;
            PosPoint = origin + PointOffset;
            NegPoint = origin - PointOffset;

            // 3. Draw Lines 

            DrawLinesAtPoint(PosPoint, drawColor);
            DrawLinesAtPoint(NegPoint, drawColor);

            // 4. Figure out when to stop. 
            if ((PosPoint.x < 0 || PosPoint.x > Screen.width) && (PosPoint.y < 0 || PosPoint.y > Screen.height))
            { posOut = true; }

            if ((NegPoint.x < 0 || NegPoint.x > Screen.width) && (NegPoint.y < 0 || NegPoint.y > Screen.height))
            { negOut = true; }



            lineIndex++;
            // TO DO: Improve this Later 
            if (posOut && negOut)
            {
                isDrawing = false;
            }


        }

        
    }

    public void DrawObjects()
    {
        if (!isDrawingObjects) { return; }

        foreach (DrawingObject drawObject in drawObjects)
        {
            drawObject.Draw(this);
        }

    }


    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOriginAxis()
    {

        Vector3 originXStart = origin;
        originXStart.x = 0;
        Vector3 originXEnd = origin;
        originXEnd.x = Screen.width;
        Vector3 originYStart = origin;
        originYStart.y = 0;
        Vector3 originYEnd = origin;
        originYEnd.y = Screen.height;

        // Draw origin x axis
        DrawLine(originXStart, originXEnd, axisColor, false);
        //Draw Origin Y axis
        DrawLine(originYStart, originYEnd, axisColor, false);

    }

    public void DrawLinesAtPoint(Vector3 point, Color drawingColor)
    {

        Vector3 XStart = point;
        XStart.x = 0;
        Vector3 XEnd = point;
        XEnd.x = Screen.width;
        Vector3 YStart = point;
        YStart.y = 0;
        Vector3 YEnd = point;
        YEnd.y = Screen.height;

        // Draw origin x axis
        DrawLine(XStart, XEnd, drawingColor, false);
        //Draw Origin Y axis
        DrawLine(YStart, YEnd, drawingColor, false);

    }

    public void DrawOrigin(Color drawingColor)
    {

        Vector3 pointOne = origin;
        pointOne.x += originSize * gridSize;
        Vector3 pointTwo = origin;
        pointTwo.y -= originSize * gridSize;
        Vector3 pointThree = origin;
        pointThree.x -= originSize * gridSize;
        Vector3 pointFour = origin;
        pointFour.y += originSize * gridSize;

        DrawLine(pointOne, pointTwo, drawingColor, false); 
        DrawLine(pointTwo, pointThree, drawingColor, false);
        DrawLine(pointThree, pointFour, drawingColor, false);
        DrawLine(pointFour, pointOne, drawingColor, false);
    }




    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        Vector3 screenSpace = Vector3.zero;
        screenSpace = origin + (gridSpace * gridSize);
        return screenSpace;
    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <returns>Vector3 translated to Grid Space</returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        Vector3 gridSpace = Vector3.zero;
        gridSpace = (screenSpace - origin) / gridSize;
        return gridSpace;
    }

    public static float V3ToAngle(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 difference = endPoint - startPoint;
        return Mathf.Rad2Deg * Mathf.Atan2(difference.x, difference.y);
    }

    public static float LineToAngle(Line line)
    {
        return V3ToAngle(line.start, line.end);
    }

    /// <summary>
    /// Draws the given line object. If you are creating new line object, use the overload that takes parameters instead. 
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(Line line, bool drawOnGrid = true)
    {
        if (drawOnGrid)
        {
            DrawLine(line.start, line.end, line.color, drawOnGrid);
        }
        else
        {
            Glint.AddCommand(line);
        }
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color, bool drawOnGrid = true)
    {
        if (drawOnGrid)
        {
            Glint.AddCommand(new Line(GridToScreen(start), GridToScreen(end), color));
        }
        else
        {
            Glint.AddCommand(new Line(start, end, color));
        }

       
    }

    public void DrawObjects(DrawingObject lineObj, bool DrawOnGrid = true)
    {
        lineObj.Draw(this);
    }
}