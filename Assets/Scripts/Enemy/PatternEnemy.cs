using UnityEngine;

public class PatternEnemy : Enemy
{
    [Header("Movement")]
    [SerializeField] private MovementType movementType;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float amplitude = 2f;
    [SerializeField] private float frequency = 2f;
    
    private IMovementPattern movementPattern;
    
    public enum MovementType
    {
        Straight,
        SineWave,
        Dive
    }
    
    protected override void Start()
    {
        base.Start();
        
        switch (movementType)
        {
            case MovementType.Straight:
                movementPattern = new StraightMovement(moveSpeed);
                break;
            case MovementType.SineWave:
                movementPattern = new SineWaveMovement(moveSpeed, amplitude, frequency);
                break;
            case MovementType.Dive:
                movementPattern = new DiveMovement(moveSpeed, 5f, -3f);
                break;
        }
    }
    
    protected virtual void Update()
    {
        if (movementPattern != null)
        {
            movementPattern.Move(transform, Time.deltaTime);
        }
    }
}