using UnityEngine;

public class MovingObject : DrawableObject
{
    public Vector3 Velocity = Vector3.zero;
    public float MaxVelocity = 50; 
    public float CollisionRadius = 10;
    public DrawableObject CollisionCircle;
    public bool willDrawCollision = false;
    public bool willScreenWarp = true; 

    public override void Initalize()
    {
        base.Initalize();
    }

    public override void Tick()
    {
        base.Tick();
        UpdatePostion();
        DrawCollision(); 
    }
    
    public void UpdatePostion()
    {
        /*Newton's laws of motion
         *  1: Positions - where you are at
         *  2: Velocity - The change of position over time (meters/sec)
         *  3: Acceleration - change in velocity over time
         *  4: Acceleration/Decelleration
         *  4a: Acceleration is when the absolute change in velocity is increasing (away from 0)
         *  4b: Decelleration is when the absolute change in velocity is decreasing (towards 0)
         *  
         * */
        // for gameplay
        if (Velocity.magnitude > MaxVelocity)
        {
            Velocity = Velocity.normalized * MaxVelocity;
        }
        Position += Velocity * Time.deltaTime;

        //ScreenWarping
        if (willScreenWarp && Position.magnitude > SpaceWarGrid.self.MagicCircleRadius)
        {
            Position *= -1;
        }

    }

    public void DrawCollision()
    {
        if (CollisionCircle != null)
        {
            CollisionCircle.PerformDraw = willDrawCollision;
            CollisionCircle.Position = Position;
            CollisionCircle.Scale = Scale; 
        }
    }

    public void CreateCollision(float Radius, DrawableGrid grid, int sceneIndex)
    {
        CollisionRadius = Radius;
        CollisionCircle = DrawingTools.CreateCircleObject(Vector3.zero, Radius, 36, Color.magenta);
        grid.AddObjectToScene(sceneIndex, CollisionCircle);
    }

    public bool CheckForCollisionWith(MovingObject other)
    {
        Vector3  distanceVector = other.Position - this.Position;
        float combinedRadii = other.CollisionRadius + this.CollisionRadius;
        return (distanceVector.magnitude < combinedRadii); 
    }
}
