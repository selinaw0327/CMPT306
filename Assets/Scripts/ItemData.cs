using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

 [System.Serializable]
public class ItemData 
{
    
    

    public string name;
    
    
    public float[] position;
    
    public byte[] spriteTex;
    public int spriteW;
    public int spriteH;

    public ItemData(Item item)
    {
        Texture2D spriteTexture =  new Texture2D((int)item.itemSprite.rect.width,(int)item.itemSprite.rect.width);
        Sprite sprite =  item.itemSprite;
        //SetTextureImporterFormat(item.itemSprite.texture, true);
        Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
                                                    (int)sprite.textureRect.y, 
                                                    (int)sprite.textureRect.width, 
                                                    (int)sprite.textureRect.height );
        
        spriteTexture.SetPixels(newColors);
        spriteTexture.Apply();
        spriteTex = spriteTexture.GetRawTextureData();
        spriteH = item.itemSprite.texture.height;
        spriteW = item.itemSprite.texture.width;

        position = new float[2];
        position[0] = item.transform.position.x;
        position[1] = item.transform.position.y;

        name = item.name;
    }

    public static void SetTextureImporterFormat( Texture2D texture, bool isReadable)
{
        if ( null == texture ) return;

        string assetPath = AssetDatabase.GetAssetPath( texture );
        var tImporter = AssetImporter.GetAtPath( assetPath ) as TextureImporter;
        if ( tImporter != null )
        {
            tImporter.textureType = TextureImporterType.Advanced;

            tImporter.isReadable = isReadable;

            AssetDatabase.ImportAsset( assetPath );
            AssetDatabase.Refresh();
    }
}

}
