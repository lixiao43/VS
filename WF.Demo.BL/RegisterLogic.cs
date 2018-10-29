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
    public class RegisterLogic : AbstractCommonBL<Register, BD_EXP_REGISTER>
    {
        protected override BD_EXP_REGISTER ConvertToDao(Register model)
        {
            BD_EXP_REGISTER dao = new BD_EXP_REGISTER();
            dao.REGISTER_ID.Value = model.Patient_id;
            dao.REGISTER_COUNT.Value = model.Number;
            dao.PATIENT_NAME.Value = model.Patient_Name;
            dao.PATIENT_SEX.Value = model.Patient_Sex;
            dao.ID_CARD_NUMBER.Value = model.Id_Number;
            dao.DATE_OF_BIRTH.Value = model.Date_Of_Birth;
            return dao;
        }

        protected override Register ConvertToModel(BD_EXP_REGISTER dao)
        {
            Register model = new Register();
            model.Patient_id = dao.REGISTER_ID.Value.ToString();
            model.Number = dao.REGISTER_COUNT.Value.ToString();
            model.Patient_Name = dao.PATIENT_NAME.Value.ToString();
            model.Patient_Sex = dao.PATIENT_SEX.Value.ToString();
            model.Id_Number = dao.ID_CARD_NUMBER.Value.ToString();
            model.Date_Of_Birth = dao.DATE_OF_BIRTH.Value.ToString();
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
            dao.REGISTER_ID.Value = p_id;
            return this.QueryList(new WF.Common.Sql.Field[] { dao.REGISTER_ID });
        }
        public List<Register> QueryRegisterListSex(string p_sex)
        {
            BD_EXP_REGISTER dao = new BD_EXP_REGISTER();
            dao.PATIENT_SEX.Value = p_sex;
            return this.QueryList(new WF.Common.Sql.Field[] { dao.PATIENT_SEX });
        }

        //public List<Register> QueryRegisterListNumber()
        //{
        //    BD_EXP_REGISTER dao = new BD_EXP_REGISTER();
        //    return this.QueryList(dao);
        //}
        //public int UpdateRegisterList(Register register)
        //{
        //    BD_EXP_REGISTER dao = new BD_EXP_REGISTER();
        //    //dao.REGISTER_ID.Value = register.Patient_id;
        //    dao.PATIENT_NAME.Value = register.Patient_Name;
        //    dao.PATIENT_SEX.Value = register.Patient_Sex;
        //    dao.REGISTER_COUNT.Value = register.Number;
        //    return dao[this].Update(new WF.Common.Sql.Field[] { dao.PATIENT_NAME, dao.PATIENT_SEX, dao.REGISTER_COUNT});
        //}


    }
}
