using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace AutoRetku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Boolean runonce = false;
        public Boolean running = false;
        public Boolean logged = false;
        public Boolean activated = false;
        public Boolean desc_received = false;
        public Boolean workerStarted = false;
        public Boolean StatusChecked = false;

        public Boolean useTwitch = false;
        public Boolean useHitbox = false;
        public Boolean runTwitch = false;
        public Boolean runHitbox = false;

        public string username = "";
        public string password = "";

        public string service_user = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly()
                                           .GetName()
                                           .Version
                                           .ToString());

            DateTime current = DateTime.Now;
            comboBox_starting_hour.Text = current.ToString("HH");
            comboBox_starting_minute.Text = current.ToString("mm");
            current = current.AddHours(1);
            comboBox_ending_hour.Text = current.ToString("HH");
            comboBox_ending_minute.Text = current.ToString("mm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Url == new Uri("http://www.nesretku.com/index.php?user=" + username))
            {
                if(running == false)
                {
                    button_timer.Text = "Pysäytä";
                    running = true;

                    timer_refresh.Start();
                }
                else
                {
                    button_timer.Text = "Ajasta";
                    running = false;
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (runonce == false)
            {
                runonce = true;
                LogOut();
            }
            else
            {
                if (webBrowser1.DocumentText.Contains("Stream tila"))
                {
                    if (StatusChecked == false && logged == true)
                    {
                        if (webBrowser1.DocumentText.Contains("alt=\"Kiinni\""))
                        {
                            pictureBox1.Image = AutoRetku.Properties.Resources.red;
                        }

                        if (webBrowser1.DocumentText.Contains("alt=\"Tauolla\""))
                        {
                            pictureBox1.Image = AutoRetku.Properties.Resources.yolo;
                        }

                        if (webBrowser1.DocumentText.Contains("alt=\"Päällä\""))
                        {
                            pictureBox1.Image = AutoRetku.Properties.Resources.green;
                        }
                        StatusChecked = true;
                    }

                    logged = true;
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

                    if (workerStarted == false)
                    {
                        workerStarted = true;
                        backgroundWorker1.RunWorkerAsync();
                    }
                }
            }

            if (logged == false && webBrowser1.Url != new Uri("http://www.nesretku.com/index.php"))
            {
                webBrowser1.Navigate("http://www.nesretku.com/index.php");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (logged == true)
            {
                if (webBrowser1.Url == new Uri("http://www.nesretku.com/index.php"))
                {
                    webBrowser1.Navigate("http://www.nesretku.com/index.php?user=" + username);
                }

                if (running == true)
                {
                    DateTime time = DateTime.Now;
                    if ((time.ToString("HH") == comboBox_starting_hour.Text) && (time.ToString("mm") == comboBox_starting_minute.Text))
                    {
                        if (activated == false)
                        {
                            activated = true;
                            pictureBox1.Image = AutoRetku.Properties.Resources.green;
                            webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=2");
                            pictureBox1.Image = AutoRetku.Properties.Resources.green;
                        }
                    }

                    if ((time.ToString("HH") == comboBox_ending_hour.Text) && (time.ToString("mm") == comboBox_ending_minute.Text))
                    {
                        webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=0");
                        running = false;
                        activated = false;
                        pictureBox1.Image = AutoRetku.Properties.Resources.red;
                        button_timer.Text = "Ajoita";
                    }
                }

                if (webBrowser1.Url == new Uri("http://www.nesretku.com/index.php?user=" + username))
                {
                    if (desc_received == false)
                    {
                        desc_received = true;
                        HtmlElementCollection input;
                        HtmlElement desc;
                        input = webBrowser1.Document.GetElementsByTagName("input");
                        desc = input["kuvaus"];
                        textBox_desc.Text = desc.GetAttribute("value");
                    }
                }
            }
            backgroundWorker1.RunWorkerAsync();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            running = false;
            webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=0");
            pictureBox1.Image = AutoRetku.Properties.Resources.red;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=1");
            pictureBox1.Image = AutoRetku.Properties.Resources.yolo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=2");
            pictureBox1.Image = AutoRetku.Properties.Resources.green;
        }

        private void button_update_desc_Click(object sender, EventArgs e)
        {
            HtmlElementCollection input;
            HtmlElement desc, save;
            input = webBrowser1.Document.GetElementsByTagName("input");
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

            if (textBox_service_user.Text != "")
            {
                worker_streamsvc.RunWorkerAsync();
            }

            if (logged == false)
            {
                HtmlElementCollection input;
                HtmlElement input_username, input_password, login;
                input = webBrowser1.Document.GetElementsByTagName("input");
                input_username = input["username"];
                input_password = input["password"];
                login = input["login"];
                input_username.SetAttribute("value", username);
                input_password.SetAttribute("value", password);
                login.InvokeMember("click");
            }
        }

        public void LogOut()
        {
            try
            {
                logged = false;
                string[] logoutparse1 = webBrowser1.DocumentText.Split(new string[] { "http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=" }, StringSplitOptions.None);
                string[] logoutparse2 = logoutparse1[1].Split(new string[] { "&logout=t&redirect=http://www.nesretku.com/index.php" }, StringSplitOptions.None);
                //MessageBox.Show(logoutparse2[0]);
                webBrowser1.Navigate("http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=" + logoutparse2[0] + "&logout=t&redirect=http://www.nesretku.com/index.php");
            }
            catch (Exception)
            {

            }
        }

        public void CheckStatus()
        {
            if (webBrowser1.DocumentText.Contains("alt=\"Kiinni\""))
            {
                pictureBox1.Image = AutoRetku.Properties.Resources.red;
            }

            if (webBrowser1.DocumentText.Contains("alt=\"Tauolla\""))
            {
                pictureBox1.Image = AutoRetku.Properties.Resources.yolo;
            }

            if (webBrowser1.DocumentText.Contains("alt=\"Päällä\""))
            {
                pictureBox1.Image = AutoRetku.Properties.Resources.green;
            }
            StatusChecked = true;
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            LogOut();
            e.Cancel = false;
            Application.Exit();
        }

        private void timer_refresh_Tick(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.nesretku.com/index.php?user=" + service_user);
        }

        public string GetSource(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                useTwitch = true;
            }
        }

        private void checkBox_service_start_CheckedChanged(object sender, EventArgs e) ///////////////// PAKKO TARKISTAA ERIKSEEN checkBox_service_start_CheckedChanged
        {
            if (useTwitch == true)
            {
                if (checkBox_service_start.Checked == true || checkBox_service_end.Checked == true)
                {
                    runTwitch = true;
                }
                else
                {
                    runTwitch = false;
                }
            }

            if (useHitbox == true)
            {
                if (checkBox_service_start.Checked == true || checkBox_service_end.Checked == true)
                {
                    runHitbox = true;
                }
                else
                {
                    runHitbox = false;
                }
            }
        }

        private void worker_service_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }

        private void worker_service_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (runTwitch == true)
            {
                string source = GetSource("https://api.twitch.tv/kraken/streams?channel=" + service_user);

                if (checkBox_service_start.Checked == true)
                {
                    if (source.Contains("\"status\""))
                    {
                        webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=2");
                        pictureBox1.Image = AutoRetku.Properties.Resources.green;
                    }
                    else
                    {
                        if (checkBox_service_end.Checked == true)
                        {
                            webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=0");
                            pictureBox1.Image = AutoRetku.Properties.Resources.red;
                        }
                    }
                }
            }

            if (runHitbox == true)
            {
                string source = GetSource("http://api.hitbox.tv/media/live/" + service_user);

                if (checkBox_service_start.Checked == true)
                {
                    if (source.Contains("\"media_is_live\":\"1\""))
                    {
                        webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=2");
                        pictureBox1.Image = AutoRetku.Properties.Resources.green;
                    }
                    else
                    {
                        if (checkBox_service_end.Checked == true)
                        {
                            webBrowser1.Navigate("http://www.nesretku.com/cmd/stream_switch.php?back=/index.php?user=" + username + "&switch=0");
                            pictureBox1.Image = AutoRetku.Properties.Resources.red;
                        }
                    }
                }
            }
            worker_streamsvc.RunWorkerAsync();
        }

        public Boolean isLive(int serviceid, string service_user)
        {
            if (serviceid == 1)
            {
                string source = GetSource("https://api.twitch.tv/kraken/streams?channel=" + service_user);

                if (source.Contains("\"status\""))
                {
                    return true;
                }
            }

            if (serviceid == 2)
            {
                string source = GetSource("http://api.hitbox.tv/media/live/" + service_user);

                if (source.Contains("\"media_is_live\":\"1\""))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
