using UnityEngine;

public class Player : MonoBehaviour
{
    const float normalGrvity = 1;
    const float fallFravity = 5;

    float moveX;
    Rigidbody2D myRigid;

    [SerializeField] float jumpForce;
    [SerializeField] float horizontlSpeed;

    [Space(10)]
    [SerializeField] Transform rayPoint;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!Manager.IsStarted)
        {
            return;
        }

        MirrorPosControl();
        DetectInput();
        UpdateJump();
    }

    private void FixedUpdate()
    {
        if (!Manager.IsStarted)
        {
            return;
        }

        myRigid.velocity = new Vector2(moveX, myRigid.velocity.y);
    }

    void UpdateJump()
    {
        myRigid.gravityScale = myRigid.velocity.y > 0 ? normalGrvity : fallFravity;

        RaycastHit2D hit = Physics2D.CircleCast(rayPoint.position, 0.2f, transform.TransformDirection(Vector2.down), 0.05f);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("platform") && myRigid.velocity.y <= 0)
            {
                if (hit.collider.name == "1")
                {
                    Manager.Instance.AddScore();
                }

                hit.collider.GetComponent<Platform>().Interact();
                myRigid.velocity = new Vector2(myRigid.velocity.x, jumpForce);

                SoundManager.Instance.PlayEffect(1);
                VibrateManager.Instance.TryVibrate();
            }
        }
    }

    void MirrorPosControl()
    {
        Vector2 _pos = Camera.main.WorldToScreenPoint(transform.position);

        if (_pos.x < 0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, _pos.y));
        }
        else if (_pos.x > Screen.width)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector2(0, _pos.y));
        }
    }

    void DetectInput()
    {
        if (Input.GetMouseButton(0))
        {
            moveX = Input.mousePosition.x > Screen.width / 2 ? horizontlSpeed : -horizontlSpeed;
        }

        if(Input.GetMouseButtonUp(0))
        {
            moveX = 0;
        }
    }

    public void SetActive(bool isActive)
    {
        myRigid.isKinematic = !isActive;
    }
}
