using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 [System.Serializable]
public class InventoryItemData 
{
    public string name;
    public byte[] spriteTex;
    public int spriteW;
    public int spriteH;
    

    public InventoryItemData(UseDrop item){
        Debug.Log(item.sprite);
        Texture2D spriteTexture =  new Texture2D((int)item.sprite.rect.width,(int)item.sprite.rect.width);
        Sprite sprite =  item.sprite;
        
        Color[] newColors =  sprite.texture.GetPixels((int)sprite.rect.x, 
                                                    (int)sprite.rect.y, 
                                                    (int)sprite.rect.width, 
                                                    (int)sprite.rect.height );
        
        spriteTexture.SetPixels(newColors);
        spriteTexture.Apply();
        spriteTex = spriteTexture.GetRawTextureData();
        spriteH = (int)sprite.rect.height;
        spriteW = (int)sprite.rect.width;

        name = item.name;
    }
}
