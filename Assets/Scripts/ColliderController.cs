using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(Screen.width, -5, 0);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
        worldPosition = new Vector3(worldPosition.x, worldPosition.y, 0);
        gameObject.transform.position = worldPosition;
    }
}
