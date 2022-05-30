// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using Modbus.Device;
using System;
using System.Net.Sockets;

namespace Modbus_TCP_IP_NModbus4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Master device :D");
            string ipAddr = "192.168.127.003"; //Ip address Endress+Hauser heat meter
            int tcpPort = 502;//Port address Endress+Hauser heat meter

            //Create TCP Client and begin connection
            TcpClient tcpClient = new TcpClient();
            tcpClient.BeginConnect(ipAddr,tcpPort,null,null);

            //Create Modbus Master using TCP client connection
            //ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);

            ushort startRef, noOfRefs;

            //Discrete Outputs or Coils: Read/Write single bit references

            //Read
            startRef = 0;//Discrete output to start reading
            noOfRefs = 5;//Discrete output number registers to read
            //bool[] dOutputs=master.ReadCoils(startRef,noOfRefs); //Read discrete output

            //Write
            //-----    master.WriteMultipleCoils(startRef,new bool[] {true,false,true,false ,true});


            //Discrete Input: Read only single bit references

            //Read
            ushort startRefInputD, noOfRefsInputD;
            startRefInputD = 0;
            noOfRefsInputD = 20;

            //bool[] dInput = master.ReadInputs(startRefInputD, noOfRefsInputD);
            //string inputStr=String.Join("|",dInput);
            //Console.WriteLine("Discrete Input:   "+inputStr);

            //Holding Registers: Read/Write only 16bit references

            //Read
           // ushort[] outputHoldingRegister=master.ReadHoldingRegisters(startRef,noOfRefs);

            //Write 
            //-----    master.WriteMultipleRegisters(startRef, new ushort[] { 5, 10, 15, 20, 25 });

            //Input Registers: Read only single 16bit references 

            //Read
            ushort startRefInputReg, noOfRefsInputReg;

            startRefInputReg = 1006;

               
            noOfRefsInputReg = 2; 

            ushort[] inputRegister=master.ReadInputRegisters(startRefInputReg, noOfRefsInputReg);
            string inputRegStr=String.Join("|",inputRegister);         

            Console.WriteLine("Input Registers:  "+inputRegStr);



            
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(inputRegister[1] & 0xFF);
            bytes[1] = (byte)(inputRegister[1] >> 8);
            bytes[2] = (byte)(inputRegister[0] & 0xFF);
            bytes[3] = (byte)(inputRegister[0] >> 8);
            float value = BitConverter.ToSingle(bytes, 0);

            Console.WriteLine("val1  " + value);

            //int val1 = inputRegister.ElementAt(0);
            //int val2 = inputRegister.ElementAt(1);

            //Console.WriteLine("val1  " + val1);
            //Console.WriteLine("val2  " + val2);

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();






        }

    }
}