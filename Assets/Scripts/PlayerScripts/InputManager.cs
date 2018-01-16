using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool m_Button1 = false;
    public bool m_Button2 = false;
    public bool m_Button3 = false;
    public bool m_Button4 = false;

    public float m_DirectionHorizontal = 0.0f;
    public float m_DirectionVertical = 0.0f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Read the inputs.
        m_Button1 = Input.GetButton("Button1");
        m_Button2 = Input.GetButton("Button2");
        m_Button3 = Input.GetButton("Button3");
        m_Button4 = Input.GetButton("Button4");

        //bool crouch = Input.GetKey(KeyCode.DownArrow);
        m_DirectionHorizontal = Input.GetAxis("Horizontal");
        m_DirectionVertical   = Input.GetAxis("Vertical");
    }
}
