// Decompiled with JetBrains decompiler
// Type: LSILib.FB.SlowValve.ControlWindow
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using InSAT.Library.Gui;
using InSAT.OPC;
using LSILib.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace LSILib.FB.SlowValve
{
  public class ControlWindow : Form
  {
    private ControlBase _par;
    private int timer_counter;
    private bool blink;
    private int iOldState = -1;
    private OpcQuality old_q;
    private bool opening;
    private bool closing;
    private Timer myTimer;
    private IContainer components;
    private Label label_cmd_close;
    private PictureBox pic_cmd_slow;
    private Button button_to_auto;
    private Button button_to_manual;
    private Button button_change;
    private Label label_command;
    private PictureBox pic_cmd_fast;
    private Label label_tz_close;
    private PictureBox pic_tz_close;
    private Label label_tz_open;
    private PictureBox pic_tz_open;
    private Label label_block_close;
    private PictureBox pic_block_close;
    private Label label_block_open;
    private PictureBox pic_block_open;
    private Label label_es_closed;
    private PictureBox pic_es_closed;
    private Label label_es_opened;
    private PictureBox pic_es_opened;
    private Label label_es_not_good;
    private PictureBox pic_es_not_good;
    private Label label_es_collision;
    private PictureBox pic_es_collision;
    private Label label_not_closed;
    private PictureBox pic_not_closed;
    private Label label_not_opened;
    private PictureBox pic_not_oppened;
    private Label label_unreable;
    private PictureBox pic_unreable_open;
    private Button button_ack;
    private Button button_diag;
    private Button button_close;
    private Button button_full;
    private PictureBox pictureBox1;
    private Button button_slow;

    public ControlWindow(ControlBase par, Point _location)
    {
      this.InitializeComponent();
      this._par = par;
      this.Location = _location;
      this.Text = this._par.tt == title_type.title_long ? this._par.FBConnector.FBName : this._par.FBConnector.FBName.Substring(this._par.FBConnector.FBName.LastIndexOf('.') + 1);
    }

    private void TimerEventProcessor(object myObject, EventArgs myEventArgs) => this.show_info();

    private void show_info()
    {
      ++this.timer_counter;
      bool flag1 = this.timer_counter >= 3;
      if (flag1)
      {
        this.blink = !this.blink;
        this.timer_counter = 0;
      }
      int num = this._par.FBConnector.GetPinValue<int>(300);
      OpcQuality pinQuality = this._par.FBConnector.GetPinQuality(300);
      if (num != this.iOldState || pinQuality != this.old_q)
      {
        this.iOldState = num;
        this.old_q = pinQuality;
        if (pinQuality != OpcQuality.Ok)
          num = 0;
        Image lampOff = (Image) Resources.lamp_off;
        Image lampRed = (Image) Resources.lamp_red;
        Image lampGreen = (Image) Resources.lamp_green;
        Image lampYellow = (Image) Resources.lamp_yellow;
        flag1 = true;
        this.opening = false;
        this.closing = false;
        if ((num & 8) != 0)
          this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_openned_full);
        else if ((num & 16) != 0)
          this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_opened_slow);
        else if ((num & 32) != 0)
          this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_closed);
        else if ((num & 64) != 0)
          this.opening = true;
        else if ((num & 128) != 0)
          this.closing = true;
        else
          this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_wtf);
        this.button_full.Enabled = (num & 2048) != 0;
        this.button_slow.Enabled = (num & 4096) != 0;
        this.button_close.Enabled = (num & 8192) != 0;
        this.button_change.Enabled = (num & 3072) == 3072 || (num & 5632) == 4096 || (num & 8704) == 8704;
        this.pic_unreable_open.Image = (num & 1) == 0 ? lampRed : lampOff;
        this.pic_not_oppened.Image = (num & 2097152) != 0 ? lampRed : lampOff;
        this.pic_not_closed.Image = (num & 4194304) != 0 ? lampRed : lampOff;
        this.pic_es_collision.Image = (num & 1048576) != 0 ? lampRed : lampOff;
        this.pic_es_not_good.Image = (num & 8388608) != 0 ? lampRed : lampOff;
        this.pic_es_opened.Image = (num & 16384) != 0 ? lampGreen : lampOff;
        this.pic_es_closed.Image = (num & 32768) != 0 ? lampGreen : lampOff;
        this.pic_cmd_fast.Image = (num & 512) != 0 ? lampGreen : lampOff;
        this.pic_cmd_slow.Image = (num & 1024) != 0 ? lampGreen : lampOff;
        this.pic_block_open.Image = (num & 65536) != 0 ? lampYellow : lampOff;
        this.pic_block_close.Image = (num & 131072) != 0 ? lampYellow : lampOff;
        this.pic_tz_open.Image = (num & 262144) != 0 ? lampYellow : lampOff;
        this.pic_tz_close.Image = (num & 524288) != 0 ? lampYellow : lampOff;
      }
      if (flag1)
      {
        if (this.blink)
        {
          if (this.opening)
            this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_openning);
          if (this.closing)
            this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_closing);
        }
        else
        {
          if (this.opening)
            this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_closed);
          if (this.closing)
            this.pictureBox1.Image = this._par.getPicForForm(ControlBase.v_state.v_openned_full);
        }
      }
      Graphics graphics = this.CreateGraphics();
      bool flag2 = (num & 2) != 0;
      bool flag3 = (num & 4) != 0;
      if (this.blink && flag3 || flag2 && !flag3)
        FrameDrawer.Draw(graphics, new Rectangle(9, 9, 51, 53), Color.Red, 2);
      else
        FrameDrawer.Draw(graphics, new Rectangle(9, 9, 51, 53), this.BackColor, 2);
    }

    private void ControlWindow_Load(object sender, EventArgs e)
    {
      if (this.myTimer == null)
      {
        this.myTimer = new Timer();
        this.myTimer.Tick += new EventHandler(this.TimerEventProcessor);
        this.myTimer.Interval = 100;
        this.myTimer.Start();
      }
      this.show_info();
    }

    private void ControlWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.myTimer == null)
        return;
      this.myTimer.Stop();
    }

    private void close_button_Click(object sender, EventArgs e)
    {
      this._par.FBConnector.SetPinValue(312, (object) true);
    }

    private void open_full_Click(object sender, EventArgs e)
    {
      this._par.FBConnector.SetPinValue(310, (object) true);
    }

    private void button_slow_Click(object sender, EventArgs e)
    {
      this._par.FBConnector.SetPinValue(311, (object) true);
    }

    private void button_change_Click(object sender, EventArgs e)
    {
      this._par.FBConnector.SetPinValue(313, (object) true);
    }

    private void button_ack_Click(object sender, EventArgs e)
    {
      this._par.FBConnector.SetPinValue(314, (object) true);
    }

    private void button_to_manual_Click(object sender, EventArgs e)
    {
      this._par.FBConnector.SetPinValue(315, (object) true);
    }

    private void button_to_auto_Click(object sender, EventArgs e)
    {
      this._par.FBConnector.SetPinValue(316, (object) true);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ControlWindow));
      this.label_cmd_close = new Label();
      this.pic_cmd_slow = new PictureBox();
      this.button_to_auto = new Button();
      this.button_to_manual = new Button();
      this.button_change = new Button();
      this.label_command = new Label();
      this.pic_cmd_fast = new PictureBox();
      this.label_tz_close = new Label();
      this.pic_tz_close = new PictureBox();
      this.label_tz_open = new Label();
      this.pic_tz_open = new PictureBox();
      this.label_block_close = new Label();
      this.pic_block_close = new PictureBox();
      this.label_block_open = new Label();
      this.pic_block_open = new PictureBox();
      this.label_es_closed = new Label();
      this.pic_es_closed = new PictureBox();
      this.label_es_opened = new Label();
      this.pic_es_opened = new PictureBox();
      this.label_es_not_good = new Label();
      this.pic_es_not_good = new PictureBox();
      this.label_es_collision = new Label();
      this.pic_es_collision = new PictureBox();
      this.label_not_closed = new Label();
      this.pic_not_closed = new PictureBox();
      this.label_not_opened = new Label();
      this.pic_not_oppened = new PictureBox();
      this.label_unreable = new Label();
      this.pic_unreable_open = new PictureBox();
      this.button_ack = new Button();
      this.button_diag = new Button();
      this.button_close = new Button();
      this.button_full = new Button();
      this.pictureBox1 = new PictureBox();
      this.button_slow = new Button();
      ((ISupportInitialize) this.pic_cmd_slow).BeginInit();
      ((ISupportInitialize) this.pic_cmd_fast).BeginInit();
      ((ISupportInitialize) this.pic_tz_close).BeginInit();
      ((ISupportInitialize) this.pic_tz_open).BeginInit();
      ((ISupportInitialize) this.pic_block_close).BeginInit();
      ((ISupportInitialize) this.pic_block_open).BeginInit();
      ((ISupportInitialize) this.pic_es_closed).BeginInit();
      ((ISupportInitialize) this.pic_es_opened).BeginInit();
      ((ISupportInitialize) this.pic_es_not_good).BeginInit();
      ((ISupportInitialize) this.pic_es_collision).BeginInit();
      ((ISupportInitialize) this.pic_not_closed).BeginInit();
      ((ISupportInitialize) this.pic_not_oppened).BeginInit();
      ((ISupportInitialize) this.pic_unreable_open).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.label_cmd_close.AutoSize = true;
      this.label_cmd_close.Location = new Point(30, 348);
      this.label_cmd_close.Name = "label_cmd_close";
      this.label_cmd_close.Size = new Size(97, 13);
      this.label_cmd_close.TabIndex = 97;
      this.label_cmd_close.Text = "Команда 'Плавно'";
      this.pic_cmd_slow.Location = new Point(4, 344);
      this.pic_cmd_slow.Name = "pic_cmd_slow";
      this.pic_cmd_slow.Size = new Size(20, 20);
      this.pic_cmd_slow.TabIndex = 96;
      this.pic_cmd_slow.TabStop = false;
      this.button_to_auto.Location = new Point(140, 69);
      this.button_to_auto.Name = "button_to_auto";
      this.button_to_auto.Size = new Size(83, 23);
      this.button_to_auto.TabIndex = 95;
      this.button_to_auto.Text = "Автомат";
      this.button_to_auto.UseVisualStyleBackColor = true;
      this.button_to_auto.Click += new EventHandler(this.button_to_auto_Click);
      this.button_to_manual.Location = new Point(140, 39);
      this.button_to_manual.Name = "button_to_manual";
      this.button_to_manual.Size = new Size(83, 23);
      this.button_to_manual.TabIndex = 94;
      this.button_to_manual.Text = "Ручной";
      this.button_to_manual.UseVisualStyleBackColor = true;
      this.button_to_manual.Click += new EventHandler(this.button_to_manual_Click);
      this.button_change.Location = new Point(74, 99);
      this.button_change.Name = "button_change";
      this.button_change.Size = new Size(59, 23);
      this.button_change.TabIndex = 93;
      this.button_change.Text = "Сменить";
      this.button_change.UseVisualStyleBackColor = true;
      this.button_change.Click += new EventHandler(this.button_change_Click);
      this.label_command.AutoSize = true;
      this.label_command.Location = new Point(30, 322);
      this.label_command.Name = "label_command";
      this.label_command.Size = new Size(97, 13);
      this.label_command.TabIndex = 92;
      this.label_command.Text = "Команда 'Быстро'";
      this.pic_cmd_fast.Location = new Point(4, 318);
      this.pic_cmd_fast.Name = "pic_cmd_fast";
      this.pic_cmd_fast.Size = new Size(20, 20);
      this.pic_cmd_fast.TabIndex = 91;
      this.pic_cmd_fast.TabStop = false;
      this.label_tz_close.AutoSize = true;
      this.label_tz_close.Location = new Point(31, 452);
      this.label_tz_close.Name = "label_tz_close";
      this.label_tz_close.Size = new Size(72, 13);
      this.label_tz_close.TabIndex = 90;
      this.label_tz_close.Text = "ТЗ 'Закрыть'";
      this.pic_tz_close.Location = new Point(5, 448);
      this.pic_tz_close.Name = "pic_tz_close";
      this.pic_tz_close.Size = new Size(20, 20);
      this.pic_tz_close.TabIndex = 89;
      this.pic_tz_close.TabStop = false;
      this.label_tz_open.AutoSize = true;
      this.label_tz_open.Location = new Point(31, 426);
      this.label_tz_open.Name = "label_tz_open";
      this.label_tz_open.Size = new Size(72, 13);
      this.label_tz_open.TabIndex = 88;
      this.label_tz_open.Text = "ТЗ 'Открыть'";
      this.pic_tz_open.Location = new Point(5, 422);
      this.pic_tz_open.Name = "pic_tz_open";
      this.pic_tz_open.Size = new Size(20, 20);
      this.pic_tz_open.TabIndex = 87;
      this.pic_tz_open.TabStop = false;
      this.label_block_close.AutoSize = true;
      this.label_block_close.Location = new Point(31, 400);
      this.label_block_close.Name = "label_block_close";
      this.label_block_close.Size = new Size(120, 13);
      this.label_block_close.TabIndex = 86;
      this.label_block_close.Text = "Блокировка закрытия";
      this.pic_block_close.Location = new Point(5, 396);
      this.pic_block_close.Name = "pic_block_close";
      this.pic_block_close.Size = new Size(20, 20);
      this.pic_block_close.TabIndex = 85;
      this.pic_block_close.TabStop = false;
      this.label_block_open.AutoSize = true;
      this.label_block_open.Location = new Point(31, 374);
      this.label_block_open.Name = "label_block_open";
      this.label_block_open.Size = new Size(119, 13);
      this.label_block_open.TabIndex = 84;
      this.label_block_open.Text = "Блокировка открытия";
      this.pic_block_open.Location = new Point(5, 370);
      this.pic_block_open.Name = "pic_block_open";
      this.pic_block_open.Size = new Size(20, 20);
      this.pic_block_open.TabIndex = 83;
      this.pic_block_open.TabStop = false;
      this.label_es_closed.AutoSize = true;
      this.label_es_closed.Location = new Point(30, 296);
      this.label_es_closed.Name = "label_es_closed";
      this.label_es_closed.Size = new Size(101, 13);
      this.label_es_closed.TabIndex = 82;
      this.label_es_closed.Text = "Концевик 'Закрыт'";
      this.pic_es_closed.Location = new Point(4, 292);
      this.pic_es_closed.Name = "pic_es_closed";
      this.pic_es_closed.Size = new Size(20, 20);
      this.pic_es_closed.TabIndex = 81;
      this.pic_es_closed.TabStop = false;
      this.label_es_opened.AutoSize = true;
      this.label_es_opened.Location = new Point(30, 270);
      this.label_es_opened.Name = "label_es_opened";
      this.label_es_opened.Size = new Size(101, 13);
      this.label_es_opened.TabIndex = 80;
      this.label_es_opened.Text = "Концевик 'Открыт'";
      this.pic_es_opened.Location = new Point(4, 266);
      this.pic_es_opened.Name = "pic_es_opened";
      this.pic_es_opened.Size = new Size(20, 20);
      this.pic_es_opened.TabIndex = 79;
      this.pic_es_opened.TabStop = false;
      this.label_es_not_good.AutoSize = true;
      this.label_es_not_good.Location = new Point(30, 244);
      this.label_es_not_good.Name = "label_es_not_good";
      this.label_es_not_good.Size = new Size(143, 13);
      this.label_es_not_good.TabIndex = 78;
      this.label_es_not_good.Text = "Нет данных от концевиков";
      this.pic_es_not_good.Location = new Point(4, 240);
      this.pic_es_not_good.Name = "pic_es_not_good";
      this.pic_es_not_good.Size = new Size(20, 20);
      this.pic_es_not_good.TabIndex = 77;
      this.pic_es_not_good.TabStop = false;
      this.label_es_collision.AutoSize = true;
      this.label_es_collision.Location = new Point(30, 218);
      this.label_es_collision.Name = "label_es_collision";
      this.label_es_collision.Size = new Size(113, 13);
      this.label_es_collision.TabIndex = 76;
      this.label_es_collision.Text = "Колизия концевиков";
      this.pic_es_collision.Location = new Point(4, 214);
      this.pic_es_collision.Name = "pic_es_collision";
      this.pic_es_collision.Size = new Size(20, 20);
      this.pic_es_collision.TabIndex = 75;
      this.pic_es_collision.TabStop = false;
      this.label_not_closed.AutoSize = true;
      this.label_not_closed.Location = new Point(30, 192);
      this.label_not_closed.Name = "label_not_closed";
      this.label_not_closed.Size = new Size(74, 13);
      this.label_not_closed.TabIndex = 74;
      this.label_not_closed.Text = "Не закрылся";
      this.pic_not_closed.Location = new Point(4, 188);
      this.pic_not_closed.Name = "pic_not_closed";
      this.pic_not_closed.Size = new Size(20, 20);
      this.pic_not_closed.TabIndex = 73;
      this.pic_not_closed.TabStop = false;
      this.label_not_opened.AutoSize = true;
      this.label_not_opened.Location = new Point(30, 166);
      this.label_not_opened.Name = "label_not_opened";
      this.label_not_opened.Size = new Size(73, 13);
      this.label_not_opened.TabIndex = 72;
      this.label_not_opened.Text = "Не открылся";
      this.pic_not_oppened.Location = new Point(4, 162);
      this.pic_not_oppened.Name = "pic_not_oppened";
      this.pic_not_oppened.Size = new Size(20, 20);
      this.pic_not_oppened.TabIndex = 71;
      this.pic_not_oppened.TabStop = false;
      this.label_unreable.AutoSize = true;
      this.label_unreable.Location = new Point(30, 140);
      this.label_unreable.Name = "label_unreable";
      this.label_unreable.Size = new Size(158, 13);
      this.label_unreable.TabIndex = 70;
      this.label_unreable.Text = "Аппаратная недостоверность";
      this.pic_unreable_open.Location = new Point(4, 136);
      this.pic_unreable_open.Name = "pic_unreable_open";
      this.pic_unreable_open.Size = new Size(20, 20);
      this.pic_unreable_open.TabIndex = 69;
      this.pic_unreable_open.TabStop = false;
      this.button_ack.Location = new Point(140, 9);
      this.button_ack.Name = "button_ack";
      this.button_ack.Size = new Size(83, 23);
      this.button_ack.TabIndex = 68;
      this.button_ack.Text = "Квитировать";
      this.button_ack.UseVisualStyleBackColor = true;
      this.button_ack.Click += new EventHandler(this.button_ack_Click);
      this.button_diag.Location = new Point(5, 69);
      this.button_diag.Name = "button_diag";
      this.button_diag.Size = new Size(63, 23);
      this.button_diag.TabIndex = 67;
      this.button_diag.Text = "Больше";
      this.button_diag.UseVisualStyleBackColor = true;
      this.button_close.Location = new Point(75, 69);
      this.button_close.Name = "button_close";
      this.button_close.Size = new Size(59, 23);
      this.button_close.TabIndex = 66;
      this.button_close.Text = "Закрыть";
      this.button_close.UseVisualStyleBackColor = true;
      this.button_close.Click += new EventHandler(this.close_button_Click);
      this.button_full.Location = new Point(75, 9);
      this.button_full.Name = "button_full";
      this.button_full.Size = new Size(59, 23);
      this.button_full.TabIndex = 65;
      this.button_full.Text = "Быстро";
      this.button_full.UseVisualStyleBackColor = true;
      this.button_full.Click += new EventHandler(this.open_full_Click);
      this.pictureBox1.Image = (Image) Resources.pump_right_unreable;
      this.pictureBox1.Location = new Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(45, 47);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 64;
      this.pictureBox1.TabStop = false;
      this.button_slow.Location = new Point(75, 40);
      this.button_slow.Name = "button_slow";
      this.button_slow.Size = new Size(59, 23);
      this.button_slow.TabIndex = 98;
      this.button_slow.Text = "Плавно";
      this.button_slow.UseVisualStyleBackColor = true;
      this.button_slow.Click += new EventHandler(this.button_slow_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(228, 473);
      this.Controls.Add((Control) this.button_slow);
      this.Controls.Add((Control) this.label_cmd_close);
      this.Controls.Add((Control) this.pic_cmd_slow);
      this.Controls.Add((Control) this.button_to_auto);
      this.Controls.Add((Control) this.button_to_manual);
      this.Controls.Add((Control) this.button_change);
      this.Controls.Add((Control) this.label_command);
      this.Controls.Add((Control) this.pic_cmd_fast);
      this.Controls.Add((Control) this.label_tz_close);
      this.Controls.Add((Control) this.pic_tz_close);
      this.Controls.Add((Control) this.label_tz_open);
      this.Controls.Add((Control) this.pic_tz_open);
      this.Controls.Add((Control) this.label_block_close);
      this.Controls.Add((Control) this.pic_block_close);
      this.Controls.Add((Control) this.label_block_open);
      this.Controls.Add((Control) this.pic_block_open);
      this.Controls.Add((Control) this.label_es_closed);
      this.Controls.Add((Control) this.pic_es_closed);
      this.Controls.Add((Control) this.label_es_opened);
      this.Controls.Add((Control) this.pic_es_opened);
      this.Controls.Add((Control) this.label_es_not_good);
      this.Controls.Add((Control) this.pic_es_not_good);
      this.Controls.Add((Control) this.label_es_collision);
      this.Controls.Add((Control) this.pic_es_collision);
      this.Controls.Add((Control) this.label_not_closed);
      this.Controls.Add((Control) this.pic_not_closed);
      this.Controls.Add((Control) this.label_not_opened);
      this.Controls.Add((Control) this.pic_not_oppened);
      this.Controls.Add((Control) this.label_unreable);
      this.Controls.Add((Control) this.pic_unreable_open);
      this.Controls.Add((Control) this.button_ack);
      this.Controls.Add((Control) this.button_diag);
      this.Controls.Add((Control) this.button_close);
      this.Controls.Add((Control) this.button_full);
      this.Controls.Add((Control) this.pictureBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ControlWindow);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (ControlWindow);
      this.TopMost = true;
      this.Load += new EventHandler(this.ControlWindow_Load);
      this.FormClosing += new FormClosingEventHandler(this.ControlWindow_FormClosing);
      ((ISupportInitialize) this.pic_cmd_slow).EndInit();
      ((ISupportInitialize) this.pic_cmd_fast).EndInit();
      ((ISupportInitialize) this.pic_tz_close).EndInit();
      ((ISupportInitialize) this.pic_tz_open).EndInit();
      ((ISupportInitialize) this.pic_block_close).EndInit();
      ((ISupportInitialize) this.pic_block_open).EndInit();
      ((ISupportInitialize) this.pic_es_closed).EndInit();
      ((ISupportInitialize) this.pic_es_opened).EndInit();
      ((ISupportInitialize) this.pic_es_not_good).EndInit();
      ((ISupportInitialize) this.pic_es_collision).EndInit();
      ((ISupportInitialize) this.pic_not_closed).EndInit();
      ((ISupportInitialize) this.pic_not_oppened).EndInit();
      ((ISupportInitialize) this.pic_unreable_open).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
