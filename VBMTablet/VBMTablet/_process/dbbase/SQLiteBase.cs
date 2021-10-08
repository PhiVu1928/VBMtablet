using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace VBMTablet._process
{
    public class SQLiteBase
    {

        private SQLiteConnection conn;

        public SQLiteBase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<DBStore>();
            GetAllDB();
        }

        public void GetAllDB()
        {
            var members = (from mem in conn.Table<DBStore>() select mem);
            LstValues = members.ToList();
        }

        public bool ChangeDBStore(string DBKey, string DBValue)
        {
            try
            {
                DBStore obj = new DBStore();
                obj.DBKey = DBKey;
                obj.DBValue = DBValue;
                List<DBStore> lstCheck = conn.Query<DBStore>($"SELECT * FROM DBStore WHERE DBKey = '" + DBKey + "'");
                if (lstCheck.Count == 0)
                {
                    conn.Insert(obj);
                }
                else
                {
                    obj = lstCheck[0];
                    obj.DBValue = DBValue;
                    conn.Update(obj);
                }
                return true;
            }
            catch { return false; }

        }

        public bool DeleteDBStore(int AutoID)
        {
            try
            {
                conn.Delete<DBStore>(AutoID);
                return true;
            }
            catch { return false; }
        }

        public string GetValueKey(string key)
        {
            GetAllDB();
            string data = "";
            foreach (var item in LstValues)
            {
                if (item.DBKey == key)
                {
                    data = item.DBValue;
                }
            }
            if (data == "")
            {
                return null;
            }
            else
            {
                return data;
            }
        }

        public class DBStore
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            public string DBKey { get; set; }
            public string DBValue { get; set; }
        }

        public List<DBStore> LstValues { get; set; } = new List<DBStore>();

    }
}
