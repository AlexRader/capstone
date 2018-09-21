using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FOV : MonoBehaviour
{
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask, obsticleMask;

    [HideInInspector]
    public List<Transform> visTarg = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visTarg.Clear();
        Collider[] targetsInVR = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInVR.Length; ++i )
        {
            Transform target = targetsInVR[i].transform;
            Vector3 dir2Target = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dir2Target) < viewAngle / 2)
            {
                float dist2Target = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dir2Target, dist2Target, obsticleMask))
                {
                    visTarg.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
