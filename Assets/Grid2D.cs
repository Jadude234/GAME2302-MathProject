using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Grid2D : MonoBehaviour
{

    public Vector3 screenSize;
    public Vector3 origin;

    float gridSize = 10f;
    float minGridSize = 2f;
    public float originSize = .6f;

    int divisionCount = 5;
    int divisionCountMult = 1;
    int minDivisionCount = 2;
    int minDivisionCountMult = 1;

    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;
    public bool isDrawing = false;



    private void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height);
        origin = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        GetInput();
        isDrawing = true;
        DrawGrid(); 
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
    }

    /// <summary>
    /// Draws the grid
    /// </summary>
    void DrawGrid()
    {
        while (isDrawing)
        {
            DrawMinDivision(minDivisionCountMult);
            DrawDivision(divisionCountMult);
            DrawOriginAxis();
            DrawOrigin();
            isDrawing = false;
        }
    }

    /// <summary>
    /// Draws the Diamond symbol at the Origin
    /// </summary>
    public void DrawOriginAxis()
    {
        if (isDrawingAxis)
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
            DrawLine(originXStart, originXEnd, axisColor);
            //Draw Origin Y axis
            DrawLine(originYStart, originYEnd, axisColor);
        }
    } 

    public void DrawOrigin()
    {
        if (isDrawingOrigin)
        {
            //
        }
    }

    public void DrawDivision(int mult)
    {
        Vector3 divisionXStart = origin;
        divisionXStart.x = 0;
        divisionXStart.y += (divisionCount * gridSize * mult);
        Vector3 divisionXEnd = origin;
        divisionXEnd.x = Screen.width;
        divisionXEnd.y += (divisionCount * gridSize * mult);
        Vector3 divisionYStart = origin;
        divisionYStart.x += (divisionCount * gridSize * mult);
        divisionYStart.y = 0;
        Vector3 divisionYEnd = origin;
        divisionYEnd.x += (divisionCount * gridSize * mult);
        divisionYEnd.y = Screen.height;

        DrawLine(divisionXStart, divisionXEnd, divisionColor);

        DrawLine(divisionYStart, divisionYEnd, divisionColor);
    }

    public void DrawMinDivision(int mult)
    {
        Vector3 minDivisionXStart = origin;
        minDivisionXStart.x = 0;
        minDivisionXStart.y += (minDivisionCount * gridSize * mult);
        Vector3 minDivisionXEnd = origin;
        minDivisionXEnd.x = Screen.width;
        minDivisionXEnd.y += (minDivisionCount * gridSize * mult);
        Vector3 minDivisionYStart = origin;
        minDivisionYStart.x += (minDivisionCount * gridSize * mult); 
        minDivisionYStart.y = 0;
        Vector3 minDivisionYEnd = origin;
        minDivisionYEnd.x += (minDivisionCount * gridSize * mult);
        minDivisionYEnd.y = Screen.height;

        DrawLine(minDivisionXStart, minDivisionXEnd, lineColor);

        DrawLine(minDivisionYStart, minDivisionYEnd, lineColor);
    }


    /// <summary>
    /// Takes the potential grid space and outputs it into screen space
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <returns>Vector3 translated to Screen Space</returns>
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <returns>Vector3 translated to Grid Space</returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Draws the given line object. If you are creating new line object, use the overload that takes parameters instead. 
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(Line line)
    {
        Glint.AddCommand(line);
    }

    /// <summary>
    /// Draws a line, This overload takes line parameters
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    public void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        Glint.AddCommand(new Line(start, end, color));
    }

    //Draws the Origin Point (or Symbol)

}