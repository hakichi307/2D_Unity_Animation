using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int walkSpeed = 2;
    public int runSpeed = 4;
    public float traiPhai;
    public bool isFacingRight = true;
    public bool isRunning = false;
    public float lucNhay = 5;
    public Animator anim;
    private bool duocPhepNhay;
    private bool nhayDoi;
    public Transform _duocPhepNhay;
    public LayerMask san;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        duocPhepNhay = Physics2D.OverlapCircle(_duocPhepNhay.position, 0.2f, san);
        traiPhai = Input.GetAxisRaw("Horizontal");
        // Check for run input (replace with your desired condition)
        isRunning = Input.GetKey(KeyCode.LeftShift);
        // Calculate speed based on run state
        int currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(currentSpeed * traiPhai, rb.velocity.y);
        if (isFacingRight == true && traiPhai == -1)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            // Tạo một Vector3 mới, giữ nguyên x và y, đặt z về 0
            transform.localPosition = new Vector3(transform.localPosition.x - 1.3f, transform.localPosition.y, 0);
            isFacingRight = false;
        }
        if (isFacingRight == false && traiPhai == 1)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            transform.localPosition = new Vector3(transform.localPosition.x + 1.3f, transform.localPosition.y, 0);
            isFacingRight = true;
        }
        //Animation
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Dame");
        }

        anim.SetFloat("Speed", Mathf.Abs(traiPhai));
        if (isRunning)
        {
            anim.SetFloat("Speed", 0);
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        
        if (!Input.GetKey(KeyCode.Space) && duocPhepNhay)
        {

            nhayDoi = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
            if (duocPhepNhay || nhayDoi)
            {
                rb.velocity = new Vector2(rb.velocity.x, lucNhay);
                nhayDoi = !nhayDoi;
            }
            
        }
    }

}
