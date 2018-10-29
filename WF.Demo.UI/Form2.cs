using DevExpress.Spreadsheet;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WF.Demo.BP;
using WF.Demo.Models;

namespace WF.Demo.UI
{
    public partial class Form2 : Form,Interface1<Register>
    {
        RegisterManager registerManager;
        List<Register> list = new List<Register>();
        TreeListNode parentnode;
        Worksheet worksheet;
        static int h_id;
        static int h_count;
        static int i = 0;
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

        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            PatientSex.Properties.Items.Add("男");
            PatientSex.Properties.Items.Add("女");
            parentnode = treeList1.AppendNode(null, null);
            parentnode.SetValue(treeList1.Columns[0], "刚登记的患者");
            //spreadsheetControl1.ReadOnly = false;
            worksheet = spreadsheetControl1.ActiveWorksheet;
            worksheet.Columns["A"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["A"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            worksheet.Columns["B"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["B"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            worksheet.Columns["C"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["C"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            worksheet.Columns["D"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["D"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            worksheet.Columns["E"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["E"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            worksheet.Columns["F"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            worksheet.Columns["F"].Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            worksheet.Columns["A"].ColumnWidth = 300;
            worksheet.Columns["B"].ColumnWidth = 300;
            worksheet.Columns["E"].ColumnWidth = 500;
            worksheet.Columns["F"].ColumnWidth = 500;

            worksheet.Cells[0, 0].Value = "住院号";
            worksheet.Cells[0, 1].Value = "姓名";
            worksheet.Cells[0, 2].Value = "性别";
            worksheet.Cells[0, 3].Value = "住院次数";
            worksheet.Cells[0, 4].Value = "身份证号";
            worksheet.Cells[0, 5].Value = "出生年月";
            //worksheet.Rows.Remove(2);
        }
        public void Updatedata(Register up)
        {
            RegisterManager.UpdateRegister(up);
        }
        public void Adddata(Register m)
        {
            RegisterManager.InsertRegister(m);
        }
        public void Deletedata(Register m)
        {
            RegisterManager.DeleteRegister(m);
        }
        private Register Assignment()
        {
            Register up = new Register();
            up.Patient_id = RegisterID2.Text;
            up.Patient_Name = PatientName.Text;
            up.Patient_Sex = PatientSex.Text;
            up.Number = RegisterCount.Text;
            up.Id_Number = IDNumber.Text;
            up.Date_Of_Birth = DateOfBirth.Text;
            return up;
        }

        public void Adddata()
        {
            h_count = 1;
            Register m = new Register();
            m.Patient_id = h_id.ToString();
            m.Patient_Name = PatientName.Text;
            m.Patient_Sex = PatientSex.Text;
            m.Number = h_count.ToString();
            m.Id_Number = IDNumber.Text;
            m.Date_Of_Birth = DateOfBirth.Text;

            MessageBox.Show("登记成功,您的住院号是：" + h_id.ToString());
            Adddata(m);
            parentnode.Nodes.Add().SetValue(treeList1.Columns[0], m.Patient_Name + "【" + m.Patient_id + "】");
            parentnode.ExpandAll();
        }
        public void SetSpreadsheet()
        {
            i = 1;
            foreach(TreeListNode node in parentnode.Nodes)
            {
                string[] sarray = node.GetDisplayText(treeList1.Columns[0]).Split('【', '】');
                var results2 = RegisterManager.QueryRegisterList(sarray[1]);
                foreach(var mm in results2)
                {
                    worksheet.Cells[i, 0].Value = mm.Patient_id;
                    worksheet.Cells[i, 1].Value = mm.Patient_Name;
                    worksheet.Cells[i, 2].Value = mm.Patient_Sex;
                    worksheet.Cells[i, 3].Value = mm.Number;
                    worksheet.Cells[i, 4].Value = mm.Id_Number;
                    worksheet.Cells[i, 5].Value = mm.Date_Of_Birth;
                    i++;
                }
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!RegisterID2.Text.Equals(""))
            {
                Register up = new Register();
                up.Patient_id = RegisterID2.Text;
                up.Patient_Name = PatientName.Text;
                up.Patient_Sex = PatientSex.Text;
                up.Number = RegisterCount.Text;
                up.Id_Number = IDNumber.Text;
                up.Date_Of_Birth = DateOfBirth.Text;
                Updatedata(up);
                MessageBox.Show("修改成功");
            }
            else
            {
                bool flag = true;
                string id_number = IDNumber.Text;
                //string name = PatientName.Text;
                var results = RegisterManager.QueryRegisterListNumber();
                int max = 100000;
                foreach(var result in results)
                {
                    if(int.Parse(result.Patient_id)>=max)
                    {
                        max = int.Parse(result.Patient_id);
                    }
                }
                h_id = max;
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
                        parentnode.RootNode.Nodes.Add().SetValue(treeList1.Columns[0], register.Patient_Name + "【" + register.Patient_id + "】");
                        parentnode.ExpandAll();
                        break;
                    }
                }
                if (flag == true)
                {
                    h_id += 1;
                    Adddata();
                }
            }
            SetSpreadsheet();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string id = RegisterID.Text;
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
                        PatientName.Text = m.Patient_Name;
                        PatientSex.Text = m.Patient_Sex;
                        RegisterCount.Text = m.Number;
                        RegisterID2.Text = m.Patient_id;
                        IDNumber.Text = m.Id_Number;
                        DateOfBirth.Text = m.Date_Of_Birth;
                    }
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            PatientName.Text = "";
            PatientSex.Text = "";
            RegisterID.Text = "";
            RegisterID2.Text = "";
            RegisterCount.Text = "";
            DateOfBirth.Text = "";
            IDNumber.Text = "";
        }

        private void treeList1_AfterFocusNode(object sender, NodeEventArgs e)
        {
            if (e.Node.GetDisplayText(treeList1.Columns[0]).ToString()!="刚登记的患者")
            {
                string[] sarray = e.Node.GetDisplayText(treeList1.Columns[0]).ToString().Split('【', '】');
                var results = RegisterManager.QueryRegisterList(sarray[1]);
                foreach (var m in results)
                {
                    PatientName.Text = m.Patient_Name;
                    PatientSex.Text = m.Patient_Sex;
                    RegisterCount.Text = m.Number;
                    RegisterID2.Text = m.Patient_id;
                    IDNumber.Text = m.Id_Number;
                    DateOfBirth.Text = m.Date_Of_Birth;
                }
            }

        }
        private void spreadsheetControl1_RowsRemoved(object sender, RowsChangedEventArgs e)
        {
            int a = e.StartIndex;
            string[] sarray = parentnode.Nodes[a - 1].GetDisplayText(treeList1.Columns[0]).Split('【', '】');
            var results = RegisterManager.QueryRegisterList(sarray[1]);
            if(int.Parse(results[0].Number)>1)
            {
                results[0].Number = (int.Parse(results[0].Number) - 1).ToString();
                Updatedata(results[0]);
            }
            else
            {
                Deletedata(results[0]);
            }
            parentnode.Nodes.Remove(parentnode.Nodes[a-1]);
            SetSpreadsheet();

        }

        private void spreadsheetControl1_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {
            for (int i = 1; i <= parentnode.Nodes.Count; i++)
            {
                Register up = new Register();
                up.Patient_id = worksheet.GetCellValue(0, i).ToString();
                up.Patient_Name = worksheet.GetCellValue(1, i).ToString(); ;
                up.Patient_Sex = worksheet.GetCellValue(2, i).ToString();
                up.Number = worksheet.GetCellValue(3, i).ToString();
                up.Id_Number = worksheet.GetCellValue(4, i).ToString();
                up.Date_Of_Birth = worksheet.GetCellValue(5, i).ToString();
                Updatedata(up);
            }
            MessageBox.Show("修改成功");
        }
    }

}
