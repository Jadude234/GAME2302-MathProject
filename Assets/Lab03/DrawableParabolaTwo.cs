using UnityEngine;

public class DrawableParabolaTwo : DrawableObject
{
    public override void Initalize()
    {
        for (int x = -100; x < 100; x++)
        {
            AddLineToObject(new Vector2(x, GetYPointatXof(x)), new Vector2((x + 1), GetYPointatXof(x + 1)), Color.magenta);
        }
    }

    public float GetYPointatXof(float xValue)
    {
        float yValue = (Mathf.Pow(xValue, 2)) + (xValue * 2) + 1;
        return yValue;
    }
}
