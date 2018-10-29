using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Common.Sql;

namespace WF.Demo.DAO
{
    /// <summary>
    /// ********************************************************
    /// 功能描述：发票表
    /// 创建日期：2015-03-23
    /// 创 建 人：caolin
    /// 修改日期：
    /// 修 改 人：
    /// 修改内容：
    /// ********************************************************
    /// </summary>
    [Serializable]
    public partial class BD_EXP_INVOICE : Table
    {
        #region Field


        private Field invoice_id = null;

        /// <summary>
        /// 领用流水号
        /// </summary>
        public Field INVOICE_ID
        {
            get
            {
                if (invoice_id == null)
                {
                    invoice_id = new Field(this, "INVOICE_ID", DbType.Varchar, 16);
                }

                return invoice_id;
            }
        }
        private Field use_time = null;

        /// <summary>
        /// 领用时间
        /// </summary>
        public Field USE_TIME
        {
            get
            {
                if (use_time == null)
                {
                    use_time = new Field(this, "USE_TIME", DbType.Date, 7);
                }

                return use_time;
            }
        }
        private Field use_oper_id = null;

        /// <summary>
        /// 领用人
        /// </summary>
        public Field USE_OPER_ID
        {
            get
            {
                if (use_oper_id == null)
                {
                    use_oper_id = new Field(this, "USE_OPER_ID", DbType.Varchar, 8);
                }

                return use_oper_id;
            }
        }
        private Field invoice_type_id = null;

        /// <summary>
        /// 发票类型 R 挂号 C 门诊 I 住院 P 预交金 A 账户
        /// </summary>
        public Field INVOICE_TYPE_ID
        {
            get
            {
                if (invoice_type_id == null)
                {
                    invoice_type_id = new Field(this, "INVOICE_TYPE_ID", DbType.Varchar, 2);
                }

                return invoice_type_id;
            }
        }
        private Field invoice_type_name = null;

        /// <summary>
        /// 发票类型名称
        /// </summary>
        public Field INVOICE_TYPE_NAME
        {
            get
            {
                if (invoice_type_name == null)
                {
                    invoice_type_name = new Field(this, "INVOICE_TYPE_NAME", DbType.Varchar, 16);
                }

                return invoice_type_name;
            }
        }
        private Field invoice_prefix = null;

        /// <summary>
        /// 发票号前缀
        /// </summary>
        public Field INVOICE_PREFIX
        {
            get
            {
                if (invoice_prefix == null)
                {
                    invoice_prefix = new Field(this, "INVOICE_PREFIX", DbType.Varchar, 4);
                }

                return invoice_prefix;
            }
        }
        private Field start_number = null;

        /// <summary>
        /// 开始号（数字部分）
        /// </summary>
        public Field START_NUMBER
        {
            get
            {
                if (start_number == null)
                {
                    start_number = new Field(this, "START_NUMBER", DbType.Varchar, 10);
                }

                return start_number;
            }
        }
        private Field end_number = null;

        /// <summary>
        /// 终止号（数字部分）
        /// </summary>
        public Field END_NUMBER
        {
            get
            {
                if (end_number == null)
                {
                    end_number = new Field(this, "END_NUMBER", DbType.Varchar, 10);
                }

                return end_number;
            }
        }
        private Field used_number = null;

        /// <summary>
        /// 已用号（数字部分）
        /// </summary>
        public Field USED_NUMBER
        {
            get
            {
                if (used_number == null)
                {
                    used_number = new Field(this, "USED_NUMBER", DbType.Varchar, 10);
                }

                return used_number;
            }
        }
        private Field data_state = null;

        /// <summary>
        /// 数据状态1：使用，0：未用，-1：已用
        /// </summary>
        public Field DATA_STATE
        {
            get
            {
                if (data_state == null)
                {
                    data_state = new Field(this, "DATA_STATE", DbType.Int32, 22);
                }

                return data_state;
            }
        }
        private Field pub_flag = null;

