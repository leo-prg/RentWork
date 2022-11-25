using RentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentService
{
    public interface IRenterService
    {
        void AddRenter(RenterRect renter);
        List<RenterRect> GetAllRenters();
        RenterRect GetRenterById(int id);
        void UpdateRenter(RenterRect oldR, RenterRect newR);
        void DropRenter(RenterRect renter);
        byte[] GetFileById(int id);
        void KillFile(int id);
        // получим список файлов прикрепленных к арендатору
        IEnumerable<RenterFileList> GetRenterFileList(int id);
        void AddRenterFile(int id, byte[] f, RenterFileList fr);

    }
}
