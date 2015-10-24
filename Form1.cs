using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string FilePath = "";
        //新建按钮点击事件
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            FilePath = "";
        }
        //打开文件对话框
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "*.txt|*.txt";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.richTextBox1.Text = "";
                string openPath = this.openFileDialog1.FileName;
                FilePath = openPath;
                if (this.自动换行ToolStripMenuItem.Checked == true)
                {
                    Read_line(openPath);
                }
                else
                {
                    Read_notline(openPath);
                }
                
            }
        }
        //读取文本文件 - 自动换行
        public void Read_line(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                this.richTextBox1.Text += line.ToString()+"\n";
            }
            sr.Close();
        }
        //读取文本文件 - 自动换行
        public void Read_notline(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                this.richTextBox1.Text += line.ToString();
            }
            sr.Close();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = "";
            if (!FilePath.Equals(""))
            {
                temp = richTextBox1.Text;
                if (!temp.Equals(""))
                {
                    writeToTxt(FilePath, temp);
                }
                this.toolStripStatusLabel3.Text = "已保存";
            }
            else
            {
                temp = richTextBox1.Text;
                this.saveFileDialog1.Filter = "*.txt|*.txt";
                string savePath = "";
                if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    savePath = this.saveFileDialog1.FileName;
                }
                if (temp != null && !(temp.Equals("")) && !(savePath.Equals("")))
                {
                    writeToTxt(savePath, temp);
                }
                this.toolStripStatusLabel3.Text = "已保存";
            }
           
          
        }
        //写入文本文件
        private void writeToTxt(string path,string values)
        {           
            System.IO.File.WriteAllText(path, values, Encoding.UTF8);         
        }
        //另存为
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = richTextBox1.Text;
            this.saveFileDialog1.Filter = "*.txt|*.txt";
            string savePath = "";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savePath = this.saveFileDialog1.FileName;
                FilePath = savePath;
            }
            if (temp != null && !(temp.Equals("")) && !(savePath.Equals("")))
            {
                writeToTxt(savePath, temp);
            }
        }
        //退出
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //自动换行
        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (自动换行ToolStripMenuItem.Checked == false)
            {
                this.richTextBox1.Text = "";
                if (!FilePath.Equals(""))
                {
                    Read_notline(FilePath);
                }
            }
            else
            {
                this.richTextBox1.Text = "";
                if (!FilePath.Equals(""))
                {
                    Read_line(FilePath);
                }
            }
                   
        }
        //获取光标的位置
        private void Ranks()
        {
            /*  得到光标行第一个字符的索引，
             *  即从第1个字符开始到光标行的第1个字符索引*/
            int index = richTextBox1.GetFirstCharIndexOfCurrentLine();
            /*  得到光标行的行号,第1行从0开始计算，习惯上我们是从1开始计算，所以+1。 */
            int line = richTextBox1.GetLineFromCharIndex(index) + 1;
            /*  SelectionStart得到光标所在位置的索引
             *  再减去
             *  当前行第一个字符的索引
             *  = 光标所在的列数(从0开始)  */
            int column = richTextBox1.SelectionStart - index + 1;
            /*  选择打印输出的控件  */
            this.toolStripStatusLabel1.Text=string.Format("第：{0}行 {1}列", line.ToString(), column.ToString());
        }
        //获取当前的日期
        private void getDate()
        {
            this.toolStripStatusLabel2.Text =DateTime.Now.ToLongDateString()+ DateTime.Now.ToLongTimeString();
        }
        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ranks();
        }
        int index = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Ranks();
            getDate();
            if (index++ % 3 == 0)
            {
                this.toolStripStatusLabel3.Text = "";
            }
            
        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.状态栏ToolStripMenuItem.Checked == true) //显示状态栏
            {
                this.statusStrip1.Visible = true;
            }
            else
            {
                this.statusStrip1.Visible = false;
            }
        }
        //改变字体（未实现）
        private void toolStripComboBox1_changed(object sender, EventArgs e)
        {
            this.richTextBox1.Font = new Font(this.toolStripComboBox1.SelectedItem.ToString(), Int32.Parse(this.toolStripComboBox2.SelectedItem.ToString()), FontStyle.Regular);
        }
        //改变字体大小
        private void FontSize_Changed(object sender, EventArgs e)
        {
            this.richTextBox1.Font = new Font(this.toolStripComboBox1.SelectedItem.ToString(), Int32.Parse(this.toolStripComboBox2.SelectedItem.ToString()), FontStyle.Regular);

        }
        //改变字体样式
        private void FontStyle_changed(object sender, EventArgs e)
        {
            if (this.toolStripComboBox3.SelectedItem.ToString().Equals("常规"))
            {
                this.richTextBox1.Font = new Font(this.toolStripComboBox1.SelectedItem.ToString(), Int32.Parse(this.toolStripComboBox2.SelectedItem.ToString()), FontStyle.Regular);
            }
            else if (this.toolStripComboBox3.SelectedItem.ToString().Equals("粗体"))
            {
                this.richTextBox1.Font = new Font(this.toolStripComboBox1.SelectedItem.ToString(), Int32.Parse(this.toolStripComboBox2.SelectedItem.ToString()), FontStyle.Bold);
            }
            else if (this.toolStripComboBox3.SelectedItem.ToString().Equals("斜体"))
            {
                this.richTextBox1.Font = new Font(this.toolStripComboBox1.SelectedItem.ToString(), Int32.Parse(this.toolStripComboBox2.SelectedItem.ToString()), FontStyle.Italic);
            }
            else if (this.toolStripComboBox3.SelectedItem.ToString().Equals("下划线"))
            {
                this.richTextBox1.Font = new Font(this.toolStripComboBox1.SelectedItem.ToString(), Int32.Parse(this.toolStripComboBox2.SelectedItem.ToString()), FontStyle.Underline);
            }   
          
        }
        //改变字体颜色
        private void TextColor_Changed(object sender, EventArgs e)
        {
            if (this.toolStripComboBox4.SelectedItem.ToString().Equals("黑"))
            {
                this.richTextBox1.ForeColor = Color.Black;
            }
            else if (this.toolStripComboBox4.SelectedItem.ToString().Equals("白"))
            {
                this.richTextBox1.ForeColor = Color.White;
            }
            else if (this.toolStripComboBox4.SelectedItem.ToString().Equals("红"))
            {
                this.richTextBox1.ForeColor = Color.Red;
            }
            else if (this.toolStripComboBox4.SelectedItem.ToString().Equals("黄"))
            {
                this.richTextBox1.ForeColor = Color.Yellow;
            }
            else if (this.toolStripComboBox4.SelectedItem.ToString().Equals("蓝"))
            {
                this.richTextBox1.ForeColor = Color.Blue;
            }
            else if (this.toolStripComboBox4.SelectedItem.ToString().Equals("绿"))
            {
                this.richTextBox1.ForeColor = Color.Green;
            }            
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By SoftStat");
        }
        //粘贴选项
        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // GetDataObject检索当前剪贴板上的数据
            IDataObject iData = Clipboard.GetDataObject();
            // 将数据与指定的格式进行匹配，返回bool
            if (iData.GetDataPresent(DataFormats.Text))
            {
                // GetData检索数据并指定一个格式
                this.richTextBox1.Text += (string)iData.GetData(DataFormats.Text);
            }
            else
            {
                MessageBox.Show("剪贴板中数据不可转换为文本", "错误");
            }
        }
    }
}
