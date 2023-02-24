using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Bill_BUS
    {
        public static List<Bill_DTO> LoadBill()
        {
            return Bill_DAO.LayDanhSachBill();
        }
    }
}
