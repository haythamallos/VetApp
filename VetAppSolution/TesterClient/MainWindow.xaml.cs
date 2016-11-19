using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using RESTUtilLib;
using System.Net;
//using Vetapp.Client.Proxy;

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
            string url = this.serviceUrlTextBox.Text.Trim() + "/api/home";
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

        private void userCreateButton_Click(object sender, RoutedEventArgs e)
        {
            //StringBuilder parameters = new StringBuilder();
            //string url = this.serviceUrlTextBox.Text.Trim() + "/api/user";

            //UserProxy useritem = new UserProxy() { Firstname = "Haytham" };
            //string jsonBody = ToJson(useritem, useritem.GetType());
            //RESTUtil.EncodeAndAddItem(ref parameters, "UserItemID", "");
            //RESTUtil.EncodeAndAddItem(ref parameters, "UserID", "");
            //RESTUtil.EncodeAndAddItem(ref parameters, "FirstName", "Haytham");
            //RESTUtil.EncodeAndAddItem(ref parameters, "MiddleName", "");
            //RESTUtil.EncodeAndAddItem(ref parameters, "LastName", "Allos");
            //RESTUtil.EncodeAndAddItem(ref parameters, "Email", "haytham.allos@gmail.com");

            //var Headers = new List<KeyValuePair<string, string>>();
            //Headers.Add(new KeyValuePair<string, string>("user-key", this.userKeyTextBox.Text.Trim()));
            //HttpWebRequest request = RESTUtil.createPostRequest(url, jsonBody, "POST", Headers);

            //string responseBody = null;
            //string responseStatusCode = null;
            //HttpWebResponse response = RESTUtil.ExecuteAction(request, ref responseBody, ref responseStatusCode);
            //this.responseRichTextBox.AppendText(responseBody + Environment.NewLine);
            //this.responseStatusCodeTextBox.Text = responseStatusCode;

        }

        public string ToJson(object Obj, Type ObjType)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Obj);
        }
    }
}
