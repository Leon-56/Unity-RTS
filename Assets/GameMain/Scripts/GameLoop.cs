﻿using UnityEngine;
using System;
using System.Collections;
using RTS.Scene;

namespace RTS
{
    // Game Main Loop.
    public class GameLoop : MonoBehaviour
    {
        // Scene State.
        SceneStateController m_SceneStateController = new SceneStateController();

        void Awake()
        {
            // Transform scene will not be deleted.
            DontDestroyOnLoad(gameObject);

            // Random number seed.
            UnityEngine.Random.seed = (int) DateTime.Now.Ticks;
        }

        void Start()
        {
            // Set initial scene.
            m_SceneStateController.SetState(new StartState(m_SceneStateController), "");
        }

        void Update()
        {
            m_SceneStateController.StateUpdate();
        }
    }

}