using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Interface
{
    public class IEntity<TEntity>
    {
        public void Create(int Id)
        {
            var entity = this as ICreationAudited;
            entity.CreatorUserId = Id;
            entity.CreatorTime = DateTime.Now;
        }

        public void Modify(int Id)
        {
            var entity = this as IModificationAudited;
            entity.LastModifyTime = DateTime.Now;
            entity.LastModifyUserId = Id;
        }

        public void Remove(int Id)
        {
            var entity = this as IDeleteAudited;
            entity.DeleteMark = true;
            entity.DeleteTime = DateTime.Now;
            entity.DeleteUserId = Id;
        }
    }
}
