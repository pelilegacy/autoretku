using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace AutoRetku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Boolean timerRunning = false;
        public Boolean logged = false;
        public Boolean desc_received = false;
        public Boolean workerStarted = false;
        public Boolean StatusChecked = false;
        public int notificationStatus = 0;
        public Boolean allowServiceChange = false;

        public string retrieved_status;
        private string username = "";
        private string password = "";
        public string log = "";

        public string service_user;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " (" + System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString() + ")";

            // Setting dropdown boxes automatically
            DateTime current = DateTime.Now;
            comboBox_starting_hour.Text = current.ToString("HH");
            comboBox_starting_minute.Text = current.ToString("mm");
            current = current.AddHours(1); // Setting time to one hour from now
            comboBox_ending_hour.Text = current.ToString("HH");
            comboBox_ending_minute.Text = current.ToString("mm");
        }

        private void timer_refresh_Tick(object sender, EventArgs e) // timer to refresh nesretku.com to prevent logout
        {
            webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);
        }

        public string GetSource(string url) // Download website source code
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        #region UI events

        private void button_timer_Click(object sender, EventArgs e)
        {
            if (webBrowser_retku.Url == new Uri("http://www.nesretku.com/index.php?user=" + username))
            {
                if (timerRunning == false)
                {
                    button_timer.Text = "Pysäytä";
                    timerRunning = true;

                    timer_refresh.Start();
                }
                else
                {
                    button_timer.Text = "Ajasta";
                    timerRunning = false;
                }
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            setRetkuOff();
        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            setRetkuPause();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            setRetkuOn();
        }

        private void button_update_desc_Click(object sender, EventArgs e)
        {
            HtmlElementCollection input;
            HtmlElement desc, save;
            input = webBrowser_retku.Document.GetElementsByTagName("input");
            desc = input["kuvaus"];
            save = input["userConfSubmit"];
            desc.SetAttribute("value", textBox_desc.Text);
            save.InvokeMember("click");
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            username = textBox_username.Text;
            password = textBox_password.Text;
            service_user = textBox_service_user.Text;

            webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);

            if (textBox_service_user.Text == "")
            {
                worker_streamsvc.RunWorkerAsync();
            }
        }

        private void comboBox_service_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (allowServiceChange)
            {
                HtmlElementCollection select, input;
                HtmlElement service_option, save;
                select = webBrowser_retku.Document.GetElementsByTagName("select");
                input = webBrowser_retku.Document.GetElementsByTagName("input");
                service_option = select["palvelin"];
                save = input["userConfSubmit"];

                if (selectedService() == 1)
                {
                    service_option.SetAttribute("value", "3");
                }
                else if (selectedService() == 2)
                {
                    service_option.SetAttribute("value", "5");
                }

                save.InvokeMember("click");
            }
            else
            {
                allowServiceChange = true;
            }
        }

        private void button_retrievedesc_Click(object sender, EventArgs e)
        {
            service_user = textBox_service_user.Text;
            if (service_user != "")
            {
                if (selectedService() == 1)
                {
                    string source = GetSource("https://api.twitch.tv/kraken/channels/" + service_user); // Get json source code from Twitch API

                    if (source != "")
                    {
                        if (source.Contains("\"status\"")) // If "status" exists in json, stream is Live
                        {
                            retrieved_status = stringBetween(source, "status\":\"", "\"");
                        }
                    }
                }
                else if (selectedService() == 2)
                {
                    string source = GetSource("http://api.hitbox.tv/media/user/" + service_user); // Get json source code from Hitbox API

                    if (source != "")
                    {
                        if (source.Contains("\"media_status\"")) // If media_is_live":"1" exists in json, stream is Live
                        {
                            retrieved_status = stringBetween(source, "media_status\":\"", "\"");
                        }
                    }
                }

                textBox_desc.Text = retrieved_status;
            }
        }

        #endregion

        #region BackgroundWorker Events

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (logged == true)
            {
                if (webBrowser_retku.Url != new Uri("http://www.nesretku.com/index.php?user=" + username))
                {
                    webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);
                }

                if (timerRunning == true)
                {
                    DateTime time = DateTime.Now;
                    if ((time.ToString("HH") == comboBox_starting_hour.Text) && (time.ToString("mm") == comboBox_starting_minute.Text))
                    {
                        if (notificationStatus == 0)
                        {
                            setRetkuOn();
                        }
                    }

                    if ((time.ToString("HH") == comboBox_ending_hour.Text) && (time.ToString("mm") == comboBox_ending_minute.Text))
                    {
                        setRetkuOff();
                    }
                }

                if (webBrowser_retku.Url == new Uri("http://www.nesretku.com/index.php?user=" + username))
                {
                    if (desc_received == false)
                    {
                        desc_received = true;
                        HtmlElementCollection input;
                        HtmlElement desc;
                        input = webBrowser_retku.Document.GetElementsByTagName("input");
                        desc = input["kuvaus"];
                        textBox_desc.Text = desc.GetAttribute("value");
                    }
                }
            }
            backgroundWorker1.RunWorkerAsync();
        }

        private void worker_service_DoWork(object sender, DoWorkEventArgs e)  // worker_service_RunWorkerCompleted runs after this thread finishes.
        {
            System.Threading.Thread.Sleep(5000);
        }

        private void worker_service_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //  Running in background to monitor and edit stream status
        {
            if (textBox_service_user.Text != "")
            {
                if (isStartOnOnline() && notificationStatus == 0) // if checkBox_service_start is checked
                {
                    if (selectedService() == 1) // if selected service is Twitch
                    {
                        if (isLive(1, textBox_service_user.Text)) // if Twitch is Live
                        {
                            setRetkuOn();
                        }
                    }
                    else if (selectedService() == 2) // if selected service is Hitbox
                    {
                        if (isLive(2, textBox_service_user.Text)) // if Hitbox is Live
                        {
                            setRetkuOn();
                        }
                    }
                }

                if (isStopOnOffline() && (notificationStatus == 2 || notificationStatus == 1))  // if checkBox_service_end is checked
                {
                    if (selectedService() == 1) // if selected service is Twitch
                    {
                        if (!isLive(1, textBox_service_user.Text)) // if Twitch is Live
                        {
                            setRetkuOff();
                        }
                    }
                    else if (selectedService() == 2) // if selected service is Hitbox
                    {
                        if (!isLive(2, textBox_service_user.Text)) // if Hitbox is Live
                        {
                            setRetkuOff();
                        }
                    }
                }
            }

            worker_streamsvc.RunWorkerAsync();
        }

        #endregion

        #region webBrowser events

        private void webBrowser_retku_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser_retku.DocumentText.Contains("Stream tila"))
            {
                logged = true;
                this.Height = 255;
                label_username.Visible = false;
                label_password.Visible = false;
                textBox_username.Visible = false;
                textBox_password.Visible = false;
                button_login.Visible = false;
                checkBox_remember.Visible = false;

                button_timer.Visible = true;
                button_start.Visible = true;
                button_pause.Visible = true;
                button_stop.Visible = true;
                label_starting.Visible = true;
                label_ending.Visible = true;
                comboBox_starting_hour.Visible = true;
                comboBox_starting_minute.Visible = true;
                comboBox_ending_hour.Visible = true;
                comboBox_ending_minute.Visible = true;
                label_desc.Visible = true;
                textBox_desc.Visible = true;
                button_update_desc.Visible = true;
                pictureBox1.Visible = true;

                timer_refresh.Start();

                if (StatusChecked == false)
                {
                    HtmlElementCollection select;
                    HtmlElement service_option;
                    select = webBrowser_retku.Document.GetElementsByTagName("select");
                    service_option = select["palvelin"];

                    if (service_option.GetAttribute("value").ToString() == "3")
                    {
                        comboBox_service.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox_service.SelectedIndex = 1;
                    }

                    if (webBrowser_retku.DocumentText.Contains("alt=\"Kiinni\""))
                    {
                        pictureBox1.Image = AutoRetku.Properties.Resources.red;
                    }

                    if (webBrowser_retku.DocumentText.Contains("alt=\"Tauolla\""))
                    {
                        pictureBox1.Image = AutoRetku.Properties.Resources.yolo;
                    }

                    if (webBrowser_retku.DocumentText.Contains("alt=\"Päällä\""))
                    {
                        pictureBox1.Image = AutoRetku.Properties.Resources.green;
                    }
                    StatusChecked = true;
                }

                if (workerStarted == false)
                {
                    workerStarted = true;
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else
            {
                if (webBrowser_retku.Url != new Uri("http://www.nesretku.com/index.php?user=" + username))
                {
                    webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);
                }

                HtmlElementCollection input;
                HtmlElement input_username, input_password, login;
                input = webBrowser_retku.Document.GetElementsByTagName("input");
                input_username = input["username"];
                input_password = input["password"];
                login = input["login"];
                input_username.SetAttribute("value", username);
                input_password.SetAttribute("value", password);
                login.InvokeMember("click");
            }
        }

        #endregion

        #region Own functions

        private void retkuLogOut()
        {
            try
            {
                logged = false;
                webBrowser_retku.Navigate("http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=" + stringBetween(webBrowser_retku.DocumentText, "http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=", "&logout=t&redirect=http://www.nesretku.com/index.php") + "&logout=t&redirect=http://www.nesretku.com/index.php");
            }
            catch (Exception)
            {

            }
        }

        private void CheckStatus()
        {
            if (webBrowser_retku.DocumentText.Contains("alt=\"Kiinni\""))
            {
                pictureBox1.Image = AutoRetku.Properties.Resources.red;
            }

            if (webBrowser_retku.DocumentText.Contains("alt=\"Tauolla\""))
            {
                pictureBox1.Image = AutoRetku.Properties.Resources.yolo;
            }

            if (webBrowser_retku.DocumentText.Contains("alt=\"Päällä\""))
            {
                pictureBox1.Image = AutoRetku.Properties.Resources.green;
            }
            StatusChecked = true;
        }

        public Boolean isLive(int serviceid, string service_user) // 1 = Twitch, 2 = Hitbox, service_user = username at streaming service
        {
            if (serviceid == 1)
            {
                string source = GetSource("https://api.twitch.tv/kraken/streams?channel=" + service_user); // Get json source code from Twitch API

                if (source != "")
                {
                    if (source.Contains("\"status\"")) // If "status" exists in json, stream is Live
                    {
                        retrieved_status = stringBetween(source, "\"status\":\"", "\"");
                        return true;
                    }
                }
            }
            else if (serviceid == 2)
            {
                string source = GetSource("http://api.hitbox.tv/media/live/" + service_user); // Get json source code from Hitbox API

                if (source != "")
                {
                    if (source.Contains("\"media_is_live\":\"1\"")) // If media_is_live":"1" exists in json, stream is Live
                    {
                        retrieved_status = stringBetween(source, "\"status\":\"", "\"");
                        return true;
                    }
                }
            }

            retrieved_status = "";
            return false;
        }

        public int selectedService()
        {
            if (comboBox_service.SelectedIndex == 0) // if Dropbox's selected item's index is 0 = Twitch
            {
                return 1;
            }

            if (comboBox_service.SelectedIndex == 1) // if Dropbox's selected item's index is 1 = Hitbox
            {
                return 2;
            }

            return 0;
        }

        public Boolean isStartOnOnline() // Check if user wants to start Retku when stream goes Live
        {
            if (checkBox_service_start.Checked == true) // if Checkbox is checked
            {
                return true;
            }

            return false;
        }

        public Boolean isStopOnOffline() // Check if user wants to start Retku when stream goes Live
        {
            if (checkBox_service_end.Checked == true) // if Checkbox is checked
            {
                return true;
            }

            return false;
        }

        private void setRetkuStatus(int status) // 2 = Online, 1 = Paused, 0 = Offline
        {
            webBrowser_retku.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=" + status); // Navigate web browser control to url
        }

        public void setRetkuOn()
        {
            notificationStatus = 2;
            setRetkuStatus(2);
            pictureBox1.Image = AutoRetku.Properties.Resources.green; // Set notification image to green.png
        }

        public void setRetkuPause()
        {
            notificationStatus = 1;
            setRetkuStatus(1);
            pictureBox1.Image = AutoRetku.Properties.Resources.yolo; // Set notification image to yolo.png
        }

        public void setRetkuOff()
        {
            notificationStatus = 0;
            button_timer.Text = "Ajasta";
            timerRunning = false;
            setRetkuStatus(0);
            pictureBox1.Image = AutoRetku.Properties.Resources.red;  // Set notification image to red.png
        }

        public void checkNotification()
        {
            return;
        }

        public string stringBetween(string str_full, string str_first, string str_second)
        {
            string[] strparse = str_full.Split(new string[] { str_first }, StringSplitOptions.None);
            string[] str_return = strparse[1].Split(new string[] { str_second }, StringSplitOptions.None);
            return str_return[0];
        }

        #endregion
    }
}
