using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime = 0.05f; //give a small life time to the laser, because we don't want it existing for long.
    private LineRenderer line;
    private void Awake() //Awake happens when the object is loading, and start happens when it begins
    {
        line = GetComponent<LineRenderer>();
    }
    public void Init(Color c, Vector3 start, Vector3 end)
    {
        line.startColor = c;
        line.endColor = c;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        Invoke("DestroyMe", lifeTime); //"Invoke" takes a string name for a function to call after the duration. Invoke is of the form Invoke(string functionNameToCall, float durationBeforeCalling);
    }
    private void DestroyMe() //this destroys the game object; in this case, itself
    {
        Destroy(this.gameObject);
    }

}
