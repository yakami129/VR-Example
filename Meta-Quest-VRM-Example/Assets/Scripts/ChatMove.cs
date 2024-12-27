using UnityEngine;


public class ChatMove : MonoBehaviour
{
    public float speed = 3f;

    // 旋转速度
    public float turnSpeed = 10f;

    public float changeDirectionInterval = 2f;
    private float timer;
    private Vector3 move;
    private Rigidbody rb;

    public Animator animator;

    float forwardAmount;
    float turnAmount;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Debug.Log("ChatMove已启动");
    }

    void Update()
    {

        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");

        // 将世界坐标系下的移动方向转换为本地坐标系下的移动方向
        move = new Vector3(x, 0, z);
        Vector3 localMove = transform.InverseTransformDirection(move);

        forwardAmount = localMove.z;
        turnAmount = Mathf.Atan2(localMove.x, localMove.z);

        ChangeAnimation();
    }

    private void FixedUpdate()
    {
        rb.velocity = forwardAmount * transform.forward * speed;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turnAmount * turnSpeed, 0));
    }


    private void ChangeAnimation()
    {
        animator.SetFloat("speed", move.magnitude);
        Debug.Log($"动画速度已更新: {move.magnitude:F2}");
    }
}
