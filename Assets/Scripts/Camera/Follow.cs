using UnityEngine;

public class Follow : MonoBehaviour {
    public GameObject follow;

    void Update () {
        transform.position = new Vector3(follow.transform.position.x, 
            follow.transform.position.y, 
            follow.transform.position.z - 1.5f);
    }
}
