using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public interface ISenderBluetoothService
    {
        Task Send(Device selectDevice, string data);
        Task send(Device selectDevice, string data);
    }
}