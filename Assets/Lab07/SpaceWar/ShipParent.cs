using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipParent : MovingObject
{
    public DrawableObject ship;
    public DrawableObject thrust;
    public bool IsShipA = true; 
    public float ShipMaxVelocity = 50;
    public float ShipRotationSpeed = 120.0f;
    public float ShipThrust = 20.0f;
    public float MissleSpawnRadius = 13;
    public float MissleCollisionRadius = 2;

    public Line LaserObject;
    public float laserStart = 5;
    public float laserEnd = 200;
    public bool Drawlaser = false;
    public float LaserShowTime = .5f;
    public float LaserShowCoutner = 0;

    public void SetupA(DrawableGrid grid, int sceneIndex)
    {
        IsShipA = true;
        ship = new ShipA();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipAThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        MaxVelocity = ShipMaxVelocity;

        LaserObject = new Line();
        LaserObject.color = Color.yellow;
    }

    public void SetupB(DrawableGrid grid, int sceneIndex)
    {
        IsShipA = false;
        ship = new ShipB();
        grid.AddObjectToScene(sceneIndex, ship);

        thrust = new ShipBThrust();
        grid.AddObjectToScene(sceneIndex, thrust);

        MaxVelocity = ShipMaxVelocity;

        LaserObject = new Line();
        LaserObject.color = Color.yellow;
    }

    public override void Tick()
    {
        base.Tick();
        UpdateSubObjects();
        UpdateLaser(); 


    }

    public void UpdateLaser()
    {
        if (!Drawlaser) { return; }
        // If not drawing laser, don't care.. 

        SpaceWarGrid.self.DrawLine(LaserObject);

        LaserShowCoutner -= Time.deltaTime; 
        if (LaserShowCoutner < 0)
        {
            Drawlaser = false; 
        }

        CheckForLaserCollison(); 

    }

    public void CheckForLaserCollison()
    {
        foreach( MovingObject item in SpaceWarGrid.self.MovingObjectlist)
        {
            // DO Collection Detection here... 
            if (CollisionTools.DoesLineIntersectCircle(LaserObject.start, LaserObject.end, item.Position, item.CollisionRadius ))
            {
                if (item is Missle)
                {
                    Missle other = (Missle)item;
                    other.RemoveMissle(); 

                }   
                
                if (item is ShipParent)
                {
                    ShipParent other = (ShipParent)item;
                    if (other.IsShipA != this.IsShipA)
                    {
                        SpaceWarGrid.self.AddScore(this.IsShipA); 
                    }


                }

            }

        }

    }

    public void UpdateSubObjects()
    {
        ship.Position = this.Position;
        thrust.Position = this.Position;

        ship.Rotation = this.Rotation;
        thrust.Rotation = this.Rotation;

        ship.Scale = this.Scale;
        thrust.Scale = this.Scale;

        LaserObject.start = this.Position + DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), laserStart);
        LaserObject.end = this.Position + DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), laserEnd);
    }


    public void AddThrust()
    {
        thrust.PerformDraw = true;
        Velocity += DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), 1) * ShipThrust * Time.deltaTime;
    }

    public void NoThrust()
    {
        thrust.PerformDraw = false;
    }

    public void RotateShip(float value)
    {
        this.Rotation += (value * ShipRotationSpeed * Time.deltaTime * Mathf.Deg2Rad);
    }

    public void FireMissle(DrawableGrid grid, int sceneIndex)
    {
        Missle missleObject = new Missle();
        missleObject.Position = this.Position;
        missleObject.Position += DrawingTools.CircleRadiusPoint(Vector3.zero, GetRotationinDegrees(), MissleSpawnRadius);
        //missleObject.SetRotationinDegrees(75);
        missleObject.CreateCollision(MissleCollisionRadius, grid, sceneIndex);
        //missleObject.willDrawCollision = true;
        missleObject.LaunchMissle(this.GetRotationinDegrees());
        SpaceWarGrid.self.AddObjectToScene(sceneIndex, missleObject);
        SpaceWarGrid.self.MovingObjectlist.Add(missleObject);
    }

    public void FireLaser(DrawableGrid grid, int sceneIndex)
    {
        Drawlaser = true;
        LaserShowCoutner = LaserShowTime;
    }
}
