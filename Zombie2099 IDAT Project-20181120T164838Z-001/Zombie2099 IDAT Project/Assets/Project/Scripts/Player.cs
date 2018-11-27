using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public event System.Action OnReachedEndOfLevel;

    public float speed = 10.0f;
    private float translation;
    private float straffe;
    bool disabled;

    // Use this for initialization
    void Start() {
        Guard.OnGuardHasSpottedPlayer += Disable;
    }

    // Update is called once per frame
    void Update() {
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (!disabled)
        {
            transform.Translate(straffe, 0, translation);
        }

        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void OnTriggerEnter(Collider hitCollider)
    {
        if (hitCollider.tag == "Lift")
        {
            Disable();
            if(OnReachedEndOfLevel != null)
            {
                OnReachedEndOfLevel();
            }
        }
    }

    void Disable() {
        disabled = true;
    }

    private void OnDestroy() {
        Guard.OnGuardHasSpottedPlayer -= Disable;
    }
}