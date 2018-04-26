using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Interface
{
    public class IEntity<TEntity>
    {
        public void Create()
        {
            var entity = this as ICreationAudited;
            //var LoginInfo = OperatorProvider.Provider.GetCurrent();
            //if (LoginInfo != null)
            //{
            //    entity.CreatorUserId = LoginInfo.UserId;
            //}
            entity.CreatorTime = DateTime.Now;
        }

        public void Modify()
        {
            var entity = this as IModificationAudited;
            entity.LastModifyTime = DateTime.Now;
            entity.LastModifyUserId = 0;
        }

        public void Remove()
        {
            var entity = this as IDeleteAudited;
            entity.DeleteMark = true;
            entity.DeleteTime = DateTime.Now;
        }
    }
}
