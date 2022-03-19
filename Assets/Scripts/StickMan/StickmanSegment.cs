using UnityEngine;

public class StickmanSegment : StickMan
{
    [SerializeField] private TrolleySegment _trolleySegment;

    private void OnEnable()
    {
        _trolleySegment.EncounteredObstacle += Destruction;
    }

    private void OnDisable()
    {
        _trolleySegment.EncounteredObstacle -= Destruction;
    }
}
