using UnityEngine;
using System.Collections;

public class CanOpen : CanAction {

    public UIStore uiStore;

    protected override void Awake() {
        base.Awake();
        state = ActionController.StatePriority.Open;
    }

    public override void StartAction() {
        base.StartAction();
        uiStore.Show(target.gameObject.GetComponent<CanStore>());
    }

    public override void Reset() {
        base.Reset();
        uiStore.Hide();
    }
}
