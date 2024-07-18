using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class soundManager : MonoBehaviour
{
    [SerializeField] private UnityEvent clickSound;
    [SerializeField] private UnityEvent winningSound;
    [SerializeField] private UnityEvent BackSound;
    public void SelectButtonSound()
    {
        clickSound.Invoke();
    }
    public void BackButtonSound()
    {
        BackSound.Invoke();
    }
    public void CertificateSound()
    {
        winningSound.Invoke();
    }
}
