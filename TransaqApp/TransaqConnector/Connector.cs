using System;
using Serilog;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks.Dataflow;


namespace TransaqApp
{
    internal static class XmlConnector
    {   
        private static readonly ILogger Logger;
        private static string _path = Directory.GetCurrentDirectory();
        public static BufferBlock<string> bufferMessages = new BufferBlock<string>();

        public static bool Init()
        {
            CallBackDelegate predicate = data =>
            {
                var result = MarshalUtf8.PtrToStringUtf8(data);
                FreeMemory(data);
                OnMsg(result);
                return true;
            };
            var CallbackHandle = GCHandle.Alloc(predicate);

            if (!SetCallback(predicate)) throw new Exception("Cant set callback");

            return ConnectorInitialize(_path, 3);
        }
        
        private static void OnMsg(string str)
        {
            str = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(str));
            bufferMessages.Post(str);
        }
        
        private static bool ConnectorInitialize(string path, short logLevel)
        {
            IntPtr pPath = MarshalUtf8.StringToHGlobalUtf8(path);
            IntPtr pResult = Initialize(pPath, logLevel);
            if (!pResult.Equals(IntPtr.Zero))
            {
                String result = MarshalUtf8.PtrToStringUtf8(pResult);
                Marshal.FreeHGlobal(pPath);
                FreeMemory(pResult);
                Logging.logger.Error(result);
                return false;
            }
            else
            {
                Marshal.FreeHGlobal(pPath);
                Logging.logger.Information("Initialize() OK");
                return true;
            }

        }
        
        public static void ConnectorUnInitialize()
        {
            IntPtr pResult = UnInitialize();
            if (!pResult.Equals(IntPtr.Zero))
            {
                String result = MarshalUtf8.PtrToStringUtf8(pResult);
                FreeMemory(pResult);
                Logging.logger.Error(result);
            }
            else
            {
                Logging.logger.Information("UnInitialize() OK");
            }
        }
        
        public static string ConnectorSendCommand(string command)
        {
            Logging.logger.Information(command);
            var pData = MarshalUtf8.StringToHGlobalUtf8(command);
            var msg = PtrToDescr(SendCommand(pData));
            Marshal.FreeHGlobal(pData);
            return msg;
        }
        
        private static string PtrToDescr(IntPtr pResult)
        {
            if (IntPtr.Zero == pResult)
            {
                return null;
            }
            var result = MarshalUtf8.PtrToStringUtf8(pResult);
            FreeMemory(pResult);
            Logging.logger.Debug(result);
            return result;
        }
        
        
        [DllImport("TransaqConnector/txmlconnector64.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool SetCallback(CallBackDelegate pCallBack);
        
        [DllImport("TransaqConnector/txmlconnector64.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr SendCommand(IntPtr pData);
        
        [DllImport("TransaqConnector/txmlconnector64.dll", CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr Initialize(IntPtr pPath, int logLevel);

        [DllImport("TransaqConnector/txmlconnector64.dll", CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr UnInitialize();

        [DllImport("TransaqConnector/txmlconnector64.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool FreeMemory(IntPtr pData);
        
        private delegate bool CallBackDelegate(IntPtr pData);
       
    }
}