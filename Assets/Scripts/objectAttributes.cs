using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class objectAttributes
{
   public float objectHealt;
   public objectType objType;
   public float durability;
   public resourceType resType;
   public float objectSize;
   public itemType itmType;

   public bool shouldDie()
   {
       return(objectHealt<=0);
   }

   public void gatherLoot()
   {
     switch(resType)
     {
        case resourceType.Wood:
           Debug.Log("Loot Wood");
           //objectSize ile çarpılarak loot verilecek. Daha büyük ağaç = Daha fazla odun
           break;


        case resourceType.Stone:
           Debug.Log("Loot Stone");
           break;
     }
   }

   public bool shouldDisappear()
   {
      return(durability<=0);
   }

   public enum objectType
   {
      Resource,
      Item
   }
   public enum resourceType
   {
      nar,
      Wood,
      Stone,
      Dicks
   }
   public enum itemType
   {
      nai,
      wooden,
      stone,
      iron
   }
}
