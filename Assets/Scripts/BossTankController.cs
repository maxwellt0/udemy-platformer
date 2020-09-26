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
    public Transform theTank;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    
    [Header("Mines")]
    public GameObject mine;
    public Transform minePoint;
    public float timeBetweenMines;
    private float mineCounter;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform firePoint;
    public float timeBetweenShots;
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    private void Start()
    {
        currentState = BossState.Shooting;
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossState.Shooting:
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;

                    GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theTank.localScale;
                }

                break;
            case BossState.Hurt:
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    if (hurtCounter <= 0)
                    {
                        currentState = BossState.Moving;
                        mineCounter = 0;
                    }
                }

                break;
            case BossState.Moving:
                if (moveRight)
                {
                    theTank.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theTank.position.x > rightPoint.position.x)
                    {
                        theTank.localScale = Vector3.one;
                        moveRight = false;

                        EndMovement();
                    }
                }
                else
                {
                    theTank.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

                    if (theTank.position.x < leftPoint.position.x)
                    {
                        theTank.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;

                        EndMovement();
                    }
                }

                mineCounter -= Time.deltaTime;

                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
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

    private void EndMovement()
    {
        currentState = BossState.Shooting;
        shotCounter = timeBetweenShots;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }
}