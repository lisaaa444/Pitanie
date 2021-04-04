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
    public partial class Week : Form
    {
        public Week()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Day newForm = new Day("monday");
            newForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Day newForm = new Day("tuesday");
            newForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Day newForm = new Day("wednesday");
            newForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Day newForm = new Day("thursday");
            newForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Day newForm = new Day("friday");
            newForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Day newForm = new Day("saturday");
            newForm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Day newForm = new Day("sunday");
            newForm.ShowDialog();
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
    }
}
