using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public sealed class MainViewModel : ViewModelBase
    {
        private bool _isReceiver;

        /// <summary>  
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.  
        /// </summary>  
        public MainViewModel()
        {
            PropertyChanged += MainViewModelPropertyChanged;
            IsSender = false;
        }

        /// <summary>  
        /// Gets or sets a value indicating whether is Receiver.  
        /// </summary>  
        /// <value>  
        /// The is Receiver.  
        /// </value>  
        public bool IsReceiver
        {
            get
            {
                return _isReceiver;
            }
            set
            {
                Set(() => IsReceiver, ref _isReceiver, value);
                RaisePropertyChanged(() => IsSender);
            }
        }

        /// <summary>  
        /// Gets or sets a value indicating whether is Sender.  
        /// </summary>  
        /// <value>  
        /// The is Sender.  
        /// </value>  
        public bool IsSender
        {
            get
            {
                return !_isReceiver;
            }
            set
            {
                Set(() => IsSender, ref _isReceiver, !value);
                RaisePropertyChanged(() => IsReceiver);
            }
        }

        /// <summary>  
        /// Gets or sets the Receiver visibility.  
        /// </summary>  
        /// <value>  
        /// The Receiver visibility.  
        /// </value>  
        public Visibility ReceiverVisibility
        {
            get
            {
                return _isReceiver ? Visibility.Visible : Visibility.Collapsed;
            }
            set
            {
                _isReceiver = value == Visibility.Visible;
            }
        }

        /// <summary>  
        /// Gets or sets the Sender visibility.  
        /// </summary>  
        /// <value>  
        /// The Sender visibility.  
        /// </value>  
        public Visibility SenderVisibility
        {
            get
            {
                return !_isReceiver ? Visibility.Visible : Visibility.Collapsed;
            }
            set
            {
                _isReceiver = value != Visibility.Visible;
            }
        }

        /// <summary>  
        /// Mains the view model property changed.  
        /// </summary>  
        /// <param name="sender">The sender.</param>  
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>  
        private void MainViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReceiver" || e.PropertyName == "IsSender")
            {
                RaisePropertyChanged(() => ReceiverVisibility);
                RaisePropertyChanged(() => SenderVisibility);

                if (e.PropertyName == "IsReceiver")
                {
                    Messenger.Default.Send(IsSender ? new Message(true) : new Message(false));
                }
            }
        }
    }

    public class Visibility
    {
        public static Visibility Visible { get; internal set; }
        public static Visibility Collapsed { get; internal set; }
    }
}
