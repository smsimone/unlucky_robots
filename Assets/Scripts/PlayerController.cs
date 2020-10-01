using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRb;
    private Animator animator;
    public bool moveLeft = true;
    public bool moveRight = true;
    public List<Transform> frontPoints;
    public bool hasPowerUp;

    public float raycastLength = 4f;
    public float powerupStrength = 1.1f;
    public float powerupDuration = 10f;

    private GameObject hitObj;
    private Rigidbody hitObjRigid;

    public GameObject grabPoint;
    public float rotationValue = 1.5f;

    public int playerNumber = 1;
    private float normalSpeed;
    private string horizontal_axis;
    private string vertical_axis;
    private string fire_btn;
    private SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        horizontal_axis = "Horizontal_" + playerNumber;
        vertical_axis = "Vertical_" + playerNumber;
        fire_btn = "Fire1_" + playerNumber;
        normalSpeed = speed; soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        HandleMouse();
    }

    bool GrabDown()
    {
        return Input.GetButtonDown(fire_btn);
    }

    bool GrabUp()
    {
        return Input.GetButtonUp(fire_btn);
    }

    bool LaunchDown()
    {
        return Input.GetButtonDown("Launch_" + playerNumber);
    }

    public void RunPowerup()
    {
        speed *= powerupStrength;
        StartCoroutine(PowerupCountdownRoutine());
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        speed = normalSpeed;

    }

    void HandleMouse()
    {
        if (GrabDown() && hitObj == null)
        {
            animator.SetTrigger("grab");
            animator.SetBool("hold",true);
            foreach (Transform frontPoint in frontPoints)
            {
                if (hitObj == null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(frontPoint.position, frontPoint.TransformDirection(Vector3.forward), out hit, raycastLength))
                    {
                        //Debug.DrawRay(frontPoint.position, frontPoint.TransformDirection(Vector3.forward) * raycastLength, Color.black, raycastLength);
                        if (hit.collider.gameObject.CompareTag("Robot") || hit.collider.gameObject.CompareTag("PowerUp"))
                        {

                            //soundManager.OnNewItemsGrab(hitObj.transform);

                            hitObj = hit.collider.gameObject;
                            hitObj.GetComponent<MoveForward>().canMove = false;
                            hitObj.GetComponent<BoxCollider>().enabled = false;
                            hitObj.GetComponent<MeshCollider>().enabled = true;

                            hitObjRigid = hitObj.GetComponent<Rigidbody>();

                            hitObjRigid.useGravity = false;
                            hitObjRigid.constraints = RigidbodyConstraints.FreezePositionY;

                            Transform[] hitPivots = hitObj.transform.GetComponentsInChildren<Transform>();

                            Quaternion rotationDelta = Quaternion.FromToRotation(hitObj.transform.forward, gameObject.transform.forward);

                            Quaternion rotation = hitObj.transform.rotation * rotationDelta;

                            GameObject hitPivot = null;
                            foreach (Transform t in hitPivots)
                            {
                                if (t.CompareTag("Pivot"))
                                {
                                    hitPivot = t.gameObject;
                                }
                            }
                            
                            if (hitPivot != null)
                            {
                                hitPivot.transform.Translate(grabPoint.transform.position, Space.World);
                                hitObjRigid.transform.parent = grabPoint.transform;
                            }
                            else
                            {
                                hitObj.transform.Translate(grabPoint.transform.position);
                                hitObjRigid.transform.Translate(grabPoint.transform.position, Space.World);
                                hitObj.transform.parent = grabPoint.transform;
                            }

                            hitObj.transform.rotation = rotation;
                            hitObjRigid.constraints = RigidbodyConstraints.FreezeAll;
                        }
                    }
                    else
                    {
                        //Debug.DrawRay(frontPoint.position, frontPoint.TransformDirection(Vector3.forward) * raycastLength, Color.red, raycastLength);
                    }
                }
            }

        }
        else if (GrabUp() && hitObj != null)
        {
            //soundManager.OnNewItemsDrop(hitObj.transform);
            animator.SetBool("hold", false);
            hitObj.GetComponent<BoxCollider>().isTrigger = false;
            RobotObject robotObj;
            if (hitObj.TryGetComponent<RobotObject>(out robotObj))
            {
                robotObj.StartDestroy();
            }

            hitObjRigid.useGravity = true;

            hitObjRigid.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hitObjRigid.transform.parent = null;
            hitObj = null;
            hitObjRigid = null;
        }
    }

    //move the player and make it jump
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis(horizontal_axis);
        float verticalInput = Input.GetAxis(vertical_axis);

        Vector3 movement = new Vector3(horizontalInput * speed, 0f, verticalInput * speed);

        if (movement != Vector3.zero)
        {
            animator.SetBool("walking", true);
            Vector3 dir = Camera.main.transform.forward;
            dir.y = 0f;
            dir.Normalize();
            transform.Translate(dir * speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationValue);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

}
