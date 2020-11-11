
using UnityEngine;


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
        
        Color[] newColors =  sprite.texture.GetPixels((int)sprite.rect.x, 
                                                    (int)sprite.rect.y, 
                                                    (int)sprite.rect.width, 
                                                    (int)sprite.rect.height );
        
        spriteTexture.SetPixels(newColors);
        spriteTexture.Apply();
        spriteTex = spriteTexture.GetRawTextureData();
        spriteH = (int)sprite.rect.height;
        spriteW = (int)sprite.rect.width;

        position = new float[2];
        position[0] = item.transform.position.x;
        position[1] = item.transform.position.y;

        name = item.name;
    }



}
