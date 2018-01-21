using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spout;

public class SpoutLightCookieReceiver : SpoutReceiver {
    

    // Update is called once per frame
    public override void TexShared(TextureInfo texInfo)
    {
        base.TexShared(texInfo);
        GetComponent<Light>().cookie = texture;
    }
}
