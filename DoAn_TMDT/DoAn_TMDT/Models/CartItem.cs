﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_TMDT.Models
{
    public class CartItem
    {
        public string Hinh { get; set; }
        public string SanPhamID { get; set; }
        public string TenSanPham { get; set; }
        public string DonGia { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien
        {
            get
            {
                return SoLuong * int.Parse(DonGia);
            }
        }
    }
}