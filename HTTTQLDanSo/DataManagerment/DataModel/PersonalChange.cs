using System;

namespace HTTTQLDanSo.DataManagerment.DataModel
{
    public class PersonalChange
    {
        public int Personal_ID { get; set; }

        public string Full_Name { get; set; }

        public string ChangeType_Code { get; set; }

        public string ChangeType_Name { get; set; }

        public DateTime Change_Date { get; set; }

        public string ChangeDate
        {
            get { return Change_Date.ToString("dd/MM/yyyy"); }
        }

        public int Change_ID { get; set; }

        public DateTime? Come_date { get; set; }
    }
}