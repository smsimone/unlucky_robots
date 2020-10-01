using UnityEngine;
using System.Collections;

public class RobotObject : MonoBehaviour
{
    private float timeToDestroy = 10f;

    public void StartDestroy()
    {
        StartCoroutine(WaitDestroy());
    }

    private IEnumerator WaitDestroy()
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / timeToDestroy;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("RobotToBuild"))
        {
            if (this.transform.parent == null)
            {
                if (other.gameObject.GetComponent<CreateRobot>().AddElement(gameObject))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}
