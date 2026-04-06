using UnityEngine;

public class Lab04Grid : DrawableGrid
{
    int sceneIndex;
    DrawableObject newObject;
    public override void SetupScenes()
    {

        AddScene("Use Tab to switch scenes");

        sceneIndex = AddScene("1. Diamond");
        newObject = new DrawableDiamond();
        AddObjectToScene(sceneIndex, newObject);

        sceneIndex = AddScene("2. Diamond scale 20");
        newObject = new DrawableDiamond();
        newObject.Scale = (Vector3.one * 20);
        AddObjectToScene(sceneIndex, newObject);

        sceneIndex = AddScene("3. Diamond scale 20 10");
        newObject = new DrawableDiamond();
        newObject.Scale = new Vector3(20, 10, 1);
        AddObjectToScene(sceneIndex, newObject);

        sceneIndex = AddScene("4. Diamond scale 20 10, rotation 45");
        newObject = new DrawableDiamond();
        newObject.Scale = new Vector3(20, 10, 1);
        newObject.Rotation = 45 * Mathf.Deg2Rad;
        AddObjectToScene(sceneIndex, newObject);

        sceneIndex = AddScene("5. Rotating Diamond");
        newObject = new RotatingDiamond();
        AddObjectToScene(sceneIndex, newObject);

        sceneIndex = AddScene("6. Facing Box");
        newObject = new FacingBox();
        AddObjectToScene(sceneIndex, newObject);

    }
}
