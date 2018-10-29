using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using System.Xml;
namespace FirstPractice
{
    public partial class Form1 : Form
    {
        static System.Data.DataTable dt;
        static int cl;
        static int ID;
        static int i=0;
        int dtcl;
        int j = 0;
        static int h_id = 0;
        string q,q1,q2,q3;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;

            cl = dt.Rows.Count;
            ID = int.Parse(id);


            if (id.Equals(""))
            {
                MessageBox.Show("查询时住院号不能为空");
            }
            else
            {

                if (cl < ID + 1)
                    MessageBox.Show("查询的住院号不存在");
                else
                {
                    try
                    {
                        string m = dt.Rows[ID]["ID"].ToString();
                        string m2 = dt.Rows[ID]["name"].ToString();
                        String m3 = dt.Rows[ID]["sex"].ToString();
                        String m4 = dt.Rows[ID]["Count"].ToString();
                        textBox1.Text = m;
                        textBox2.Text = m2;
                        comboBox1.Text = m3;
                        textBox5.Text = m;
                        textBox26.Text = m4;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //saveTodatatable(h_id);
            if (!textBox5.Text.Equals(""))
            {
                update();
            }
            else {
                saveTodatatable(h_id);
                h_id += 1;
            }
                

        }

        private void saveTodatatable(int h_id)
        {
            string name = textBox2.Text;
            string sex = comboBox1.Text;
            string count = textBox26.Text;
            if (i==0)
            {
                dt = new System.Data.DataTable();
                DataColumn dc = null;
                dc = dt.Columns.Add("ID", Type.GetType("System.Int32"));

                dc.AutoIncrement = true;//自动增加
                dc.AutoIncrementSeed = 1;//起始为1
                dc.AutoIncrementStep = 1;//步长为1
                dc.AllowDBNull = false;
                dc = dt.Columns.Add("Name", Type.GetType("System.String"));
                dc = dt.Columns.Add("Sex", Type.GetType("System.String"));
                dc = dt.Columns.Add("Count", Type.GetType("System.String"));
                i++;
            }


            DataRow newRow;
            newRow = dt.NewRow();
            newRow["ID"] = h_id;
            newRow["Name"] = name;
            newRow["Sex"] = sex;
            newRow["Count"] = count;
            dt.Rows.Add(newRow);
            MessageBox.Show("登记成功,您的住院号是：" + h_id);
            CreateXml();


        }
        private void update()
        {
            int Id = int.Parse(textBox5.Text);
            string n1= dt.Rows[Id]["name"].ToString();
            dt.Rows[Id]["name"] = textBox2.Text;
            String n2 = dt.Rows[Id]["sex"].ToString();
            dt.Rows[Id]["sex"] = comboBox1.Text;
            String n3 = dt.Rows[Id]["Count"].ToString();
            dt.Rows[Id]["Count"] = textBox26.Text;
            MessageBox.Show("修改成功!");
        }
        public void CreateXml()
        {
            //创建xml
            //string Name = DateTime.Now.ToString("yyyymmddhhmmss");
            //XmlTextWriter writer = new XmlTextWriter("xml/" + Name + ".xml", null);
            XmlTextWriter writer = new XmlTextWriter("hhh.xml", null);
            //使用自动缩进便于阅读
            writer.Formatting = Formatting.Indented;
            //写入根目录
            writer.WriteStartElement("items");
            writer.WriteStartElement("item");
            //写入属性及属性名字
            dtcl = dt.Rows.Count;
            for(j=0;j<dtcl;j++)
            {
                q = dt.Rows[j]["ID"].ToString();
                q1 = dt.Rows[j]["name"].ToString();
                q2 = dt.Rows[j]["sex"].ToString();
                q3 = dt.Rows[j]["Count"].ToString();
                //writer.WriteAttributeString("品质", "文学");
                //加入子元素
                writer.WriteElementString("ID", q);
                writer.WriteElementString("Name", q1);
                writer.WriteElementString("Sex", q2);
                writer.WriteElementString("Count", q3);
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            //将XML写入文件并且关闭XmlTextWriter
            writer.Close();

        }
        private void readxml()
        {
            System.Data.DataTable dt2 = new System.Data.DataTable();
            XmlDocument xmlDoc = new XmlDocument();
            //加载指定xml文件
            xmlDoc.Load(@"C:\Users\lixiao\source\repos\FirstPractice\FirstPractice\bin\Debug\hhh.xml");
            //查找第一个节点
            XmlNode xn = xmlDoc.SelectSingleNode("items");
            //查找该节点下所有子节点
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode item in xnl)
            {
                XmlElement xe = (XmlElement)item;
               //Console.WriteLine(xe.GetAttribute("类别"));//显示属性值
               // Console.WriteLine(xe.GetAttribute("品质"));
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    MessageBox.Show(xn2.InnerText);//显示子节点点文本
                }
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox26.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";

        }
    }
}
