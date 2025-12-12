using UnityEngine;

// Movement pattern interface
public interface IMovementPattern
{
    void Move(Transform transform, float deltaTime);
}

// Straight movement (basic)
public class StraightMovement : IMovementPattern
{
    private float speed;
    
    public StraightMovement(float speed)
    {
        this.speed = speed;
    }
    
    public void Move(Transform transform, float deltaTime)
    {
        transform.position += Vector3.left * speed * deltaTime;
    }
}

// Sine wave movement
public class SineWaveMovement : IMovementPattern
{
    private float speed;
    private float amplitude;
    private float frequency;
    private float time;
    private float startY;
    
    public SineWaveMovement(float speed, float amplitude, float frequency)
    {
        this.speed = speed;
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.time = 0;
    }
    
    public void Move(Transform transform, float deltaTime)
    {
        if (startY == 0)
        {
            startY = transform.position.y;
        }
        
        time += deltaTime;
        Vector3 pos = transform.position;
        pos.x -= speed * deltaTime;
        pos.y = startY + Mathf.Sin(time * frequency) * amplitude;
        transform.position = pos;
    }
}

// Dive movement (swoops down)
public class DiveMovement : IMovementPattern
{
    private float speed;
    private float diveSpeed;
    private float targetY;
    private bool diving;
    
    public DiveMovement(float speed, float diveSpeed, float targetY)
    {
        this.speed = speed;
        this.diveSpeed = diveSpeed;
        this.targetY = targetY;
        this.diving = false;
    }
    
    public void Move(Transform transform, float deltaTime)
    {
        transform.position += Vector3.left * speed * deltaTime;
        
        if (!diving && transform.position.y > targetY)
        {
            diving = true;
        }
        
        if (diving)
        {
            transform.position += Vector3.down * diveSpeed * deltaTime;
        }
    }
}