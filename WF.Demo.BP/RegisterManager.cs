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
    public class RegisterManager : WF.Common.Core.AbstractCommonBP
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
        [WF.Common.Attribute.Connection]
        public List<Register> QueryRegisterListSex(string p_sex)
        {
            RegisterLogic registerLogic = this.AbstractProvider.GetAbstractCommonBL<RegisterLogic>();
            return registerLogic.QueryRegisterListSex(p_sex);
        }
        [WF.Common.Attribute.Connection]
        public List<Register> QueryRegisterListNumber()
        {
            RegisterLogic registerLogic = this.AbstractProvider.GetAbstractCommonBL<RegisterLogic>();
            return registerLogic.QueryList();
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
            return registerLogic.Delete(register.Patient_id);
        }
    }
}
