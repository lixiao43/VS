using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WF.Common.Core;
using WF.Common.Util;
using WF.Demo.DAO;
using WF.Demo.Models;

namespace WF.Demo.BL
{
    /// <summary>
    /// ********************************************************
    /// 功能描述：发票类管理
    /// 创建日期：2015-03-23 12:31:21
    /// 创 建 人：caolin
    /// 修改日期：
    /// 修 改 人：
    /// 修改内容：
    /// ********************************************************
    /// </summary>
    public class InvoiceLogic : AbstractCommonBL<Invoice, BD_EXP_INVOICE>
    {
        #region Convert

        /// <summary>
        /// WF.Expense.Models.Common.Invoice转换成WF.Expense.Dao.Common.BD_EXP_INVOICE
        /// </summary>
        /// <param name="model">参数对象WF.Expense.Models.Common.Invoice</param>
        /// <returns>返回对象WF.Expense.Dao.Common.BD_EXP_INVOICE</returns>
        protected override BD_EXP_INVOICE ConvertToDao(Invoice model)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.INVOICE_ID.Value = model.ID;
            dao.USE_TIME.Value = model.UseTime;
            dao.USE_OPER_ID.Value = model.UseOper.ID;
            dao.INVOICE_TYPE_ID.Value = model.InvoiceKind.ID;
            dao.INVOICE_TYPE_NAME.Value = model.InvoiceKind.Name;
            dao.INVOICE_PREFIX.Value = Regex.Replace(model.BeginNO, "[0-9]", "", RegexOptions.IgnoreCase);
            dao.START_NUMBER.Value = Regex.Replace(model.BeginNO, "[a-z][A-Z]", "", RegexOptions.IgnoreCase);
            dao.END_NUMBER.Value = Regex.Replace(model.EndNO, "[a-z][A-Z]", "", RegexOptions.IgnoreCase);
            dao.USED_NUMBER.Value = Regex.Replace(model.UsedNO, "[a-z][A-Z]", "", RegexOptions.IgnoreCase);
            dao.DATA_STATE.Value = model.DataState;
            dao.PUB_FLAG.Value = WF.Common.Util.WConvert.ToInt32(model.IsPublic);
            dao.OPER_ID.Value = model.Oper.Oper.ID;
            dao.OPER_TIME.Value = model.Oper.Time;
            return dao;
        }

        /// <summary>
        /// WF.Expense.Dao.Common.BD_EXP_INVOICE转换成WF.Expense.Models.Common.Invoice
        /// </summary>
        /// <param name="dao">参数对象WF.Expense.Dao.Common.BD_EXP_INVOICE</param>
        /// <returns>返回对象WF.Expense.Models.Common.Invoice</returns>
        protected override Invoice ConvertToModel(BD_EXP_INVOICE dao)
        {
            Invoice model = new Invoice();
            model.ID = dao.INVOICE_ID.Value.ToString();
            model.UseTime = WConvert.ToDateTime(dao.USE_TIME.Value);
            model.UseOper.ID = dao.USE_OPER_ID.Value.ToString();
            model.InvoiceKind.ID = dao.INVOICE_TYPE_ID.Value.ToString();
            model.InvoiceKind.Name = dao.INVOICE_TYPE_NAME.Value.ToString();
            model.PreFix = dao.INVOICE_PREFIX.Value.ToString();
            model.BeginNO = dao.INVOICE_PREFIX.Value + dao.START_NUMBER.Value.ToString();
            model.EndNO = dao.INVOICE_PREFIX.Value + dao.END_NUMBER.Value.ToString();
            model.UsedNO = dao.INVOICE_PREFIX.Value + dao.USED_NUMBER.Value.ToString();
            model.DataState = dao.DATA_STATE.Value.ToString();
            model.IsPublic = WConvert.ToBoolean(dao.PUB_FLAG.Value);
            model.Oper.Oper.ID = dao.OPER_ID.Value.ToString();
            model.Oper.Time = WConvert.ToDateTime(dao.OPER_TIME.Value);
            return model;
        }

        #endregion

