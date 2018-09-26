using SerialArduino;
using System;


namespace TestSerialArduino
{
    class Program
    {
        static void Main(string[] args)
        {

            var serialTester = new SerialTester();

            serialTester.Test("COM3");

            Console.ReadKey();

            serialTester.Close();
        }

    }
}
