﻿/*
  LoRaWAN.h
  2013 Copyright (c) Seeed Technology Inc.  All right reserved.

  Author: Wayne Weng
  Date: 2016-10-17

  add rgb backlight fucnction @ 2013-10-15
  
  The MIT License (MIT)

  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in
  all copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
  THE SOFTWARE.1  USA
*/

using System;
using System.IO.Ports;
using System.Threading;

namespace SerialArduino
{
    class LoRaWanClass
    {
        private SerialPort serialPort;

        int DEFAULT_TIMEWAIT = 100;

        public enum _class_type_t { CLASS_A = 0, CLASS_C };
        public enum _physical_type_t { EU434 = 0, EU868, US915, US915HYBRID, AU915, AU915OLD, CN470, CN779, AS923, KR920, IN865 };
        public enum _device_mode_t { LWABP = 0, LWOTAA, TEST };
        public enum _otaa_join_cmd_t { JOIN = 0, FORCE };
        public enum _window_delay_t { RECEIVE_DELAY1 = 0, RECEIVE_DELAY2, JOIN_ACCEPT_DELAY1, JOIN_ACCEPT_DELAY2 };
        public enum _band_width_t { BW125 = 125, BW250 = 250, BW500 = 500 };
        public enum _spreading_factor_t { SF12 = 12, SF11 = 11, SF10 = 10, SF9 = 9, SF8 = 8, SF7 = 7 };
        public enum _data_rate_t { DR0 = 0, DR1, DR2, DR3, DR4, DR5, DR6, DR7, DR8, DR9, DR10, DR11, DR12, DR13, DR14, DR15 };


        public LoRaWanClass(SerialPort sp)
        {
            serialPort = sp;
 
        }


        public void setId(string DevAddr, string DevEUI, string AppEUI)
        {
        

            if (!String.IsNullOrEmpty(DevAddr))
            {
           
                string cmd = $"AT+ID=DevAddr,{DevAddr}\r\n";
                sendCommand(cmd);

                 Thread.Sleep(DEFAULT_TIMEWAIT);
            }

            if (!String.IsNullOrEmpty(DevEUI))
            {
           
                string cmd = $"AT+ID=DevEui,{DevEUI}\r\n";
                sendCommand(cmd);
                    Thread.Sleep(DEFAULT_TIMEWAIT);
            }

            if (!String.IsNullOrEmpty(AppEUI))
            {
            
                string cmd = $"AT+ID=AppEui,{AppEUI}\r\n";
                sendCommand(cmd);
                    Thread.Sleep(DEFAULT_TIMEWAIT);
            }
        }

        public void setKey(string NwkSKey, string AppSKey, string AppKey)
        {
       

            if (!String.IsNullOrEmpty(NwkSKey))
            {
          
                string cmd = $"AT+KEY=NWKSKEY,{NwkSKey}\r\n";
                sendCommand(cmd);
                    Thread.Sleep(DEFAULT_TIMEWAIT);
            }

            if (!String.IsNullOrEmpty(AppSKey))
            {
                
                    string cmd = $"AT+KEY=APPSKEY,{AppSKey}\r\n";
                sendCommand(cmd);

                    Thread.Sleep(DEFAULT_TIMEWAIT);
            }

            if (!String.IsNullOrEmpty(AppKey))
            {
          
                    string cmd = $"AT+KEY= APPKEY,{AppKey}\r\n";
                sendCommand(cmd);

                    Thread.Sleep(DEFAULT_TIMEWAIT);
            }
        }

