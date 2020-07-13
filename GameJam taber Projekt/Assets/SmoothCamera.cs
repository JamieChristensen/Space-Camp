using UnityEngine;

public class SmoothCamera : MonoBehaviour {

	public Transform target;

	public float smoothTime = 0.125f;
	public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

	void FixedUpdate ()
	{
        //offset = target.forward * -10;
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
        //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
		transform.position = smoothedPosition;

        //transform.rotat
		transform.LookAt(target);
	}

}
