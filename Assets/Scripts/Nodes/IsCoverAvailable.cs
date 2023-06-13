using UnityEngine;

public class IsCoverAvailable : Node
{
    private readonly EnemyAI _ai;
    private readonly Cover[] _availableCovers;
    private readonly Transform _target;

    public IsCoverAvailable(Cover[] availableCovers, Transform target, EnemyAI ai)
    {
        _availableCovers = availableCovers;
        _target = target;
        _ai = ai;
    }

    public override NodeState Evaluate()
    {
        var bestSpot = FindBestCoverSpot();
        _ai.SetBestCoverSpot(bestSpot);
        return bestSpot != null ? NodeState.Success : NodeState.Failure;
    }

    private Transform FindBestCoverSpot()
    {
        if (_ai.GetBestCoverSpot() != null)
            if (CheckIfSpotIsValid(_ai.GetBestCoverSpot()))
                return _ai.GetBestCoverSpot();
        float minAngle = 90;
        Transform bestSpot = null;
        for (var i = 0; i < _availableCovers.Length; i++)
        {
            var bestSpotInCover = FindBestSpotInCover(_availableCovers[i], ref minAngle);
            if (bestSpotInCover != null) bestSpot = bestSpotInCover;
        }

        return bestSpot;
    }

    private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
    {
        var availableSpots = cover.GetCoverSpots();
        Transform bestSpot = null;
        for (var i = 0; i < availableSpots.Length; i++)
        {
            var direction = _target.position - availableSpots[i].position;
            if (CheckIfSpotIsValid(availableSpots[i]))
            {
                var angle = Vector3.Angle(availableSpots[i].forward, direction);
                if (angle < minAngle)
                {
                    minAngle = angle;
                    bestSpot = availableSpots[i];
                }
            }
        }

        return bestSpot;
    }

    private bool CheckIfSpotIsValid(Transform spot)
    {
        RaycastHit hit;
        var direction = _target.position - spot.position;
        if (Physics.Raycast(spot.position, direction, out hit))
            if (hit.collider.transform != _target)
                return true;

        return false;
    }
}