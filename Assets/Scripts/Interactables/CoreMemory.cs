using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMemory : Interactable
{
    [SerializeField] Vector2 newTargetCooldownRange;
    [SerializeField] Vector2 targetDistanceSearch;

    [SerializeField] int maxSearchIterations;
    [SerializeField] LayerMask blockingMask;

    [SerializeField] float flySpeed;

    Vector3 currentTarget;
    Vector3 currentVelocity;

    IEnumerator Start()
    {
        currentTarget = transform.position;
        while(true)
        {
            yield return new WaitForSeconds(newTargetCooldownRange.RandomFromRange());
            AquireTarget();
        }
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, currentTarget, ref currentVelocity, 1/flySpeed);
    }

    private void AquireTarget()
    {
        for (int i = 0; i < maxSearchIterations; i++)
        {
            Vector3 targetDirection = Random.insideUnitSphere * targetDistanceSearch;

            RaycastHit hit;

            Ray ray = new Ray(transform.position, targetDirection);
            // Does the ray intersect any objects excluding the player layer
            if (!Physics.Raycast(ray, out hit, targetDirection.magnitude, blockingMask))
            {

                if(!Physics.SphereCast(ray, 0.5f, targetDirection.magnitude, blockingMask))
                {
                    currentTarget = transform.position + targetDirection;
                    return;
                }
            }
        }
    }

    protected override void OnInteracted()
    {
        base.OnInteracted();

        GameState.Instance.Story.RememberPerson();
        transform.DOScale(0, 1).OnComplete(() =>
        {
            GameState.Instance.Story.WakeUp();
            Destroy(gameObject);
        });
    }
}
