using Unity.Netcode.Components;
using UnityEngine;

public class S_ClientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false; 
    }
}
