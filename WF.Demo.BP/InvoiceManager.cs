using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WF.Common.Core;
using WF.Demo.BL;
using WF.Demo.Models;

namespace WF.Demo.BP
{
    public class InvoiceManager : WF.Common.Core.AbstractCommonBP
    {
        public InvoiceManager(WF.Common.Core.Session s) : base(s)
        {
        }

        public InvoiceManager(WF.Common.Core.AbstractProvider abstractProvider) : base(abstractProvider)
        {
        }

        #region Invoice
        /// <summary>
        /// 通过人员编号,和发票类别查询和是否财务组该人员的发票信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="type"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Connection]
        public List<Invoice> QueryInvoiceList(string employeeID, string type, bool isGroup)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();
            return invoiceLogic.QueryInvoiceList(employeeID, type, isGroup);
        }

        /// <summary>
        /// 通过和发票类别查询和是否财务组该人员的发票信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="type"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Connection]
        public List<Invoice> QueryInvoiceList(string type, bool isGroup)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();
            return invoiceLogic.QueryInvoiceList(type, isGroup);
        }

        /// <summary>
        /// 查询操作员所有分配的发票
        /// </summary>
        /// <param name="operID"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Connection]
        public List<Invoice> QueryInvoceListByOperID(string operID)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();
            return invoiceLogic.QueryInvoceListByOperID(operID);
        }

        /// <summary>
        /// 更新发票信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Transaction]
        public int UpdateInvoice(Invoice invoice)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();
            return invoiceLogic.Update(invoice);
        }

        /// <summary>
        /// 插入发票信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Transaction]
        public int InsertInvoice(Invoice invoice)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();
            return invoiceLogic.Insert(invoice);
        }

        /// <summary>
        /// 批量插入发票信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Transaction]
        public int InsertInvoiceList(List<Invoice> invoices)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();
            if (invoices == null || invoices.Count == 0)
            {
                return -1;
            }
            int param = 0;
            foreach (Invoice invoice in invoices)
            {
                if (string.IsNullOrEmpty(invoice.ID))
                {
                    invoice.ID = invoiceLogic.GetPrimaryKeyInt64().ToString();
                }
                param = invoiceLogic.Insert(invoice);

                if (param <= 0)
                {
                    return param;
                }
            }
            return param;
        }

        /// <summary>
        /// 删除发票信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Transaction]
        public int DeleteInvoice(Invoice invoice)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();
            return invoiceLogic.Delete(invoice.ID);
        }

        /// <summary>
        /// 检测所给的起始号和发票数量是否有效：

        /// </summary>
        /// <param name="startNO">起始号</param>
        /// <param name="endNO">发票数量</param>
        /// <param name="invoiceType">发票类型</param>
        /// <returns>有效true, 无效 false</returns>
        [WF.Common.Attribute.Connection]
        public bool InvoicesIsValid(string prefix, string startNO, string endNO, string invoiceType)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();

            if (WF.Common.Util.WConvert.ToInt64(endNO) < WF.Common.Util.WConvert.ToInt64(startNO))
            {
                this.ErrorText = "输入的终止号大于起始号!";

                return false;
            }

            string sql = string.Empty;

            List<Invoice> invoices = new List<Invoice>();

            invoices = invoiceLogic.QueryInvoiceList(invoiceType);

            //如果没有符合条件的发票,说明可以生成
            if (invoices == null)
            {
                return true;
            }

            for (int i = 0; i < invoices.Count; i++)
            {

                Invoice invoice = invoices[i] as Invoice;

                string curPrefix = Regex.Replace(invoice.BeginNO, "[0-9]*$", "", RegexOptions.IgnoreCase);

                if (prefix == curPrefix && WF.Common.Util.StringUtil.GetNumber(invoice.BeginNO) <= WF.Common.Util.WConvert.ToInt64(startNO) && WF.Common.Util.WConvert.ToInt64(startNO) <= WF.Common.Util.StringUtil.GetNumber(invoice.EndNO))
                {
                    return false;
                }
                if (prefix == curPrefix && WF.Common.Util.StringUtil.GetNumber(invoice.BeginNO) <= WF.Common.Util.WConvert.ToInt64(endNO) && WF.Common.Util.WConvert.ToInt64(endNO) <= WF.Common.Util.StringUtil.GetNumber(invoice.EndNO))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 更新收费员发票
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Connection]
        public int UpdateInvoiceDefaultState(Invoice invoice)
        {
            InvoiceLogic invoiceLogic = this.AbstractProvider.GetAbstractCommonBL<InvoiceLogic>();

            return invoiceLogic.UpdateInvoiceDefaultState(invoice);
        }
        #endregion
    }
    /*public class RegisterManager : WF.Common.Core.AbstractCommonBP
    {
        public RegisterManager(Session session) : base(session)
        {
        }

        public RegisterManager(AbstractProvider abstractProvider) : base(abstractProvider)
        {
        }


        [WF.Common.Attribute.Connection]
        public List<Register> QueryRegisterList(string p_id)
        {
            RegisterLogic registerLogic = this.AbstractProvider.GetAbstractCommonBL<RegisterLogic>();
            return registerLogic.QueryRegisterList(p_id);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Transaction]
        public int UpdateRegister(Register register)
        {
            RegisterLogic registerLogic = this.AbstractProvider.GetAbstractCommonBL<RegisterLogic>();
            return registerLogic.Update(register);
        }

        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Transaction]
        public int InsertRegister(Register register)
        {
            RegisterLogic registerLogic = this.AbstractProvider.GetAbstractCommonBL<RegisterLogic>();
            return registerLogic.Insert(register);
        }

        /// <summary>
        /// 删除发票信息
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        [WF.Common.Attribute.Transaction]
        public int DeleteRegister(Register register)
        {
            RegisterLogic registerLogic = this.AbstractProvider.GetAbstractCommonBL<RegisterLogic>();
            return registerLogic.Delete(register.ID);
        }
    }*/
}
