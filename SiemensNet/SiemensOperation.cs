using HslCommunication;
using HslCommunication.Profinet.Siemens;

namespace SiemensNet
{
    public class SiemensOperation
    {
        private SiemensS7Net siemensTcpNet = null;

        public bool IsConnState { get; set; }

        public SiemensS7Net SiemensTcpNet
        {
            get { return siemensTcpNet; }
        }

        SiemensPLCS siemensPLCS = 0;

        public SiemensOperation(string plcType, string Ip)
        {
            if (plcType == "SiemensPLCS.S200Smart")
                siemensPLCS = SiemensPLCS.S200Smart;
            else if(plcType== "SiemensPLCS.S1500")
                siemensPLCS = SiemensPLCS.S1500;
            else if(plcType == "SiemensPLCS.S1200")
                siemensPLCS= SiemensPLCS.S1200;

            siemensTcpNet = new SiemensS7Net(siemensPLCS);
            siemensTcpNet.IpAddress = Ip;

            SiementsConnection();
        }

        /// <summary>
        /// 连接成功
        /// </summary>
        private void SiementsConnection()
        {

            OperateResult connect = siemensTcpNet.ConnectServer();

            if (connect.IsSuccess)
            {
                IsConnState = true;
            }

        }

        private void SiementsClose()
        {
            siemensTcpNet.ConnectClose();
        }


        public string ReadResult(string address)
        {
            string o_ret = "";
            if (address != null)
            {
                readResultRender<short>(siemensTcpNet.ReadInt16(address), address, out o_ret);
            }

            return o_ret;
        }

        public string WriteResult(string address, string value)
        {
            string o_ret = "";
            if (address != null)
            {
                writeResultRender(siemensTcpNet.Write(address, ushort.Parse(value)), address, out o_ret);
            }

            return o_ret;
        }



        /// <summary>
        /// 统一的读取结果的数据解析，显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="address"></param>
        /// <param name="textBox"></param>
        private void readResultRender<T>(OperateResult<T> result, string address, out string o_reValue)
        {
            if (result.IsSuccess)
            {
                o_reValue = result.Content.ToString();
            }
            else
            {
                o_reValue = result.ToString();
            }
        }


        public bool WriteUshort(string address, ushort value)
        {
            var r = siemensTcpNet.Write(address, value);
            return r.IsSuccess;
        }

        /// <summary>
        /// 统一的数据写入的结果显示
        /// </summary>
        /// <param name="result"></param>
        /// <param name="address"></param>
        private void writeResultRender(OperateResult result, string address, out string o_reValue)
        {
            if (result.IsSuccess)
            {
                o_reValue = "Success";

            }
            else
            {
                o_reValue = result.ToString();

            }
        }
    }
}
