using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Threading;
using System.Text;

namespace Game_Test
{
    class ArduinoCmds
    {
        private SerialPort CurrentPort;

        public ArduinoCmds(SerialPort port)
        {
            CurrentPort = port;
        }

        public void Save()
        {
            byte[] buffer = new byte[5];
            buffer[0] = Convert.ToByte(16);
            buffer[1] = Convert.ToByte(64);

            CurrentPort.Open();
            CurrentPort.Write(buffer, 0, 1);
            Thread.Sleep(1000);
            CurrentPort.Close();
        }
    }
}
