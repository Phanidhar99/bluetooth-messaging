﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WindowsFormsApp1
{
    public sealed class SenderViewModel : ViewModelBase
    {
        private readonly ISenderBluetoothService _senderBluetoothService;
        private string _data;
        private Device _selectDevice;
        private string _resultValue;
#pragma warning disable CS0169 // The field 'SenderViewModel.await_senderBluetoothService' is never used
        private readonly object await_senderBluetoothService;
#pragma warning restore CS0169 // The field 'SenderViewModel.await_senderBluetoothService' is never used
        

        /// <summary>  
        /// Initializes a new instance of the <see cref="SenderViewModel"/> class.  
        /// </summary>  
        /// <param name="senderBluetoothService">  
        /// The Sender bluetooth service.  
        /// </param>  
        public SenderViewModel(ISenderBluetoothService senderBluetoothService)
        {
            _senderBluetoothService = senderBluetoothService;
            ResultValue = "N/D";
            SendCommand = new RelayCommand(
                SendData,
                () => !string.IsNullOrEmpty(Data) && SelectDevice != null && SelectDevice.DeviceInfo != null);
            Devices = new ObservableCollection<Device>
        {
            new Device(null) { DeviceName = "Searching..." }
        };
            Messenger.Default.Register<Message>(this, ShowDevice);
        }

        /// <summary>  
        /// Gets or sets the devices.  
        /// </summary>  
        /// <value>  
        /// The devices.  
        /// </value>  
        public ObservableCollection<Device> Devices
        {
            get; set;
        }

        /// <summary>  
        /// Gets or sets the select device.  
        /// </summary>  
        /// <value>  
        /// The select device.  
        /// </value>  
        public Device SelectDevice
        {
            get { return _selectDevice; }
            set { Set(() => SelectDevice, ref _selectDevice, value); }
        }

        /// <summary>  
        /// Gets or sets the data.  
        /// </summary>  
        /// <value>  
        /// The data.  
        /// </value>  
        public string Data
        {
            get { return _data; }
            set { Set(() => Data, ref _data, value); }
        }

        /// <summary>  
        /// Gets or sets the result value.  
        /// </summary>  
        /// <value>  
        /// The result value.  
        /// </value>  
        public string ResultValue
        {
            get { return _resultValue; }
            set { Set(() => ResultValue, ref _resultValue, value); }
        }

        /// <summary>  
        /// Gets the send command.  
        /// </summary>  
        /// <value>  
        /// The send command.  
        /// </value>  
        public ICommand SendCommand { get; private set; }

        private async void SendData()
        {
            ResultValue = "N/D";
            var wasSent = await _senderBluetoothService.Send(SelectDevice, Data);
            if (wasSent)
            {
                ResultValue = "The data was sent.";
            }
            else
            {
                ResultValue = "The data was not sent.";
            }
        }   

        /// <summary>  
        /// Shows the device.  
        /// </summary>  
        /// <param name="deviceMessage">The device message.</param>  
        private async void ShowDevice(Message deviceMessage)
        {
            if (deviceMessage.IsToShowDevices)
            {
                Devices.Clear();
                var items = await _senderBluetoothService.GetDevices();
                foreach (var dev in items)
                {
                    Devices.Add(dev);
                }

                Data = string.Empty;
            }
        }
    }

}
