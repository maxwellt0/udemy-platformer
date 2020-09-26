using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum BossState
    {
        Shooting,
        Hurt,
        Moving
    };

    public BossState currentState;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")] public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;

    [Header("Shooting")] public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")] public float hurtTime;
    private float hurtCounter;

    private void Start()
    {
        currentState = BossState.Shooting;
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossState.Shooting:
                break;
            case BossState.Hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentState = BossState.Moving;
                    }
                }
                break;
            case BossState.Moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = Vector3.one;
                        moveRight = false;
                        currentState = BossState.Shooting;
                        shotCounter = timeBetweenShots;
                        anim.SetTrigger("StopMoving");
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f,1f,1f);
                        moveRight = true;
                        currentState = BossState.Shooting;
                        shotCounter = timeBetweenShots;
                        anim.SetTrigger("StopMoving");
                    }
                }
                break;
            default:
                break;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif
    }

    public void TakeHit()
    {
        currentState = BossState.Hurt;
        hurtCounter = hurtTime;
        
        anim.SetTrigger("Hit");
    }
}