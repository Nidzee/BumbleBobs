using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;


    [SerializeField] PlayerController _player;
    public PlayerController Player => _player;


    void Awake()
    {
        Instance = this;
    }
}