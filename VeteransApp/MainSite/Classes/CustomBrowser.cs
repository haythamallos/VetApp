﻿using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MainSite.Classes
{
    /// <summary>
    /// Summary description for CustomBrowser
    /// </summary>
    public class CustomBrowser
    {
        public CustomBrowser()
        {
        }

        protected string _url;
        string html = "";
        public string GetWebpage(string url)
        {
            _url = url;
            // WebBrowser is an ActiveX control that must be run in a
            // single-threaded apartment so create a thread to create the
            // control and generate the thumbnail
            Thread thread = new Thread(new ThreadStart(GetWebPageWorker));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            string s = html;
            return s;
        }

        protected void GetWebPageWorker()
        {
            using (WebBrowser browser = new WebBrowser())
            {
                //  browser.ClientSize = new Size(_width, _height);
                browser.ClientSize = new Size(800, 600);
                browser.ScrollBarsEnabled = false;
                browser.ScriptErrorsSuppressed = true;

                browser.AllowNavigation = true;

                browser.Navigate(_url);
                browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentCompleted);
                browser.Navigating += new WebBrowserNavigatingEventHandler(IEBrowser_Navigating);

                // Wait for control to load page
                while (browser.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();

                html = browser.DocumentText;

            }
        }

        // Navigating event handle
        void IEBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            // navigation count increases by one
            //navigationCounter++;
            // write url into the form's caption
            //if (form != null) form.Text = e.Url.ToString();
        }

        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            HtmlDocument doc = browser.Document;
            //using (Bitmap bitmap = new Bitmap(browser.Width, browser.Height))
            //{
            //    browser.DrawToBitmap(bitmap, new Rectangle(0, 0, browser.Width, browser.Height));
            //    using (MemoryStream stream = new MemoryStream())
            //    {
            //        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //        byte[] bytes = stream.ToArray();
            //        imgScreenShot.Visible = true;
            //        imgScreenShot.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
            //    }
            //}
        }

    }

    public static class WebBrowserExtensions
    {

        /// <summary>
        /// Load an HTML document from a Stream and pass the text through a filter before the page is
        /// rendered in the WebBrowser control.
        /// </summary>
        /// <param name="browser">control that renders the filtered HTML</param>
        /// <param name="source">Stream containing the content to filter and render</param>
        /// <param name="filter">Delegate used to filter the source Stream</param>
        public static void ProcessRequest(this WebBrowser browser, Stream source, Func<HtmlDocument, HtmlDocument> filter)
        {

            //temporary WebBrowser that gets us an empty HtmlDocument.
            //we do this because WebBrowser is hideously expensive in terms of resources and this
            //allows for automatic cleanup. Further, we don't want the actual browser control we were given to fire any
            //of its registered events.
            using (WebBrowser tempBrowser = new WebBrowser())
            {

                //all data from the source as a string
                string sourceText = string.Empty;

                try
                {

                    //read all the data from the source Stream
                    using (StreamReader sourceReader = new StreamReader(source))
                    {

                        sourceText = sourceReader.ReadToEnd();

                    }

                }
                catch (IOException ex)
                {

                    throw new Exception("Could not read data from source stream", ex);

                }

                //process any text we read from the source Stream
                if (!string.IsNullOrEmpty(sourceText))
                {

                    HtmlDocument tempDocument = null;
                    HtmlElement htmlRoot = null;

                    //navigate to "about: blank" to initialize an empty document
                    tempBrowser.Navigate("about: blank");

                    //load the sourceText into the document.
                    tempBrowser.Document.Write(sourceText);

                    //now filter the document if a filter was specified
                    if (filter != null)
                        tempDocument = filter(tempBrowser.Document);

                    //if the filter did not return a document, or no filter was specified, use the original document
                    if (tempDocument == null)
                        tempDocument = tempBrowser.Document;

                    //find the root HTML element, there can be only one!
                    var htmlElements = tempDocument.GetElementsByTagName("html");

                    if (htmlElements != null && htmlElements.Count > 0)
                        htmlRoot = htmlElements[0];

                    //now, extract the text and set it on the actual browser
                    browser.DocumentText = htmlRoot.OuterHtml;

                }

                //text was either read, or it wasn't.  At this point there is nothing left 
                //to do

            }

        }

    }
}