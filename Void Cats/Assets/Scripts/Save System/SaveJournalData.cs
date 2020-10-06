using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveJournalData
{    
    //texture data in unique format which can be serialised
    public UniqueTextureFormat[] fishSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] dogSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] tigerSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] dragonSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] cowSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] duckSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] catSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] rabbitSprites = new UniqueTextureFormat[4];

    public UniqueTextureFormat[] beetleSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] snailSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] wormSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] slugSprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] butterflySprites = new UniqueTextureFormat[4];
    public UniqueTextureFormat[] antSprites = new UniqueTextureFormat[4];

    public UniqueTextureFormat[] miscSprites = new UniqueTextureFormat[16];

    //bool arrays which stores if a photo is made
    public bool[] fish = new bool[4];
    public bool[] dog = new bool[4];
    public bool[] tiger = new bool[4];
    public bool[] dragon = new bool[4];
    public bool[] cow = new bool[4];
    public bool[] duck = new bool[4];
    public bool[] cat = new bool[4];
    public bool[] rabbit = new bool[4];

    public bool[] beetle = new bool[4];
    public bool[] snail = new bool[4];
    public bool[] worm = new bool[4];
    public bool[] slug = new bool[4];
    public bool[] butterfly = new bool[4];
    public bool[] ant = new bool[4];

    public bool[] misc = new bool[16];

    public bool[] tp = new bool[6];

    public SaveJournalData(JournalDataStorage storage)
    {
        //set up all photos (except the misc ones as they have bigger size)
        for (int i = 0; i < storage.FishPhotosIsTaken.Length; i++)
        {
            fish[i] = storage.FishPhotosIsTaken[i];
            dog[i] = storage.DogPhotosIsTaken[i];
            tiger[i] = storage.TigerPhotosIsTaken[i];
            dragon[i] = storage.DragonPhotosIsTaken[i];
            cow[i] = storage.CowPhotosIsTaken[i];
            duck[i] = storage.DuckPhotosIsTaken[i];
            cat[i] = storage.CatPhotosIsTaken[i];
            rabbit[i] = storage.RabbitPhotosIsTaken[i];

            beetle[i] = storage.BeetlePhotosIsTaken[i];
            snail[i] = storage.SnailPhotosIsTaken[i];
            worm[i] = storage.WormPhotosIsTaken[i];
            slug[i] = storage.SlugPhotosIsTaken[i];
            butterfly[i] = storage.ButterflyPhotosIsTaken[i];
            ant[i] = storage.AntPhotosIsTaken[i];

            fishSprites[i] = SerialiseTexture.Serialise(storage.FishSprites[i]);
            dogSprites[i] = SerialiseTexture.Serialise(storage.DogSprites[i]);
            tigerSprites[i] = SerialiseTexture.Serialise(storage.TigerSprites[i]);
            dragonSprites[i] = SerialiseTexture.Serialise(storage.DragonSprites[i]);
            cowSprites[i] = SerialiseTexture.Serialise(storage.CowSprites[i]);
            duckSprites[i] = SerialiseTexture.Serialise(storage.DuckSprites[i]);
            catSprites[i] = SerialiseTexture.Serialise(storage.CatSprites[i]);
            rabbitSprites[i] = SerialiseTexture.Serialise(storage.RabbitSprites[i]);

            beetleSprites[i] = SerialiseTexture.Serialise(storage.BeetleSprites[i]);
            snailSprites[i] = SerialiseTexture.Serialise(storage.SnailSprites[i]);
            wormSprites[i] = SerialiseTexture.Serialise(storage.WormSprites[i]);
            slugSprites[i] = SerialiseTexture.Serialise(storage.SlugSprites[i]);
            butterflySprites[i] = SerialiseTexture.Serialise(storage.ButterflySprites[i]);
            antSprites[i] = SerialiseTexture.Serialise(storage.AntSprites[i]);
        }

        //setup the misc photos seperately as they have bigger size than the rest
        for (int i = 0; i < storage.MiscPhotoIsTaken.Length; i++)
        {
            misc[i] = storage.MiscPhotoIsTaken[i];
            miscSprites[i] = SerialiseTexture.Serialise(storage.MiscSprites[i]);
        }

        //setup tp found seperately as it also has a different size
        for (int i = 0; i < storage.TeleportersFound.Length; i++)
        {
            tp[i] = storage.TeleportersFound[i];            
        }
    }

}
