using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class WalkerData
{
    public Transform character;
    public float moveSpeed = 2f; // Units per second
    public int numJumps = 15;     // Number of hops per movement
}

public class ShadowWalk : MonoBehaviour
{
    [Header("Objects & Settings")]
    [SerializeField] private List<WalkerData> leftToRightObjects;
    [SerializeField] private List<WalkerData> rightToLeftObjects;
    [SerializeField] private float moveDistance = 5f; // How far each object moves

    [Header("Delay Settings")]
    [SerializeField] private float minDelay = 1f;
    [SerializeField] private float maxDelay = 3f;

    [Header("Hop Settings")]
    [SerializeField] private float jumpPower = 1f; // Default hop height

    private List<WalkerData> leftPool;
    private List<WalkerData> rightPool;

    private void Start()
    {
        leftPool = new List<WalkerData>(leftToRightObjects);
        rightPool = new List<WalkerData>(rightToLeftObjects);

        StartCoroutine(MoveObjectsLoop());
    }

    private IEnumerator MoveObjectsLoop()
    {
        float initialDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            WalkerData walker;
            Vector3 startPos, endPos;

            // Refill pools if empty
            if (leftPool.Count == 0) leftPool.AddRange(leftToRightObjects);
            if (rightPool.Count == 0) rightPool.AddRange(rightToLeftObjects);

            // Pick which direction
            bool pickLeftToRight = (Random.value > 0.5f);

            if (pickLeftToRight && leftPool.Count > 0)
            {
                int index = Random.Range(0, leftPool.Count);
                walker = leftPool[index];
                leftPool.RemoveAt(index);

                startPos = walker.character.position;
                endPos = startPos - Vector3.forward * moveDistance;
            }
            else if (!pickLeftToRight && rightPool.Count > 0)
            {
                int index = Random.Range(0, rightPool.Count);
                walker = rightPool[index];
                rightPool.RemoveAt(index);

                startPos = walker.character.position;
                endPos = startPos + Vector3.forward * moveDistance;
            }
            else
            {
                yield return null;
                continue;
            }

            // Calculate duration based on this character's speed
            float duration = moveDistance / walker.moveSpeed;

            // Move with hopping
            Tween moveTween = walker.character.DOJump(endPos, jumpPower, walker.numJumps, duration).SetEase(Ease.Linear);

            yield return moveTween.WaitForCompletion();

            // Reset position
            walker.character.position = startPos;

            // Wait a random delay
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDisable()
    {
        // Kill all tweens in this script’s objects
        DOTween.KillAll();

        // Or only kill tweens related to specific transforms:
        foreach (var walker in leftToRightObjects)
            if (walker.character != null) DOTween.Kill(walker.character);

        foreach (var walker in rightToLeftObjects)
            if (walker.character != null) DOTween.Kill(walker.character);

        StopAllCoroutines();
    }
}