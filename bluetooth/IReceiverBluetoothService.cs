using System;
using System.ComponentModel;

namespace WindowsFormsApp1
{
    internal interface IReceiverBluetoothService
    {
        Action<object, PropertyChangedEventArgs> PropertyChanged { get; set; }
        bool WasStarted { get; }

        void Start(Action<string> setData);
        void Stop();
    }
}