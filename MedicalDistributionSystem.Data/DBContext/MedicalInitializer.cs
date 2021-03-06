﻿using MedicalDistributionSystem.Common;
using MedicalDistributionSystem.Domain.Entity;
using MedicalDistributionSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data.DBContext
{
    public class MedicalChangesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MDDbContext>
    {
        public override void InitializeDatabase(MDDbContext context)
        {
            if (context.Database.Connection.State == System.Data.ConnectionState.Open)
            {
                context.Database.Connection.Close();
            }
            base.InitializeDatabase(context);
        }

        protected override void Seed(MDDbContext context)
        {
            base.Seed(context);
            //初始化必要的数据库记录
            var proxies = new List<Proxy>
            {
                new Proxy()
                {
                    ProxyName="Admin",
                    Mobile= "18000000000",
                    Password=MD5Helper.GetMd5Hash("123456"),
                    Province="北京",
                    City="北京",
                    ProxyCode="",
                    CurrentMoney = 0,
                    BackMoneyPercent = 0.5d,
                    ProxyLevel=(int)Medical.ProxyLevelEnums.LevelSuper,
                    CreatorUserId=0,
                    CreatorTime = DateTime.Now,

                }
            };
            proxies.ForEach(p => { context.Proxies.Add(p); });
            context.SaveChanges();
        }
    }

    public class MedicalNotExistInitializer : System.Data.Entity.CreateDatabaseIfNotExists<MDDbContext>
    {
        public override void InitializeDatabase(MDDbContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(MDDbContext context)
        {
            base.Seed(context);
            //初始化必要的数据库记录
            var proxies = new List<Proxy>
            {
                new Proxy()
                {
                    ProxyName="Admin",
                    Mobile= "18000000000",
                    Password=MD5Helper.GetMd5Hash("123456"),
                    Province="北京",
                    City="北京",
                    ProxyCode="",
                    CurrentMoney = 0,
                    BackMoneyPercent = 0.5d,
                    ProxyLevel=(int)Medical.ProxyLevelEnums.LevelSuper,
                    CreatorUserId=0,
                    CreatorTime = DateTime.Now,
                }
            };
            proxies.ForEach(p => { context.Proxies.Add(p); });
            context.SaveChanges();
        }
    }
}
