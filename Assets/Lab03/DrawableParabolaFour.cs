using UnityEngine;

public class DrawableParabolaFour : DrawableObject
{
    public override void Initalize()
    {
        for (int y = -100; y < 100; y++)
        {
            AddLineToObject(new Vector2(GetXPointatYof(y), y), new Vector2(GetXPointatYof(y + 1), (y + 1)), Color.magenta);
        }
    }

    public float GetXPointatYof(float yValue)
    {
        float xValue = -1 * Mathf.Pow(yValue, 3);
        return xValue;
    }
}
