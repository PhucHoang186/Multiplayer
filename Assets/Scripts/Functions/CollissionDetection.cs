using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionDetection : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] float radius;
    [SerializeField] float angle;
    [SerializeField] LayerMask targetLayer;
    private Collider[] colliders = new Collider[10];
    private bool isTrigger;
    private bool isChecking;

    public void Update()
    {
        if (!isChecking)
        {
            isTrigger = false;
            return;
        }
        Vector3 targetPosition = transform.position + transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z;
        int number = Physics.OverlapSphereNonAlloc(targetPosition, radius, colliders, targetLayer);
        if (number > 0)
        {
            if (isTrigger)
                return;
            isTrigger = true;

            // callback
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] == null)
                    continue;

                if (colliders[i].TryGetComponent<Ball>(out var ball))
                {
                    ball.AddForce(200f, (ball.transform.position - transform.position).normalized);
                    // StopTime();
                    return;                    
                }
            }
        }
        else if (isTrigger)
        {
            isTrigger = false;
        }
    }

    public void StartTrigger()
    {
        ToggleTrigger(true);
    }

    public void StopTrigger()
    {
        ToggleTrigger(false);
    }

    private void ToggleTrigger(bool isTrigger)
    {
        this.isChecking = isTrigger;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z, radius);
    }

    private void StopTime()
    {
        StartCoroutine(CorStopTime());
    }

    private IEnumerator CorStopTime()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }
}
