﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIXIRETD.DATA.DATA_ACCESS_LAYER.MODELS.SETUP_MODEL
{
    public class Material : BaseEntity
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public int ItemCategoryId { get; set; }
        public Uom Uom { get; set; }
        public int UomId { get; set; }
        public int BufferLevel { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string AddedBy { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
