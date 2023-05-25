using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Items
{
    [Serializable]
    public class Stash
    {
        public int maxStackAmount;
        public List<ItemStack> items;

        public event Action OnChange;

        public Stash()
        {
            items = new List<ItemStack>();
        }
        
        public void AddItem(Item item, int amount = 1)
        {
            var same = items
                .FirstOrDefault(x => x.item == item && x.amount != maxStackAmount);

            if (same is not null)
            {
                RefillStack(same, amount);
            }
            else
            {
                AddStack(item);
                AddItem(item, amount);
            }
            
            OnChange?.Invoke();
        }

        public void RemoveItem(Item item, int amount = 1)
        {
            var same = items
                .FirstOrDefault(x => x.item == item);

            if (same is not null)
            {
                if (same.amount > amount)
                {
                    same.amount -= amount;
                }
                else
                {
                    RemoveStack(same);
                    RemoveItem(item, amount - same.amount);
                }
                
                OnChange?.Invoke();
            }
        }

        public void RemoveItemFromStack(ItemStack stack, int amount = 1)
        {
            var has = items.Any(x => x.Equals(stack));

            if (has)
            {
                if (stack.amount > amount)
                {
                    stack.amount -= amount;
                }
                else
                {
                    RemoveStack(stack);
                    RemoveItem(stack.item, amount - stack.amount);
                }
                
                OnChange?.Invoke();
            }
        }

        private void RefillStack(ItemStack stack, int amount)
        {
            var freeAmounts = maxStackAmount - stack.amount;

            if(freeAmounts >= amount)
            {
                stack.amount += amount;
            }
            else
            {
                stack.amount += amount;
                AddItem(stack.item, amount - freeAmounts);
            }
        }

        private void AddStack(Item item)
        {
            var stack = new ItemStack(item);
            items.Add(stack);
        }

        private void RemoveStack(ItemStack stack)
        {
            items.Remove(stack);
        }
    }

    [Serializable]
    public class ItemStack
    {
        public Item item;
        public int amount;
        
        public ItemStack(Item i)
        {
            item = i;
            amount = 0;
        }
    }
}