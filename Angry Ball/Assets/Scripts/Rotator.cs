using UnityEngine;

public class Rotator : MonoBehaviour 
{
    private void Start()
	{
		int rand = Random.Range (1, 4);
        switch (rand)
        {
            case 1:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 2:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 3:
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
    }

    private void Update() 
	{
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
