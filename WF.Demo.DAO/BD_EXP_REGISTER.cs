using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WF.Common.Sql;

namespace WF.Demo.DAO
{
    public partial class BD_EXP_REGISTER : Table
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
        private Field patient_name = null;

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
        private Field id_card_number = null;
        /// <summary>
        /// 身份证号码
        /// </summary>
        public Field ID_CARD_NUMBER
        {
            get
            {
                if (id_card_number == null)
                {
                    id_card_number = new Field(this, "ID_CARD_NUMBER", DbType.Varchar, 25);
                 }

                return id_card_number;
            }
        }
        private Field date_of_birth = null;
        /// <summary>
        /// 出生年月
        /// </summary>
        public Field DATE_OF_BIRTH
        {
            get
            {
                if (date_of_birth == null)
                {
                    date_of_birth = new Field(this, "DATE_OF_BIRTH", DbType.Varchar, 25);
                }

                return date_of_birth;
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
                    all = new Field[] { REGISTER_ID, PATIENT_NAME, PATIENT_SEX, REGISTER_COUNT, ID_CARD_NUMBER };
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
    }
}
