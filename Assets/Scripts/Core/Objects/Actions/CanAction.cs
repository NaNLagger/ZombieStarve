using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanMove))]
public class CanAction : MonoBehaviour {

    public float SpeedAction;
    public Image TimerImage; 
    protected ActionController.StatePriority state;
    protected CanMove moveComponent;
    protected SimpleObject target;
    protected Animator animator;
    protected bool flagState = false;
    private float currentTimeAction = 0;

    protected virtual void Awake() {
        moveComponent = GetComponent<CanMove>();
        animator = GetComponent<Animator>();
    }

    public void Action(SimpleObject obj) {
        if (target == obj && animator.GetInteger("State") == (int) state)
            return;
        Reset();
        target = obj;
        if (Vector2.Distance(transform.position, obj.transform.position) > 1f) {
            moveComponent.Move(obj.transform.position, 1f);
            moveComponent.OnDone += StartAction;
            moveComponent.OnChangeTarget += Reset;
        }
        else {
            StartAction();
        }
    }

    public virtual void StartAction() {
        animator.SetInteger("State", (int) state);
        flagState = true;
        StartTimer();
    }

    public virtual void EndAction() {
        animator.SetInteger("State", -1);
        Reset();
    }

    public virtual void BreakAction() {

    }
    
    private void Update() {
        if(flagState) {
            if(animator.GetInteger("State") != (int) state) {
                Reset();
                BreakAction();
            }
            currentTimeAction += Time.deltaTime;
            UpdateTimer();
            if (currentTimeAction > SpeedAction)
                EndAction();
        }
    }

    public virtual void Reset() {
        flagState = false;
        moveComponent.OnDone -= StartAction;
        moveComponent.OnChangeTarget -= Reset;
        target = null;
        currentTimeAction = 0;
        EndTimer();
    }

    private void OnDrawGizmos() {
        /*if (flagState)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);*/
    }

    private void StartTimer() {
        if (TimerImage != null) {
            TimerImage.fillAmount = 0;
            TimerImage.gameObject.SetActive(true);
        }
    }

    private void UpdateTimer() {
        if (TimerImage != null) {
            TimerImage.fillAmount = currentTimeAction / SpeedAction;
            var text = TimerImage.gameObject.transform.GetComponentInChildren<Text>();
            if (text != null)
                text.text = string.Format("{0:0.00}", SpeedAction - TimerImage.fillAmount);
        }
    }

    private void EndTimer() {
        if (TimerImage != null) {
            TimerImage.fillAmount = 0;
            TimerImage.gameObject.SetActive(false);
        }
    }
}
