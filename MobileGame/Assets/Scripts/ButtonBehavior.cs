using System.Diagnostics.Contracts;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [Header("Atirbutes")]
    public bool Team = false;
    public bool Close = false;
    public bool Far = true;
    [SerializeField] public float CloseRange = 3f;


}
