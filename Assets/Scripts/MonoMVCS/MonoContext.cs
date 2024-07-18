using RMC.Mini;
using RMC.Mini.Experimental.ContextLocators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoMVCS
{
    public class MonoContext : BaseContext
    {
        protected static Dictionary<string, int> _contextCount = new();
        protected readonly string _contextKey = "";
        public MonoContext rootContext => ContextLocator.Instance.GetItem<MonoContext>(_contextKey);
        private MonoContext(string contextKey = "") : base()
        {
            _contextKey = contextKey;
            if (ContextLocator.Instance.HasItem<MonoContext>(_contextKey))
            {
                _contextCount[_contextKey] += 1;
                // Debug.Log("MonoContext increase ref count: " + _contextCount[_contextKey]);
            }
            else
            {
                // Debug.Log("MonoContext Create: " + _contextKey);
                ContextLocator.Instance.AddItem(this, _contextKey);
                _contextCount[_contextKey] = 1;
            }
        }

        public override void Dispose()
        {
            if (_contextCount[_contextKey] > 1)
            {
                _contextCount[_contextKey] -= 1;
                // Debug.Log("MonoContext decrease ref count: " + _contextCount[_contextKey]);
            }
            else
            {
                // Debug.Log("MonoContext Dispose: " + _contextKey);
                if (ContextLocator.Instance.HasItem<MonoContext>(_contextKey))
                {
                    // fixed: ContextLocator.Instance is destroyed when exiting play mode
                    // so that, RemoveItem will throw an exception
                    ContextLocator.Instance.RemoveItem<MonoContext>(_contextKey);
                }
                _contextCount.Remove(_contextKey);
            }
        }

        public static MonoContext CreateContext(string contextKey = null)
        {
            return new MonoContext(contextKey ?? new Guid().ToString());
        }

        public static MonoContext SharedContex(string contextKey = "")
        {
            // should use create context for keeping ref count
            MonoContext shared = CreateContext(contextKey);
            return shared.rootContext;
        }
    }
}
