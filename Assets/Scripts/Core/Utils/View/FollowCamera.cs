using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public GameObject objFollow;

    private void LateUpdate() {
        //if(Vector2.Distance(transform.position, objFollow.transform.position) > 0.1f)
        transform.position = new Vector3(objFollow.transform.position.x, objFollow.transform.position.y, -10);
    }
}
