﻿using Enterprise_Dot_Net_Core_WebApp.Core.Interface.DesignPatterns.BehavioralPatterns.Observer;
using System;
using System.Collections.Generic;

namespace Enterprise_Dot_Net_Core_WebApp.Core.DesignPatterns.Observer
{
    public class Subject : ISubject
    {
        private List<IObserver> _observersList = new List<IObserver>();

        public object Obj { get; set; } = new object();

        public Subject(object NewObj)
        {
            Obj = NewObj;
        }

        public Subject() { }

        public void Attach(IObserver observer)
        {
            _observersList.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observersList.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observerItem in _observersList)
                observerItem.Update(this);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Subject() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
