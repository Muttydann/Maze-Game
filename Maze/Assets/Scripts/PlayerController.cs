using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float mvX;
    private float mvY;
    public float speed = 0;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    private Collider coll;
    private float dist2Gr;
    public GameObject winTextObj;

    // Start is called before the first frame update
    void Start()
    {
        winTextObj.SetActive(false);
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        dist2Gr = coll.bounds.extents.y;
        SetScoreText();
    }

    void OnMove(InputValue mvVal)
    {
        Vector2 mvVec = mvVal.Get<Vector2>();
        mvX = mvVec.x;
        mvY = mvVec.y;
    }

    void OnJump()
    {
        if(isGrounded())
        rb.AddForce(0,300,0);
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, dist2Gr + 0.2f);
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void FixedUpdate()
    {
        Vector3 mv = new Vector3(mvX, 0.0f, mvY);
        rb.AddForce(mv*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickUp"))
        {
            other.gameObject.SetActive(false);
            score += 100;
            SetScoreText();
            if (GameObject.FindGameObjectWithTag("pickUp") == null)
            {
                winTextObj.SetActive(true);
            }
        }

        if (other.gameObject.CompareTag("KillPlane")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
