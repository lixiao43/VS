using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using WF.Demo.BP;
using WF.Demo.Models;

namespace FirstPractice
{
    //interface INter
    //{
    //    void Adddata();
    //    void Updatedata();
    //    void QueryBtn();
    //    void SaveBtn();
    //}

    public partial class Form1 : Form
    {
        RegisterManager registerManager;
        /// <summary>
        /// 数据操作
        /// </summary>
        private RegisterManager RegisterManager
        {
            get
            {
                if (registerManager == null)
                {
                    registerManager = WF.Common.Core.Application.Instance.CreateCommonBP<RegisterManager>();
                }
                return registerManager;
            }
        }
        List<Register> list = new List<Register>();
        static int h_id;
        static int h_count;
        static int i = 0;
        TreeNode tmp = new TreeNode("刚刚登记的患者");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryBtn();
        }
        public void QueryBtn()
        {
            string id = Pid_TBox.Text;
            if (id.Equals(""))
            {
                MessageBox.Show("查询时住院号不能为空");
            }
            else
            {
                var results = RegisterManager.QueryRegisterList(id);
                if (results.Count == 0)
                {
                    MessageBox.Show("查询时住院号不存在");
                }
                else
                {
                    foreach (Register m in results)
                    {
                        Settext(m);
                    }
                }
                //MessageBox.Show(string.Format("获取到了{0}条记录", results.Count));
            }
        }
        private void Settext(Register m)
        {
            name_TBox.Text = m.Patient_Name;
            sex_CBox.Text = m.Patient_Sex;
            count_TBox.Text = m.Number;
            pid_TBox2.Text = m.Patient_id;
            id_TBox.Text = m.Id_Number;
            dateTimePicker1.Text = m.Date_Of_Birth;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveBtn();  
        }
        public void SaveBtn()
        {
            if (!pid_TBox2.Text.Equals(""))
            {
                Register up = new Register();
                up.Patient_id = pid_TBox2.Text; 
                up.Patient_Name = name_TBox.Text;
                up.Patient_Sex = sex_CBox.Text;
                up.Number = count_TBox.Text;
                up.Id_Number = id_TBox.Text;
                up.Date_Of_Birth = dateTimePicker1.Value.Date.ToLongDateString();
                Updatedata(up);
                MessageBox.Show("修改成功");
            }
            else
            {
                bool flag = true;
                string id_number = id_TBox.Text;
                string name = name_TBox.Text;
                var results = RegisterManager.QueryRegisterListNumber();
                h_id = results.Count() + 1;
                h_count = 1;
                foreach (var register in results)
                {
                    h_count = int.Parse(register.Number);
                    if (register.Id_Number == id_number)
                    {

                        h_count += 1;
                        flag = false;
                        register.Number = h_count.ToString();
                        RegisterManager.UpdateRegister(register);
                        MessageBox.Show(string.Format("已存在住院号，登记成功，住院次数为{0}", h_count.ToString()));
                        treeView1.SelectedNode = tmp;
                        treeView1.SelectedNode.Nodes.Add(register.Patient_Name + "【" + register.Patient_id + "】");
                        treeView1.ExpandAll();
                        break;
                    }
                }
                if (flag == true)
                {
                    Adddata();
                }
            }
            SetFpspread();
        }
        public void Adddata()
        {
            h_count = 1;
            String pnum = h_id.ToString().PadLeft(6, '0');
            pnum = "1" + pnum;
            string name = name_TBox.Text;
            string sex = sex_CBox.Text;
            string time= dateTimePicker1.Value.Date.ToLongDateString();
            //string count = textBox26.Text;
            string id_number = id_TBox.Text;
            Register m = new Register();
            m.Patient_id = pnum;
            m.Patient_Name = name;
            m.Patient_Sex = sex;
            m.Number = h_count.ToString();
            m.Id_Number = id_number;
            m.Date_Of_Birth = time;
            //SetFpspread(m);
            //list.Add(m);
            MessageBox.Show("登记成功,您的住院号是：" +pnum);
            treeView1.SelectedNode = tmp;
            //TreeView tv = new TreeView();
            TreeNode node = new TreeNode(m.Patient_Name + "【" + m.Patient_id + "】");
            treeView1.SelectedNode.Nodes.Add(node);
            treeView1.ExpandAll();

            RegisterManager.InsertRegister(m);
            h_id++;
        }
        public void Updatedata(Register up)
        {
            RegisterManager.UpdateRegister(up);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var results = RegisterManager.QueryRegisterListNumber();
            ////foreach(Register m in results)
            ////{
            ////    MessageBox.Show(m.Patient_Name);
            ////}
            MessageBox.Show(string.Format("获取到了{0}条记录", results.Count));
            //var results1 = RegisterManager.QueryRegisterListSex("男");
            //var results2 = RegisterManager.QueryRegisterListSex("女");
            //h_id = results1.Count() + results2.Count() + 1;
            //MessageBox.Show(h_id.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            name_TBox.Text = "";
            sex_CBox.Text = "";
            count_TBox.Text = "";
            Pid_TBox.Text = "";
            pid_TBox2.Text = "";
            id_TBox.Text = "";
            dateTimePicker1.MinDate = DateTime.MinValue;
            dateTimePicker1.Text = dateTimePicker1.MinDate.ToLongDateString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Add(tmp);
            treeView1.SelectedNode = tmp;


            //FarPoint.Win.Spread.DefaultSkins.Colorful4.
            //Apply(fpSpread1.Sheets[0]);
            //fpSpread1.Sheets[0].RowHeaderVisible = false;


            dateTimePicker1.MinDate = DateTime.MinValue;
            dateTimePicker1.Text = dateTimePicker1.MinDate.ToLongDateString();
        }

        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //FarPoint.Win.Spread.SheetView newsheet = new
            //FarPoint.Win.Spread.SheetView();
            //newsheet.SheetName = "North";
            //newsheet.ColumnCount = 3;
            //newsheet.RowCount = 5;
            //// Add the new sheet to the control
            //fpSpread1.Sheets.Add(newsheet);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(tmp.GetNodeCount(false).ToString());

