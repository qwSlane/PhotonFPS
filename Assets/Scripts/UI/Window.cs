﻿using UnityEngine;

namespace DefaultNamespace.UI
{
    public abstract class Window : MonoBehaviour
    {
        public bool IsOpen { get; private set; }

        public delegate void CloseEventHandler(Window sender);

        public event CloseEventHandler OnClose;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            IsOpen = true;
            gameObject.SetActive(true);
            SelfOpen();
        }

        protected abstract void SelfOpen();

        public void Close()
        {
            IsOpen = false;
            OnClose?.Invoke(this);
            SelfClose();
        }

        protected abstract void SelfClose();
    }
}