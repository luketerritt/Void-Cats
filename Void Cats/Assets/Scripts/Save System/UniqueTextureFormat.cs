using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a custom class made so the sprites can be saved alongside the save system
// as unity classes cannot be serialized to write/read from files directly
[Serializable]
public class UniqueTextureFormat
{
    public int x;
   
    public int y;
    
    public byte[] bytes;
}

//class to apply the serialization
public static class SerialiseTexture
{

    public static UniqueTextureFormat Serialise(Sprite inputSprite)
    {
        Debug.Log("Starting to serialise texture");
        Texture2D tex = inputSprite.texture;
        UniqueTextureFormat temp = new UniqueTextureFormat();
        temp.x = tex.width;
        temp.y = tex.height;
        temp.bytes = ImageConversion.EncodeToPNG(tex);
        Debug.Log("Serialisation finished for a texture");
        return temp;
    }

    public static Sprite DeSerialise(UniqueTextureFormat texture)
    {
        Debug.Log("Starting to Deserialise texture");
        Texture2D tex = new Texture2D(texture.x, texture.y);
        ImageConversion.LoadImage(tex, texture.bytes);
        Sprite temp = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
        Debug.Log("Deserialisation finished for a texture");
        return temp;
    }
}


