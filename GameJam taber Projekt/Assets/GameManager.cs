using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float levelStartTime;
    private float timer;

    [SerializeField]
    private float fieldOfView;

    public Camera[] cameras;
    public AnimationCurve transitionCurve;

    bool firstFrame, transitionOver;
    // Start is called before the first frame update
    void Start()
    {
        firstFrame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame && timer < levelStartTime)
        {
            firstFrame = false;
            foreach (Camera cam in cameras)
            {
                cam.fieldOfView = 179;
                timer = 0;
            }
        }

        if (transitionOver)
        {
            return;
        }
        if (timer < levelStartTime)
        {
            timer += Time.deltaTime;
            Debug.Log("timer " + timer);
            foreach (Camera cam in cameras)
            {
                float eval = Mathf.Clamp(timer / levelStartTime, 0, 1);
                float curveStep = transitionCurve.Evaluate(eval);
                float f = Mathf.LerpUnclamped(0, 99, curveStep);
                cam.fieldOfView = 180 - f;
                Debug.Log(180 - f);
            }
        }
    }
}
