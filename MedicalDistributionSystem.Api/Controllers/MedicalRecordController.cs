using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Data;
using MedicalDistributionSystem.Domain.Entity;
using MedicalDistributionSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;

namespace MedicalDistributionSystem.Api.Controllers
{
    public class MedicalRecordController : BaseController
    {
        /// <summary>
        /// 获取消费记录/病历（分页）所有的现金流从这里进入
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<IList<MedicalRecord>> Get(int pageIndex = 1, int pageSize = 10)
        {
            ApiResult<IList<MedicalRecord>> result = new ApiResult<IList<MedicalRecord>>();
            using (var db = new MDDbContext())
            {
                var list = (from u in db.MedicalRecords where u.DeleteMark == false orderby u.Id ascending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (list == null)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                }
                else
                {
                    result.Data = list;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取指定消费记录/病历
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<MedicalRecord> GetSingle(int id)
        {
            ApiResult<MedicalRecord> result = new ApiResult<MedicalRecord>();
            using (var db = new MDDbContext())
            {
                var medicalRecord = db.MedicalRecords.Find(id);
                if (null == medicalRecord)
                {
                    result.Code = 500;
                    result.Data = null;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = medicalRecord;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建消费记录/病历
        /// </summary>
        /// <param name="medicalRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<MedicalRecord> Create(MedicalRecord medicalRecord)
        {
            ApiResult<MedicalRecord> result = new ApiResult<MedicalRecord>();
            using (var db = new MDDbContext())
            {
                using (var trans = new TransactionScope())
                {
                    try
                    {
                        medicalRecord.Create(medicalRecord.MemberId);
                        db.Entry<MedicalRecord>(medicalRecord).State = System.Data.Entity.EntityState.Added;
                        db.MedicalRecords.Add(medicalRecord);
                        //db.SaveChanges();
                        var member = db.Members.Find(medicalRecord.MemberId);
                        //获取当前会员的上级代理
                        //一般会员的上级代理拿到的是会员消费的全额 即下属会员消费100%
                        var proxy = db.Proxies.Find(member.ProxyId);
                        var parentProxy = db.Proxies.Find(proxy.CreatorUserId);
                        
                        //db.SaveChanges();
                        //佣金所属者 创建佣金流水记录
                        Commission commission = new Commission();
                        commission.Create(proxy.Id);
                        commission.DeleteMark = false;
                        commission.DeleteTime = null;
                        commission.DeleteUserId = 0;
                        commission.IncomeType = (int)Medical.IncomeType.FromMember;
                        commission.LastModifyTime = null;
                        commission.LastModifyUserId = 0;
                        commission.Money = medicalRecord.Fee - Calculate(db, parentProxy, medicalRecord.Fee);//会员消费金额
                        commission.ProxyId = proxy.Id;
                        db.Commissions.Add(commission);
                        db.Entry<Commission>(commission).State = System.Data.Entity.EntityState.Added;

                        proxy.CurrentMoney += commission.Money;
                        proxy.Modify(parentProxy.Id);
                        db.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ////必须调用.Complete()，不然数据不会保存
                        trans.Complete();
                    }
                    catch (Exception e)
                    {
                        ////出了using代码块如果还没调用Complete()，所有操作就会自动回滚
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 逻辑删除消费记录/病历
        /// </summary>
        /// <param name="medicalRecordId"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult<bool> Delete(int medicalRecordId)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (var db = new MDDbContext())
            {
                var medicalRecord = db.MedicalRecords.Find(medicalRecordId);
                if (medicalRecord == null)
                {
                    result.Data = false;
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.Msg = Resource.ENTITY_NOT_FOUND;
                    return result;
                }
                medicalRecord.Remove(0);
                db.Entry<MedicalRecord>(medicalRecord).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// 递归计算抽成
        /// </summary>
        /// <param name="context"></param>
        /// <param name="proxy"></param>
        /// <param name="money"></param>
        /// <returns>返回抽成金额</returns>
        private decimal Calculate(MDDbContext context,Proxy proxy,decimal money)
        {
            var parent = context.Proxies.Find(proxy.CreatorUserId);
            var choucheng = (decimal)proxy.BackMoneyPercent * money;
            var realChoucheng = parent==null?choucheng : choucheng - (choucheng * (decimal)parent.BackMoneyPercent);
            proxy.CurrentMoney += realChoucheng;
            proxy.LastModifyTime = DateTime.Now;
            context.Entry<Proxy>(proxy).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            //递归创建佣金流水
            Commission commission = new Commission();
            commission.Create(proxy.Id);
            commission.DeleteMark = false;
            commission.DeleteTime = null;
            commission.DeleteUserId = 0;
            commission.IncomeType = (int)Medical.IncomeType.FromProxy;
            commission.LastModifyTime = null;
            commission.LastModifyUserId = 0;
            commission.Money = realChoucheng;//抽成金额
            commission.ProxyId = proxy.Id;
            context.Commissions.Add(commission);
            context.Entry<Commission>(commission).State = System.Data.Entity.EntityState.Added;
            if (proxy.ProxyLevel != (int)Medical.ProxyLevelEnums.LevelSuper)
            {
                var parentProxy = context.Proxies.Find(proxy.CreatorUserId);
                //var subChoucheng = Calculate(context, parentProxy, choucheng);
                Calculate(context, parentProxy, choucheng);
                return choucheng;
            }
            else
            {
                return choucheng;
            }
        }
    }
}
