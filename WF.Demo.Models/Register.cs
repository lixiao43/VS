using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Demo.Models
{


    [Serializable]
    public class Register : WF.Common.Base.BaseObject
    {
        private string id;
        /// <summary>
        /// 住院号
        /// </summary>
        public string Patient_id
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

        private string id_number;
        /// <summary>
        ///病人身份证号码
        /// </summary>
        public string Id_Number
        {
            get
            {
                if (this.id_number == null)
                {
                    this.id_number = string.Empty;
                }
                return this.id_number;
            }
            set
            {
                this.id_number = value;
            }
        }
        private string date_of_birth;
        /// <summary>
        ///病人出生年月
        /// </summary>
        public string Date_Of_Birth
        {
            get
            {
                if (this.date_of_birth == null)
                {
                    this.date_of_birth = string.Empty;
                }
                return this.date_of_birth;
            }
            set
            {
                this.date_of_birth = value;
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
       /* public new Invoice Clone()
        {
            Invoice Invoice = base.Clone() as Invoice;
            Invoice.InvoiceKind = this.InvoiceKind.Clone();
            Invoice.UseOper = this.UseOper.Clone();
            Invoice.Oper = this.Oper.Clone();
            return Invoice;
        }*/
    }


}
