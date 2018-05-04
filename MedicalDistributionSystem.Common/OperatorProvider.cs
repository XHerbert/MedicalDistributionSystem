//==============================================================
//  作者：徐洪波  (xuhb@foxmail.com)
//  时间：2018/5/4 10:50:18
//  文件名：OperatorProvider
//  版本：V1.0.0 
//  说明：  
//==============================================================
using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Common
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        private string LoginUserKey = "loginuserkey";


        public OperatorModel GetCurrent()
        {
            OperatorModel operatorModel = new OperatorModel();
            operatorModel = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<OperatorModel>();
            return operatorModel;
        }
        public void AddCurrent(OperatorModel operatorModel)
        {
            WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(operatorModel.ToJson()), 6000);
        }
        public void RemoveCurrent()
        {
            WebHelper.RemoveCookie(LoginUserKey.Trim());
        }
    }
}
