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
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Linq;

namespace AutoRetku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Boolean startlogin = false;

        private Boolean runonce = false;
        private Boolean timerRunning = false;
        private Boolean logged = false;
        private Boolean workerStarted = false;
        private Boolean StatusChecked = false;
        private int notificationStatus = 0;
        private Boolean allowServiceChange = false;
        private Boolean donelogout = false;

        private string retrieved_status;
        private string username = "";
        private string password = "";
        private string userlist = "";
        private static string PasswordHash = "";
        private static string SaltKey = "";
        private static string VIKey = "";
        // private string log = ""; commented logging for later development

        public string service_user;

        private void webBrowser_retku_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string currentUrl = webBrowser_retku.Url.ToString();
            string documentText = webBrowser_retku.DocumentText;

            if (donelogout == false)
            {
                if (currentUrl.Contains("http://www.nesretku.com"))
                {
                    if (documentText.Contains("Stream tila"))
                    {
                        Debug.WriteLine("KIRJAUTUNU YRITETÄÄN ULOS");
                        webBrowser_logout.Navigate("http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=" + stringBetween(documentText, "http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=", "&logout=t&redirect=" + currentUrl) + "&logout=t&redirect=" + currentUrl);
                    }
                    else
                    {
                        donelogout = true;
                    }
                }
                else
                {
                    webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);
                }
            }

            if (donelogout == true)
            {
                if (startlogin && currentUrl == "http://www.nesretku.com/index.php?user=" + username)
                {
                    startlogin = false;
                    Debug.WriteLine("Logging in");
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
                else if (documentText.Contains("Stream tila") && logged == false)
                {
                    Debug.WriteLine("Logged in");
                    logged = true;
                }

                if (currentUrl == "http://www.nesretku.com/phpBB3/ucp.php?mode=login")
                {
                    Debug.WriteLine("Moved to http://www.nesretku.com/phpBB3/ucp.php?mode=login");
                    if (documentText.Contains("Olet antanut väärän salasanan."))
                    {
                        Debug.WriteLine("Olet antanut väärän salasanan.");
                        label_loginmsg.Text = "Olet antanut väärän salasanan.";
                        button_login.Enabled = true;
                    }
                    else if (documentText.Contains("Olet antanut väärän käyttäjätunnuksen."))
                    {
                        Debug.WriteLine("Olet antanut väärän käyttäjätunnuksen.");
                        label_loginmsg.Text = "Olet antanut väärän käyttäjätunnuksen.";
                        button_login.Enabled = true;
                    }
                }

                if (logged == true)
                {
                    Debug.WriteLine("Running after logged");

                    if (currentUrl != "http://www.nesretku.com/index.php?user=" + username)
                    {
                        Debug.WriteLine("Redirecting to http://www.nesretku.com/index.php?user=" + username);
                        webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);
                    }

                    if (!runonce)
                    {
                        runonce = true;
                        this.Height = 295;
                        label_loginmsg.Visible = false;
                        label_username.Visible = false;
                        label_password.Visible = false;
                        comboBox_username.Visible = false;
                        textBox_password.Visible = false;
                        button_login.Visible = false;
                        checkBox_remember.Visible = false;
                        button_deletelogin.Visible = false;

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

                        if (checkBox_remember.Checked == true)
                        {
                            writeUserData(username, password);
                        }

                        timer_refresh.Start();                    
                        worker_service.RunWorkerAsync();
                    }

                    if (StatusChecked == false)
                    {
                        if (documentText.Contains("name=\"palvelin\""))
                        {
                            Debug.WriteLine("Checking status & service");
                            HtmlElementCollection select, input;
                            HtmlElement service_option, desc;
                            select = webBrowser_retku.Document.GetElementsByTagName("select");
                            input = webBrowser_retku.Document.GetElementsByTagName("input");
                            service_option = select["palvelin"];
                            desc = input["kuvaus"];
                            textBox_desc.Text = desc.GetAttribute("value");

                            if (service_option.GetAttribute("value").ToString() == "3")
                            {
                                Debug.WriteLine("Setting status to Twitch");
                                comboBox_service.SelectedIndex = 0;
                            }
                            else
                            {
                                Debug.WriteLine("Setting status to Hitbox");
                                comboBox_service.SelectedIndex = 1;
                            }

                            StatusChecked = true;
                        }

                        if (documentText.Contains("alt=\"Kiinni\""))
                        {
                            Debug.WriteLine("Setting status to red");
                            pictureBox1.Image = AutoRetku.Properties.Resources.red;
                            notificationStatus = 0;
                        }

                        if (documentText.Contains("alt=\"Tauolla\""))
                        {
                            Debug.WriteLine("Setting status to yellow");
                            pictureBox1.Image = AutoRetku.Properties.Resources.yolo;
                            notificationStatus = 1;
                        }

                        if (documentText.Contains("alt=\"Päällä\""))
                        {
                            Debug.WriteLine("Setting status to green");
                            pictureBox1.Image = AutoRetku.Properties.Resources.green;
                            notificationStatus = 2;
                        }
                    }

                    if (workerStarted == false)
                    {
                        Debug.WriteLine("Starting worker_retku");
                        workerStarted = true;
                        worker_retku.RunWorkerAsync();
                    }
                }
                else
                {
                    Debug.WriteLine(webBrowser_retku.Url.ToString());
                }
            }
        }

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

            checkDB();
            listUsersDB();
        }

        private string randomString(int lenght)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, lenght)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        private void writeUserData(string user, string pwd)
        {
            dbquery("insert into users (user, pwd) values ('" + Encrypt(user) + "', '" + Encrypt(pwd) + "')");
        }

        private void deleteUserData(string user)
        {
            dbquery("delete from users where user like '" + Encrypt(user) + "'");
            comboBox_username.Text = "";
            textBox_password.Text = "";
            listUsersDB();
        }

        private string readPwd(string user)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=database.db;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select pwd from users where user like '" + Encrypt(user) + "'", c))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader["pwd"].ToString();
                        }
                        return "";
                    }
                }
            }
        }

        private void checkDB()
        {
            if (!File.Exists("database.db"))
            {
                SQLiteConnection.CreateFile("database.db");

                dbquery("create table users (user char(28), pwd char(20))");
                dbquery("create table keys (pwdhash char(72), salt char(72), vi char(72))");
                dbquery("insert into keys (pwdhash, salt, vi) values ('" + randomString(64) + "', '" + randomString(64) + "', '" + randomString(16) + "')");
            }

            using (SQLiteConnection c = new SQLiteConnection("Data Source=database.db;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select pwdhash, salt, vi from keys", c))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PasswordHash = reader["pwdhash"].ToString();
                            SaltKey = reader["salt"].ToString();
                            VIKey = reader["vi"].ToString();
                        }
                    }
                }
            }
        }

        private void dbquery(string sql)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=database.db;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void listUsersDB()
        {
            comboBox_username.Items.Clear();
            using (SQLiteConnection c = new SQLiteConnection("Data Source=database.db;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select user from users", c))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (userlist == "")
                            {
                                userlist = reader["user"].ToString();
                                comboBox_username.Items.Add(Decrypt(reader["user"].ToString()));
                                Debug.WriteLine(reader["user"].ToString());
                            }
                            else
                            {
                                userlist = userlist + "||" + reader["user"].ToString();
                                comboBox_username.Items.Add(Decrypt(reader["user"].ToString()));
                                Debug.WriteLine(reader["user"].ToString());
                            }
                        }
                        Debug.WriteLine(userlist);
                    }
                }
            }
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
            if (webBrowser_retku.Url.ToString() == "http://www.nesretku.com/index.php?user=" + username)
            {
                if (timerRunning == false)
                {
                    button_timer.Text = "Pysäytä";

                    checkBox_service_start.Checked = false;
                    checkBox_service_end.Checked = false;
                    checkBox_service_start.Enabled = false;
                    checkBox_service_end.Enabled = false;

                    timerRunning = true;
                }
                else
                {
                    button_timer.Text = "Ajasta";

                    checkBox_service_start.Enabled = true;
                    checkBox_service_end.Enabled = true;

                    timerRunning = false;
                }
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            comboBox_service.Enabled = true;
            checkBox_service_start.Checked = false;
            checkBox_service_end.Checked = false;
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
            Debug.WriteLine("Clicked login");
            button_login.Enabled = false;
            username = comboBox_username.Text;
            password = textBox_password.Text;

            startlogin = true;

            webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);
        }

        private void comboBox_service_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (allowServiceChange)
            {
                System.Threading.Thread.Sleep(1000);
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
                    string source = GetSource("https://api.twitch.tv/kraken/streams?channel=" + service_user); // Get json source code from Twitch API

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

        private void worker_retku_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }

        private void worker_retku_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (logged == true)
            {
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
            }

            worker_retku.RunWorkerAsync();
        }

        private void worker_service_DoWork(object sender, DoWorkEventArgs e)  // worker_service_RunWorkerCompleted runs after this thread finishes.
        {
            System.Threading.Thread.Sleep(5000);
        }

        private void worker_service_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //  Running in background to monitor and edit stream status
        {
            Debug.WriteLine("Running worker_service");
            service_user = textBox_service_user.Text;

            if (textBox_service_user.Text != "")
            {
                if (isStartOnOnline() && notificationStatus != 2) // if checkBox_service_start is checked
                {
                    Debug.WriteLine("mainos on pois päältä ja striimi online, jatketaan");
                    if (selectedService() == 1) // if selected service is Twitch
                    {
                        if (isLive(1, service_user)) // if Twitch is Live
                        {
                            comboBox_service.Enabled = false;
                            setRetkuOn();
                        }
                    }
                    else if (selectedService() == 2) // if selected service is Hitbox
                    {
                        if (isLive(2, service_user)) // if Hitbox is Live
                        {
                            comboBox_service.Enabled = false;
                            setRetkuOn();
                        }
                    }
                }

                if (isStopOnOffline() && notificationStatus != 0)  // if checkBox_service_end is checked
                {
                    if (selectedService() == 1) // if selected service is Twitch
                    {
                        if (!isLive(1, service_user)) // if Twitch is Live
                        {
                            comboBox_service.Enabled = true;
                            setRetkuOff();
                        }
                    }
                    else if (selectedService() == 2) // if selected service is Hitbox
                    {
                        if (!isLive(2, service_user)) // if Hitbox is Live
                        {
                            comboBox_service.Enabled = true;
                            setRetkuOff();
                        }
                    }
                }
            }

            if (!worker_service.IsBusy)
            {
                worker_service.RunWorkerAsync();
            }
        }

        #endregion

        #region webBrowser events

        

        #endregion

        #region Own functions

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
                string source = GetSource("http://api.hitbox.tv/media/user/" + service_user); // Get json source code from Hitbox API

                Debug.WriteLine(source + " " + service_user);

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
            label_statusmsgcontent.Text = "Ilmoitus asetettiin päälle";
            notificationStatus = 2;
            setRetkuStatus(2);
            pictureBox1.Image = AutoRetku.Properties.Resources.green; // Set notification image to green.png
        }

        public void setRetkuPause()
        {
            if (notificationStatus == 2)
            {
                label_statusmsgcontent.Text = "Ilmoitus asetettiin tauolle";
                notificationStatus = 1;
                setRetkuStatus(1);
                pictureBox1.Image = AutoRetku.Properties.Resources.yolo; // Set notification image to yolo.png
            }
            else
            {
                label_statusmsgcontent.Text = "Taukoa ei voida asettaa";
            }
        }

        public void setRetkuOff()
        {
            label_statusmsgcontent.Text = "Ilmoitus asetettiin pois päältä";
            notificationStatus = 0;
            button_timer.Text = "Ajasta";
            timerRunning = false;
            setRetkuStatus(0);
            pictureBox1.Image = AutoRetku.Properties.Resources.red;  // Set notification image to red.png
        }

        public string stringBetween(string str_full, string str_first, string str_second)
        {
            try
            {
                string[] strparse = str_full.Split(new string[] { str_first }, StringSplitOptions.None);
                string[] str_return = strparse[1].Split(new string[] { str_second }, StringSplitOptions.None);
                return str_return[0];
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion

        private void textBox_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter pressed
            {
                button_login.PerformClick();
            }
        }

        private void textBox_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Enter pressed
            {
                button_login.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser_retku.Navigate("http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=" + stringBetween(webBrowser_retku.DocumentText, "http://www.nesretku.com/phpBB3/ucp.php?mode=logout&sid=", "&logout=t&redirect=" + webBrowser_retku.Url.ToString()) + "&logout=t&redirect=" + webBrowser_retku.Url.ToString());
        }

        private void webBrowser_logout_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser_logout.Url.ToString().Contains("http://www.nesretku.com/phpBB3/ucp.php?mode=logout"))
            {
                donelogout = true;
                webBrowser_logout.Navigate("");
                webBrowser_retku.Navigate("http://www.nesretku.com/index.php?user=" + username);
            }
        }

        #region Crypting

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        #endregion

        private Boolean WriteDB(SQLiteConnection database)
        {
            return true;
        }

        private Boolean ReadDB(SQLiteConnection database, string select)
        {
            return true;
        }

        private void button_deletelogin_Click(object sender, EventArgs e)
        {
            deleteUserData(comboBox_username.Text);
            MessageBox.Show("Käyttäjä " + comboBox_username.Text + " poistettu!");
        }

        private void comboBox_username_SelectedValueChanged(object sender, EventArgs e)
        {
            string user = comboBox_username.Text;
            textBox_password.Text = Decrypt(readPwd(user));
        }
    }
}
