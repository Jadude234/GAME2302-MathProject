using UnityEngine;

public class Missle : MovingObject
{
    public float MoveSpeed = 50f; 

    public override void Initalize()
    {
        base.Initalize();

        AddLineToObject(new Vector3(2, 0, 0), new Vector3(-2, 2, 0), Color.yellow);
        AddLineToObject(new Vector3(-2, 2, 0), new Vector3(-1, 0, 0), Color.yellow);
        AddLineToObject(new Vector3(-1, 0, 0), new Vector3(-2, -2, 0), Color.yellow);
        AddLineToObject(new Vector3(-2, -2, 0), new Vector3(2, 0, 0), Color.yellow);

    }

    public override void Tick()
    {
        base.Tick();
        
        if (CheckForCollisionWith(SpaceWarGrid.self.ShipAObject))
        {
            Debug.Log("Hit Ship A");
        }

        if (CheckForCollisionWith(SpaceWarGrid.self.ShipBObject))
        {
            Debug.Log("Hit Ship B");
        }
    }

    public void MakeMissle(float angle, Vector3 spawnPosition, Grid grid, int sceneIndex)
    {
        //keep in mind radius of ship and missile is ten and two respectively for spawn position, angle is angle of the ship or based off it
        Missle missle = new Missle();
        LaunchMissle(angle);
    }

    public void LaunchMissle(float angle)
    {
        SetRotationinDegrees(angle); 
        Velocity = DrawingTools.CircleRadiusPoint(Vector3.zero, angle, 1) * MoveSpeed; 
    }
}
