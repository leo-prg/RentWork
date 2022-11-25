using RentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentService
{
    public interface IObjectService
    {
        void AddObject(ObjectRect obj);
        void RemoveObject(ObjectRect obj);
        void UpdateObject(ObjectRect oldO, ObjectRect newO);
        List<ObjectRect> GetAllObjects();
        ObjectRect GetObjectById(int id);

    }
}
