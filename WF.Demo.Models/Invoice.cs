using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Demo.Models
{
    /// <summary>
    /// ********************************************************
    /// 功能描述：发票信息
    /// 创建日期：2014-12-26
    /// 创 建 人：Zhaojing
    /// 修改日期：
    /// 修 改 人：
    /// 修改内容：
    /// ********************************************************
    /// </summary>
    [System.Serializable]
    public class Invoice : WF.Common.Base.BaseObject
    {
        /// <summary>
        /// 发票类型
        /// </summary>
        private WF.Common.Base.BaseObject invoiceKind;

        /// <summary>
        /// 发票类型
        /// </summary>
        public WF.Common.Base.BaseObject InvoiceKind
        {
            get
            {
                if (this.invoiceKind == null)
                {
                    this.invoiceKind = new WF.Common.Base.BaseObject();
                }
                return this.invoiceKind;
            }
            set
            {
                this.invoiceKind = value;
            }
        }

        /// <summary>
        /// 数据状态
        /// </summary>
        private string dataState;

        /// <summary>
        /// 数据状态
        /// </summary>
        public string DataState
        {
            get
            {
                if (this.dataState == null)
                {
                    this.dataState = string.Empty;
                }
                return this.dataState;
            }
            set
            {
                this.dataState = value;
            }
        }

        /// <summary>
        /// 获得发票的操作员信息
        /// </summary>
        private WF.Common.Base.BaseObject useOper;

        /// <summary>
        /// 获得发票的操作员信息
        /// </summary>
        public WF.Common.Base.BaseObject UseOper
        {
            get
            {
                if (this.useOper == null)
                {
                    this.useOper = new WF.Common.Base.BaseObject();
                }
                return this.useOper;
            }
            set
            {
                this.useOper = value;
            }
        }

        /// <summary>
        /// 领取时间
        /// </summary>
        private DateTime useTime;

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime UseTime
        {
            get
            {
                return this.useTime;
            }
            set
            {
                this.useTime = value;
            }
        }

        /// <summary>
        /// 发票起始号
        /// </summary>
        private string beginNO;

        /// <summary>
        /// 发票起始号
        /// </summary>
        public string BeginNO
        {
            get
            {
                if (this.beginNO == null)
                {
                    this.beginNO = string.Empty;
                }
                return this.beginNO;
            }
            set
            {
                this.beginNO = value;
            }
        }

        /// <summary>
        /// 发票中止号
        /// </summary>
        private string endNO;

        /// <summary>
        /// 发票中止号
        /// </summary>
        public string EndNO
        {
            get
            {
                if (this.endNO == null)
                {
                    this.endNO = string.Empty;
                }
                return this.endNO;
            }
            set
            {
                this.endNO = value;
            }
        }

        /// <summary>
        /// 当前使用号
        /// </summary>
        private string usedNO;

        /// <summary>
        /// 当前使用号
        /// </summary>
        public string UsedNO
        {
            get
            {
                if (this.usedNO == null)
                {
                    this.usedNO = string.Empty;
                }
                return this.usedNO;
            }
            set
            {
                this.usedNO = value;
            }
        }

        /// <summary>
        /// 发票数目
        /// </summary>
        private int qty;

        /// <summary>
        /// 发票数目
        /// </summary>
        public int Qty
        {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
            }
        }

        /// <summary>
        /// 是否公用
        /// </summary>
        private bool isPublic;

        /// <summary>
        /// 是否公用
        /// </summary>
        public bool IsPublic
        {
            get
            {
                return this.isPublic;
            }
            set
            {
                this.isPublic = value;
            }
        }

        /// <summary>
        /// 操作信息
        /// </summary>
        private WF.Common.Base.Operation oper;

        /// <summary>
        /// 操作信息
        /// </summary>
        public WF.Common.Base.Operation Oper
        {
            get
            {
                if (this.oper == null)
                {
                    this.oper = new WF.Common.Base.Operation();
                }
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }

        private string preFix;
        /// <summary>
        /// 发票前缀
        /// </summary>
        public string PreFix
        {
            get
            {
                if (preFix == null)
                {
                    preFix = string.Empty;
                }
                return this.preFix;
            }
            set
            {
                this.preFix = value;
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new Invoice Clone()
        {
            Invoice Invoice = base.Clone() as Invoice;
            Invoice.InvoiceKind = this.InvoiceKind.Clone();
            Invoice.UseOper = this.UseOper.Clone();
            Invoice.Oper = this.Oper.Clone();
            return Invoice;
        }
    }
    /*public class Register: WF.Common.Base.BaseObject
    {
        private int id;
        /// <summary>
        /// 住院号
        /// </summary>
        public int Patient_id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private string number;
        /// <summary>
        /// 住院次数
        /// </summary>
        public string Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
            }
        }

        private string name;
        /// <summary>
        ///病人姓名
        /// </summary>
        public string Patient_Name
        {
            get
            {
                if (this.name == null)
                {
                    this.name = string.Empty;
                }
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        private string sex;
        /// <summary>
        ///病人姓名
        /// </summary>
        public string Patient_Sex
        {
            get
            {
                if (this.sex == null)
                {
                    this.sex = string.Empty;
                }
                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public new Invoice Clone()
        {
            Invoice Invoice = base.Clone() as Invoice;
            Invoice.InvoiceKind = this.InvoiceKind.Clone();
            Invoice.UseOper = this.UseOper.Clone();
            Invoice.Oper = this.Oper.Clone();
            return Invoice;
        }
    }*/
}