        public void setDataRate(_data_rate_t dataRate, _physical_type_t physicalType)
        {
        

            if (physicalType == _physical_type_t.EU434) sendCommand("AT+DR=EU433\r\n");
            else if (physicalType == _physical_type_t.EU868) sendCommand("AT+DR=EU868\r\n");
            else if (physicalType == _physical_type_t.US915) sendCommand("AT+DR=US915\r\n");
            else if (physicalType == _physical_type_t.US915HYBRID) sendCommand("AT+DR=US915HYBRID\r\n");
            else if (physicalType == _physical_type_t.AU915) sendCommand("AT+DR=AU915\r\n");
            else if (physicalType == _physical_type_t.AU915OLD) sendCommand("AT+DR=AU915OLD\r\n");
            else if (physicalType == _physical_type_t.CN470) sendCommand("AT+DR=CN470\r\n");
            else if (physicalType == _physical_type_t.CN779) sendCommand("AT+DR=CN779\r\n");
            else if (physicalType == _physical_type_t.AS923) sendCommand("AT+DR=AS923\r\n");
            else if (physicalType == _physical_type_t.KR920) sendCommand("AT+DR=KR920\r\n");
            else if (physicalType == _physical_type_t.IN865) sendCommand("AT+DR=IN865\r\n");




            Thread.Sleep(DEFAULT_TIMEWAIT);

       
           string cmd = $"AT+DR={dataRate}\r\n";
            sendCommand(cmd);

                Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setPower(short power)
        {
        

       
            string cmd= $"AT+POWER={power}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setPort(int port)
        {
       

        
            string cmd = "AT+PORT={port}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setAdaptiveDataRate(bool command)
        {
            if (command) sendCommand("AT+ADR=ON\r\n");
            else sendCommand("AT+ADR=OFF\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setChannel(int channel, float frequency)
        {
        

        
            string cmd = $"AT+CH={channel},{(short)frequency}.{(short)(frequency * 10) % 10}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setChannel(char channel, float frequency, _data_rate_t dataRata)
        {


            string cmd = $"AT+CH={channel},{(short)frequency}.{(short)(frequency * 10) % 10},{dataRata}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setChannel(char channel, float frequency, _data_rate_t dataRataMin, _data_rate_t dataRataMax)
        {


            string cmd = $"AT+CH={channel},{(short)frequency}.{(short)(frequency * 10) % 10},{dataRataMin},{dataRataMax}\r\n";
            
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public bool transferPacket(string buffer, int timeout)
        {
       
            sendCommand("AT+MSG=\"");
          
            sendCommand(buffer);

            sendCommand("\"\r\n");

            DateTime start = DateTime.Now;
           
            while(true)
            {
                if (SerialTester.LastLine.StartsWith("+MSG: Done"))
                    return true;
                else if(start.AddSeconds(timeout)<DateTime.Now)
                    return false;
            }

       
       
        }

        public bool transferPacketWithConfirmed(string buffer, int timeout)
        {
           

            sendCommand("AT+CMSG=\"");
            sendCommand(buffer);
            sendCommand("\"\r\n");


            DateTime start = DateTime.Now;

            while (true)
            {
                if (SerialTester.LastLine.StartsWith("+CMSG: ACK Received"))
                    return true;
                else if (start.AddSeconds(timeout) < DateTime.Now)
                    return false;
            }

            //if (_buffer.Contains("+CMSG: ACK Received")) return true;
           
        }

        public void setUnconfirmedMessageRepeatTime(uint time)
        {
        

            if (time > 15)
                    time = 15;
            else if (time == 0)
                    time = 1;

        
            string cmd =$"AT+REPT={time}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setConfirmedMessageRetryTime(uint time)
        {
        

            if (time > 15) time = 15;
            else if (time == 0) time = 1;

        
            string cmd = $"AT+RETRY={time}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setReceiceWindowFirst(bool command)
        {
            if (command) sendCommand("AT+RXWIN1=ON\r\n");
            else sendCommand("AT+RXWIN1=OFF\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setReceiceWindowFirst(int channel, float frequency)
        {
        

        
            string cmd = $"AT+RXWIN1={channel},{(short)frequency}.{(short)(frequency * 10) % 10}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setReceiceWindowSecond(float frequency, _data_rate_t dataRate)
        {
        

        
            string cmd = $"AT+RXWIN2={(short)frequency}.{(short)(frequency * 10) % 10},{dataRate}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setReceiceWindowSecond(float frequency, _spreading_factor_t spreadingFactor, _band_width_t bandwidth)
        {
        

        
            string cmd = $"AT+RXWIN2={(short)frequency}.{(short)(frequency * 10) % 10},{spreadingFactor},{bandwidth}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setDutyCycle(bool command)
        {
            if (command) sendCommand("AT+LW=DC, ON\r\n");
            else sendCommand("AT+LW=DC, OFF\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setJoinDutyCycle(bool command)
        {
            if (command) sendCommand("AT+LW=JDC,ON\r\n");
            else sendCommand("AT+LW=JDC,OFF\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setReceiceWindowDelay(_window_delay_t command, short _delay)
        {

            string cmd="";
           
            if (command == _window_delay_t.RECEIVE_DELAY1)  cmd = $"AT+DELAY=RX1,{_delay}\r\n";
            else if (command == _window_delay_t.RECEIVE_DELAY2)  cmd = $"AT+DELAY=RX2,{_delay}\r\n";
            else if (command == _window_delay_t.JOIN_ACCEPT_DELAY1)  cmd = $"AT+DELAY=JRX1,{_delay}\r\n";
            else if (command == _window_delay_t.JOIN_ACCEPT_DELAY2)  cmd = $"AT+DELAY=JRX2,{_delay}\r\n";
            sendCommand(cmd);



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setClassType(_class_type_t type)
        {
            if (type == _class_type_t.CLASS_A) sendCommand("AT+CLASS=A\r\n");
            else if (type == _class_type_t.CLASS_C) sendCommand("AT+CLASS=C\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setDeciveMode(_device_mode_t mode)
        {
            if (mode == _device_mode_t.LWABP) sendCommand("AT+MODE=LWABP\r\n");
            else if (mode == _device_mode_t.LWOTAA) sendCommand("AT+MODE=LWOTAA\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public bool setOTAAJoin(_otaa_join_cmd_t command,  int timeout)
        {
       

            if (command == _otaa_join_cmd_t.JOIN) sendCommand("AT+JOIN\r\n");
            else if (command == _otaa_join_cmd_t.FORCE) sendCommand("AT+JOIN=FORCE\r\n");


            Thread.Sleep(DEFAULT_TIMEWAIT);


            DateTime start = DateTime.Now;

            while (true)
            {
                if (SerialTester.LastLine.StartsWith("+JOIN: Done"))
                    return true;
                else if (SerialTester.LastLine.StartsWith("+JOIN: LoRaWAN modem is busy"))
                    return false;
                else if (SerialTester.LastLine.StartsWith("+JOIN: Join failed"))
                    return false;
                else if (start.AddMilliseconds(timeout) < DateTime.Now)
                    return false;
            }

            //    ptr = _buffer.Contains("+JOIN: Join failed");
            //if (ptr) return false;
            //ptr = _buffer.Contains("+JOIN: LoRaWAN modem is busy");
            //if (ptr) return false;

        
        }

        public void setDeviceLowPower()
        {
            sendCommand("AT+LOWPOWER\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setDeviceReset()
        {
            sendCommand("AT+RESET\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        public void setDeviceDefault()
        {
            sendCommand("AT+FDEFAULT=RISINGHF\r\n");



            Thread.Sleep(DEFAULT_TIMEWAIT);
        }

        short getBatteryVoltage()
        {
            short battery=0;

            //pinMode(CHARGE_STATUS_PIN, OUTPUT);
            //digitalWrite(CHARGE_STATUS_PIN, LOW);
            //Thread.Sleep(DEFAULT_TIMEWAIT);
            //battery = (analogRead(BATTERY_POWER_PIN) * 3300 * 11) >> 10;
            //pinMode(CHARGE_STATUS_PIN, INPUT);

            return battery;
        }

        public void sendCommand(string command)
        {
                serialPort.Write(command);
        
        }

    }
}
