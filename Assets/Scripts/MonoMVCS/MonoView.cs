using RMC.Mini;
using RMC.Mini.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoMVCS
{
    public abstract class MonoView : MonoBehaviour, IView, IDisposable
    {
        public bool IsInitialized => _isInitialized;

        public IContext Context => _context;

        private bool _isInitialized = false;
        private IContext _context;

        protected virtual void Start()
        {
            RequireIsInitialized();
        }

        public virtual void Initialize(IContext context)
        {
            _context = context;
            _isInitialized = true;
        }

        public virtual void RequireIsInitialized()
        {
            if (!_isInitialized)
            {
                Debug.LogError("BaseView: MustBeInitialized from MonoMVCS::Awake");
                throw new Exception("MustBeInitialized");
            }
        }

        public virtual void InitView(MonoModel data)
        {
        }

        public abstract void UpdateView(MonoModel data);

        public virtual void Dispose()
        {
        }
    }
}