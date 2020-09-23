using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;
    public SpriteRenderer theSR;

    public float distanceToAttackPlayer, chaseSpeed;

    private Vector3 attackTarget;
    public float waitAfterAttack;
    private float attackCounter;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (Transform point in points)
        {
            point.parent = null;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (attackCounter >= 0)
        {
            attackCounter -= Time.deltaTime;
            return;
        }

        Vector3 playerPos = PlayerController.instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) > distanceToAttackPlayer)
        {
            FlapAround();
            return;
        }

        if (attackTarget == Vector3.zero)
        {
            attackTarget = playerPos;
        }

        MoveTowards(attackTarget, chaseSpeed);

        if (Vector3.Distance(transform.position, attackTarget) > .1f)
        {
            return;
        }

        attackCounter = waitAfterAttack;
        attackTarget = Vector3.zero;
    }

    private void FlapAround()
    {
        MoveTowards(points[currentPoint].position, moveSpeed);

        if (Vector3.Distance(transform.position, points[currentPoint].position) > .05f)
        {
            return;
        }

        currentPoint++;
        if (currentPoint > points.Length - 1)
        {
            currentPoint = 0;
        }
    }

    private void HandleTurnToTarget(Vector3 targetPos)
    {
        if (transform.position.x < targetPos.x)
        {
            theSR.flipX = true;
        }
        else if (transform.position.x > targetPos.x)
        {
            theSR.flipX = false;
        }
    }

    private void MoveTowards(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        HandleTurnToTarget(target);
    }
}