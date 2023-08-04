using System.Runtime.InteropServices;

namespace Cafebazaar.Communicators
{
    public class JavascriptCommunicator
    {
        [DllImport("__Internal")]
        public static extern string GetUrl();
    }
}