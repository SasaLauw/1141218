using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace WpfApp9
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetAqiButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadDataAsync();
                MessageBox.Show("資料抓取成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("抓取失敗：" + ex.Message);
            }
        }

        private async Task LoadDataAsync()
        {
            // 環境部 API 網址
            string url = "https://data.moenv.gov.tw/api/v2/aqx_p_432?api_key=4c89a32a-a214-461b-bf29-30ff32a61a8a&limit=10&format=JSON";

            using (HttpClient client = new HttpClient())
            {
                string json = await client.GetStringAsync(url);
                // 這裡改用我們新定義的 AqiRoot
                AqiRoot result = JsonConvert.DeserializeObject<AqiRoot>(json);
                AqiDataGrid.ItemsSource = result.records;
            }
        }
    }

    // 將類別名稱全部改掉，避開「Record」這個關鍵字
    public class AqiRoot
    {
        public List<AqiInfo> records { get; set; }
    }

    public class AqiInfo
    {
        public string sitename { get; set; } // 站點
        public string aqi { get; set; }      // 指數
        public string status { get; set; }   // 狀態
    }
}