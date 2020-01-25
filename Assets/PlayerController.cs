using System.Net;
using System.Text; 
using System.IO;  
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    private Rigidbody2D body;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float direccion = Input.GetAxis("Horizontal");
        body.velocity = new Vector3(direccion * speed, body.velocity.y, transform.position.z);   

        if(Input.GetKeyDown(KeyCode.Space)){
            
            body.AddForce(jump * jumpForce, ForceMode2D.Impulse);

            var request = (HttpWebRequest)WebRequest.Create("http://localhost:3000/test");

            var postData = "jump=1";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Debug.Log(responseString);
        }
    }
}
