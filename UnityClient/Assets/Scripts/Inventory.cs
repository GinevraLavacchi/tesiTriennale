using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public string type="NONE";
        public int count=0;
        public int maxAllowed=99;
        public Sprite icon;

        public Slot()
        {

        }

        public Slot(string type, int count, Sprite icon)
        {
            this.type = type;
            this.count = count;
            this.icon = icon;
        }
        public bool CanAddItem()
        {
            if (count < maxAllowed) return true;
            return false;
        }
        public void AddItem(string type, Sprite icon)
        {
            this.type = type;
            this.icon= icon;
            count++;
        }
        public void RemoveItem(int val)
        {
            Debug.Log(count + "-" + val);
            count=count-val;
        }
    }
    
    public List<Slot> slots=new List<Slot>();
    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
        
    }

    /* public void AddSlot(string typetoAdd,Sprite icon)
     {
         foreach (Slot slot in slots) 
         {
             if (slot.type == typetoAdd && slot.CanAddItem())
             {
                 slot.AddItem(typetoAdd,icon);
                 return;
             }
         }
         foreach (Slot slot in slots)
         {
             if(slot.type=="NONE")
             {
                 slot.AddItem(typetoAdd,icon);
                 return;
             }
         }
     }
     public void AddSlot(string typetoAdd, Sprite icon, int amount)
     {
         for (int i = 0; i < amount; i++)
         {
             foreach (Slot slot in slots)
             {
                 if (slot.type == typetoAdd && slot.CanAddItem())
                 {
                     slot.AddItem(typetoAdd, icon);
                     break;
                 }
             }

             bool added = false;

             foreach (Slot slot in slots)
             {
                 if (slot.type == typetoAdd && slot.CanAddItem())
                 {
                     slot.AddItem(typetoAdd, icon);
                     added = true;
                     break;
                 }
             }

             if (!added)
             {
                 foreach (Slot slot in slots)
                 {
                     if (slot.type == "NONE")
                     {
                         slot.AddItem(typetoAdd, icon);
                         added = true;
                         break;
                     }
                 }
             }

             if (!added)
             {
                 Debug.LogWarning("Inventario pieno, impossibile aggiungere: " + typetoAdd);
                 return;
             }
         }
     }*/
    public void AddSlot(string typetoAdd, Sprite icon)
    {
        AddSlot(typetoAdd, icon, 1);
    }

    public void AddSlot(string typetoAdd, Sprite icon, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            bool added = false;

            foreach (Slot slot in slots)
            {
                if (slot.type == typetoAdd && slot.CanAddItem())
                {
                    slot.AddItem(typetoAdd, icon);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                foreach (Slot slot in slots)
                {
                    if (slot.type == "NONE")
                    {
                        slot.AddItem(typetoAdd, icon);
                        added = true;
                        break;
                    }
                }
            }

            if (!added)
            {
                Debug.LogWarning("Inventario pieno, impossibile aggiungere: " + typetoAdd);
                return;
            }
        }
    }
    public void FindItemToRemove(string typeRemove,int val)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].type== typeRemove)
            {
                if(slots[i].count==val)
                {
                    slots[i].type="NONE";
                    slots[i].icon=null;
                    slots[i].count = 0;
                }
                else
                {
                    slots[i].RemoveItem(val);
                }
            }
        }
        
    }
    public bool isFull(string type)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type=="NONE")
            {
                return false;
            }
            if(slot.type == type&&slot.CanAddItem())
            {
                return false;
            }
        }
        return true;
    }
}
