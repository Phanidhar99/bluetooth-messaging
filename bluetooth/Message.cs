namespace WindowsFormsApp1
{
    internal class Message
    {
        private bool v;

        public Message(bool v)
        {
            this.v = v;
        }

        public bool IsToShowDevices { get; internal set; }
    }
}