using UnityEngine;
using System.Linq;
public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance = null;
    [HideInInspector]
    public bool tap;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    public Vector3 dir;

    public BallController[] balls;
    public HoleController[] holes;
    private void Start()
    {
        balls = FindObjectsOfType<BallController>();
        holes = FindObjectsOfType<HoleController>();
    }
    private void Update()
    {
        tap = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the distance?
        if (swipeDelta.magnitude > 100)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                {
                    dir = Vector3.left;
                }
                else
                {
                    dir = Vector3.right;
                }
            }
            else
            {
                //Up or Down
                if (y < 0)
                {
                    dir = Vector3.back;
                }
                else
                {
                    dir = Vector3.forward;
                }

            }
            foreach (var item in balls)
            {
                item.Swipe(dir);
            }

            foreach (var item in holes)
            {
                item.Swipe(dir);
            }

            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}