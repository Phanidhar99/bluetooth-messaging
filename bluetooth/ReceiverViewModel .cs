using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WindowsFormsApp1
{
    public sealed class ReceiverViewModel : ViewModelBase
    {
        private readonly IReceiverBluetoothService _receiverBluetoothService;
        private string _data;
        private bool _isStarEnabled;
        private string _status;
        #pragma warning disable CS0649 // Field 'ReceiverViewModel.receiverBluetoothService' is never assigned to, and will always have its default value null
        private IReceiverBluetoothService receiverBluetoothService;
        #pragma warning restore CS0649 // Field 'ReceiverViewModel.receiverBluetoothService' is never assigned to, and will always have its default value null

        /// <summary>  
        /// Initializes a new instance of the <see cref="ReceiverViewModel" /> class.  
        /// </summary>  
        ///   
        public ReceiverViewModel()
        {
            _receiverBluetoothService = receiverBluetoothService;
            _receiverBluetoothService.PropertyChanged += ReceiverBluetoothService_PropertyChanged;
            IsStarEnabled = true;
            Data = "N/D";
            Status = "N/D";
            StartCommand = new RelayCommand(() =>
            {
                _receiverBluetoothService.Start(SetData);
                IsStarEnabled = false;
                Data = "Can receive data.";
            });

            StopCommand = new RelayCommand(() =>
            {
                _receiverBluetoothService.Stop();
                IsStarEnabled = true;
                Data = "Cannot receive data.";
            });

            Messenger.Default.Register<Message>(this, ResetAll);
        }

        /// <summary>  
        /// Resets all.  
        /// </summary>  
        /// <param name="message">The message.</param>  
        private void ResetAll(Message message)
        {
            if (!message.IsToShowDevices)
            {
                if (_receiverBluetoothService.WasStarted)
                {
                    _receiverBluetoothService.Stop();
                }
                IsStarEnabled = true;
                Data = "N/D";
                Status = "N/D";
            }
        }

        /// <summary>  
        /// The set data received.  
        /// </summary>  
        /// <param name="data">  
        /// The data.  
        /// </param>  
        public void SetData(string data)
        {
            Data = data;
        }

        /// <summary>  
        /// Gets or sets the data.  
        /// </summary>  
        /// <value>  
        /// The data received.  
        /// </value>  
        public string Data
        {
            get { return _data; }
            set { Set(() => Data, ref _data, value); }
        }

        /// <summary>  
        /// Gets the start command.  
        /// </summary>  
        /// <value>  
        /// The start command.  
        /// </value>  
        public ICommand StartCommand { get; private set; }

        /// <summary>  
        /// Gets the stop command.  
        /// </summary>  
        /// <value>  
        /// The stop command.  
        /// </value>  
        public ICommand StopCommand { get; private set; }

        /// <summary>  
        /// Gets or sets a value indicating whether is star enabled.  
        /// </summary>  
        /// <value>  
        /// The is star enabled.  
        /// </value>  
        public bool IsStarEnabled
        {
            get
            {
                return _isStarEnabled;
            }
            set
            {
                Set(() => IsStarEnabled, ref _isStarEnabled, value);
                RaisePropertyChanged(() => IsStopEnabled);
            }
        }

        /// <summary>  
        /// Gets or sets a value indicating whether is stop enabled.  
        /// </summary>  
        /// <value>  
        /// The is stop enabled.  
        /// </value>  
        public bool IsStopEnabled
        {
            get
            {
                return !_isStarEnabled;
            }
            set
            {
                Set(() => IsStopEnabled, ref _isStarEnabled, !value);
                RaisePropertyChanged(() => IsStarEnabled);
            }
        }

        /// <summary>  
        /// Gets or sets the status.  
        /// </summary>  
        /// <value>The status.</value>  
        public string Status
        {
            get { return _status; }
            set { Set(() => Status, ref _status, value); }
        }

        /// <summary>  
        /// Handles the PropertyChanged event of the ReceiverBluetoothService control.  
        /// </summary>  
        /// <param name="sender">The source of the event.</param>  
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>  
        private void ReceiverBluetoothService_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WasStarted")
            {
                IsStarEnabled = true;
            }
        }
    }

}