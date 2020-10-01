using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRobot : MonoBehaviour
{
    private Transform[] children;
    public PlayerController owner;

    private void Start()
    {
        children = GetComponentsInChildren<Transform>();
        foreach (Transform t in children)
        {
            MeshRenderer render;
            if (t.TryGetComponent<MeshRenderer>(out render))
            {
                render.enabled = false;
            }
        }
    }

    public bool HasFinished()
    {
        bool ended = true;
        foreach (Transform t in children)
        {
            MeshRenderer renderer;
            if (t.TryGetComponent<MeshRenderer>(out renderer))
            {
                ended = ended && renderer.enabled;
            }
        }
        return ended;
    }

    public bool AddElement(GameObject robotItem)
    {
        if (robotItem.CompareTag("Robot"))
        {
            Mesh mesh = robotItem.GetComponent<MeshFilter>().mesh;
            foreach (Transform t in children)
            {
                if (mesh.name.Contains(t.name))
                {
                    MeshRenderer render;
                    if (t.TryGetComponent<MeshRenderer>(out render) && !render.enabled)
                    {
                        Material material = robotItem.GetComponent<MeshRenderer>().material;
                        render.material = material;
                        render.enabled = true;
                        return true;
                    }
                }
            }
        }
        else if (robotItem.CompareTag("PowerUp"))
        {
            owner.RunPowerup();
            return true;
        }
        return false;
    }

}
