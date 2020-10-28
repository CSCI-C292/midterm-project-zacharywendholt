using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] Object nextLevel;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        
    }
    void OnTriggerEnter2D(Collider2D collider) {
        SceneManager.LoadScene(nextLevel.name, LoadSceneMode.Single);
        collider.transform.position = new Vector2(0,0);
    }
}
