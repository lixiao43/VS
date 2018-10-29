using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WF.Demo.Models
{
    public interface Interface1<T>
    {
        void Adddata(T t);
        void Updatedata(T t);
        void Deletedata(T t);
        //void QueryBtn();
        //void SaveBtn();
    }
}