            for (int i=0;i< tmp.GetNodeCount(false);i++)
            {
                UpdateFpspread(i);
            }
            MessageBox.Show("修改成功");
            //FarPoint.Win.Spread.SheetView newsheet = new
            //FarPoint.Win.Spread.SheetView();
            //newsheet.SheetName = "North";
            //newsheet.ColumnCount = 3;
            //newsheet.RowCount = 5;
            //// Add the new sheet to the control
            //fpSpread1.Sheets.Add(newsheet);
        }
        private void SetFpspread()
        {

            i = 0;
            foreach (TreeNode n in tmp.Nodes)
            {
                string[] sarray = n.Text.Split('【', '】');
                var results2 = RegisterManager.QueryRegisterList(sarray[1]);
                foreach (var mm in results2)
                {
                    fpSpread1.Sheets[0].Cells[i, 0].Text = mm.Patient_id;
                    fpSpread1.Sheets[0].Cells[i, 1].Text = mm.Patient_Name;
                    fpSpread1.Sheets[0].Cells[i, 2].Text = mm.Patient_Sex;
                    fpSpread1.Sheets[0].Cells[i, 3].Text = mm.Number;
                    fpSpread1.Sheets[0].Cells[i, 4].Text = mm.Id_Number;
                    fpSpread1.Sheets[0].Cells[i, 5].Text = mm.Date_Of_Birth;
                    i++;
                }
            }
        }
        private void UpdateFpspread(int i)
        {
            Register up = new Register();
            up.Patient_id = fpSpread1.Sheets[0].Cells[i, 0].Text; 
            up.Patient_Name = fpSpread1.Sheets[0].Cells[i, 1].Text;
            up.Patient_Sex = fpSpread1.Sheets[0].Cells[i, 2].Text;
            up.Number = fpSpread1.Sheets[0].Cells[i, 3].Text;
            up.Id_Number = fpSpread1.Sheets[0].Cells[i, 4].Text;
            up.Date_Of_Birth = fpSpread1.Sheets[0].Cells[i, 5].Text;
            Updatedata(up);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text != "刚刚登记的患者")
            {
                string[] sarray = e.Node.Text.Split('【', '】');
                var results = RegisterManager.QueryRegisterList(sarray[1]);
                foreach (var m in results)
                {
                    Settext(m);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int a = fpSpread1.ActiveSheet.ActiveCell.Row.Index;
            bool flag = false;
            for(int i=0; i<fpSpread1.ActiveSheet.RowCount;i++)
            {               
                if (i!= a && fpSpread1.ActiveSheet.Cells[i,0].Text.ToString()== fpSpread1.ActiveSheet.Cells[a, 0].Text.ToString())
                {
                    flag = true;
           
                    string[] sarray = tmp.Nodes[a].Text.Split('【', '】');
                    var results = RegisterManager.QueryRegisterList(sarray[1]);
                    results[0].Number = (int.Parse(results[0].Number) - 1).ToString();
                    Updatedata(results[0]);
                    tmp.Nodes[a].Remove();
                    SetFpspread();
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            if(flag==false)
            {
                string[] sarray = tmp.Nodes[a].Text.Split('【', '】');
                var results = RegisterManager.QueryRegisterList(sarray[1]);
                RegisterManager.DeleteRegister(results[0]);
                tmp.Nodes[a].Remove();

            }
            fpSpread1.Sheets[0].RemoveRows(a, 1);
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {

        }
    }
}