        /// <summary>
        /// 公用标志0 否 1 是
        /// </summary>
        public Field PUB_FLAG
        {
            get
            {
                if (pub_flag == null)
                {
                    pub_flag = new Field(this, "PUB_FLAG", DbType.Char, 1);
                }

                return pub_flag;
            }
        }
        private Field oper_id = null;

        /// <summary>
        /// 操作员ID
        /// </summary>
        public Field OPER_ID
        {
            get
            {
                if (oper_id == null)
                {
                    oper_id = new Field(this, "OPER_ID", DbType.Varchar, 8);
                }

                return oper_id;
            }
        }
        private Field oper_time = null;

        /// <summary>
        /// 操作时间
        /// </summary>
        public Field OPER_TIME
        {
            get
            {
                if (oper_time == null)
                {
                    oper_time = new Field(this, "OPER_TIME", DbType.Date, 7);
                }

                return oper_time;
            }
        }

        private Field[] primary = null;

        /// <summary>
        /// 主键集
        /// </summary>
        public override Field[] Primary
        {
            get
            {
                if (primary == null)
                {
                    primary = new Field[] { INVOICE_ID };
                }

                return primary;
            }
        }

        private Field[] all = null;

        /// <summary>
        /// 列集
        /// </summary>
        public override Field[] All
        {
            get
            {
                if (all == null)
                {
                    all = new Field[] { INVOICE_ID, USE_TIME, USE_OPER_ID, INVOICE_TYPE_ID, INVOICE_TYPE_NAME, INVOICE_PREFIX, START_NUMBER, END_NUMBER, USED_NUMBER, DATA_STATE, PUB_FLAG, OPER_ID, OPER_TIME };
                }

                return all;
            }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public override string TableName
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// 序列名
        /// </summary>
        public override string SequenceName
        {
            get { return "SEQ_" + this.TableName; }
        }

        #endregion

    }
    /*public partial class BD_EXP_REGISTER:Table
    {
        private Field register_id = null;

        /// <summary>
        /// 住院号
        /// </summary>
        public Field REGISTER_ID
        {
            get
            {
                if (register_id == null)
                {
                    register_id = new Field(this, "REGISTER_ID", DbType.Varchar, 16);
                }

                return register_id;
            }
        }
        private Field patient_name= null;

        /// <summary>
        /// 病人姓名
        /// </summary>
        public Field PATIENT_NAME
        {
            get
            {
                if (patient_name == null)
                {
                    patient_name = new Field(this, "PATIENT_NAME", DbType.Varchar, 16);
                }

                return patient_name;
            }
        }
        private Field patient_sex = null;

        /// <summary>
        /// 病人性别
        /// </summary>
        public Field PATIENT_SEX
        {
            get
            {
                if (patient_sex == null)
                {
                    patient_sex = new Field(this, "PATIENT_SEX", DbType.Varchar, 16);
                }

                return patient_sex;
            }
        }
        private Field register_count = null;

        /// <summary>
        /// 住院次数
        /// </summary>
        public Field REGISTER_COUNT
        {
            get
            {
                if (register_count == null)
                {
                    register_count = new Field(this, "REGISTER_COUNT", DbType.Varchar, 16);
                }

                return register_count;
            }
        }

        private Field[] primary = null;

        /// <summary>
        /// 主键集
        /// </summary>
        public override Field[] Primary
        {
            get
            {
                if (primary == null)
                {
                    primary = new Field[] { REGISTER_ID };
                }

                return primary;
            }
        }

        private Field[] all = null;

        /// <summary>
        /// 列集
        /// </summary>
        public override Field[] All
        {
            get
            {
                if (all == null)
                {
                    all = new Field[] { REGISTER_ID, PATIENT_NAME, PATIENT_SEX, REGISTER_COUNT };
                }

                return all;
            }
        }

        /// <summary>
        /// 表名
        /// </summary>
        public override string TableName
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// 序列名
        /// </summary>
        public override string SequenceName
        {
            get { return "SEQ_" + this.TableName; }
        }
    }*/
}
