﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PictureState : MonoBehaviour {

    public int State;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
}
