using System;
using System.IO.Ports;

namespace SerialArduino
{
    public class SerialTester
    {
        public static string LastLine { get; set; }

        private SerialPort SerialPort { get; set; }

      
        public void Test(string serialPort)
        {

            

            this.SerialPort = new SerialPort(serialPort)
            {
                BaudRate = 115200,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                DtrEnable = true,
                Handshake = Handshake.None
            };

            // Subscribe to the DataReceived event.
            this.SerialPort.DataReceived += SerialPortDataReceived;

            // Now open the port.
            this.SerialPort.Open();

            LoRaWanClass lora = new LoRaWanClass(SerialPort);

            string deviceId = null;
            string appKey = null;
            string appEui = null;
            string appSKey = null;
            string nwkSKey = null;
            string devAddr = null;

            lora.setDataRate(LoRaWanClass._data_rate_t.DR6, LoRaWanClass._physical_type_t.EU868);

            lora.setChannel(0, 868.1F);
            lora.setChannel(1, 868.3F);
            lora.setChannel(2, 868.5F);

            lora.setReceiceWindowFirst(0, 868.1F);
            lora.setReceiceWindowSecond(868.5F, LoRaWanClass._data_rate_t.DR2);

            lora.setAdaptiveDataRate(false);

            lora.setDutyCycle(false);
            lora.setJoinDutyCycle(false);

            lora.setPower(14);

            //ABP
            // deviceId = "46AAC86800430028";
            // devAddr = "0028B1B1";
            // appSKey = "2B7E151628AED2A6ABF7158809CF4F3C";
            // nwkSKey = "3B7E151628AED2A6ABF7158809CF4F3C";
            //lora.setDeciveMode(LoRaWanClass._device_mode_t.LWABP);
            //lora.setId(devAddr, deviceId, appEui);
            //lora.setKey(nwkSKey, appSKey, appKey);

            //OTAA
            deviceId = "47AAC86800430028";
            appKey = "8AFE71A145B253E49C3031AD068277A1";
            appEui = "BE7A0000000014E2";
            lora.setDeciveMode(LoRaWanClass._device_mode_t.LWOTAA);
            lora.setId(devAddr, deviceId, appEui);
            lora.setKey(nwkSKey, appSKey, appKey);

            while (!lora.setOTAAJoin(LoRaWanClass._otaa_join_cmd_t.JOIN, 20000)) ;

            bool result = lora.transferPacket("100", 10);

            result = lora.transferPacketWithConfirmed("50", 10);

        }

        public void Close()
        {
            this.SerialPort.Close();
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;

            var serialdata = serialPort.ReadLine();
            LastLine = serialdata;
            
            Console.WriteLine(serialdata);
        }
    }
}
