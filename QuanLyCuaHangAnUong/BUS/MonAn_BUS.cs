using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public class MonAn_BUS
    {
        public static List<MonAn_DTO> LoadMonAn()
        {
            return MonAn_DAO.LayMonAn();
        }

        public static int layIDMonAn(string tenSP)
        {
            return MonAn_DAO.layIDMonAn(tenSP);
        }

        public static List<MonAn_DTO> TimMonAn(string tenMonAn)
        {
            return MonAn_DAO.TimMonAn(tenMonAn);
        }

        public static int layloai(string tenloai)
        {
            return MonAn_DAO.Layloai(tenloai);
        }

        public static bool ThemSP(MonAn_DTO sp)
        {
            return MonAn_DAO.ThemSP(sp);
        }

        public static bool SuaSP(MonAn_DTO sp)
        {
            return MonAn_DAO.SuaSP(sp);
        }
        public static bool XoaSP(MonAn_DTO sp)
        {
            return MonAn_DAO.XoaSP(sp);
        }
    }
}
