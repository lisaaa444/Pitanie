using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Day : Form
    {
        public static string day;
        static string[] comand;

        public Day(string week)
        {
            InitializeComponent();
            day = week;
        }

        private void Day_Load(object sender, EventArgs e)
        {
            comand = Exchange("127.0.0.1", 8888, $"view@{day}").Split(new char[] { '#' });
        }

        static string Exchange(string address, int port, string outMessage)
        {
            try
            {
                // Инициализация
                TcpClient client = new TcpClient(address, port);
                Byte[] data = Encoding.UTF8.GetBytes(outMessage);
                NetworkStream stream = client.GetStream();
                try
                {
                    // Отправка сообщения
                    stream.Write(data, 0, data.Length);
                    // Получение ответа
                    Byte[] readingData = new Byte[256];
                    String responseData = String.Empty;
                    StringBuilder completeMessage = new StringBuilder();
                    int numberOfBytesRead = 0;
                    do
                    {
                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                        completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);
                    responseData = completeMessage.ToString();
                    return responseData;
                }
                finally
                {
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception)
            {
                return ("Ожидание сервера...");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ваш завтрак:\n" + comand[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ваш обед:\n" + comand[1]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ваш ужин:\n" + comand[2]);
        }     
    }
}
