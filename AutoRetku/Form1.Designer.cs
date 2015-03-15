namespace AutoRetku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.webBrowser_retku = new System.Windows.Forms.WebBrowser();
            this.button_timer = new System.Windows.Forms.Button();
            this.label_starting = new System.Windows.Forms.Label();
            this.label_ending = new System.Windows.Forms.Label();
            this.comboBox_starting_hour = new System.Windows.Forms.ComboBox();
            this.comboBox_starting_minute = new System.Windows.Forms.ComboBox();
            this.comboBox_ending_minute = new System.Windows.Forms.ComboBox();
            this.comboBox_ending_hour = new System.Windows.Forms.ComboBox();
            this.textBox_desc = new System.Windows.Forms.TextBox();
            this.label_desc = new System.Windows.Forms.Label();
            this.worker_retku = new System.ComponentModel.BackgroundWorker();
            this.button_start = new System.Windows.Forms.Button();
            this.button_pause = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_update_desc = new System.Windows.Forms.Button();
            this.label_username = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_login = new System.Windows.Forms.Button();
            this.checkBox_remember = new System.Windows.Forms.CheckBox();
            this.worker_streamsvc = new System.ComponentModel.BackgroundWorker();
            this.timer_refresh = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_retrievedesc = new System.Windows.Forms.Button();
            this.comboBox_service = new System.Windows.Forms.ComboBox();
            this.checkBox_service_end = new System.Windows.Forms.CheckBox();
            this.checkBox_service_start = new System.Windows.Forms.CheckBox();
            this.textBox_service_user = new System.Windows.Forms.TextBox();
            this.label_service_user = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser_retku
            // 
            this.webBrowser_retku.Location = new System.Drawing.Point(15, 212);
            this.webBrowser_retku.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_retku.Name = "webBrowser_retku";
            this.webBrowser_retku.Size = new System.Drawing.Size(594, 240);
            this.webBrowser_retku.TabIndex = 0;
            this.webBrowser_retku.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser_retku.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_retku_DocumentCompleted);
            // 
            // button_timer
            // 
            this.button_timer.Location = new System.Drawing.Point(12, 12);
            this.button_timer.Name = "button_timer";
            this.button_timer.Size = new System.Drawing.Size(75, 23);
            this.button_timer.TabIndex = 1;
            this.button_timer.Text = "Ajasta";
            this.button_timer.UseVisualStyleBackColor = true;
            this.button_timer.Visible = false;
            this.button_timer.Click += new System.EventHandler(this.button_timer_Click);
            // 
            // label_starting
            // 
            this.label_starting.AutoSize = true;
            this.label_starting.Location = new System.Drawing.Point(9, 45);
            this.label_starting.Name = "label_starting";
            this.label_starting.Size = new System.Drawing.Size(61, 13);
            this.label_starting.TabIndex = 3;
            this.label_starting.Text = "Aloitusaika:";
            this.label_starting.Visible = false;
            // 
            // label_ending
            // 
            this.label_ending.AutoSize = true;
            this.label_ending.Location = new System.Drawing.Point(177, 45);
            this.label_ending.Name = "label_ending";
            this.label_ending.Size = new System.Drawing.Size(68, 13);
            this.label_ending.TabIndex = 4;
            this.label_ending.Text = "Lopetusaika:";
            this.label_ending.Visible = false;
            // 
            // comboBox_starting_hour
            // 
            this.comboBox_starting_hour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_starting_hour.FormattingEnabled = true;
            this.comboBox_starting_hour.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.comboBox_starting_hour.Location = new System.Drawing.Point(79, 42);
            this.comboBox_starting_hour.Name = "comboBox_starting_hour";
            this.comboBox_starting_hour.Size = new System.Drawing.Size(43, 21);
            this.comboBox_starting_hour.TabIndex = 5;
            this.comboBox_starting_hour.Visible = false;
            // 
            // comboBox_starting_minute
            // 
            this.comboBox_starting_minute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_starting_minute.FormattingEnabled = true;
            this.comboBox_starting_minute.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.comboBox_starting_minute.Location = new System.Drawing.Point(128, 42);
            this.comboBox_starting_minute.Name = "comboBox_starting_minute";
            this.comboBox_starting_minute.Size = new System.Drawing.Size(43, 21);
            this.comboBox_starting_minute.TabIndex = 6;
            this.comboBox_starting_minute.Visible = false;
            // 
            // comboBox_ending_minute
            // 
            this.comboBox_ending_minute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ending_minute.FormattingEnabled = true;
            this.comboBox_ending_minute.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.comboBox_ending_minute.Location = new System.Drawing.Point(300, 42);
            this.comboBox_ending_minute.Name = "comboBox_ending_minute";
            this.comboBox_ending_minute.Size = new System.Drawing.Size(43, 21);
            this.comboBox_ending_minute.TabIndex = 8;
            this.comboBox_ending_minute.Visible = false;
            // 
            // comboBox_ending_hour
            // 
            this.comboBox_ending_hour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ending_hour.FormattingEnabled = true;
            this.comboBox_ending_hour.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.comboBox_ending_hour.Location = new System.Drawing.Point(251, 42);
            this.comboBox_ending_hour.Name = "comboBox_ending_hour";
            this.comboBox_ending_hour.Size = new System.Drawing.Size(43, 21);
            this.comboBox_ending_hour.TabIndex = 7;
            this.comboBox_ending_hour.Visible = false;
            // 
            // textBox_desc
            // 
            this.textBox_desc.Location = new System.Drawing.Point(61, 71);
            this.textBox_desc.Name = "textBox_desc";
            this.textBox_desc.Size = new System.Drawing.Size(282, 20);
            this.textBox_desc.TabIndex = 9;
            this.textBox_desc.Visible = false;
            // 
            // label_desc
            // 
            this.label_desc.AutoSize = true;
            this.label_desc.Location = new System.Drawing.Point(9, 74);
            this.label_desc.Name = "label_desc";
            this.label_desc.Size = new System.Drawing.Size(46, 13);
            this.label_desc.TabIndex = 10;
            this.label_desc.Text = "Kuvaus:";
            this.label_desc.Visible = false;
            // 
            // worker_retku
            // 
            this.worker_retku.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_retku_DoWork);
            this.worker_retku.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_retku_RunWorkerCompleted);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(145, 12);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 15;
            this.button_start.Text = "Käynnistä";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Visible = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_pause
            // 
            this.button_pause.Location = new System.Drawing.Point(226, 12);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(75, 23);
            this.button_pause.TabIndex = 16;
            this.button_pause.Text = "Keskeytä";
            this.button_pause.UseVisualStyleBackColor = true;
            this.button_pause.Visible = false;
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(307, 12);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 17;
            this.button_stop.Text = "Pysäytä";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Visible = false;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_update_desc
            // 
            this.button_update_desc.Location = new System.Drawing.Point(349, 71);
            this.button_update_desc.Name = "button_update_desc";
            this.button_update_desc.Size = new System.Drawing.Size(91, 20);
            this.button_update_desc.TabIndex = 18;
            this.button_update_desc.Text = "Päivitä kuvaus";
            this.button_update_desc.UseVisualStyleBackColor = true;
            this.button_update_desc.Visible = false;
            this.button_update_desc.Click += new System.EventHandler(this.button_update_desc_Click);
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(12, 15);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(80, 13);
            this.label_username.TabIndex = 19;
            this.label_username.Text = "Käyttäjätunnus:";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(12, 38);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(54, 13);
            this.label_password.TabIndex = 20;
            this.label_password.Text = "Salasana:";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(98, 12);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(261, 20);
            this.textBox_username.TabIndex = 21;
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(98, 35);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.PasswordChar = '*';
            this.textBox_password.Size = new System.Drawing.Size(261, 20);
            this.textBox_password.TabIndex = 22;
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(365, 12);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(75, 43);
            this.button_login.TabIndex = 23;
            this.button_login.Text = "Kirjaudu";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // checkBox_remember
            // 
            this.checkBox_remember.AutoSize = true;
            this.checkBox_remember.Location = new System.Drawing.Point(98, 61);
            this.checkBox_remember.Name = "checkBox_remember";
            this.checkBox_remember.Size = new System.Drawing.Size(85, 17);
            this.checkBox_remember.TabIndex = 24;
            this.checkBox_remember.Text = "Muista minut";
            this.checkBox_remember.UseVisualStyleBackColor = true;
            // 
            // worker_streamsvc
            // 
            this.worker_streamsvc.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_service_DoWork);
            this.worker_streamsvc.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_service_RunWorkerCompleted);
            // 
            // timer_refresh
            // 
            this.timer_refresh.Interval = 30000;
            this.timer_refresh.Tick += new System.EventHandler(this.timer_refresh_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AutoRetku.Properties.Resources.red;
            this.pictureBox1.Location = new System.Drawing.Point(388, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_retrievedesc);
            this.groupBox1.Controls.Add(this.comboBox_service);
            this.groupBox1.Controls.Add(this.checkBox_service_end);
            this.groupBox1.Controls.Add(this.checkBox_service_start);
            this.groupBox1.Controls.Add(this.textBox_service_user);
            this.groupBox1.Controls.Add(this.label_service_user);
            this.groupBox1.Location = new System.Drawing.Point(12, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 101);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            // 
            // button_retrievedesc
            // 
            this.button_retrievedesc.Location = new System.Drawing.Point(251, 20);
            this.button_retrievedesc.Name = "button_retrievedesc";
            this.button_retrievedesc.Size = new System.Drawing.Size(170, 23);
            this.button_retrievedesc.TabIndex = 29;
            this.button_retrievedesc.Text = "Nouda kuvaus palvelusta";
            this.button_retrievedesc.UseVisualStyleBackColor = true;
            this.button_retrievedesc.Click += new System.EventHandler(this.button_retrievedesc_Click);
            // 
            // comboBox_service
            // 
            this.comboBox_service.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_service.FormattingEnabled = true;
            this.comboBox_service.Items.AddRange(new object[] {
            "Twitch",
            "Hitbox"});
            this.comboBox_service.Location = new System.Drawing.Point(8, 0);
            this.comboBox_service.Name = "comboBox_service";
            this.comboBox_service.Size = new System.Drawing.Size(78, 21);
            this.comboBox_service.TabIndex = 28;
            this.comboBox_service.SelectedIndexChanged += new System.EventHandler(this.comboBox_service_SelectedIndexChanged);
            // 
            // checkBox_service_end
            // 
            this.checkBox_service_end.AutoSize = true;
            this.checkBox_service_end.Location = new System.Drawing.Point(9, 71);
            this.checkBox_service_end.Name = "checkBox_service_end";
            this.checkBox_service_end.Size = new System.Drawing.Size(236, 17);
            this.checkBox_service_end.TabIndex = 3;
            this.checkBox_service_end.Text = "Kytke ilmoitus pois päältä kun lähetys loppuu";
            this.checkBox_service_end.UseVisualStyleBackColor = true;
            // 
            // checkBox_service_start
            // 
            this.checkBox_service_start.AutoSize = true;
            this.checkBox_service_start.Location = new System.Drawing.Point(9, 48);
            this.checkBox_service_start.Name = "checkBox_service_start";
            this.checkBox_service_start.Size = new System.Drawing.Size(207, 17);
            this.checkBox_service_start.TabIndex = 2;
            this.checkBox_service_start.Text = "Kytke ilmoitus päälle kun lähetys alkaa";
            this.checkBox_service_start.UseVisualStyleBackColor = true;
            // 
            // textBox_service_user
            // 
            this.textBox_service_user.Location = new System.Drawing.Point(92, 22);
            this.textBox_service_user.Name = "textBox_service_user";
            this.textBox_service_user.Size = new System.Drawing.Size(153, 20);
            this.textBox_service_user.TabIndex = 1;
            // 
            // label_service_user
            // 
            this.label_service_user.AutoSize = true;
            this.label_service_user.Location = new System.Drawing.Point(6, 25);
            this.label_service_user.Name = "label_service_user";
            this.label_service_user.Size = new System.Drawing.Size(80, 13);
            this.label_service_user.TabIndex = 0;
            this.label_service_user.Text = "Käyttäjätunnus:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 464);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_update_desc);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_pause);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.label_desc);
            this.Controls.Add(this.textBox_desc);
            this.Controls.Add(this.comboBox_ending_minute);
            this.Controls.Add(this.comboBox_ending_hour);
            this.Controls.Add(this.comboBox_starting_minute);
            this.Controls.Add(this.comboBox_starting_hour);
            this.Controls.Add(this.label_ending);
            this.Controls.Add(this.label_starting);
            this.Controls.Add(this.button_timer);
            this.Controls.Add(this.checkBox_remember);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.webBrowser_retku);
            this.Controls.Add(this.button_login);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AutoRetku";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser_retku;
        private System.Windows.Forms.Button button_timer;
        private System.Windows.Forms.Label label_starting;
        private System.Windows.Forms.Label label_ending;
        private System.Windows.Forms.ComboBox comboBox_starting_hour;
        private System.Windows.Forms.ComboBox comboBox_starting_minute;
        private System.Windows.Forms.ComboBox comboBox_ending_minute;
        private System.Windows.Forms.ComboBox comboBox_ending_hour;
        private System.Windows.Forms.TextBox textBox_desc;
        private System.Windows.Forms.Label label_desc;
        private System.ComponentModel.BackgroundWorker worker_retku;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_pause;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_update_desc;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.CheckBox checkBox_remember;
        private System.ComponentModel.BackgroundWorker worker_streamsvc;
        private System.Windows.Forms.Timer timer_refresh;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_service_end;
        private System.Windows.Forms.CheckBox checkBox_service_start;
        private System.Windows.Forms.TextBox textBox_service_user;
        private System.Windows.Forms.Label label_service_user;
        private System.Windows.Forms.ComboBox comboBox_service;
        private System.Windows.Forms.Button button_retrievedesc;
    }
}

