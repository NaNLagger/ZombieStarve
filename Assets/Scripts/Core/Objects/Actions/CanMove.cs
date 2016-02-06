using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UpdateOrder))]
public class CanMove : MonoBehaviour {

    public enum LineVector {
        RIGHT,
        LEFT,
        TOP,
        BOTTOM
    }

    public Utils.EmptyEvent OnChangeTarget;
    public Utils.EmptyEvent OnDone;

    public GameObject DownBody;
    public GameObject UpBody;
    public GameObject RLBody;

    private LineVector lineVector;
    private Vector2 target;
    private Animator animator;
    private bool state = false;
    private float distance = 0.2f;

    [SerializeField] private float speed;
    [SerializeField] private float standartDistance;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if(state) {
            if (Vector2.Distance(transform.position, target) > distance) {
                move(target);
            }
            else {
                ChangeState(false);
                if (OnDone != null)
                    OnDone();
            }
        }
    }

    private void move(Vector2 pos) {
        Vector2 tempPos = new Vector2(transform.position.x, transform.position.y);
        float angle = Vector2.Angle(pos - tempPos, new Vector2(0, -1));
        if (pos.x - tempPos.x < 0)
            angle = 360 - angle;
        if (angle > 45 && angle < 135) {
            lineVector = LineVector.RIGHT;
            RLBody.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            BodyHide(RLBody);
        }
        if (angle > 135 && angle < 225) {
            lineVector = LineVector.TOP;
            BodyHide(UpBody);
        }
        if (angle > 225 && angle < 315) {
            lineVector = LineVector.LEFT;
            RLBody.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            BodyHide(RLBody);
        }
        if (angle < 45 || angle > 315) {
            lineVector = LineVector.BOTTOM;
            BodyHide(DownBody);
        }
        transform.position += new Vector3(pos.x - tempPos.x, pos.y - tempPos.y).normalized * speed * Time.deltaTime;
        GetComponent<UpdateOrder>().SetOrder();
        if (animator != null) {
            animator.SetInteger("MoveLine", (int) lineVector);
        }
    }

    public void Move(Vector2 pos) {
        Move(pos, standartDistance);
    }

    public void Move(Vector2 pos, float distance) {
        /*if (!animator.GetBool("MoveState") && animator != null) {
            animator.Play("idle");
        }*/
        ChangeTarget(pos);
        this.distance = distance;
        ChangeState(true);
    }

    public void Stop() {
        ChangeState(false);
        if (OnChangeTarget != null)
            OnChangeTarget();
    }

    private void ChangeState(bool nState) {
        state = nState;
        if (nState) {
            animator.SetInteger("State", (int)ActionController.StatePriority.Move);
        } else {
            animator.SetInteger("State", -1);
        }
    }

    private void ChangeTarget(Vector2 nTarget) {
        target = nTarget;
        if (OnChangeTarget != null)
            OnChangeTarget();
    }

    private void BodyHide(GameObject body) {
        DownBody.SetActive(false);
        UpBody.SetActive(false);
        RLBody.SetActive(false);
        body.SetActive(true);
    }
}
