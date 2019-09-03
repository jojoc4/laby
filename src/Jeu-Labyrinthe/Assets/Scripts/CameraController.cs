using System.Collections;
using UnityEngine;

/// <summary>
/// Move the camera according to the input user.
/// </summary>
public class CameraController : MonoBehaviour
{
    public int speed = 5;
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    public float midpoint = 1.0f;

    private float timer = 0.0f;
    private Vector2 currentPosition;
    private Transform myBody;

    void Start()
    {
        myBody = this.transform.parent.transform;
    }

    private void FixedUpdate()
    {
        // Manage the mouse movement on the camera
        Vector2 inputUser = new Vector2(speed * Input.GetAxisRaw("Mouse X"),
            speed * Input.GetAxisRaw("Mouse Y"));

        currentPosition += inputUser;

        if (currentPosition.y > 50f)
        {
            currentPosition.y = 50f;
        }
        else if (currentPosition.y < -50f)
        {
            currentPosition.y = -50f;
        }

        this.transform.localRotation =
            Quaternion.AngleAxis(-currentPosition.y, Vector3.right);

        // Move the player left and right.
        myBody.localRotation =
            Quaternion.AngleAxis(currentPosition.x, Vector3.up);
    }

    private void Update()
    {
        // Refresh the waveslices of the player when walking
        float waveslice = 0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer -= (Mathf.PI * 2);
            }
        }

        if (waveslice != 0f)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0f, 1f);
            translateChange = totalAxes * translateChange;
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, midpoint + translateChange, this.transform.localPosition.z);
        }
        else
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, midpoint, this.transform.localPosition.z);
        }
    }

}
