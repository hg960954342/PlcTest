using HslCommunication;
using HslCommunication.Profinet.Siemens;
using HslCommunication.Core.Net;
using System;
using System.Collections.Generic;

namespace PlcTest
{
    class SiemensPLC
    {
        /// <summary>
        /// 西门子连接对象
        /// </summary>
        private SiemensS7Net siemensTcpNet = null;

        public bool IsConnState { get; private set; }

        /// <summary>
        /// 连接方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ip"></param>
        public void Connect(SiemensPLCS type, string ip)
        {
            if (siemensTcpNet != null) return;

            siemensTcpNet = new SiemensS7Net(type, ip)
            {
                ConnectTimeOut = 5000
            };
        }



        /// <summary>
        /// 连接成功
        /// </summary>
        public bool SiementsConnection()
        {

            OperateResult connect = siemensTcpNet.ConnectServer();

            if (connect.IsSuccess)
            {
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// 一楼入库称
        /// </summary>
        /// <param name="addres"></param>
        /// <param name="taskid"></param>
        /// <param name="commandInfo"></param>
        /// <param name="shortbarcode"></param>
        /// <returns></returns>
        public bool WriteBindingBox(string addres, string taskid, string commandInfo, string shortbarcode)
        {
            return WritePlc(addres, int.Parse(taskid), int.Parse(commandInfo), int.Parse(shortbarcode));
        }

        /// <summary>
        /// 一楼入库分流
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool WriteOneFL(string addres, string taskid, string commandInfo, string shortbarcode)
        {

            return WritePlc(addres, int.Parse(taskid), int.Parse(commandInfo), int.Parse(shortbarcode));
        }
        /// <summary>
        /// 一楼出库复合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool WriteOneCK(string addres, string taskid, string commandInfo, string shortbarcode)
        {

            return WritePlc(addres, int.Parse(taskid), int.Parse(commandInfo), int.Parse(shortbarcode));
        }
        /// <summary>
        /// 二楼入库1号移栽点位
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool TwoComposite(string addres, string taskid, string commandInfo, string shortbarcode)
        {

            return WritePlc(addres, int.Parse(taskid), int.Parse(commandInfo), int.Parse(shortbarcode));
        }
        /// <summary>
        ///二楼入库2号移栽点位
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool TwoComposites(string addres, string taskid, string commandInfo, string shortbarcode)
        {
            return WritePlc(addres, int.Parse(taskid), int.Parse(commandInfo), int.Parse(shortbarcode));
        }
        /// <summary>
        ///二楼出库分流
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool TwoWeritCK(string addres, string taskid, string commandInfo, string shortbarcode)
        {
            return WritePlc(addres, int.Parse(taskid), int.Parse(commandInfo), int.Parse(shortbarcode));
        }
        /// <summary>
        /// 三楼入库移栽
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool SortLine(string addres, string taskid, string commandInfo, string shortbarcode)
        {
            return WritePlc(addres, int.Parse(taskid), int.Parse(commandInfo), int.Parse(shortbarcode));
        }
        /// <summary>
        /// 处理给plc写值的方法
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool WritePlc(string address, int taskid, int commandInfo, int shortbarcode)
        {
            List<byte> list = new List<byte>();
            var a = BitConverter.GetBytes(taskid);
            var b = BitConverter.GetBytes(commandInfo);
            var c = BitConverter.GetBytes(shortbarcode);
            var d = BitConverter.GetBytes(0);
            var e = BitConverter.GetBytes(0);

            list.AddRange(a);
            list.AddRange(b);
            list.AddRange(c);
            list.AddRange(d);
            list.AddRange(e);


            var buffer = list.ToArray();

            var plc = siemensTcpNet.Write(address, buffer);

            return plc.IsSuccess;
        }


        #region 读、写方法


        /// <summary>
        /// 写入16位到PLC
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool WriteUshort(string address, ushort value)
        {
            var r = siemensTcpNet.Write(address, value);
            return r.IsSuccess;
        }

        /// <summary>
        /// 写入bool类型
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool WriteBool(string address, bool value)
        {
            var r = siemensTcpNet.Write(address, value);
            return r.IsSuccess;
        }

        private bool WriteBtye(string address, byte value)
        {
            var r = siemensTcpNet.Write(address, value);
            return r.IsSuccess;
        }

        private bool WriteInt(string address, int value)
        {
            var r = siemensTcpNet.Write(address, value);
            return r.IsSuccess;
        }

        /// <summary>
        /// 批量读取PLC字软元件指定地址的Int16数据
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="uSize"></param>
        /// <param name="sData"></param>
        /// <returns></returns>


        /// <summary>
        /// 读取bool类型
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private bool ReadBool(string address)
        {
            var r = siemensTcpNet.ReadBool(address);
            return r.Content;
        }

        /// <summary>
        /// 读取16位整数
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private ushort ReadUInt16(string address)
        {
            var r = siemensTcpNet.ReadUInt16(address);
            return r.Content;
        }

        public uint ReadInt(string address)
        {
            var r = siemensTcpNet.ReadUInt32(address);
            return r.Content;
        }

        /// <summary>
        /// 读取byte类型
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private byte ReadByte(string address)
        {
            var r = siemensTcpNet.ReadByte(address);
            return r.Content;
        }
        #endregion
    }
}
