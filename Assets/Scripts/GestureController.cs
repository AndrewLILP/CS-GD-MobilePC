using UnityEngine;
using System.Collections;

public class GestureController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forceMagnitude = 50f;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        TouchHandling();
    }

    void TouchHandling()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                DetectSwipe();

            }
        }

        void DetectSwipe()
        {
            Vector2 swipeDelta = endTouchPosition - startTouchPosition;
            if (swipeDelta.magnitude < 50f)
                return; // too short to be considered a swipe - maybe add user feedback here

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                // Horizontal swipe
                if (swipeDelta.x > 0)
                    AddForce(Vector3.right);
                else
                    AddForce(Vector3.left);
            }
            else
            {
                // Vertical swipe
                if (swipeDelta.y > 0)
                    AddForce(Vector3.up);
                else
                    AddForce(Vector3.down);
            }

        }

        void AddForce(Vector3 direction)
        {
            if (rb != null)
            {
                rb.AddForce(direction * forceMagnitude, ForceMode.Impulse);
                StartCoroutine(ForceBack(direction)); // Start coroutine to apply force back after a delay
            }
            else
            {
                Debug.LogWarning("Rigidbody is not assigned. Please assign a Rigidbody to the GestureController.");
            }

        }

        IEnumerator ForceBack(Vector3 direction)
        {
            yield return new WaitForSeconds(0.2f); // Wait for half a second before applying the force back
            rb.AddForce(-direction * (forceMagnitude / 2));
        }
    }
}
