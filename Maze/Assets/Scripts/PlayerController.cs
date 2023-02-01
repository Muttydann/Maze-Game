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
    public float speed = 1;
    private int hp = 10;
    public GameObject hpTextObj;
    private TextMeshProUGUI hpText;
    public GameObject timTextObj;
    private TextMeshProUGUI timText;
    private Collider coll;
    private float dist2Gr;
    public GameObject winTextObj;
    private Color hpCol;
    private Renderer rend;
    private float tim = 0;

    // Start is called before the first frame update
    void Start()
    {
        winTextObj.SetActive(false);
        hpText = hpTextObj.GetComponent<TextMeshProUGUI>();
        timText = timTextObj.GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        dist2Gr = coll.bounds.extents.y;
        SetHPText();
        rend = GetComponent<Renderer>();
        rend.material.color = Color.green;
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

    void SetHPText()
    {
        hpText.text = "HP: " + hp.ToString();
    }

    private void Update()
    {
        tim += Time.deltaTime;
        timText.SetText(tim.ToString("F2"));
    }

    private void FixedUpdate()
    {
        Vector3 mv = new Vector3(mvX, 0.0f, mvY);
        rb.AddForce(mv*speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("pickUp"))
        {
            hp--;
            SetHPText();
            hpCol = new Color(0f + .1f*(10-hp), 1.0f - .1f*(10-hp), 0f, 1.0f);
            rend.material.color = hpCol;
            if (hp <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }else if (hp <= 3)
            {
                hpText.color = Color.red;
            }
        }
        else if (other.gameObject.CompareTag("KillPlane")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            Destroy(other.gameObject);
            winTextObj.GetComponent<TextMeshProUGUI>().SetText("You're Win!" + "\n" + "Time: " + tim.ToString("F2"));
            winTextObj.SetActive(true);
            timTextObj.SetActive(false);
            hpTextObj.SetActive(false);
        }
    }

}