        #region Query/Update/Insert/Delete
        /// <summary>
        /// 通过人员编号,和发票类别查询和是否财务组该人员的发票信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="type"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public List<Invoice> QueryInvoiceList(string employeeID, string type, bool isGroup)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.USE_OPER_ID.Value = employeeID;
            dao.INVOICE_TYPE_ID.Value = type;
            dao.PUB_FLAG.Value = WF.Common.Util.WConvert.ToInt32(isGroup);
            return this.QueryList(new WF.Common.Sql.Field[] { dao.USE_OPER_ID, dao.INVOICE_TYPE_ID, dao.PUB_FLAG });
        }

        /// <summary>
        /// 通过发票类型 是否财务组查询该人员的发票信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public List<Invoice> QueryInvoiceList(string type, bool isGroup)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.INVOICE_TYPE_ID.Value = type;
            dao.PUB_FLAG.Value = WF.Common.Util.WConvert.ToInt32(isGroup);
            return this.QueryList(new WF.Common.Sql.Field[] { dao.INVOICE_TYPE_ID, dao.PUB_FLAG });
        }

        /// <summary>
        /// 通过发票类型查询发票信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public List<Invoice> QueryInvoiceList(string type)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.INVOICE_TYPE_ID.Value = type;
            return this.QueryList(new WF.Common.Sql.Field[] { dao.INVOICE_TYPE_ID });
        }

        /// <summary>
        /// 查询操作员所有分配的发票
        /// </summary>
        /// <param name="operID"></param>
        /// <returns></returns>
        public List<Invoice> QueryInvoceListByOperID(string operID)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.USE_OPER_ID.Value = operID;
            return this.QueryList(new WF.Common.Sql.Field[] { dao.USE_OPER_ID });
        }

        /// <summary>
        /// 根据操作员、发票类型更新已用号
        /// </summary>
        /// <param name="newUseNumber"></param>
        /// <param name="useOpeID"></param>
        /// <param name="invoiceTypeID"></param>
        /// <returns></returns>
        public int UpdateUsedNO(string newUseNumber, string useOpeID, string invoiceTypeID)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.USED_NUMBER.Value = newUseNumber;
            dao.USE_OPER_ID.Value = useOpeID;
            dao.INVOICE_TYPE_ID.Value = invoiceTypeID;
            dao.DATA_STATE.Value = "1";
            return dao[this].Update(new WF.Common.Sql.Field[] { dao.USED_NUMBER }, new WF.Common.Sql.Field[] { dao.USE_OPER_ID, dao.INVOICE_TYPE_ID, dao.DATA_STATE });
        }

        /// <summary>
        /// 更新收费员发票
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int UpdateInvoiceDefaultState(Invoice invoice)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.USE_OPER_ID.Value = invoice.UseOper.ID;
            dao.DATA_STATE.Value = invoice.DataState;
            dao.USE_TIME.Value = invoice.UseTime;
            return dao[this].Update(new WF.Common.Sql.Field[] { dao.DATA_STATE }, new WF.Common.Sql.Field[] { dao.USE_OPER_ID, dao.USE_TIME });
        }

        /// <summary>
        /// 获取收费员一年内领用号段
        /// </summary>
        /// <param name="invoiceType"></param>
        /// <param name="operID"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public List<Invoice> QueryInvoiceList(string invoiceType, string operID, DateTime beginTime)
        {
            BD_EXP_INVOICE dao = new BD_EXP_INVOICE();
            dao.INVOICE_TYPE_ID.Value = invoiceType;
            dao.USE_OPER_ID.Value = operID;
            dao.USE_TIME.Value = beginTime;
            dao.USE_TIME.OperateCharacter = WF.Common.Sql.OperateCharacter.GreaterEqual;
            return this.QueryList(dao.All, new WF.Common.Sql.Field[] { dao.INVOICE_TYPE_ID, dao.USE_OPER_ID, dao.USE_TIME });
        }
        #endregion
    }
    /*public class RegisterLogic : AbstractCommonBL<Register, BD_EXP_REGISTER>
    {
        protected override BD_EXP_REGISTER ConvertToDao(Register model)
        {
            BD_EXP_REGISTER dao = new BD_EXP_REGISTER();
            dao.REGISTER_ID.Value = model.ID;
            dao.REGISTER_COUNT.Value = model.Number;
            dao.PATIENT_NAME.Value = model.Name;
            dao.PATIENT_SEX.Value = model.Patient_Sex;
            return dao;
        }

        protected override Register ConvertToModel(BD_EXP_REGISTER dao)
        {
            Register model = new Register();
            model.ID = dao.REGISTER_ID.Value.ToString();
            model.Number= dao.REGISTER_COUNT.Value.ToString();
            model.Name= dao.PATIENT_NAME.Value.ToString();
            model.Patient_Sex = dao.PATIENT_SEX.Value.ToString();
            return model;
        }

        /// <summary>
        /// 通过人员编号,和发票类别查询和是否财务组该人员的发票信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="type"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public List<Register> QueryRegisterList(string p_id)
        {
            BD_EXP_REGISTER dao = new BD_EXP_REGISTER();
            dao.REGISTER_ID.Value =p_id;
            return this.QueryList(new WF.Common.Sql.Field[] { dao.REGISTER_ID });
        }

    }*/
}
