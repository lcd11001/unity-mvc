using RMC.Mini;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MonoMVCS
{
    public abstract class MonoModelLocator<T> : MonoModel
        where T : IScriptableModel
    {
        public bool keepModel = true;
        protected string defaultKey = "";
        public override void Initialize(IContext context)
        {
            base.Initialize(context);

            var item = GenericGetItem();
            bool hasItem = item != null;

            if (hasItem == false)
            {
                context.ModelLocator.AddItem(this);
            }
            else
            {
                if (keepModel)
                {
                    // apply value from existing model to this model
                    var existingModel = (MonoModelLocator<T>)item;
                    this.CloneFrom(existingModel);
                }
                else
                {
                    // remove existing model
                    GenericRemoveItem();
                    context.ModelLocator.AddItem(this);
                }
            }
        }

        public virtual void CloneFrom(MonoModelLocator<T> model)
        {
        }

        public override void Dispose()
        {
            base.Dispose();

            if (!keepModel)
            {
                GenericRemoveItem();
                //Context.ModelLocator.RemoveItem<T>();
            }
        }

        private void GenericRemoveItem()
        {
            Type type = Locator.GetLowestType(this.GetType());
            // Use reflection to call the generic method with a runtime type
            MethodInfo removeItemMethod = Context.ModelLocator.GetType().GetMethod(nameof(Context.ModelLocator.RemoveItem));
            MethodInfo genericRemoveItemMethod = removeItemMethod.MakeGenericMethod(type);
            genericRemoveItemMethod.Invoke(Context.ModelLocator, new object[] { defaultKey });
        }

        private MonoModelLocator<T> GenericGetItem()
        {
            Type type = Locator.GetLowestType(this.GetType());
            // Use reflection to call the generic method with a runtime type
            MethodInfo getItemMethod = Context.ModelLocator.GetType().GetMethod(nameof(Context.ModelLocator.GetItem));
            MethodInfo genericGetItemMethod = getItemMethod.MakeGenericMethod(type);
            MonoModelLocator<T> item = (MonoModelLocator<T>)genericGetItemMethod.Invoke(Context.ModelLocator, new object[] { defaultKey });
            return item;
        }

        private bool GenericHasItem()
        {
            Type type = Locator.GetLowestType(this.GetType());
            // Use reflection to call the generic method with a runtime type
            MethodInfo hasItemMethod = Context.ModelLocator.GetType().GetMethod(nameof(Context.ModelLocator.HasItem));
            MethodInfo genericHasItemMethod = hasItemMethod.MakeGenericMethod(type);
            bool hasItem = (bool)genericHasItemMethod.Invoke(Context.ModelLocator, new object[] { defaultKey });
            return hasItem;
        }
    }
}
