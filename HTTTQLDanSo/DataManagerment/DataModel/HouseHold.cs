namespace HTTTQLDanSo.DataManagerment.DataModel
{
    public class HouseHold
    {
        public int HouseHold_ID { get; set; }

        public int Address_ID { get; set; }

        public string HouseHold_Code { get; set; }

        public string HouseHold_Number { get; set; }

        public string Region_ID { get; set; }

        public bool IsBigHouseHold { get; set; }

        public string HouseHold_Status { get; set; }

        public string Notes { get; set; }

        public bool IsChecked { get; set; }
    }
}