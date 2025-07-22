// Decompiled with JetBrains decompiler
// Type: LSILib.FB.Table.TableControl
// Assembly: LSILib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A392302C-E791-44B3-B83D-F2A1681DB763
// Assembly location: C:\Program Files (x86)\MPSSoft\MasterSCADA\LSILib.dll

using FB.VisualFB;
using InSAT.OPC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace LSILib.FB.Table
{
  [ComVisible(true)]
  [DisplayName("Таблица")]
  [Guid("1C79CF0D-A84A-4cc5-A838-5858AAD74D53")]
  public class TableControl : VisualControlBase
  {
    private const int _max_data_lenght_modbus = 100;
    private IContainer components;
    private DataGridView dataGridView1;
    private Button button_add_after;
    private Button button_add_befor;
    private Button button_del;
    private TextBox DbgMsg;
    private Button button_save;
    private Button button_open;
    private OpenFileDialog openFileDialog1;
    private SaveFileDialog saveFileDialog1;
    private table_edit _table_type = table_edit.view;
    private Font _header_font = new Font(FontFamily.GenericSansSerif, 10f);
    private bool _header_font_changed = true;
    private Color _control_bg_color = Color.White;
    private Color _table_bg_color = Color.White;
    private Color _header_text_color = Color.Black;
    private bool _header_text_color_changed = true;
    private Color _header_bg_color = Color.DarkGray;
    private bool _header_bg_color_changed = true;
    private Font _line_font = new Font(FontFamily.GenericSansSerif, 10f);
    private Font _selected_line_font = new Font(FontFamily.GenericSansSerif, 12f);
    private Font _passed_line_font = new Font(FontFamily.GenericSansSerif, 11f);
    private Color _line_text_color = Color.Gray;
    private Color _selected_line_text_color = Color.Black;
    private Color _passed_line_text_color = Color.DarkGray;
    private Color _line_bg_color = Color.White;
    private Color _selected_line_bg_color = Color.Green;
    private Color _passed_line_bg_color = Color.Yellow;
    private int _buttons_size = 40;
    private Color _buttons_color;
    private bool _resize = true;
    private string _init_path = "c:\\";
    private string _table_definition = "c:\\table.xml";
    private int _selected_row = 2;
    private int _selected_row_old = -2;
    private bool _to_runtime;
    private Dictionary<string, TableEnumType> ttypes = new Dictionary<string, TableEnumType>();
    private List<TableColum> colums = new List<TableColum>();
    private int _float_colum_num;
    private int _int_colum_num;
    private int _bool_colum_num;
    private string make_table_msg = "_ ";
    private DateTime starttime = DateTime.Now;
    private int make_upload;
    private ushort _modbus_transactionID;
    private LSILib.FB.Table.Table.ControllerProtokol _protocol;
    private LSILib.FB.Table.Table.SLMP_area _SLMP_Area;
    private uint _uFloatBaseAddr;
    private uint _uFloatAreaSize;
    private uint _uIntBaseAddr;
    private uint _uIntAreaSize;
    private uint _uBoolBaseAddr;
    private uint _uBoolAreaSize;
    private uint _uControlBaseAddr;
    private uint _uIP1;
    private uint _uIP2;
    private uint _uIP3;
    private uint _uIP4;
    private uint _uPort;
    private uint _uTimeout;
    private bool setting_ok;
    private List<RecipeLine> edit_readed = new List<RecipeLine>();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TableControl));
      this.dataGridView1 = new DataGridView();
      this.button_add_after = new Button();
      this.button_add_befor = new Button();
      this.button_del = new Button();
      this.DbgMsg = new TextBox();
      this.button_save = new Button();
      this.button_open = new Button();
      this.openFileDialog1 = new OpenFileDialog();
      this.saveFileDialog1 = new SaveFileDialog();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AllowUserToResizeRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new Point(308, 45);
      this.dataGridView1.MultiSelect = false;
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
      this.dataGridView1.Size = new Size(403, 208);
      this.dataGridView1.TabIndex = 1;
      this.dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
      this.button_add_after.Image = (Image) componentResourceManager.GetObject("button_add_after.Image");
      this.button_add_after.Location = new Point(378, 426);
      this.button_add_after.Name = "button_add_after";
      this.button_add_after.Size = new Size(43, 40);
      this.button_add_after.TabIndex = 13;
      this.button_add_after.UseVisualStyleBackColor = true;
      this.button_add_after.Visible = false;
      this.button_add_after.Click += new EventHandler(this.button_add_after_Click);
      this.button_add_befor.Image = (Image) componentResourceManager.GetObject("button_add_befor.Image");
      this.button_add_befor.Location = new Point(316, 426);
      this.button_add_befor.Name = "button_add_befor";
      this.button_add_befor.Size = new Size(43, 40);
      this.button_add_befor.TabIndex = 12;
      this.button_add_befor.UseVisualStyleBackColor = true;
      this.button_add_befor.Visible = false;
      this.button_add_befor.Click += new EventHandler(this.button_add_befor_Click);
      this.button_del.Image = (Image) componentResourceManager.GetObject("button_del.Image");
      this.button_del.Location = new Point(248, 426);
      this.button_del.Name = "button_del";
      this.button_del.Size = new Size(43, 40);
      this.button_del.TabIndex = 11;
      this.button_del.UseVisualStyleBackColor = true;
      this.button_del.Visible = false;
      this.button_del.Click += new EventHandler(this.button_del_Click);
      this.DbgMsg.BorderStyle = BorderStyle.None;
      this.DbgMsg.Location = new Point(144, 440);
      this.DbgMsg.Multiline = true;
      this.DbgMsg.Name = "DbgMsg";
      this.DbgMsg.ReadOnly = true;
      this.DbgMsg.Size = new Size(648, 35);
      this.DbgMsg.TabIndex = 10;
      this.DbgMsg.Text = "gfhfdgh";
      this.button_save.Image = (Image) componentResourceManager.GetObject("button_save.Image");
      this.button_save.Location = new Point(49, 433);
      this.button_save.Name = "button_save";
      this.button_save.Size = new Size(41, 40);
      this.button_save.TabIndex = 9;
      this.button_save.UseVisualStyleBackColor = true;
      this.button_save.Visible = false;
      this.button_save.Click += new EventHandler(this.button_save_Click);
      this.button_open.Enabled = false;
      this.button_open.Image = (Image) componentResourceManager.GetObject("button_open.Image");
      this.button_open.Location = new Point(0, 436);
      this.button_open.Name = "button_open";
      this.button_open.Size = new Size(43, 40);
      this.button_open.TabIndex = 8;
      this.button_open.UseVisualStyleBackColor = true;
      this.button_open.Click += new EventHandler(this.button_open_Click);
      this.openFileDialog1.Filter = "recipe(*.csv)|*.csv";
      this.openFileDialog1.InitialDirectory = "c:\\";
      this.saveFileDialog1.Filter = "recipe(*.csv)|*.csv";
      this.saveFileDialog1.InitialDirectory = "c:\\";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.button_add_after);
      this.Controls.Add((Control) this.button_add_befor);
      this.Controls.Add((Control) this.button_del);
      this.Controls.Add((Control) this.DbgMsg);
      this.Controls.Add((Control) this.button_save);
      this.Controls.Add((Control) this.button_open);
      this.Controls.Add((Control) this.dataGridView1);
      this.Name = nameof (TableControl);
      this.Size = new Size(858, 476);
      this.Load += new EventHandler(this.MainTable_Load);
      this.SizeChanged += new EventHandler(this.MainTable_SizeChanged);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    [DisplayName("Режим")]
    public table_edit table_type
    {
      get => this._table_type;
      set => this._table_type = value;
    }

    [DisplayName("Цвет фона")]
    public Color control_bg_color
    {
      get => this._control_bg_color;
      set
      {
        if (value != Color.Transparent)
          this._control_bg_color = value;
        this.BackColor = this._control_bg_color;
        this.DbgMsg.BackColor = this._control_bg_color;
      }
    }

    [DisplayName("Цвет фона таблицы")]
    public Color table_bg_color
    {
      get => this._table_bg_color;
      set
      {
        if (value != Color.Transparent)
          this._table_bg_color = value;
        this.dataGridView1.BackgroundColor = this._table_bg_color;
      }
    }

    [DisplayName("Шрифт заголовка таблицы")]
    public Font header_font
    {
      get => this._header_font;
      set
      {
        this._header_font = value;
        this._header_font_changed = true;
      }
    }

    [DisplayName("Цвет текста заголовка таблицы")]
    public Color header_text_color
    {
      get => this._header_text_color;
      set
      {
        if (value != Color.Transparent)
          this._header_text_color = value;
        this._header_text_color_changed = true;
      }
    }

    [DisplayName("Цвет фона заголовка таблицы")]
    public Color header_bg_color
    {
      get => this._header_bg_color;
      set
      {
        if (value != Color.Transparent)
          this._header_bg_color = value;
        this._header_bg_color_changed = true;
      }
    }

    [DisplayName("Шрифт строки таблицы")]
    public Font line_font
    {
      get => this._line_font;
      set
      {
        this._line_font = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Цвет текста строки таблицы")]
    public Color line_text_color
    {
      get => this._line_text_color;
      set
      {
        if (value != Color.Transparent)
          this._line_text_color = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Цвет фона строки таблицы")]
    public Color line_bg_color
    {
      get => this._line_bg_color;
      set
      {
        if (value != Color.Transparent)
          this._line_bg_color = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Шрифт текущей строки таблицы")]
    public Font selected_line_font
    {
      get => this._selected_line_font;
      set
      {
        this._selected_line_font = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Цвет текста текущей строки таблицы")]
    public Color selected_line_text_color
    {
      get => this._selected_line_text_color;
      set
      {
        if (value != Color.Transparent)
          this._selected_line_text_color = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Цвет фона текущей строки таблицы")]
    public Color selected_line_bg_color
    {
      get => this._selected_line_bg_color;
      set
      {
        if (value != Color.Transparent)
          this._selected_line_bg_color = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Шрифт пройденной строки таблицы")]
    public Font passed_line_font
    {
      get => this._passed_line_font;
      set
      {
        this._passed_line_font = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Цвет текста пройденной строки таблицы")]
    public Color passed_line_text_color
    {
      get => this._passed_line_text_color;
      set
      {
        if (value != Color.Transparent)
          this._passed_line_text_color = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Цвет фона пройденной строки таблицы")]
    public Color passed_line_bg_color
    {
      get => this._passed_line_bg_color;
      set
      {
        if (value != Color.Transparent)
          this._passed_line_bg_color = value;
        this.make_row_color_and_font();
      }
    }

    [DisplayName("Размер кнопок")]
    public int buttons_size
    {
      get => this._buttons_size;
      set
      {
        this._buttons_size = value;
        this._resize = true;
      }
    }

    [DisplayName("Цвет кнопок")]
    public Color buttons_color
    {
      get => this._buttons_color;
      set
      {
        if (value != Color.Transparent)
          this._buttons_color = value;
        this.button_open.BackColor = this._buttons_color;
        this.button_save.BackColor = this._buttons_color;
      }
    }

    [DisplayName("Путь к рецептам")]
    public string init_path
    {
      get => this._init_path;
      set
      {
        this._init_path = value;
        this.openFileDialog1.InitialDirectory = this._init_path;
        this.saveFileDialog1.InitialDirectory = this._init_path;
      }
    }

    [DisplayName("Описание таблицы")]
    public string table_definition
    {
      get => this._table_definition;
      set
      {
        this._table_definition = value;
        this.Message("Описание таблицы будет изменено");
        this.make_table(!this.FBConnector.DesignMode && this._table_type == table_edit.edit);
        this.Message("Описание таблицы изменено");
      }
    }

    private void Message(string s)
    {
      string[] strArray = new string[3];
      for (int index = 0; index < strArray.Length; ++index)
        strArray[index] = "";
      for (int index = 1; index < this.DbgMsg.Lines.Length && index < strArray.Length; ++index)
        strArray[index - 1] = this.DbgMsg.Lines[index];
      strArray[strArray.Length - 1] = s;
      this.DbgMsg.Lines = strArray;
    }

    private void read_table_description()
    {
      this.make_table_msg += "Загрузка структуры таблицы. ";
      this.ttypes.Clear();
      this.colums.Clear();
      this._float_colum_num = 0;
      this._int_colum_num = 0;
      this._bool_colum_num = 0;
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        xmlDocument.Load(this._table_definition);
      }
      catch (Exception ex)
      {
        this.make_table_msg += "load error ";
        this.make_table_msg += ex.Message;
        return;
      }
      XmlElement documentElement = xmlDocument.DocumentElement;
      foreach (XmlNode childNode1 in documentElement.ChildNodes)
      {
        if (childNode1 is XmlElement)
        {
          XmlElement xmlElement1 = (XmlElement) childNode1;
          if (xmlElement1.Name == "EnumType")
          {
            string str1 = "";
            foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlElement1.Attributes)
            {
              if (attribute.Name == "Name")
                str1 = attribute.Value;
            }
            if (!string.IsNullOrEmpty(str1) && !(str1 == "int") && !(str1 == "bool") && !(str1 == "float"))
            {
              TableEnumType tableEnumType = new TableEnumType(str1);
              foreach (XmlNode childNode2 in xmlElement1.ChildNodes)
              {
                if (childNode2 is XmlElement)
                {
                  XmlElement xmlElement2 = (XmlElement) childNode2;
                  if (!(xmlElement2.Name != "Enum"))
                  {
                    string str2 = "";
                    string s = "";
                    foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlElement2.Attributes)
                    {
                      if (attribute.Name == "Str")
                        str2 = attribute.Value;
                      if (attribute.Name == "Val")
                        s = attribute.Value;
                    }
                    int result;
                    if (!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(s) && int.TryParse(s, out result))
                      tableEnumType.add_enum(str2, result);
                  }
                }
              }
              if (this.ttypes.ContainsKey(str1))
                this.ttypes[str1] = tableEnumType;
              else
                this.ttypes.Add(str1, tableEnumType);
            }
          }
        }
      }
      foreach (XmlNode childNode in documentElement.ChildNodes)
      {
        if (childNode is XmlElement)
        {
          XmlElement xmlElement = (XmlElement) childNode;
          if (xmlElement.Name == "Colum")
          {
            string Name = "";
            string key = "";
            foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlElement.Attributes)
            {
              if (attribute.Name == "Name")
                Name = attribute.Value;
              if (attribute.Name == "Type")
                key = attribute.Value;
            }
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(key))
            {
              switch (key)
              {
                case "int":
                  this.colums.Add(new TableColum(Name, cell_types.rt_int));
                  ++this._int_colum_num;
                  continue;
                case "float":
                  this.colums.Add(new TableColum(Name, cell_types.rt_float));
                  ++this._float_colum_num;
                  continue;
                case "bool":
                  this.colums.Add(new TableColum(Name, cell_types.rt_bool));
                  ++this._bool_colum_num;
                  continue;
                default:
                  if (this.ttypes.ContainsKey(key))
                  {
                    this.colums.Add(new TableColum(Name, this.ttypes[key]));
                    ++this._int_colum_num;
                    continue;
                  }
                  continue;
              }
            }
          }
        }
      }
    }

    private void make_table(bool edit_mode)
    {
      this.make_table_msg = "Подготовка таблицы. ";
      this.dataGridView1.Rows.Clear();
      this.dataGridView1.Columns.Clear();
      this.read_table_description();
      this.make_table_msg += " Успешно.";
      foreach (TableColum colum in this.colums)
      {
        if (edit_mode)
        {
          if (colum.type == cell_types.rt_bool)
          {
            DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
            viewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            viewComboBoxColumn.Name = colum.Name;
            viewComboBoxColumn.Tag = (object) colum;
            viewComboBoxColumn.MaxDropDownItems = 2;
            viewComboBoxColumn.Items.Add((object) "Да");
            viewComboBoxColumn.Items.Add((object) "Нет");
            colum.grid_index = this.dataGridView1.Columns.Add((DataGridViewColumn) viewComboBoxColumn);
          }
          else if (colum.type == cell_types.rt_enum)
          {
            DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
            viewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            viewComboBoxColumn.Name = colum.Name;
            viewComboBoxColumn.Tag = (object) colum;
            viewComboBoxColumn.MaxDropDownItems = colum.EnumType.enum_counts;
            for (int ittr_num = 0; ittr_num < colum.EnumType.enum_counts; ++ittr_num)
              viewComboBoxColumn.Items.Add((object) colum.EnumType.get_name_by_ittr_num(ittr_num));
            colum.grid_index = this.dataGridView1.Columns.Add((DataGridViewColumn) viewComboBoxColumn);
          }
          else
          {
            DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
            viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            viewTextBoxColumn.Name = colum.Name;
            viewTextBoxColumn.Tag = (object) colum;
            colum.grid_index = this.dataGridView1.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
          }
        }
        else
        {
          DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
          viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn.Name = colum.Name;
          viewTextBoxColumn.Tag = (object) colum;
          colum.grid_index = this.dataGridView1.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
        }
      }
      this.make_table_msg += " Таблица подготовлена.";
      this.Message(this.make_table_msg);
    }

    public TableControl()
      : base(true)
    {
      this.InitializeComponent();
      this.calc_sizes();
      this.dataGridView1.Rows.Clear();
      this.dataGridView1.Columns.Clear();
      this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void calc_sizes()
    {
      if (this.Width < 100)
        this.Width = 100;
      if (this.Height < 100)
        this.Height = 100;
      this.dataGridView1.Left = 0;
      this.dataGridView1.Top = 0;
      this.dataGridView1.Width = this.Width;
      this.dataGridView1.Height = this.Height - this._buttons_size - 5;
      this.button_save.Left = this.Width - this._buttons_size - 5;
      this.button_save.Width = this._buttons_size;
      this.button_save.Top = this.Height - this._buttons_size;
      this.button_save.Height = this._buttons_size;
      this.button_open.Left = this.Width - this._buttons_size * 2 - 10;
      this.button_open.Width = this._buttons_size;
      this.button_open.Top = this.Height - this._buttons_size;
      this.button_open.Height = this._buttons_size;
      this.button_add_after.Left = this.Width - this._buttons_size * 3 - 25;
      this.button_add_after.Width = this._buttons_size;
      this.button_add_after.Top = this.Height - this._buttons_size;
      this.button_add_after.Height = this._buttons_size;
      this.button_add_befor.Left = this.Width - this._buttons_size * 4 - 30;
      this.button_add_befor.Width = this._buttons_size;
      this.button_add_befor.Top = this.Height - this._buttons_size;
      this.button_add_befor.Height = this._buttons_size;
      this.button_del.Left = this.Width - this._buttons_size * 5 - 35;
      this.button_del.Width = this._buttons_size;
      this.button_del.Top = this.Height - this._buttons_size;
      this.button_del.Height = this._buttons_size;
      this.DbgMsg.Left = 5;
      this.DbgMsg.Top = this.Height - this._buttons_size;
      this.DbgMsg.Width = (this._table_type == table_edit.view ? this.button_open.Left : this.button_del.Left) - 10;
      this.DbgMsg.Height = this._buttons_size;
    }

    private void MainTable_SizeChanged(object sender, EventArgs e) => this.calc_sizes();

    private void make_row_color_and_font()
    {
      int index;
      for (index = 0; index < this.dataGridView1.Rows.Count && index < this._selected_row; ++index)
      {
        this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = this._passed_line_bg_color;
        this.dataGridView1.Rows[index].DefaultCellStyle.Font = this._passed_line_font;
        this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = this._passed_line_text_color;
      }
      for (; index < this.dataGridView1.Rows.Count && index < this._selected_row + 1; ++index)
      {
        this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = this._selected_line_bg_color;
        this.dataGridView1.Rows[index].DefaultCellStyle.Font = this._selected_line_font;
        this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = this._selected_line_text_color;
      }
      for (; index < this.dataGridView1.Rows.Count; ++index)
      {
        this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = this._line_bg_color;
        this.dataGridView1.Rows[index].DefaultCellStyle.Font = this._line_font;
        this.dataGridView1.Rows[index].DefaultCellStyle.ForeColor = this._line_text_color;
      }
    }

    private void OnPaintViewMode()
    {
      this.read_settings();
      bool flag = false;
      if (this.make_upload == 1)
      {
        this.make_upload = 2;
        try
        {
          if (this.setting_ok)
          {
            if (this.loadrecipefromplc())
              flag = true;
          }
        }
        finally
        {
          this.make_upload = 1;
        }
      }
      if (flag)
      {
        this.make_upload = 0;
        this.make_row_color_and_font();
      }
      uint pinValue1 = this.FBConnector.GetPinValue<uint>(1017);
      OpcQuality pinQuality1 = this.FBConnector.GetPinQuality(1017);
      int pinValue2 = this.FBConnector.GetPinValue<int>(1016);
      OpcQuality pinQuality2 = this.FBConnector.GetPinQuality(1016);
      this.button_open.Enabled = this.setting_ok && pinQuality1 == OpcQuality.Ok && ((int) pinValue1 & 3) == 3;
      this.button_save.Enabled = true;
      this.button_save.Visible = true;
      if (pinQuality2 != OpcQuality.Ok || pinQuality1 != OpcQuality.Ok || ((int) pinValue1 & 4) != 4)
        return;
      this._selected_row = pinValue2;
      if (this._selected_row == this._selected_row_old)
        return;
      this.make_row_color_and_font();
      this._selected_row_old = this._selected_row;
    }

    private void OnPaintEditMode()
    {
      this.button_open.Enabled = true;
      this.button_save.Visible = true;
      this.button_save.Enabled = true;
      this.button_del.Visible = true;
      this.button_add_after.Visible = true;
      this.button_add_befor.Visible = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this._resize)
      {
        this.calc_sizes();
        this._resize = false;
      }
      if (this._header_font_changed)
      {
        this.dataGridView1.RowHeadersDefaultCellStyle.Font = this._header_font;
        this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = this._header_font;
        this._header_font_changed = false;
      }
      if (this._header_text_color_changed)
      {
        this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor = this._header_text_color;
        this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = this._header_text_color;
        this._header_text_color_changed = false;
      }
      if (this._header_bg_color_changed)
      {
        this.dataGridView1.RowHeadersDefaultCellStyle.BackColor = this._header_bg_color;
        this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = this._header_bg_color;
        this._header_bg_color_changed = false;
      }
      if (this._to_runtime)
      {
        this._selected_row = -1;
        this._selected_row_old = -2;
        this._to_runtime = false;
      }
      if (this._selected_row != this._selected_row_old)
        this.make_row_color_and_font();
      this._selected_row_old = this._selected_row;
      if (this.FBConnector.DesignMode)
        return;
      if (this._table_type == table_edit.view)
        this.OnPaintViewMode();
      else
        this.OnPaintEditMode();
    }

    private void runtime_table_view_init(List<RecipeLine> data)
    {
      this.dataGridView1.Rows.Clear();
      foreach (RecipeLine recipeLine in data)
      {
        int index = this.dataGridView1.Rows.Add(new DataGridViewRow());
        this.dataGridView1.Rows[index].HeaderCell.Value = (object) (index + 1).ToString();
        foreach (TableColum colum in this.colums)
          this.dataGridView1.Rows[index].Cells[colum.grid_index].Value = (object) recipeLine.cells[colum.grid_index].fieldval;
      }
    }

    private void runtime_table_edit_init(List<RecipeLine> data)
    {
      this.dataGridView1.Rows.Clear();
      foreach (RecipeLine recipeLine in data)
      {
        int index = this.dataGridView1.Rows.Add(new DataGridViewRow());
        this.dataGridView1.Rows[index].HeaderCell.Value = (object) (index + 1).ToString();
        foreach (TableColum colum in this.colums)
          this.dataGridView1.Rows[index].Cells[colum.grid_index].Value = (object) recipeLine.cells[colum.grid_index].fieldval;
      }
    }

    private void design_table_init() => this.make_table(false);

    protected override void ToDesign()
    {
      this.design_table_init();
      this._selected_row = 2;
      this.make_row_color_and_font();
      this.Message(this.make_table_msg);
      this.make_upload = 0;
      base.ToDesign();
    }

    protected override void ToRuntime()
    {
      this._to_runtime = true;
      this.DbgMsg.Text = "";
      this.make_table(this._table_type == table_edit.edit);
      this.make_upload = 1;
      this.dataGridView1.ReadOnly = this._table_type == table_edit.view;
      this.edit_readed.Clear();
      base.ToRuntime();
    }

    private void MainTable_Load(object sender, EventArgs e) => this.calc_sizes();

    private void MainTable_Click(object sender, EventArgs e)
    {
      int num = this.FBConnector.DesignMode ? 1 : 0;
    }

    private void write_data(NetworkStream cs, uint addr, ushort[] data)
    {
      if (this._protocol != LSILib.FB.Table.Table.ControllerProtokol.Modbus)
        throw new Exception("подерживается только Modbus");
      if (data == null)
        throw new Exception("пустой буфер передачи");
      if (data.Length > 100)
        throw new Exception("Передача за раз не более " + 100.ToString());
      byte[] numArray1 = data.Length >= 1 ? new byte[data.Length * 2 + 13] : throw new Exception("передача нулевой длины");
      int offset = 0;
      ++this._modbus_transactionID;
      this.writewordtobuf(numArray1, ref offset, this._modbus_transactionID);
      this.writewordtobuf(numArray1, ref offset, (ushort) 0);
      this.writewordtobuf(numArray1, ref offset, (ushort) (data.Length * 2 + 7));
      this.writebytetobuf(numArray1, ref offset, (byte) 0);
      this.writebytetobuf(numArray1, ref offset, (byte) 16);
      this.writewordtobuf(numArray1, ref offset, (ushort) addr);
      this.writewordtobuf(numArray1, ref offset, (ushort) data.Length);
      this.writebytetobuf(numArray1, ref offset, (byte) (data.Length * 2));
      for (int index = 0; index < data.Length; ++index)
        this.writewordtobuf(numArray1, ref offset, data[index]);
      cs.Write(numArray1, 0, offset);
      byte[] numArray2 = new byte[300];
      int num = cs.Read(numArray2, 0, 300);
      if (num <= 0)
        throw new Exception("No response from controller");
      if (num < 12)
        throw new Exception("Resp lenght error");
      offset = 0;
      if ((int) this.readwordfrombuf(numArray2, ref offset) != (int) this._modbus_transactionID)
        throw new Exception("Modbus Wrong transaction ID");
      if (this.readwordfrombuf(numArray2, ref offset) != (ushort) 0)
        throw new Exception("Modbus Wrong field 0");
      if (this.readwordfrombuf(numArray2, ref offset) != (ushort) 6)
        throw new Exception("Modbus Wrong field Lenght");
      if (this.readbytefrombuf(numArray2, ref offset) != (byte) 0)
        throw new Exception("Modbus Wrong field dev addr");
      if (this.readbytefrombuf(numArray2, ref offset) != (byte) 16)
        throw new Exception("Modbus Wrong field fun");
      if ((int) this.readwordfrombuf(numArray2, ref offset) != (int) addr)
        throw new Exception("Modbus Wrong field addr");
      if ((int) this.readwordfrombuf(numArray2, ref offset) != (int) (ushort) data.Length)
        throw new Exception("Modbus Wrong field addr");
    }

    private ushort[] read_data(NetworkStream cs, uint addr, uint count)
    {
      if (this._protocol != LSILib.FB.Table.Table.ControllerProtokol.Modbus)
        throw new Exception("подерживается только Modbus");
      if (count > 100U)
        throw new Exception("Чтение за раз не более " + 100.ToString());
      if (count < 1U)
        throw new Exception("чтение нулевой длины");
      byte[] numArray1 = new byte[12];
      int offset = 0;
      ++this._modbus_transactionID;
      this.writewordtobuf(numArray1, ref offset, this._modbus_transactionID);
      this.writewordtobuf(numArray1, ref offset, (ushort) 0);
      this.writewordtobuf(numArray1, ref offset, (ushort) 6);
      this.writebytetobuf(numArray1, ref offset, (byte) 0);
      this.writebytetobuf(numArray1, ref offset, (byte) 3);
      this.writewordtobuf(numArray1, ref offset, (ushort) addr);
      this.writewordtobuf(numArray1, ref offset, (ushort) count);
      cs.Write(numArray1, 0, offset);
      byte[] numArray2 = new byte[(IntPtr) (uint) (9 + (int) count * 2)];
      int num = cs.Read(numArray2, 0, numArray2.Length);
      if (num <= 0)
        throw new Exception("No response from controller");
      if (num < numArray2.Length)
        throw new Exception("Resp lenght error");
      offset = 0;
      if ((int) this.readwordfrombuf(numArray2, ref offset) != (int) this._modbus_transactionID)
        throw new Exception("Modbus Wrong transaction ID");
      if (this.readwordfrombuf(numArray2, ref offset) != (ushort) 0)
        throw new Exception("Modbus Wrong field 0");
      if ((int) this.readwordfrombuf(numArray2, ref offset) != 3 + (int) count * 2)
        throw new Exception("Modbus Wrong field Lenght");
      if (this.readbytefrombuf(numArray2, ref offset) != (byte) 0)
        throw new Exception("Modbus Wrong field dev addr");
      if (this.readbytefrombuf(numArray2, ref offset) != (byte) 3)
        throw new Exception("Modbus Wrong field fun");
      if ((int) this.readbytefrombuf(numArray2, ref offset) != (int) count * 2)
        throw new Exception("Modbus Wrong field addr");
      ushort[] numArray3 = new ushort[(IntPtr) count];
      for (int index = 0; (long) index < (long) count; ++index)
        numArray3[index] = this.readwordfrombuf(numArray2, ref offset);
      return numArray3;
    }

    private bool writerecipedown(List<RecipeLine> recipe_down)
    {
      TcpClient tcpClient = new TcpClient();
      try
      {
        tcpClient.ReceiveTimeout = (int) this._uTimeout;
        tcpClient.SendTimeout = (int) this._uTimeout;
        IAsyncResult asyncResult = tcpClient.BeginConnect(new IPAddress((long) this._uIP1 & (long) byte.MaxValue | ((long) this._uIP2 & (long) byte.MaxValue) << 8 | ((long) this._uIP3 & (long) byte.MaxValue) << 16 | ((long) this._uIP4 & (long) byte.MaxValue) << 24), (int) this._uPort, (AsyncCallback) null, (object) null);
        WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
        try
        {
          if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds((double) this._uTimeout / 1000.0), false))
          {
            tcpClient.Close();
            throw new TimeoutException();
          }
          tcpClient.EndConnect(asyncResult);
        }
        finally
        {
          asyncWaitHandle.Close();
        }
        NetworkStream stream = tcpClient.GetStream();
        ushort[] data1 = new ushort[1]{ (ushort) 2 };
        this.write_data(stream, this._uControlBaseAddr + 1U, data1);
        ushort[] data2 = this.read_data(stream, this._uControlBaseAddr, 1U);
        if (data2[0] != (ushort) 2)
          throw new Exception("запись заблокирована контроллером");
        data2[0] = (ushort) 0;
        this.write_data(stream, this._uControlBaseAddr + 2U, data2);
        int num1 = 0;
        int index1 = 0;
        ushort[] numArray1 = new ushort[recipe_down.Count * this._float_colum_num * 2];
        ushort[] numArray2 = new ushort[recipe_down.Count * this._int_colum_num];
        ushort[] numArray3 = new ushort[recipe_down.Count * this._bool_colum_num / 16 + (recipe_down.Count * this._bool_colum_num % 16 > 0 ? 1 : 0)];
        for (int index2 = 0; index2 < numArray3.Length; ++index2)
          numArray3[index2] = (ushort) 0;
        int num2 = 0;
        foreach (RecipeLine recipeLine in recipe_down)
        {
          foreach (TCell cell in recipeLine.cells)
          {
            if (cell.colum.type == cell_types.rt_int || cell.colum.type == cell_types.rt_enum)
            {
              numArray2[index1] = (ushort) cell.servalue;
              ++index1;
            }
            if (cell.colum.type == cell_types.rt_float)
            {
              numArray1[num1 * 2] = (ushort) (cell.servalue & (uint) ushort.MaxValue);
              numArray1[num1 * 2 + 1] = (ushort) (cell.servalue >> 16 & (uint) ushort.MaxValue);
              ++num1;
            }
            if (cell.colum.type == cell_types.rt_bool)
            {
              numArray3[num2 / 16] += (ushort) (cell.servalue << num2 % 16);
              ++num2;
            }
          }
        }
        ushort[] data3;
        for (int index3 = 0; index3 < numArray2.Length; index3 += data3.Length)
        {
          data3 = new ushort[numArray2.Length - index3 > 100 ? 100 : numArray2.Length - index3];
          for (int index4 = 0; index4 < data3.Length; ++index4)
            data3[index4] = numArray2[index3 + index4];
          this.write_data(stream, (uint) ((ulong) this._uIntBaseAddr + (ulong) index3), data3);
        }
        ushort[] data4;
        for (int index5 = 0; index5 < numArray1.Length; index5 += data4.Length)
        {
          data4 = new ushort[numArray1.Length - index5 > 100 ? 100 : numArray1.Length - index5];
          for (int index6 = 0; index6 < data4.Length; ++index6)
            data4[index6] = numArray1[index5 + index6];
          this.write_data(stream, (uint) ((ulong) this._uFloatBaseAddr + (ulong) index5), data4);
        }
        ushort[] data5;
        for (int index7 = 0; index7 < numArray3.Length; index7 += data5.Length)
        {
          data5 = new ushort[numArray3.Length - index7 > 100 ? 100 : numArray3.Length - index7];
          for (int index8 = 0; index8 < data5.Length; ++index8)
            data5[index8] = numArray3[index7 + index8];
          this.write_data(stream, (uint) ((ulong) this._uBoolBaseAddr + (ulong) index7), data5);
        }
        data2[0] = (ushort) recipe_down.Count;
        this.write_data(stream, this._uControlBaseAddr + 2U, data2);
        data2[0] = (ushort) 1;
        this.write_data(stream, this._uControlBaseAddr + 1U, data2);
      }
      catch (Exception ex)
      {
        this.Message("Ошибка при записи рецепта в контреллер: " + ex.Message);
        return false;
      }
      finally
      {
        tcpClient.Close();
      }
      return true;
    }

    private void writedwordtobuf(byte[] buf, ref int offset, uint data)
    {
      buf[offset] = (byte) (data >> 24 & (uint) byte.MaxValue);
      buf[offset + 1] = (byte) (data >> 16 & (uint) byte.MaxValue);
      buf[offset + 2] = (byte) (data >> 8 & (uint) byte.MaxValue);
      buf[offset + 3] = (byte) (data & (uint) byte.MaxValue);
      offset += 4;
    }

    private void writewordtobuf(byte[] buf, ref int offset, ushort data)
    {
      buf[offset] = (byte) ((int) data >> 8 & (int) byte.MaxValue);
      buf[offset + 1] = (byte) ((uint) data & (uint) byte.MaxValue);
      offset += 2;
    }

    private void writebytetobuf(byte[] buf, ref int offset, byte data)
    {
      buf[offset] = (byte) ((uint) data & (uint) byte.MaxValue);
      ++offset;
    }

    private ushort readwordfrombuf(byte[] buf, ref int offset)
    {
      ushort num = (ushort) ((uint) (ushort) (0U + (uint) (ushort) ((uint) buf[offset] << 8)) + (uint) (ushort) buf[offset + 1]);
      offset += 2;
      return num;
    }

    private byte readbytefrombuf(byte[] buf, ref int offset)
    {
      byte num = buf[offset];
      ++offset;
      return num;
    }

    private void do_load(string file_name)
    {
      this.dataGridView1.Rows.Clear();
      List<RecipeLine> recipeLineList = new List<RecipeLine>();
      try
      {
        using (Stream stream = (Stream) new FileStream(file_name, FileMode.OpenOrCreate))
        {
          using (StreamReader streamReader = new StreamReader(stream))
          {
            bool flag = true;
            string fileline;
            while ((fileline = streamReader.ReadLine()) != null)
            {
              if (flag)
              {
                flag = false;
              }
              else
              {
                try
                {
                  RecipeLine recipeLine = new RecipeLine(this.colums, fileline);
                  recipeLineList.Add(recipeLine);
                }
                catch (Exception ex)
                {
                  this.Message("Ошибка при разборе строки " + (recipeLineList.Count + 1).ToString() + " : " + ex.Message);
                  return;
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        this.Message("Ошибка открытия файла: " + ex.Message);
        return;
      }
      if (recipeLineList.Count < 1)
      {
        this.Message("Рецепт пуст, загрузка не возможна");
      }
      else
      {
        foreach (RecipeLine recipeLine in recipeLineList)
        {
          int index = this.dataGridView1.Rows.Add();
          foreach (TCell cell in recipeLine.cells)
            this.dataGridView1.Rows[index].Cells[cell.colum.grid_index].Value = (object) cell;
          this.dataGridView1.Rows[index].HeaderCell.Value = (object) (index + 1).ToString();
        }
        this.Message("Данные загружены из файла " + file_name);
      }
    }

    private List<RecipeLine> read_recipe_from_file()
    {
      List<RecipeLine> recipeLineList = new List<RecipeLine>();
      using (Stream stream = (Stream) new FileStream(this.openFileDialog1.FileName, FileMode.OpenOrCreate))
      {
        using (StreamReader streamReader = new StreamReader(stream))
        {
          bool flag = true;
          string fileline;
          while ((fileline = streamReader.ReadLine()) != null)
          {
            if (flag)
            {
              flag = false;
            }
            else
            {
              try
              {
                RecipeLine recipeLine = new RecipeLine(this.colums, fileline);
                recipeLineList.Add(recipeLine);
              }
              catch (Exception ex)
              {
                throw new Exception("Ошибка при разборе строки " + (recipeLineList.Count + 1).ToString() + " : " + ex.Message);
              }
            }
          }
        }
      }
      this.Message("Данные загружены из файла " + this.openFileDialog1.FileName);
      return recipeLineList;
    }

    private void read_settings()
    {
      uint pinValue1 = this.FBConnector.GetPinValue<uint>(1001);
      if (this.FBConnector.GetPinQuality(1001) != OpcQuality.Ok)
      {
        this.setting_ok = false;
      }
      else
      {
        switch (pinValue1)
        {
          case 1:
            this._protocol = LSILib.FB.Table.Table.ControllerProtokol.Modbus;
            uint pinValue2 = this.FBConnector.GetPinValue<uint>(1002);
            if (this.FBConnector.GetPinQuality(1002) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            switch (pinValue2)
            {
              case 1:
                this._SLMP_Area = LSILib.FB.Table.Table.SLMP_area.D;
                break;
              case 2:
                this._SLMP_Area = LSILib.FB.Table.Table.SLMP_area.R;
                break;
              default:
                this.setting_ok = false;
                return;
            }
            uint pinValue3 = this.FBConnector.GetPinValue<uint>(1003);
            if (this.FBConnector.GetPinQuality(1003) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uFloatBaseAddr = pinValue3;
            uint pinValue4 = this.FBConnector.GetPinValue<uint>(1004);
            if (this.FBConnector.GetPinQuality(1004) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uFloatAreaSize = pinValue4;
            uint pinValue5 = this.FBConnector.GetPinValue<uint>(1005);
            if (this.FBConnector.GetPinQuality(1005) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uIntBaseAddr = pinValue5;
            uint pinValue6 = this.FBConnector.GetPinValue<uint>(1006);
            if (this.FBConnector.GetPinQuality(1006) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uIntAreaSize = pinValue6;
            uint pinValue7 = this.FBConnector.GetPinValue<uint>(1007);
            if (this.FBConnector.GetPinQuality(1007) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uBoolBaseAddr = pinValue7;
            uint pinValue8 = this.FBConnector.GetPinValue<uint>(1008);
            if (this.FBConnector.GetPinQuality(1008) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uBoolAreaSize = pinValue8;
            uint pinValue9 = this.FBConnector.GetPinValue<uint>(1009);
            if (this.FBConnector.GetPinQuality(1009) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uControlBaseAddr = pinValue9;
            uint pinValue10 = this.FBConnector.GetPinValue<uint>(1010);
            if (this.FBConnector.GetPinQuality(1010) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uIP1 = pinValue10;
            uint pinValue11 = this.FBConnector.GetPinValue<uint>(1011);
            if (this.FBConnector.GetPinQuality(1011) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uIP2 = pinValue11;
            uint pinValue12 = this.FBConnector.GetPinValue<uint>(1012);
            if (this.FBConnector.GetPinQuality(1012) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uIP3 = pinValue12;
            uint pinValue13 = this.FBConnector.GetPinValue<uint>(1013);
            if (this.FBConnector.GetPinQuality(1013) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uIP4 = pinValue13;
            uint pinValue14 = this.FBConnector.GetPinValue<uint>(1014);
            if (this.FBConnector.GetPinQuality(1014) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uPort = pinValue14;
            uint pinValue15 = this.FBConnector.GetPinValue<uint>(1015);
            if (this.FBConnector.GetPinQuality(1015) != OpcQuality.Ok)
            {
              this.setting_ok = false;
              break;
            }
            this._uTimeout = pinValue15;
            this.setting_ok = true;
            break;
          case 2:
            this._protocol = LSILib.FB.Table.Table.ControllerProtokol.SLMP_not_implimated;
            this.setting_ok = false;
            break;
          default:
            this.setting_ok = false;
            break;
        }
      }
    }

    private void load_recipe_to_view()
    {
      this.read_settings();
      if (!this.setting_ok)
      {
        this.Message("Ошибка чтения настроеек. Нет связи, продолжение загрузки рецепта не возможно");
      }
      else
      {
        int num = -1;
        if (this._float_colum_num > 0)
          num = (int) this._uFloatAreaSize / 2 / this._float_colum_num;
        if (this._int_colum_num > 0)
        {
          if (num < 0)
            num = (int) this._uIntAreaSize / this._int_colum_num;
          else if ((int) this._uIntAreaSize / this._int_colum_num < num)
            num = (int) this._uIntAreaSize / this._int_colum_num;
        }
        if (this._bool_colum_num > 0)
        {
          if (num < 0)
            num = (int) this._uBoolAreaSize * 16 / this._bool_colum_num;
          else if ((int) this._uBoolAreaSize * 16 / this._bool_colum_num < num)
            num = (int) this._uBoolAreaSize * 16 / this._bool_colum_num;
        }
        if (num < 0)
          this.Message("Описание не загружено или ошибки при загрузки описания");
        else if (num == 0)
        {
          this.Message("Не выделены отдельные области памяти");
        }
        else
        {
          List<RecipeLine> recipeLineList = new List<RecipeLine>();
          List<RecipeLine> recipe_down;
          try
          {
            recipe_down = this.read_recipe_from_file();
          }
          catch (Exception ex)
          {
            this.Message(ex.Message);
            return;
          }
          if (num < recipe_down.Count)
          {
            this.Message("Слишком длинный рецепт, загрузка не возможна");
            recipeLineList = (List<RecipeLine>) null;
          }
          else
          {
            this.writerecipedown(recipe_down);
            Thread.Sleep(200);
            this.loadrecipefromplc();
          }
        }
      }
    }

    private void load_recipe_to_edit()
    {
      this.edit_readed.Clear();
      try
      {
        this.edit_readed = this.read_recipe_from_file();
      }
      catch (Exception ex)
      {
        this.Message(ex.Message);
        return;
      }
      this.runtime_table_edit_init(this.edit_readed);
    }

    private void button_open_Click(object sender, EventArgs e)
    {
      if (this.FBConnector.DesignMode || this.openFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      this.saveFileDialog1.InitialDirectory = this.openFileDialog1.InitialDirectory;
      if (this._table_type == table_edit.view)
        this.load_recipe_to_view();
      else
        this.load_recipe_to_edit();
    }

    private bool loadrecipefromplc()
    {
      TcpClient tcpClient = new TcpClient();
      try
      {
        tcpClient.ReceiveTimeout = (int) this._uTimeout;
        tcpClient.SendTimeout = (int) this._uTimeout;
        IAsyncResult asyncResult = tcpClient.BeginConnect(new IPAddress((long) this._uIP1 & (long) byte.MaxValue | ((long) this._uIP2 & (long) byte.MaxValue) << 8 | ((long) this._uIP3 & (long) byte.MaxValue) << 16 | ((long) this._uIP4 & (long) byte.MaxValue) << 24), (int) this._uPort, (AsyncCallback) null, (object) null);
        WaitHandle asyncWaitHandle = asyncResult.AsyncWaitHandle;
        try
        {
          if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds((double) this._uTimeout / 1000.0), false))
          {
            tcpClient.Close();
            throw new TimeoutException();
          }
          tcpClient.EndConnect(asyncResult);
        }
        finally
        {
          asyncWaitHandle.Close();
        }
        NetworkStream stream = tcpClient.GetStream();
        ushort[] numArray1 = this.read_data(stream, this._uControlBaseAddr, 3U);
        ushort capacity = numArray1[0] == (ushort) 1 || numArray1[0] == (ushort) 3 ? numArray1[2] : throw new Exception("контроллер не готов к вычитыванию");
        List<RecipeLine> data = new List<RecipeLine>((int) capacity);
        if (capacity > (ushort) 0)
        {
          ushort[] int_data = new ushort[(int) capacity * this._int_colum_num];
          ushort[] float_data = new ushort[(int) capacity * this._float_colum_num * 2];
          ushort[] bool_data = new ushort[(int) capacity * this._bool_colum_num / 16 + ((int) capacity * this._bool_colum_num % 16 > 0 ? 1 : 0)];
          ushort[] numArray2;
          for (int index1 = 0; index1 < int_data.Length; index1 += numArray2.Length)
          {
            numArray2 = this.read_data(stream, (uint) ((ulong) this._uIntBaseAddr + (ulong) index1), int_data.Length - index1 > 100 ? 100U : (uint) (int_data.Length - index1));
            for (int index2 = 0; index2 < numArray2.Length; ++index2)
              int_data[index1 + index2] = numArray2[index2];
          }
          ushort[] numArray3;
          for (int index3 = 0; index3 < float_data.Length; index3 += numArray3.Length)
          {
            numArray3 = this.read_data(stream, (uint) ((ulong) this._uFloatBaseAddr + (ulong) index3), float_data.Length - index3 > 100 ? 100U : (uint) (float_data.Length - index3));
            for (int index4 = 0; index4 < numArray3.Length; ++index4)
              float_data[index3 + index4] = numArray3[index4];
          }
          ushort[] numArray4;
          for (int index5 = 0; index5 < bool_data.Length; index5 += numArray4.Length)
          {
            numArray4 = this.read_data(stream, (uint) ((ulong) this._uBoolBaseAddr + (ulong) index5), bool_data.Length - index5 > 100 ? 100U : (uint) (bool_data.Length - index5));
            for (int index6 = 0; index6 < numArray4.Length; ++index6)
              bool_data[index5 + index6] = numArray4[index6];
          }
          int int_index = 0;
          int float_index = 0;
          int bool_index = 0;
          for (int index = 0; index < (int) capacity; ++index)
            data.Add(new RecipeLine(this.colums, int_data, ref int_index, float_data, ref float_index, bool_data, ref bool_index));
        }
        this.edit_readed = data;
        this.runtime_table_view_init(data);
        this.Message("Успешная выгрузка рецепта");
      }
      catch (Exception ex)
      {
        this.Message("Load from PLC error: " + ex.Message);
        return false;
      }
      finally
      {
        tcpClient.Close();
      }
      return true;
    }

    private uint readdwordtobuf(byte[] buf, ref int offset)
    {
      uint num = 0U | (uint) buf[offset] | (uint) buf[offset + 1] << 8 | (uint) buf[offset + 2] << 16 | (uint) buf[offset + 3] << 24;
      offset += 4;
      return num;
    }

    private ushort readwordtobuf(byte[] buf, ref int offset)
    {
      ushort num = (ushort) ((uint) (ushort) (0U | (uint) (ushort) buf[offset]) | (uint) (ushort) ((uint) buf[offset + 1] << 8));
      offset += 2;
      return num;
    }

    private byte readbytetobuf(byte[] buf, ref int offset)
    {
      byte num = buf[offset];
      ++offset;
      return num;
    }

    private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
      if (((TableColum) this.dataGridView1.Columns[e.ColumnIndex].Tag).type == cell_types.rt_float)
      {
        if (!double.TryParse((string) this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, out double _))
        {
          int num = (int) MessageBox.Show("Ошибка ввода данных");
          this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object) this.edit_readed[e.RowIndex].cells[e.ColumnIndex].fieldval;
        }
        else
          this.edit_readed[e.RowIndex].cells[e.ColumnIndex].SetNewValue((string) this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
      }
      else if (((TableColum) this.dataGridView1.Columns[e.ColumnIndex].Tag).type == cell_types.rt_int)
      {
        if (!int.TryParse((string) this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, out int _))
        {
          int num = (int) MessageBox.Show("Ошибка ввода данных");
          this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object) this.edit_readed[e.RowIndex].cells[e.ColumnIndex].fieldval;
        }
        else
          this.edit_readed[e.RowIndex].cells[e.ColumnIndex].SetNewValue((string) this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
      }
      else
        this.edit_readed[e.RowIndex].cells[e.ColumnIndex].SetNewValue((string) this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
    }

    private void button_save_Click(object sender, EventArgs e)
    {
      if (this.FBConnector.DesignMode)
        return;
      if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        using (Stream stream = (Stream) new FileStream(this.saveFileDialog1.FileName, FileMode.Create))
        {
          using (StreamWriter streamWriter = new StreamWriter(stream))
          {
            string str1 = "";
            bool flag1 = true;
            foreach (TableColum colum in this.colums)
            {
              if (!flag1)
                str1 += ";";
              flag1 = false;
              str1 += colum.Name;
            }
            streamWriter.WriteLine(str1);
            foreach (RecipeLine recipeLine in this.edit_readed)
            {
              bool flag2 = true;
              string str2 = "";
              foreach (TCell cell in recipeLine.cells)
              {
                if (!flag2)
                  str2 += ";";
                flag2 = false;
                str2 += cell.fieldval;
              }
              streamWriter.WriteLine(str2);
            }
          }
        }
      }
      catch (Exception ex)
      {
        this.Message(ex.Message);
        return;
      }
      this.Message("Данные сохранены в файл " + this.saveFileDialog1.FileName);
    }

    private void button_del_Click(object sender, EventArgs e)
    {
      if (this.FBConnector.DesignMode || this._table_type == table_edit.view || this.dataGridView1.CurrentRow == null)
        return;
      this.edit_readed.RemoveAt(this.dataGridView1.CurrentRow.Index);
      this.dataGridView1.Rows.RemoveAt(this.dataGridView1.CurrentRow.Index);
      int num = 1;
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridView1.Rows)
      {
        row.HeaderCell.Value = (object) num.ToString();
        ++num;
      }
    }

    private void button_add_befor_Click(object sender, EventArgs e)
    {
      if (this.FBConnector.DesignMode || this._table_type == table_edit.view)
        return;
      if (this.edit_readed.Count == 0)
      {
        this.edit_readed.Add(new RecipeLine(this.colums));
        this.runtime_table_edit_init(this.edit_readed);
        this.dataGridView1.Rows[0].Selected = true;
        this.dataGridView1.Rows[0].Cells[0].Selected = true;
      }
      else
      {
        if (this.dataGridView1.CurrentRow == null)
          return;
        int index = this.dataGridView1.CurrentRow.Index;
        List<RecipeLine> recipeLineList = new List<RecipeLine>();
        int num = 0;
        foreach (RecipeLine recipeLine in this.edit_readed)
        {
          if (num == index)
            recipeLineList.Add(new RecipeLine(this.colums));
          ++num;
          recipeLineList.Add(recipeLine);
        }
        this.edit_readed.Clear();
        this.edit_readed = recipeLineList;
        this.runtime_table_edit_init(this.edit_readed);
        this.dataGridView1.Rows[index].Selected = true;
        this.dataGridView1.Rows[index].Cells[0].Selected = true;
      }
    }

    private void button_add_after_Click(object sender, EventArgs e)
    {
      if (this.FBConnector.DesignMode || this._table_type == table_edit.view)
        return;
      if (this.edit_readed.Count == 0)
      {
        this.edit_readed.Add(new RecipeLine(this.colums));
        this.runtime_table_edit_init(this.edit_readed);
        this.dataGridView1.Rows[0].Selected = true;
        this.dataGridView1.Rows[0].Cells[0].Selected = true;
      }
      else
      {
        if (this.dataGridView1.CurrentRow == null)
          return;
        int index = this.dataGridView1.CurrentRow.Index;
        int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
        List<RecipeLine> recipeLineList = new List<RecipeLine>();
        int num = 0;
        foreach (RecipeLine recipeLine in this.edit_readed)
        {
          recipeLineList.Add(recipeLine);
          if (num == index)
            recipeLineList.Add(new RecipeLine(this.colums));
          ++num;
        }
        this.edit_readed.Clear();
        this.edit_readed = recipeLineList;
        this.runtime_table_edit_init(this.edit_readed);
        this.dataGridView1.Rows[index + 1].Selected = true;
        this.dataGridView1.Rows[index + 1].Cells[0].Selected = true;
      }
    }
  }
}
