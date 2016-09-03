using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RESTUtilLib;
using System.Net;

namespace TesterClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void validateUserKeyButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder parameters = new StringBuilder();
            string url = this.serviceUrlTextBox.Text.Trim();
            HttpWebRequest request = RESTUtil.createGetRequest(url);

            var Headers = new List<KeyValuePair<string, string>>();
            Headers.Add(new KeyValuePair<string, string>("user-key", this.userKeyTextBox.Text.Trim()));
            if (Headers != null) { foreach (var element in Headers) { request.Headers.Add(element.Key, element.Value); } }

            string responseBody = null;
            string responseStatusCode = null;
            HttpWebResponse response = RESTUtil.ExecuteAction(request, ref responseBody, ref responseStatusCode);
            this.responseRichTextBox.AppendText(responseBody + Environment.NewLine);
            this.responseStatusCodeTextBox.Text = responseStatusCode;

        }
    }
}
