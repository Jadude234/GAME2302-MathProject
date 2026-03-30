using UnityEngine;

public class Lab04Grid : DrawableGrid
{
    public override void SetupScenes()
    {
        AddScene("FirstScene");
        AddMultipleScenes(2);
    }
}
