using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private int count;
    private int result;

    private int eaten;
    private int eatenRed;
    private int eatenGreen;
    private int eatenBlue;
    
    public Color newColour;
    public GUIText countText;
    public GUIText winText;

    public Text EatenText;
    public Text EatenRedText;
    public Text EatenGreenText;
    public Text EatenBlueText;

    private void Start()
    {
        count = 0;
        SetCountText();
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        newColour = Color.red;        
        winText.text = String.Empty;
    }

    private void Update()
    {
        countText.text = String.Format("Count: {0}/12", count);
        EatenText.text = String.Format("Eaten: {0}", eaten);
        EatenRedText.text = String.Format("Eaten red: {0}", eatenRed);
        EatenGreenText.text = String.Format("Eaten green: {0}", eatenGreen);
        EatenBlueText.text = String.Format("Eaten blue: {0}", eatenBlue);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().AddForce(movement*speed*Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !(GetComponent<Rigidbody>().position.y > 0.5f))
        {
            Jump();
        }

        if (GetComponent<Rigidbody>().position.y < -0.5f)
        {
            result = count;
            gameObject.SetActive(false);
            count = -1;
            SetCountText();
        }

        ColourChanging();
    }

    private void ColourChanging()
    {
        Color r = Color.red;
        Color g = Color.green;
        Color b = Color.blue;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            newColour = r;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            newColour = g;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            newColour = b;
        }

        gameObject.GetComponent<Renderer>().material.color = newColour;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            if (other.gameObject.GetComponent<Renderer>().material.color ==
                gameObject.GetComponent<Renderer>().material.color)
            {
                other.gameObject.SetActive(false);
                count++;
                eaten++;
                if (gameObject.GetComponent<Renderer>().material.color == Color.red)
                {
                    eatenRed++;
                }
                if (gameObject.GetComponent<Renderer>().material.color == Color.green)
                {
                    eatenGreen++;
                }
                if (gameObject.GetComponent<Renderer>().material.color == Color.blue)
                {
                    eatenBlue++;
                }

                SetCountText();
            }
            else
            {
                result = count;
                gameObject.SetActive(false);
                count = -1;
                SetCountText();
            }
        }
    }

    private void SetCountText()
    {
        if (count >= 12)
        {
            winText.text += "You win!";
        }
        if (count == -1)
        {
            countText.text = "";
            winText.text = "You lost!\n" + "Your result is " + result.ToString();
        }
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up*10, ForceMode.Impulse);
    }
}
